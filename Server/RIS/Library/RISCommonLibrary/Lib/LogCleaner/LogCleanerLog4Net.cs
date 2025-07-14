using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using RISCommonLibrary.Lib.Utils;

namespace RISCommonLibrary.Lib.LogCleaner
{
    public class LogCleanerLog4Net: ILogCleaner
    {

        private String _name;
        private String _targetDir;
        private String _targetFormat;
        private String _targetRegex;
        private Int32 _storeTerm = 30;

        private UpdatePropFunc _updatePropFunc;

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LogCleanerLog4Net(String name, String targetRegex, String targetFormat, UpdatePropFunc updatePropFunc)
        {
            this._name = name;
            this._targetRegex = targetRegex;
            this._targetFormat = targetFormat;
            this._updatePropFunc = updatePropFunc;
        }

        public void UpdateProp(String targetDir, Int32 storeTerm) 
        {
            this._targetDir = MiscUtils.ConvertToFullPath(targetDir);
            this._storeTerm = storeTerm;
        }

        #region ILogCleaner メンバ

        public string GetName()
        {
            return _name;
        }

        public void CleanUp()
        {
            if (!Directory.Exists(_targetDir))
            {
                return;
            }
            DirectoryInfo di = new DirectoryInfo(_targetDir);

            FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
            DateTime lastStoreDate = DateTime.Now.Date.AddDays(_storeTerm * -1);
            Array.ForEach<FileInfo>(files,
                delegate(FileInfo f)
                {
					try
					{
						Match m = Regex.Match(f.Name, _targetRegex, RegexOptions.IgnoreCase);
						if (m.Success != true)
						{
							return;
						}
						String yyyyMMddString = m.Groups["TargetGroup"].Value;
						DateTime dirDate;
						if (!DateTime.TryParseExact(yyyyMMddString, _targetFormat, CultureInfo.CurrentCulture,
								System.Globalization.DateTimeStyles.None, out dirDate))
						{
							return;
						}
						if (dirDate >= lastStoreDate)
						{
							return;
						}
						f.Delete();
					}
					catch (Exception e)
					{
						_log.WarnFormat("古いログファイル削除中に例外が発生しました。対象={0},{1}",
							f.Name, e);
					}
                }
            );
        }

        public void UpdateProp()
        {
            if (this._updatePropFunc == null)
            {
                return;
            }
            this._updatePropFunc(this);
        }

        #endregion

    }
}
