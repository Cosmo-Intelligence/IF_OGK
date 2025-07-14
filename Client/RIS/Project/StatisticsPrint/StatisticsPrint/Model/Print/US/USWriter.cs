using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using CoReportsCore;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Data;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.US.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.US
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class USWriter : PrintWriterBase
	{
		#region フィールド

		/// <summary>
		/// メイン部
		/// </summary>
		private USWriterPart _mainWriter;

		/// <summary>
		/// 超音波総件数部
		/// </summary>
		private USWriterPart _totalWriter;

		/// <summary>
		/// 外部連携部
		/// </summary>
		private USWriterPart _externalWriter;
		
		/// <summary>
		/// 造影剤部
		/// </summary>
		private USWriterPart _zoueizaiWriter;

		/// <summary>
		/// ラベル名設定
		/// </summary>
		private LabelConst[] _labelConsts = new []
		{
			new LabelConst(){COReportsName = "lbl_1", Name = "①心臓領域総件数"},
			new LabelConst(){COReportsName = "lbl_2", Name = "成人"},
			new LabelConst(){COReportsName = "lbl_3", Name = "小児"},
			new LabelConst(){COReportsName = "lbl_4", Name = "造影"},
			new LabelConst(){COReportsName = "lbl_5", Name = "特殊"},
			new LabelConst(){COReportsName = "lbl_6", Name = "経食道"},
			new LabelConst(){COReportsName = "lbl_7", Name = "ポータブル"},
			new LabelConst(){COReportsName = "lbl_8", Name = "②腹部等領域総件数"},
			new LabelConst(){COReportsName = "lbl_9", Name = "腹部等領域"},
			new LabelConst(){COReportsName = "lbl_10", Name = "腹部"},
			new LabelConst(){COReportsName = "lbl_11", Name = "泌尿器系"},
			new LabelConst(){COReportsName = "lbl_12", Name = "産科"},
			new LabelConst(){COReportsName = "lbl_13", Name = "その他"},
			new LabelConst(){COReportsName = "lbl_14", Name = "造影"},
			new LabelConst(){COReportsName = "lbl_15", Name = "経動脈造影"},
			new LabelConst(){COReportsName = "lbl_16", Name = "経静脈造影"},
			new LabelConst(){COReportsName = "lbl_17", Name = "治療等"},
			new LabelConst(){COReportsName = "lbl_18", Name = "ＰＦＡ／ＰＥＩＴ"},
			new LabelConst(){COReportsName = "lbl_19", Name = "生検・穿刺"},
			new LabelConst(){COReportsName = "lbl_20", Name = "ドプラ"},
			new LabelConst(){COReportsName = "lbl_21", Name = "ポータブル"},
			new LabelConst(){COReportsName = "lbl_22", Name = "③体表等領域総件数"},
			new LabelConst(){COReportsName = "lbl_23", Name = "乳腺"},
			new LabelConst(){COReportsName = "lbl_24", Name = "甲状腺"},
			new LabelConst(){COReportsName = "lbl_25", Name = "血管系"},
			new LabelConst(){COReportsName = "lbl_26", Name = "その他"},
			new LabelConst(){COReportsName = "lbl_27", Name = "ドプラ"},
			new LabelConst(){COReportsName = "lbl_28", Name = "超音波総件数"},
			new LabelConst(){COReportsName = "lbl_29", Name = "健康管理センター"},
			new LabelConst(){COReportsName = "lbl_30", Name = "病診連携"},
			new LabelConst(){COReportsName = "lbl_31", Name = "レボビスト"},
			new LabelConst(){COReportsName = "lbl_32", Name = "生食１００ｍｌ　１Ｋ"},
			new LabelConst(){COReportsName = "lbl_33", Name = "生理食塩水　１００ｍｌ"},
			new LabelConst(){COReportsName = "lbl_34", Name = "生理食塩水　　２０ｍｌ"},
			new LabelConst(){COReportsName = "lbl_45", Name = "ソナゾイド"}
		};

		#endregion

		#region プロパティ

		/// <summary>
		/// 部位データクラス
		/// </summary>
		public USItem Data
		{
			get;
			set;
		}

		/// <summary>
		/// 最大行
		/// </summary>
		protected override int MaxRowCount
		{
			get
			{
				throw new NotImplementedException();
			}

		}
		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public USWriter()
		{
			_mainWriter = new USWriterPartMain(this);
			_totalWriter = new USWriterPartUSTotal(this);
			_externalWriter = new USWriterPartExternal(this);
			_zoueizaiWriter = new USWriterPartZoueizai(this);
		}
		#endregion

		#region メソッド

		#region public

		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void Write(object sender, PrintEventArgs e)
		{
			CrForm f = e.crForm;
			//フィールドを全てクリアします
			f.ClearAllFields();

			WriteHeader(Condition, f);
			WriteConst(f);


			ClearDetailFields(f);
			
			_mainWriter.Write(Data, f);
			_totalWriter.Write(Data, f);
			_externalWriter.Write(Data, f);
			_zoueizaiWriter.Write(Data, f);

			PageCount = PrintoutCurrentPage(f, PageCount);
		}

		#endregion

		#region protected
		
		/// <summary>
		/// 明細行クリア
		/// </summary>
		/// <param name="f"></param>
		protected override void ClearDetailFields(CrForm f)
		{
			_mainWriter.ClearDetail(f);
			_totalWriter.ClearDetail(f);
			_externalWriter.ClearDetail(f);
			_zoueizaiWriter.ClearDetail(f);
		}

		/// <summary>
		/// ページ数書き込み
		/// </summary>
		/// <param name="pageCount"></param>
		protected override void WritePageCount(int pageCount, CrForm f)
		{
			//ページ数なし
		}
		#endregion

		#region private

		/// <summary>
		/// 定数書き込み
		/// </summary>
		/// <param name="f"></param>
		private void WriteConst(CrForm f)
		{
			foreach (LabelConst item in _labelConsts)
			{
				f.CrObjects[item.COReportsName].Text = item.Name;
			}
		}

		#endregion

		#endregion

		/// <summary>
		/// ラベル定数
		/// </summary>
		private class LabelConst
		{
			/// <summary>
			/// COReports上のオブジェクト名
			/// </summary>
			public string COReportsName
			{
				get;
				set;
			}

			/// <summary>
			/// 表示する名前
			/// </summary>
			public string Name
			{
				get;
				set;
			}
		}
	}

}
