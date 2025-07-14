using System;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Utils;
using RISCommonLibrary.Lib.Exceptions;

namespace RISCommonLibrary.Lib.Msg
{
	public enum RequestKindEnum
	{
		rkOrder,                //オーダ情報
		rkReceipt,              //受付(進捗)情報
		rkExam,                 //実施情報
		rkPatient,              //患者情報要求
		rkHospitalize,          //入退院情報
		rkRequestOpen,          //回線接続要求
		rkRequestClose,         //回線切断要求
		rkResponse,             //応答電文
		rkResponseWithPatient   //応答電文(患者情報付き)
	}

	/// <summary>
	/// 電文クラス
	/// </summary>
	public abstract class BaseMsg
	{

		#region field
		/// <summary>
		/// オリジナル電文
		/// </summary>
		private string _originalMessage;
		#endregion

		#region property

		#region abstruct
		/// <summary>
		/// 電文名
		/// </summary>
		public abstract string MessageNameJ
		{
			get;
		}

		/// <summary>
		/// メッセージ長
		/// </summary>
		/// <remarks>
		/// 可変長の場合は0を設定
		/// </remarks>
		public abstract int MsgLength
		{
			get;
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public abstract string[] TelegraphKinds
		{
			get;
		}

		#endregion

		#region virtual

		/// <summary>
		/// メッセージを文字列で設定・取得
		/// </summary>
		public virtual string TextMessage
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(MsgConst.MSG_ENCLOSURE_START);
				sb.Append(this.Body.Encode());
				sb.Append(MsgConst.MSG_ENCLOSURE_END);
				return sb.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				_originalMessage = value;
				string mesasgeTrim = MsgUtils.TrimEnclosure(value);
				StringIterator si = new StringIterator(mesasgeTrim);
				this.Body.Decode(si);
			}
		}

		#endregion

		/// <summary>
		/// 囲い文字がトリムされたメッセージを文字列で設定
		/// </summary>
		public string TextMessageByTrimedEnclosure
		{
			get
			{
				return MsgUtils.TrimEnclosure(TextMessage);
			}
		}

		/// <summary>
		/// 要求種別
		/// </summary>
		/// <remarks>
		/// 使ってないくさい
		/// </remarks>
		public RequestKindEnum RequestKind
		{
			get;
			set;
		}

		/// <summary>
		/// メッセージ本体
		/// </summary>
		public BaseRootNode Body
		{
			get;
			set;
		}

		/// <summary>
		/// 文字列から設定されたときのオリジナルメッセージ
		/// </summary>
		/// <remarks>送信の場合、まだ送信されていないときは""</remarks>
		public string OriginalMessage
		{
			get
			{
				return _originalMessage;
			}
		}

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseMsg()
		{

		}

		#endregion

		#region method
		
		/// <summary>
		/// 復元できるか
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public virtual bool CanDecode(string src)
		{
			if (string.IsNullOrEmpty(src))
			{
				return false;
			}

			//あえてやめておく
			#region 囲み文字がSTX、ETXじゃないとだめ
			
			//if (!src.StartsWith(Convert.ToString(SumiAccelConst.MSG_ENCLOSURE_START)))
			//{
			//    return false;
			//}

			//if (!src.EndsWith(Convert.ToString(SumiAccelConst.MSG_ENCLOSURE_END)))
			//{
			//    return false;
			//}
			#endregion

			string trimSrc = MsgUtils.TrimEnclosure(src);

			//長さチェックはしないでおく
			#region 固定長電文の長さチェック
			if (!ValidateDataLength(trimSrc))
			{
				return false;
			}
			#endregion

			if (!ValidateTelegraphKind(trimSrc))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// OriginalMessageに反映する
		/// </summary>
		public void ReflectOriginalMessage()
		{
			this._originalMessage = this.TextMessage;
		}


		#region protected

		/// <summary>
		/// データ長評価
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		/// <remarks>ここでは固定長の評価にとどめる</remarks>
		protected bool ValidateDataLength(string src)
		{
			if (MsgLength== MsgConst.MSG_LENGTH_FLEXIBLE)
			{
				return true;
			}
			string dataLengthString = GetDataLength(src);
			int dataLength;
			if (!int.TryParse(dataLengthString, out dataLength))
			{
				throw new MsgException(string.Format("データ長が数値ではありません:{0}", dataLengthString));
			}
			return MsgLength == dataLength;
		}

		/// <summary>
		/// 電文種別評価
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		protected bool ValidateTelegraphKind(string src)
		{
			string kind = GetTelegraphKind(src);
			return Array.Exists(TelegraphKinds, s => s == kind);
		}
		#endregion

		#region private
		
		/// <summary>
		/// 電文長
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		private string GetDataLength(string src)
		{
			return this.Body.GetDataLength(src);
		}

		/// <summary>
		/// 電文種別取得
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		private string GetTelegraphKind(string src)
		{
			return this.Body.GetTelegraphKind(src);
		}
		#endregion

		#endregion

	}
}
