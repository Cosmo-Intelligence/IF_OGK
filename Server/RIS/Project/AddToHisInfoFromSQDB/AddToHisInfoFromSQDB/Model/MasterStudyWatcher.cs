using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using AddToHisInfoFromSQDB.Data;
using AddToHisInfoFromSQDB.Properties;
using AddToHisInfoFromSQDB.Utils;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace AddToHisInfoFromSQDB.Model
{
	/// <summary>
	/// MasterStudyを監視するぞ
	/// </summary>
	internal class MasterStudyWatcher
	{
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// ToHisInfo書き込み処理クラス
		/// </summary>
		private ToHisInfoWriter _toHisInfoWriter = new ToHisInfoWriter();

		/// <summary>
		/// 古いログ削除クラス
		/// </summary>
		private DeleteOldLogHelper _deleteOldLogHelper = new DeleteOldLogHelper();

		/// <summary>
		/// ループ処理続行フラグ
		/// </summary>
		private bool _isContinue = true;

		/// <summary>
		/// 同期オブジェクト
		/// </summary>
		private Object _thisLock = new Object();

		/// <summary>
		/// ループ処理を続けるか？
		/// </summary>
		public bool IsContinue
		{
			get
			{
				return _isContinue;
			}
			private set
			{
				lock (_thisLock)
				{
					_isContinue = value;
				}
			}
		}

		public void StopLoop()
		{
			try
			{
				if (!IsContinue)
				{
					return;
				}
				_log.Debug("プログラムへ停止指示します");
				IsContinue = false;
			}
			catch (Exception e)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", e);
				throw;
			}
		}

		public void Execute()
		{
			Execute(null);
		}

		public void Execute(Action func)
		{
			try
			{
				IsContinue = true;
				while (IsContinue)
				{
					ExcecuteOne();

					if (func != null)
					{
						func();
					}
					int sleepTime = ConfigurationManager.AppSettings["ThreadSleepTime"].StringToInt32();
					Thread.Sleep(sleepTime);
				}
			}
			catch (Exception e)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", e);
				throw;
			}
		}

		public void ExcecuteOne()
		{
			try
			{
				String connectionStringRIS = Settings.Default.ConnectionStringRIS;
                IDbConnection connectionRIS = ODPConnectionUtils.Connect(connectionStringRIS);
				try
				{
					List<MasterStudy> sentList = MasterstudySentToRISHelper.GetDataFromDB(connectionRIS); //前回送信したリスト
					List<MasterStudy> list = this.GetNewSendList(sentList); //送信するリスト取得
					if (list == null)
					{
						return;
					}
					if (list.Count < 1)
					{
						return;
					}
					_log.Debug("古いログファイルを削除します");
					_deleteOldLogHelper.DeleteOldLog();
					_log.DebugFormat("{0}件TOHISINFOに書き込みます", list.Count);
					_toHisInfoWriter.Write(list, connectionRIS);
				}
				finally
				{
					connectionRIS.Close();
				}
			}
			catch (Exception ex)
			{
				_log.Error("致命的なエラーが発生しました。");
				_log.Error(ex.ToString());
			}
		}

		/// <summary>
		/// 送信リスト取得
		/// </summary>
		/// <param name="sentList">送信済みリスト</param>
		/// <returns></returns>
		private List<MasterStudy> GetNewSendList(List<MasterStudy> sentList)
		{
			String connectionStringSQ = Settings.Default.ConnectionStringSQ;
            IDbConnection connectionSQ = ODPConnectionUtils.Connect(connectionStringSQ);
			try
			{
				List<MasterStudy> list = MasterStudyHelper.GetDataFromDB(sentList, connectionSQ);
				if (list.Count() < 1)
				{
					return list;
				}
				#region ログ削減
				//if (sentList.Count > 0)
				//{
				//    _log.Debug("送信済み最終日時データ");
				//    WriteLogByMasterStudyList(sentList);
				//}
				//if (list.Count > 0)
				//{
				//    _log.Debug("SQ送信候補データ");
				//    WriteLogByMasterStudyList(list);
				//}
				#endregion
				IEnumerable<MasterStudy> excepteds = list.Except(sentList);
				if (excepteds.Count() > 0)
				{
					_log.Debug("送信済み最終日時データを除いたSQ送信データ");
					WriteLogByMasterStudyList(excepteds);
				}
				return excepteds.ToList<MasterStudy>();
			}
			finally
			{
				connectionSQ.Close();
			}
		}

		/// <summary>
		/// ログ出力する
		/// </summary>
		/// <param name="enumerable"></param>
		private void WriteLogByMasterStudyList(IEnumerable<MasterStudy> enumerable)
		{
			foreach (var item in enumerable)
			{
				_log.Debug(item.ToStringForLog());
			}
		}

	}
}
