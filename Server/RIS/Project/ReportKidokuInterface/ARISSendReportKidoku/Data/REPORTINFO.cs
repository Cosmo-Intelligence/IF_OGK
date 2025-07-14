using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Data
{
    public class ReportInfo
    {
        /// <summary>
        /// 所見ID
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 履歴番号
        /// </summary>
        public string Revision
        {
            get;
            set;
        }

        /// <summary>
        /// 履歴副番号（DEFAULT:0）
        /// </summary>
        public string SubRevision
        {
            get;
            set;
        }

        /// <summary>
        /// 作成日時
        /// </summary>
        public string DrawDate
        {
            get;
            set;
        }

        /// <summary>
        /// 作成医
        /// </summary>
        public string DrawDoctor
        {
            get;
            set;
        }

        /// <summary>
        /// 確定日時
        /// </summary>
        public string FixDate
        {
            get;
            set;
        }

        /// <summary>
        /// 確定者
        /// </summary>
        public string FixDoctor
        {
            get;
            set;
        }

        /// <summary>
        /// 確定解除日時
        /// </summary>
        public string OmitDate
        {
            get;
            set;
        }

        /// <summary>
        /// 確定解除者
        /// </summary>
        public string OmitDoctor
        {
            get;
            set;
        }

        /// <summary>
        /// 所見内容
        /// </summary>
        public string Findings
        {
            get;
            set;
        }

        /// <summary>
        /// 診断
        /// </summary>
        public string Impression
        {
            get;
            set;
        }

        /// <summary>
        /// 2行目以降=超音波ﾃﾝﾌﾟﾚｰﾄデータ列
        /// </summary>
        public string Remarks
        {
            get;
            set;
        }

        /// <summary>
        /// 所見ステータス
        /// </summary>
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// 確定前進捗
        /// </summary>
        public string LastStatus
        {
            get;
            set;
        }

        /// <summary>
        /// ﾄﾗﾝｽｸﾗｲﾊﾞ日付
        /// </summary>
        public string TranscribeDate
        {
            get;
            set;
        }

        /// <summary>
        /// ﾄﾗﾝｽｸﾗｲﾊﾞ名
        /// </summary>
        public string Transcriber
        {
            get;
            set;
        }

        /// <summary>
        /// ﾄﾗﾝｽｸﾗｲﾌﾞ言語（0:日本語　1:英語）
        /// </summary>
        public string TransLanguage
        {
            get;
            set;
        }

        /// <summary>
        /// チェック要フラグ（1:ﾁｪｯｸ要　ELSE:ﾁｪｯｸ不要）
        /// </summary>
        public string TranscheckFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 疑問ありフラグ（1:疑問あり　ELSE:疑問なし）
        /// </summary>
        public string TransqaFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 疑問内容
        /// </summary>
        public string TransqaComment
        {
            get;
            set;
        }

        /// <summary>
        /// 音声ファイル保存パス
        /// </summary>
        public string Filepath
        {
            get;
            set;
        }

        /// <summary>
        /// 音声ファイル名
        /// </summary>
        public string Filename
        {
            get;
            set;
        }

        /// <summary>
        /// 印刷フォームID (=PRINTINFOMASTER.ID)
        /// </summary>
        public string Printform
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rpcomment
        {
            get;
            set;
        }
        /// <summary>
        /// 印刷済みフラグ(0:未印刷　1:印刷済み)
        /// </summary>
        public string Printed
        {
            get;
            set;
        }

        /// <summary>
        /// 所見の文字修飾
        /// </summary>
        public string FindingsRich
        {
            get;
            set;
        }

        /// <summary>
        /// 所見の文字修飾
        /// </summary>
        public string FindingsHtml
        {
            get;
            set;
        }

        /// <summary>
        /// 診断の文字修飾
        /// </summary>
        public string ImpressionRich
        {
            get;
            set;
        }

        /// <summary>
        /// 診断の文字修飾
        /// </summary>
        public string ImpressionHtml
        {
            get;
            set;
        }

        /// <summary>
        /// 作成者コード
        /// </summary>
        public string DrawDoctorcode
        {
            get;
            set;
        }

        /// <summary>
        /// 確定者コード
        /// </summary>
        public string FixDoctorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 確定解除者コード
        /// </summary>
        public string OmitDoctorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 配信所見確認フラグ 0(1以外)：未確認 1：確認済み
        /// </summary>
        public string CheckFlg
        {
            get;
            set;
        }

        /// <summary>
        /// 配信所見確認日
        /// </summary>
        public string CheckDate
        {
            get;
            set;
        }

        /// <summary>
        /// 配信所見確認者
        /// </summary>
        public string CheckDoctor
        {
            get;
            set;
        }

        /// <summary>
        /// 初回確定日
        /// </summary>
        public string FirstFixDate
        {
            get;
            set;
        }
        /// <summary>
        /// 確定所見確認かどうかフラグ（1：確認済、ELSE：未確認）
        /// </summary>
        public string FixConfirmFlg
        {
            get;
            set;
        }

        /// <summary>
        /// 文字装飾をTEXTAREAに内包した後のBODY.INNERHTMLデータ
        /// </summary>
        public string DocumentHtml
        {
            get;
            set;
        }

        /// <summary>
        /// XML化した入力情報
        /// </summary>
        public string DocumentXml
        {
            get;
            set;
        }

        /// <summary>
        /// 入力データを印刷用テンプレートに展開した全HTMLデータ
        /// </summary>
        public string PrintHtml
        {
            get;
            set;
        }

        /// <summary>
        /// HTMLTEMPLATEMASTER.ID
        /// </summary>
        public string HtmlTemplateId
        {
            get;
            set;
        }

        /// <summary>
        /// HTMLFORMMASTER.ID
        /// </summary>
        public string HtmlFormId
        {
            get;
            set;
        }

        /// <summary>
        /// 確定予定者
        /// </summary>
        public string PrefixDoctor
        {
            get;
            set;
        }

        /// <summary>
        /// 確定予定者コード
        /// </summary>
        public string PrefixDoctorCode
        {
            get;
            set;
        }

        /// <summary>
        /// RPWEBでDOCUMENTHTMLの内容を表示する際に使用するCSSファイル
        /// </summary>
        public string DocumentHtmlCss
        {
            get;
            set;
        }

        /// <summary>
        /// 所見内容(4001byte以上で使用)
        /// </summary>
        public string FindingsC
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
            const String FORMAT_LOG = "ID={0}, REVISION={1}, SUBREVISION={2}, DRAWDATE={3}, DRAWDOCTOR={4}, FIXDATE={5}, FIXDOCTOR={6}, OMITDATE={7}, OMITDOCTOR={8}, FINDINGS={9}, IMPRESSION={10}, REMARKS={11}, STATUS={12}, LASTSTATUS={13}, TRANSCRIBEDATE={14}, TRANSCRIBER={15}, TRANSLANGUAGE={16}, TRANSCHECKFLAG={17}, TRANSQAFLAG={18}, TRANSQACOMMENT={19}, FILEPATH={20}, FILENAME={21}, PRINTFORM={22}, RPCOMMENT={23}, PRINTED={24}, FINDINGSRICH={25}, FINDINGSHTML={26}, IMPRESSIONRICH={27}, IMPRESSIONHTML={28}, DRAWDOCTORCODE={29}, FIXDOCTORCODE={30}, OMITDOCTORCODE={31}, CHECKFLG={32}, CHECKDATE={33}, CHECKDOCTOR={34}, FIRSTFIXDATE={35}, FIXCONFIRMFLG={36}, DOCUMENTHTML={37}, DOCUMENTXML={38}, PRINTHTML={39}, HTMLTEMPLATEID={40}, HTMLFORMID={41}, PREFIXDOCTOR={42}, PREFIXDOCTORCODE={43}, DOCUMENTHTMLCSS={44}, FINDINGSC={45}";
            return String.Format(FORMAT_LOG, Id, Revision, SubRevision, DrawDate, DrawDoctor, FixDate, FixDoctor, OmitDate, OmitDoctor, Findings, Impression, Remarks, Status, LastStatus, TranscribeDate, Transcriber, TransLanguage, TranscheckFlag, TransqaFlag, TransqaComment, Filepath, Filename, Printform, Rpcomment, Printed, FindingsRich, FindingsHtml, ImpressionRich, ImpressionHtml, DrawDoctorcode, FixDoctorCode, OmitDoctorCode, CheckFlg, CheckDate, CheckDoctor, FirstFixDate, FixConfirmFlg, DocumentHtml, DocumentXml, PrintHtml, HtmlTemplateId, HtmlFormId, PrefixDoctor, PrefixDoctorCode, DocumentHtmlCss, FindingsC);
        }


    }
}
