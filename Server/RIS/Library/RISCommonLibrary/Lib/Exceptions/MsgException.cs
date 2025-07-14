using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg;

namespace RISCommonLibrary.Lib.Exceptions
{

	/// <summary>
	/// メッセージユーザ例外規定
	/// </summary>
	public class MsgException : RISIfExceptionUserException
	{
		public MsgException()
			: base()
		{

		}

		public MsgException(string message)
			: base(message)
		{


		}

		public MsgException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}

	#region 電文のエラーステータスを持つ例外
	
	/// <summary>
	/// 電文のエラーステータスを持つ例外
	/// </summary>
	public abstract class MsgHaveStatusException : MsgException
	{

		/// <summary>
		/// エラーステータス
		/// </summary>
		public abstract string ErrStatus
		{
			get;
		}

		public MsgHaveStatusException()
			: base()
		{

		}

		public MsgHaveStatusException(string message)
			: base(message)
		{


		}

		public MsgHaveStatusException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}

	/// <summary>
	/// 例外エラー
	/// </summary>
	public class MsgAnomalyException : MsgHaveStatusException
	{
		public MsgAnomalyException()
			: base()
		{

		}

		public MsgAnomalyException(string message)
			: base(message)
		{


		}

		public MsgAnomalyException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public override string ErrStatus
		{
			get
			{
				return MsgConst.ERR_STATUS_ANOMALY;
			}
		}
	}

	/// <summary>
	/// 電文長エラー
	/// </summary>
	public class MsgLengthException : MsgHaveStatusException
	{
		public MsgLengthException()
			: base()
		{

		}

		public MsgLengthException(string message)
			: base(message)
		{


		}

		public MsgLengthException(string message, Exception inner)
			: base(message, inner)
		{

		}

		public override string ErrStatus
		{
			get
			{
				return MsgConst.ERR_STATUS_DATA_LENGTH;
			}
		}
	}

	/// <summary>
	/// 必須項目のエラー
	/// </summary>
	public class MsgNotNullException : MsgHaveStatusException
	{
		public MsgNotNullException()
			: base()
		{

		}

		public MsgNotNullException(string message)
			: base(message)
		{


		}

		public MsgNotNullException(string message, Exception inner)
			: base(message, inner)
		{

		}

		public override string ErrStatus
		{
			get
			{
				return MsgConst.ERR_STATUS_NOT_NULL;
			}
		}
	}

	/// <summary>
	/// RISマスタ存在なしエラー
	/// </summary>
	public class RISMasterNotExistsException : MsgHaveStatusException
	{
		public RISMasterNotExistsException()
			: base()
		{

		}

		public RISMasterNotExistsException(string message)
			: base(message)
		{


		}

		public RISMasterNotExistsException(string message, Exception inner)
			: base(message, inner)
		{

		}

		public override string ErrStatus
		{
			get
			{
				return MsgConst.ERR_STATUS_RIS_MASTER_NOT_EXIST;
			}
		}
	}

	/// <summary>
	/// すでに受付済みエラー
	/// </summary>
	public class MsgDoneReceiptException : MsgHaveStatusException
	{
		public MsgDoneReceiptException()
			: base()
		{

		}

		public MsgDoneReceiptException(string message)
			: base(message)
		{


		}

		public MsgDoneReceiptException(string message, Exception inner)
			: base(message, inner)
		{

		}

		public override string ErrStatus
		{
			get
			{
				return MsgConst.ERR_STATUS_DONE_RECEIPT;
			}
		}
	}

	/// <summary>
	/// ノード操作中エラー
	/// </summary>
	public class NodeOparationException : MsgException
	{
		public NodeOparationException():base()
		{

		}

		public NodeOparationException(string message):base(message)
		{


		}

		public NodeOparationException(string message, BaseNode node)
			: base(message)
		{
			Node = node;
		}

		public NodeOparationException(string message, Exception inner)
			: base(message, inner)
		{

		}

		public BaseNode Node
		{
			get;
			set;
		}
	}
	#endregion

	/// <summary>
	/// 送信時に未定義のRequestTypeがあった
	/// </summary>
	public class RequestTypeNotDefineException : MsgException
	{
		public RequestTypeNotDefineException()
			: base()
		{

		}

		public RequestTypeNotDefineException(string message)
			: base(message)
		{


		}

		public RequestTypeNotDefineException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}


	/// <summary>
	/// データなし例外
	/// </summary>
	public class DataNotFoundException : MsgException
	{
		public DataNotFoundException()
			: base()
		{

		}

		public DataNotFoundException(string message)
			: base(message)
		{


		}

		public DataNotFoundException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}

}
