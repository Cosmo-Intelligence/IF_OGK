using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Data
{
	public class ToHisInfo
	{
		/// <summary>
		/// 送信要求番号
		/// </summary>
		public string RequestID
		{
			get;
			set;
		}

		/// <summary>
		/// 送信要求日時
		/// </summary>
		public string RequestDate
		{
			get;
			set;
		}

		/// <summary>
		/// RIS識別ID
		/// </summary>
		public string RIS_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 要求ユーザID
		/// </summary>
		public string RequestUser
		{
			get;
			set;
		}

		/// <summary>
		/// 要求端末ID
		/// </summary>
		public string RequestTerminalID
		{
			get;
			set;
		}

		/// <summary>
		/// 処理タイプ識別子（GGNN） GG：処理分類(ｱﾌﾌｧﾍﾞｯﾄ2文字)  NN：処理種別（数字2文字） RC01：受付通知　RC99：受付取消通知 OP01：実施通知　OP02：実施通知（再送）  OP99：中止通知　OR01：ｵｰﾀﾞ取得要求 PR01：患者情報取得要求
		/// </summary>
		public string RequestType
		{
			get;
			set;
		}

		/// <summary>
		/// RCXX、OPｘｘ：オーダ番号 PRXX：患者ID
		/// </summary>
		public string MessageID1
		{
			get;
			set;
		}

		/// <summary>
		/// RCXX、OPｘｘ：患者ID PRXX：患者ｶﾅ名
		/// </summary>
		public string MessageID2
		{
			get;
			set;
		}

		/// <summary>
		/// 送信ｽﾃｰﾀｽ 00：未送信(ﾃﾞｨﾌｫﾙﾄ)　01：送信済　02：対象外
		/// </summary>
		public string TransferStatus
		{
			get;
			set;
		}

		/// <summary>
		/// 送信処理日時
		/// </summary>
		public string TransferDate
		{
			get;
			set;
		}

		/// <summary>
		/// 送信結果
		/// </summary>
		public string TransferResult
		{
			get;
			set;
		}

		/// <summary>
		/// 送信電文
		/// </summary>
		public string TransferText
		{
			get;
			set;
		}

		/// <summary>
        /// ログ出力用文字列取得
        /// </summary>
        /// <returns></returns>
		public String ToStringForLog()
		{
			const String FORMAT_LOG = "REQUESTID={0}, REQUESTDATE={1}, RIS_ID={2}, REQUESTUSER={3}, REQUESTTERMINALID={4}, REQUESTTYPE={5}, MESSAGEID1={6}, MESSAGEID2={7}, TRANSFERSTATUS={8}, TRANSFERDATE={9}, TRANSFERRESULT={10}, TRANSFERTEXT={11}";
			return String.Format(FORMAT_LOG, RequestID, RequestDate, RIS_ID, RequestUser, RequestTerminalID, RequestType, MessageID1, MessageID2, TransferStatus, TransferDate, TransferResult, TransferText);
		}


	}
}
