using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Exam.Detail
{
	/// <summary>
	/// オーダ情報明細
	/// </summary>
	public class ExamDetailDynamicArray : DynamicArrayNode
	{
		#region property
		
		/// <summary>
		/// インデクサ
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public new ExamDetailAggregate this[int index]
		{
			get
			{
				return (ExamDetailAggregate)base[index];
			}
		}

		/// <summary>
		/// 明細行繰返し回数ノード
		/// </summary>
		public DataNode EXAM_DETAIL_SUMM
		{
			get;
			set;
		}
		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ExamDetailDynamicArray()
			: base(ExamNodeInfo.EXAM_EXAME_DETAIL_DYNAMIC_SUMM)
		{

		}
		#endregion

		#region method

		/// <summary>
		/// 繰り返すListクラス
		/// </summary>
		/// <returns></returns>
		public override Type GetElementClass()
		{
			return typeof(ExamDetailAggregate);
		}


		/// <summary>
		/// 文字列からノードを復元する
		/// </summary>
		/// <param name="src">復元元文字列</param>
		/// <remarks></remarks>
		public override void Decode(StringIterator src)
		{
			this.ChildCount = Convert.ToInt32(this.EXAM_DETAIL_SUMM.Data);
			
			base.Decode(src);
		}

		/// <summary>
		/// ノードのデータを文字列にする
		/// </summary>
		/// <returns>エンコードされた文字列</returns>
		/// <remarks>電文中に無いのでそのまま返す</remarks>
		public override string Encode()
		{
			return base.Encode();
		}


		/// <summary>
		/// 子ノードを追加する
		/// </summary>
		/// <param name="child">子ノード</param>
		/// <returns>子ノードのインデックス</returns>
		public override int Add(BaseNode child)
		{
			int addedIndex = base.Add(child);

			this.EXAM_DETAIL_SUMM.Data = this.Count.ToString();
			return addedIndex;
		}

		/// <summary>
		/// 指定されたインデックスの子ノードを削除する
		/// </summary>
		/// <param name="index">子ノードのインデックス</param>
		public override void Delete(int index)
		{
			base.Delete(index);
			this.EXAM_DETAIL_SUMM.Data = this.Count.ToString();
		}

		#endregion

	}
}
