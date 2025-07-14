using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Management;

namespace ServiceWatcher
{
	/// <summary>
	/// サービスデータクラス
	/// </summary>
	internal class ServiceData: IDisposable
	{
		#region field

		/// <summary>
		/// 対象サービス名
		/// </summary>
		private string _serviceName;

		/// <summary>
		/// 操作タイムアウト
		/// </summary>
		private int _operationTimeout;

		/// <summary>
		/// サービスコントローラ
		/// </summary>
		private ServiceController _sc;

		/// <summary>
		/// WMIオブジェクト
		/// </summary>
		private ManagementObject _wmiService;

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#endregion

		#region プロパティ

		/// <summary>
		/// サービス名
		/// </summary>
		public string ServiceName
		{
			get
			{
				return _serviceName;
			}
		}

		/// <summary>
		/// 操作タイムアウト(ms)
		/// </summary>
		public int OperationTimeout
		{
			get
			{
				return _operationTimeout;
			}
		}

		/// <summary>
		/// 対象のサービスコントローラ
		/// </summary>
		public ServiceController TargetServiceController
		{
			get
			{
				return _sc;
			}
			set
			{
				if (value == null)
				{
					Dispose();
					return;
				}
				_sc = value;
				_wmiService = new ManagementObject("Win32_Service.Name='" + _sc.ServiceName + "'");
			}
		}

		/// <summary>
		/// 説明
		/// </summary>
		public string Description
		{
			get
			{
				if (_wmiService == null)
				{
					return "";
				}
				string s = _wmiService["Description"] as string;
				return GetStringWithoutNull(s);
			}
		}

		/// <summary>
		/// 状態
		/// </summary>
		public string StatusName
		{
			get
			{
				if (_sc == null)
				{
					return "未インストール";
				}
				return GetStatusNameJ(_sc.Status);
			}
		}

		/// <summary>
		/// スタートアップの種類
		/// </summary>
		public string StartMode
		{
			get
			{
				if (_wmiService == null)
				{
					return "";
				}
				string s = _wmiService["StartMode"] as string;
				return GetStringWithoutNull(s);
			}
		}

		/// <summary>
		/// ログイン
		/// </summary>
		public string StartName
		{
			get
			{
				if (_wmiService == null)
				{
					return "";
				}
				string s = _wmiService["StartName"] as string;
				return GetStringWithoutNull(s);
			}
		}

		/// <summary>
		/// 開始できるか?
		/// </summary>
		public bool CanStart
		{
			get
			{
				if (_sc == null)
				{
					return false;
				}
				_sc.Refresh();
				if (_sc.Status == ServiceControllerStatus.Running)
				{
					return false;
				}
				return true;
			}
		}

		/// <summary>
		/// 停止できるか?
		/// </summary>
		public bool CanStop
		{
			get
			{
				if (_sc == null)
				{
					return false;
				}
				_sc.Refresh();
				if (_sc.Status == ServiceControllerStatus.Stopped)
				{
					return false;
				}
				if (!_sc.CanStop)
				{
					return false;
				}
				return true;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="sc"></param>
		public ServiceData(string serviceName, int operationTimeout)
		{
			_serviceName = serviceName;
			_operationTimeout = operationTimeout;
		}
		#endregion

		#region メソッド

		#region IDisposable メンバ

		public void Dispose()
		{
			if (_sc != null)
			{
				_sc.Dispose();
				_sc = null;
			}
			if (_wmiService != null)
			{
				_wmiService.Dispose();
				_wmiService = null;
			}
		}

		#endregion

		#region public

		public void Start()
		{
			if (!this.CanStart)
			{
				return;
			}

			_log.DebugFormat("開始します={0}", _serviceName);
			_sc.Start();
			try
			{
				_sc.WaitForStatus(ServiceControllerStatus.Running, 
					TimeSpan.FromMilliseconds(_operationTimeout));
				_log.DebugFormat("開始しました={0}", _serviceName);
			}
			catch (System.ServiceProcess.TimeoutException ex)
			{
				_log.ErrorFormat("タイムアウトしました={0}:{1}", _serviceName, ex);
			}
		}

		public void Stop()
		{
			if (!this.CanStop)
			{
				return;
			}

			_log.DebugFormat("停止します={0}", _serviceName);
			_sc.Stop();
			try
			{
				_sc.WaitForStatus(ServiceControllerStatus.Stopped,
					TimeSpan.FromMilliseconds(_operationTimeout));
				_log.DebugFormat("停止しました={0}", _serviceName);
			}
			catch (System.ServiceProcess.TimeoutException ex)
			{
				_log.ErrorFormat("タイムアウトしました={0}:{1}", _serviceName, ex);
			}
		}

		#endregion

		#region private

		/// <summary>
		/// 状態の日本語名を返す
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		/// <remarks>該当なしは""</remarks>
		private string GetStatusNameJ(ServiceControllerStatus status)
		{
			if (status == ServiceControllerStatus.ContinuePending)
			{
				return "継続は保留中";
			}
			if (status == ServiceControllerStatus.Paused)
			{
				return "一時中断";
			}
			if (status == ServiceControllerStatus.PausePending)
			{
				return "一時中断は保留中";
			}
			if (status == ServiceControllerStatus.Running)
			{
				return "開始";
			}
			if (status == ServiceControllerStatus.Stopped)
			{
				return "";
			}
			if (status == ServiceControllerStatus.StopPending)
			{
				return "停止中";
			}
			return "";
		}

		/// <summary>
		/// スタートアップ種類の日本語名を返す
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		/// <remarks>該当なしは""</remarks>
		private string GetStartModeNameJ(string s)
		{
			if (string.Compare(s, "Manual", true) == 0)
			{
				return "手動";
			}
			if (string.Compare(s, "Auto", true) == 0)
			{
				return "自動";
			}
			return s;
		}

		private string GetStringWithoutNull(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return "";
			}
			return s;
		}

		#endregion

		#endregion
	}
}
