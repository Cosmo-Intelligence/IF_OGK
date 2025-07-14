using System;
using System.Globalization;
using System.IO;
using RISCommonLibrary.Lib.Utils;

namespace RISCommonLibrary.Lib.LogCleaner
{
	public class LogCleanerYYYYMMDDDir : ILogCleaner
    {

        private String _name;
        private String _targetFormat;
        private String _rootDir;
        private Int32 _storeTerm = 30;

        private UpdatePropFunc _updatePropFunc;

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LogCleanerYYYYMMDDDir(String name, String targetFormat, UpdatePropFunc updatePropFunc)
        {
            this._name = name;
            this._targetFormat = targetFormat;
            this._updatePropFunc = updatePropFunc;
        }

        public void UpdateProp(String rootDir, Int32 storeTerm) 
        {
            this._rootDir = MiscUtils.ConvertToFullPath(rootDir);
            this._storeTerm = storeTerm;
        }

        #region ILogCleaner メンバ

        public string GetName()
        {
            return _name;
        }

        public void CleanUp()
        {
            if (!Directory.Exists(_rootDir))
            {
                return;
            }
            string[] dirs = Directory.GetDirectories(_rootDir, "*", SearchOption.TopDirectoryOnly);
            DateTime lastStoreDate = DateTime.Now.Date.AddDays(_storeTerm * -1);
            Array.ForEach<string>(dirs, 
                delegate(string s) 
                {
					try
					{
						DirectoryInfo di = new DirectoryInfo(s);
						DateTime dirDate;
						if (!DateTime.TryParseExact(di.Name, _targetFormat, CultureInfo.CurrentCulture, 
								System.Globalization.DateTimeStyles.None,  out dirDate))
						{
							return;
						}
						if (dirDate >= lastStoreDate)
						{
							return;
						}
						Directory.Delete(di.FullName, true);
					}
					catch (Exception e)
					{
						_log.WarnFormat("古いログファイル削除中に例外が発生しました。対象={0},{1}",
							s, e);
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
