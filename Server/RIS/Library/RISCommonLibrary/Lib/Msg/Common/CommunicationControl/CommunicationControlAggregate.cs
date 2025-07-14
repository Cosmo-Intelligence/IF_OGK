using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.CommunicationControl
{
	/// <summary>
	/// 通信制御部
	/// </summary>
	public class CommunicationControlAggregate: AggregateNode
	{
		#region field
		
		/// <summary>
		/// データ長
		/// </summary>
		private DataNode _DATA_LENGTH;

		/// <summary>
		/// 電文種別
		/// </summary>
		private DataNode _TELEGRAPH_KIND;

		/// <summary>
		/// 継続フラグ
		/// </summary>
		private DataNode _CONTINUANCE_FLAG;

		/// <summary>
		/// 送信日付
		/// </summary>
		private DataNode _SEND_DATE;

		/// <summary>
		/// 送信時間
		/// </summary>
		private DataNode _SEND_TIME;

		/// <summary>
		/// エラーステータス
		/// </summary>
		private DataNode _ERR_STATUS;

		/// <summary>
		/// オーダ番号
		/// </summary>
		private DataNode _ORDER_NO;

		/// <summary>
		/// 予備
		/// </summary>
		private DataNode _RESERVE;
		
		#endregion

		#region property

		/// <summary>
		/// データ長
		/// </summary>
		public DataNode DATA_LENGTH
		{
			get
			{
				return _DATA_LENGTH;
			}
			set
			{
				_DATA_LENGTH = value;
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public DataNode TELEGRAPH_KIND
		{
			get
			{
				return _TELEGRAPH_KIND;
			}
			set
			{
				_TELEGRAPH_KIND = value;
			}
		}

		/// <summary>
		/// 継続フラグ
		/// </summary>
		public DataNode CONTINUANCE_FLAG
		{
			get
			{
				return _CONTINUANCE_FLAG;
			}
			set
			{
				_CONTINUANCE_FLAG = value;
			}
		}

		/// <summary>
		/// 送信日付
		/// </summary>
		public DataNode SEND_DATE
		{
			get
			{
				return _SEND_DATE;
			}
			set
			{
				_SEND_DATE = value;
			}
		}

		/// <summary>
		/// 送信時間
		/// </summary>
		public DataNode SEND_TIME
		{
			get
			{
				return _SEND_TIME;
			}
			set
			{
				_SEND_TIME = value;
			}
		}

		/// <summary>
		/// エラーステータス
		/// </summary>
		public DataNode ERR_STATUS
		{
			get
			{
				return _ERR_STATUS;
			}
			set
			{
				_ERR_STATUS = value;
			}
		}

		/// <summary>
		/// オーダ番号
		/// </summary>
		public DataNode ORDER_NO
		{
			get
			{
				return _ORDER_NO;
			}
			set
			{
				_ORDER_NO = value;
			}
		}

		/// <summary>
		/// 予備
		/// </summary>
		public DataNode RESERVE
		{
			get
			{
				return _RESERVE;
			}
			set
			{
				_RESERVE = value;
			}
		}

		
		#endregion

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CommunicationControlAggregate()
			: base(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ROOT)
		{
			_DATA_LENGTH = AddChildNode(new DataNodeFormat(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_DATA_LENGTH));
			_TELEGRAPH_KIND = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_TELEGRAPH_KIND));
			_CONTINUANCE_FLAG = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_CONTINUANCE_FLAG));
			_SEND_DATE = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_SEND_DATE));
			_SEND_TIME = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_SEND_TIME));
			_ERR_STATUS = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ERR_STATUS));
			_ORDER_NO = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ORDER_NO));
			_RESERVE = AddChildNode(new DataNode(CommunicationControlNodeInfo.COMMUNICATION_CONTROL_RESERVE));
		}
		#endregion

	}
}
