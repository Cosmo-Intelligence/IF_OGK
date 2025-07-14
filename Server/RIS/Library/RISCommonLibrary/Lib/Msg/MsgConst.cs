using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// 電文定義
	/// </summary>
	public class MsgConst
	{
		#region メッセージ定義
		
		/// <summary>
		/// メッセージパディング文字
		/// </summary>
		public const Char MSG_PADDING_CHAR = ' ';

		/// <summary>
		/// パス区切り文字列
		/// </summary>
		public const Char PATH_DELIMITER = '\\';

		#endregion

		#region 電文長定義

		#region 部

		/// <summary>
		/// 通信制御部電文長
		/// </summary>
		public const int PART_LENGTH_COMMUNICATION_CONTROL = 52;

		/// <summary>
		/// 患者属性電文長
		/// </summary>
		public const int PART_LENGTH_PATIENT_ATTRIBUTE = 1880;

		/// <summary>
		/// 受付（進捗）情報電文長
		/// </summary>
		public const int PART_LENGTH_RECEIPT = 40;

		#endregion

		#region 電文
		
		#endregion

		/// <summary>
		/// 可変長の場合の電文長
		/// </summary>
		public const int MSG_LENGTH_FLEXIBLE = 0;

		/// <summary>
		/// 回線接続要求電文長
		/// </summary>
		public const int MSG_LENGTH_COMMUNICATION_CONTROL_OPEN = PART_LENGTH_COMMUNICATION_CONTROL;

		/// <summary>
		/// 回線切断要求電文長
		/// </summary>
		public const int MSG_LENGTH_COMMUNICATION_CONTROL_CLOSE = PART_LENGTH_COMMUNICATION_CONTROL;

		/// <summary>
		/// オーダ情報情報電文長
		/// </summary>
		/// <remarks>可変長</remarks>
		public const int MSG_LENGTH_ORDER = MSG_LENGTH_FLEXIBLE;

		/// <summary>
		/// 受付(進捗)情報電文長
		/// </summary>
		public const int MSG_LENGTH_RECEIPT = PART_LENGTH_COMMUNICATION_CONTROL + PART_LENGTH_RECEIPT;

		/// <summary>
		/// 実施情報電文長
		/// </summary>
		/// <remarks>可変長</remarks>
		public const int MSG_LENGTH_EXAM = MSG_LENGTH_FLEXIBLE;

		/// <summary>
		/// 患者情報要求電文長
		/// </summary>
		public const int MSG_LENGTH_REQUEST_PATIENT = PART_LENGTH_COMMUNICATION_CONTROL + PART_LENGTH_PATIENT_ATTRIBUTE;

		/// <summary>
		/// 入退院情報電文長
		/// </summary>
		public const int MSG_LENGTH_HOSPITALIZE = PART_LENGTH_COMMUNICATION_CONTROL + PART_LENGTH_PATIENT_ATTRIBUTE;

		/// <summary>
		/// 応答電文長
		/// </summary>
		public const int MSG_LENGTH_RESPONSE = PART_LENGTH_COMMUNICATION_CONTROL;

		/// <summary>
		/// 応答患者情報電文長
		/// </summary>
		public const int MSG_LENGTH_RESPONSE_WITH_PATIENT = PART_LENGTH_COMMUNICATION_CONTROL + PART_LENGTH_PATIENT_ATTRIBUTE;


		#endregion

		#region 電文囲み文字

		/// <summary>
		/// 電文開始囲み文字(STX)
		/// </summary>
		public const char MSG_ENCLOSURE_START = (char)0x02;

		/// <summary>
		/// 電文終了囲み文字(ETX)
		/// </summary>
		public const char MSG_ENCLOSURE_END = (char)0x03;

		#endregion

		#region 電文種別

		#region 送信電文
		/// <summary>
		/// オーダ情報
		/// </summary>
		public const string TELEGRAPH_KIND_ORDER_INFO = "01";

		/// <summary>
		/// 受付(進捗)情報
		/// </summary>
		public const string TELEGRAPH_KIND_RECEIPT_INFO = "02";

		/// <summary>
		/// 実施情報
		/// </summary>
		public const string TELEGRAPH_KIND_EXAM_INFO = "03";

		/// <summary>
		/// 患者情報要求
		/// </summary>
		public const string TELEGRAPH_KIND_PATIENT_INFO = "04";

		/// <summary>
		/// 入退院情報
		/// </summary>
		public const string TELEGRAPH_KIND_HOSPITALIZE = "05";

		/// <summary>
		/// 既読情報
		/// </summary>
		public const string TELEGRAPH_KIND_READ_INFO = "06";

		/// <summary>
		/// 回線切断要求
		/// </summary>
		public const string TELEGRAPH_KIND_REQUEST_CLOSE = "09";

		/// <summary>
		/// 回線接続要求
		/// </summary>
		public const string TELEGRAPH_KIND_REQUEST_OPEN = "10";

		#endregion

		#region 応答電文
		/// <summary>
		/// 正常応答(ACK)
		/// </summary>
		public const string TELEGRAPH_KIND_RESPONSE_OK = "41";

		/// <summary>
		/// 異常応答(NAK)
		/// </summary>
		public const string TELEGRAPH_KIND_RESPONSE_NG = "42";

		#endregion

		#endregion

		#region 要求種別テーブル
		/// <summary>
		/// 要求種別テーブル
		/// </summary>
		public static string[] KIND_CHAR_TABLE  = new []
		{
			TELEGRAPH_KIND_ORDER_INFO,     //オーダ情報
			TELEGRAPH_KIND_RECEIPT_INFO,     // 受付(進捗)情報
			TELEGRAPH_KIND_EXAM_INFO,     //実施情報
			TELEGRAPH_KIND_PATIENT_INFO,     //患者情報要求
			TELEGRAPH_KIND_HOSPITALIZE,     //入退院情報
			TELEGRAPH_KIND_REQUEST_CLOSE,     //回線切断要求
			TELEGRAPH_KIND_REQUEST_OPEN,     //回線接続要求
			"",     //応答電文はなし
			""
		};
		#endregion

		#region 電文エラーステータス
		/// <summary>
		/// 正常
		/// </summary>
		public const string ERR_STATUS_NORMAL = "00";

		/// <summary>
		/// 電文長エラー
		/// </summary>
		public const string ERR_STATUS_DATA_LENGTH = "10";

		/// <summary>
		/// 電文の必須項目のエラー
		/// </summary>
		public const string ERR_STATUS_NOT_NULL = "11";

		/// <summary>
		/// 電文の部位項目の繰り返し順番エラー
		/// </summary>
		public const string ERR_STATUS_NOT_SEQUENCE = "12";

		/// <summary>
		/// 例外エラー（型変換など）
		/// </summary>
		public const string ERR_STATUS_ANOMALY = "13";

		/// <summary>
		/// ＨＩＳマスタ存在なしのエラー
		/// </summary>
		public const string ERR_STATUS_HIS_MASTER_NO_EXIST = "20";

		/// <summary>
		/// ＨＩＳ側DBのエラー
		/// </summary>
		public const string ERR_STATUS_HIS_DB_ERR = "21";

		/// <summary>
		/// 受付データなし
		/// </summary>
		public const string ERR_STATUS_NO_RECEIPT_DATA = "22";

		/// <summary>
		/// 実施データ重複（新規）
		/// </summary>
		public const string ERR_STATUS_NO_EXAM_DATA = "23";

		/// <summary>
		/// 実施済み
		/// </summary>
		public const string ERR_STATUS_DONE_EXAM = "24";

		/// <summary>
		/// 受付済み
		/// </summary>
		public const string ERR_STATUS_DONE_RECEIPT = "25";

		/// <summary>
		/// 会計済み
		/// </summary>
		public const string ERR_STATUS_DONE_ACCOUNT = "26";

		/// <summary>
		/// RISマスタ存在なしのエラー
		/// </summary>
		public const string ERR_STATUS_RIS_MASTER_NOT_EXIST = "30";

		/// <summary>
		/// RIS側DBのエラー
		/// </summary>
		public const string ERR_STATUS_RIS_DB_ERR = "31";

		/// <summary>
		/// 検査種別エラー
		/// </summary>
		public const string ERR_STATUS_EXAM_KIND_ERR = "32";

		#endregion

		#region 継続フラグ
		/// <summary>
		/// 継続なし
		/// </summary>
		public const string CONTINUANCE_FLAG_NOT_EXIST = "0";

		/// <summary>
		/// 継続あり
		/// </summary>
		public const string CONTINUANCE_FLAG_EXIST = "1";
		#endregion

		#region 処理タイプ
		/// <summary>
		/// 処理タイプ-新規
		/// </summary>
		public const string PROCESSING_TYPE_NEW = "1";
		
		/// <summary>
		/// 処理タイプ-削除
		/// </summary>
		public const string PROCESSING_TYPE_DELETE = "2";

		/// <summary>
		/// 処理タイプ-患者情報
		/// </summary>
		public const string PROCESSING_TYPE_PATIENT = "3";

		/// <summary>
		/// 処理タイプ-画像
		/// </summary>
		public const string PROCESSING_TYPE_IMAGE = "10";

		/// <summary>
		/// 処理タイプ-1次所見
		/// </summary>
		public const string PROCESSING_TYPE_REMARK_FIRST = "20";

		/// <summary>
		/// 処理タイプ-2次所見
		/// </summary>
		public const string PROCESSING_TYPE_REMARK_SECOND = "30";

		/// <summary>
		/// 処理タイプ-2次所見解除
		/// </summary>
		public const string PROCESSING_TYPE_REMARK_SECOND_CANCEL = "31";

		/// <summary>
		/// 処理タイプ-2次所見既読確定
		/// </summary>
		public const string PROCESSING_TYPE_REMARK_SECOND_READ_FIX = "32";

		/// <summary>
		/// 処理タイプ-2次所見既読取消（未使用）
		/// </summary>
		public const string PROCESSING_TYPE_REMARK_SECOND_READ_CANCEL = "33";

		#region なぜか実施だけコード体系が違う
		/// <summary>
		/// 処理タイプ-新規(実施)
		/// </summary>
		public const string PROCESSING_TYPE_NEW_EXAM = "1";
		
		/// <summary>
		/// 処理タイプ-修正(実施)
		/// </summary>
		public const string PROCESSING_TYPE_UPDATE_EXAM = "3";

		/// <summary>
		/// 処理タイプ-中止(実施)
		/// </summary>
		public const string PROCESSING_TYPE_STOP_EXAM = "4";
		
		#endregion

		#endregion

		#region 患者属性部
		/// <summary>
		/// 患者入外区分-入院
		/// </summary>
		public const string PATIENT_INOUT_ADMISSION = "1";

		/// <summary>
		/// 患者入外区分-外来
		/// </summary>
		public const string PATIENT_INOUT_CLINIC = "2";

		/// <summary>
		/// 結果値-検索結果-検査コード-クレアチニン
		/// </summary>
		public const string PATIENT_RESULT_VALUE_EXAMDATE_EXAM_CODE_CRE = "15";

		/// <summary>
		/// 結果値-検索結果-検査コード-eGFR
		/// </summary>
		public const string PATIENT_RESULT_VALUE_EXAMDATE_EXAM_CODE_EGFR = "234";

		#endregion

		#region オーダ部

		#region 所見要否
		/// <summary>
		/// 所見要否-至急所見
		/// </summary>
		public const string ORDER_REMARK_NECESSITY_IMMEDIATE = "1";

		/// <summary>
		/// 所見要否-所見有り
		/// </summary>
		public const string ORDER_REMARK_NECESSITY_EXIST = "2";
	
		/// <summary>
		/// 所見要否-所見無し
		/// </summary>
		public const string ORDER_REMARK_NECESSITY_NOT_EXIST = "3";

		#endregion

		#region 明細

		#region 項目区分
		/// <summary>
		/// 項目区分-撮影主行為
		/// </summary>
		public const string ORDER_ITEM_KIND_ACT = "JA";

		/// <summary>
		/// 項目区分-部位
		/// </summary>
		public const string ORDER_ITEM_KIND_BUI = "JB";

		/// <summary>
		/// 項目区分-方向
		/// </summary>
		public const string ORDER_ITEM_KIND_HOUKOU = "JH";

		/// <summary>
		/// 項目区分-体位
		/// </summary>
		public const string ORDER_ITEM_KIND_TAII = "JT";

		/// <summary>
		/// 項目区分-行為コメント
		/// </summary>
		public const string ORDER_ITEM_KIND_ACT_COMMENT = "JV";

		/// <summary>
		/// 項目区分-薬剤
		/// </summary>
		public const string ORDER_ITEM_KIND_DRUG = "JD";

		/// <summary>
		/// 項目区分-フィルム
		/// </summary>
		public const string ORDER_ITEM_KIND_FILM = "JF";

		/// <summary>
		/// 項目区分-手技
		/// </summary>
		public const string ORDER_ITEM_KIND_MANIPULATE = "JJ";

		/// <summary>
		/// 項目区分-加算
		/// </summary>
		public const string ORDER_ITEM_KIND_KASAN = "JK";

		/// <summary>
		/// 項目区分-材料
		/// </summary>
		public const string ORDER_ITEM_KIND_MATERIAL = "JL";
		 
		#endregion

		#region コメント区分

		/// <summary>
		/// コメント区分-初期値
		/// </summary>
		public const string ORDER_COMMENT_KIND_DEFAULT = "00";

		/// <summary>
		/// コメント区分-オーダーコメント
		/// </summary>
		public const string ORDER_COMMENT_KIND_ORDERCOMMENT = "73";

		/// <summary>
		/// コメント区分-検査種別ｺコメント
		/// </summary>
		public const string ORDER_COMMENT_KIND_KENSA_TYPE = "79";

		/// <summary>
		/// コメント区分-項目別コメント
		/// </summary>
		public const string ORDER_COMMENT_KIND_ITEMIZED = "71";

		#endregion

		#region 部分中止

		/// <summary>
		/// 部分中止-部分中止
		/// </summary>
		public const string SOME_CANCEL_CANCEL = "1";
		

		#endregion

		#endregion

		#endregion

	}
}
