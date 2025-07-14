using System;
using System.Collections.Generic;
using System.Linq;

namespace RISCommonLibrary.Lib.LogCleaner
{
    public class LogCleanerManager
    {
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IList<ILogCleaner> _logCleaners = new List<ILogCleaner>();

        public void Regist(ILogCleaner logCleaner) 
        {
            _logCleaners.Add(logCleaner);
        }

        public ILogCleaner GetLogCleaner(String name)
        { 
            IEnumerable<ILogCleaner> cleaners = 
                    from cleaner in _logCleaners
                    where String.Compare(cleaner.GetName(), name, true) == 0
                    select cleaner;
            if (cleaners.Count() < 1)
	        {
        		 return null;
	        }
            return cleaners.First();
        }

        public void CleanUp()
        {
            foreach (ILogCleaner item in _logCleaners)
            {
				try
				{
					item.CleanUp();
				}
				catch (Exception e)
				{
					_log.WarnFormat("古いログファイル削除中に例外が発生しました。Name={0},{1}", 
						item.GetName(), e);
				}
            }
        }

        public void Clear()
        {
            _logCleaners.Clear();
        }

        public void UpdateProp()
        {
            foreach (ILogCleaner item in _logCleaners)
            {
				try
				{
					item.UpdateProp();
				}
				catch (Exception e)
				{
					_log.WarnFormat("プロパティ更新中に例外が発生しました。Name={0},{1}",
						item.GetName(), e);
				}
            }
        }

    }
}
