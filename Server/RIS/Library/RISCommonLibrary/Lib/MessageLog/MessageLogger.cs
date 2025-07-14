using System;
using System.IO;
using System.Text;
using RISCommonLibrary.Lib.Utils;

namespace RISCommonLibrary.Lib.MessageLog
{
	/// <summary>
	/// 電文ログクラス
	/// </summary>
	/// <remarks>シングルトン</remarks>
    public class MessageLogger
	{
		#region field
		
		/// <summary>
		/// インスタンス
		/// </summary>
		private static readonly MessageLogger _instance = new MessageLogger();

		private String rootDir = @".\Log\Message";
		private String currentDir = DateTime.Now.ToString("yyyyMMdd");
		private String targetDirFormat = "yyyyMMdd";
		private Encoding enc = Encoding.GetEncoding(932);

		/// <summary>
		/// 同期オブジェクト
		/// </summary>
		private object _syncObject = new object();
		#endregion

		#region props
		/// <summary>
		/// インスタンスにアクセスするプロパティ
		/// </summary>
		public static MessageLogger Instance
		{
			get
			{
				return _instance;
			}
		}

		public string RootDir
		{
			get
			{
				return rootDir;
			}
			set
			{
				rootDir = value;
			}
		}

		public string CurrentDir
		{
			get
			{
				return currentDir;
			}
			set
			{
				currentDir = value;
			}
		}

		public string TargetDirFormat
		{
			get
			{
				return targetDirFormat;
			}
			set
			{
				targetDirFormat = value;
			}
		}

		public Encoding Enc
		{
			get
			{
				return enc;
			}
			set
			{
				enc = value;
			}
		}
		#endregion


		/// <summary>
		/// コンストラクタ
		/// </summary>
        private MessageLogger()
        {
        }

        public void UpdateCurrentDir(DateTime currentDateTime)
        {
            this.currentDir = currentDateTime.ToString(targetDirFormat);
        }

        public void WriteLog(String fileName, String message)
        {
			lock (_syncObject)
			{
				String WriteFileFullPath = GetFileNameFullPath(MiscUtils.ConvertToFullPath(rootDir), currentDir, fileName);
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(WriteFileFullPath));
				if (!directoryInfo.Exists)
				{
        			 directoryInfo.Create();
				}

				using (StreamWriter sw = new StreamWriter(WriteFileFullPath, true, enc))
				{
					sw.Write(message);
				}
			}
        }

        private String GetFileNameFullPath(String rootDir, String currentDir, String fileName) 
        {
            return CombinePaths(rootDir, currentDir, fileName);
        }

        private String CombinePaths(params string[] paths)
        {
            String rt = string.Empty;
            foreach (string item in paths)
            {
                rt = Path.Combine(rt, item);
            }
            return rt;
        }
    }
}
