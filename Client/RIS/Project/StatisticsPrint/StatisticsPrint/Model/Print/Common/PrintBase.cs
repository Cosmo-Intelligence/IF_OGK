using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using System.Configuration;
using RISCommonLibrary.Lib.Utils;
using System.IO;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Model.Print.Common;

namespace StatisticsPrint.Model.Print.Common
{
	/// <summary>
	/// 印刷クラス基底
	/// </summary>
	public abstract class PrintBase
	{

		#region フィールド
		
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		#endregion フィールド

		#region プロパティ

		/// <summary>
		/// プレビュー実行か
		/// </summary>
		public bool PreviewOn
		{
			get
			{
				const string PREVIEW_KBN_ON = "1";
				return PREVIEW_KBN_ON == ConfigurationManager.AppSettings["PreviewKbn"].StringToString();
			}
		}

		/// <summary>
		/// ドキュメント名
		/// </summary>
		/// <remarks>
		/// 印刷時は、スプーラ上に表示される印刷JOB名
		/// プレビュー時は、tempディレクトリに作成されるcidファイル名
		/// </remarks>
		public abstract string DocumentName
		{
			 get;
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public abstract string FormFilePath
		{
			get;
		}

		/// <summary>
		/// temporary領域に作成するファイルフルパス
		/// </summary>
		/// <remarks>
		/// プレビュー時に使用する
		/// </remarks>
		public string TempFilePath
		{
			get
			{
				string tempPath = ConfigurationManager.AppSettings["TempDir"].StringToString();
				if (!Directory.Exists(tempPath))
				{
					_log.InfoFormat("{0}ディレクトリ作成しました", tempPath);
					Directory.CreateDirectory(tempPath);
				}
				return Path.Combine(tempPath, string.Format("{0}.cid", DocumentName));
			}
		}
		#endregion

		#region メソッド

		/// <summary>
		/// 印刷/プレビューする
		/// </summary>
		/// <param name="condition"></param>
		public abstract void Print(ConditionPrint condition);


		/// <summary>
		/// 印刷かプレビュー実行
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="list"></param>
		/// <param name="helper"></param>
		/// <param name="writer"></param>
		protected void PrintExecute(ConditionPrint condition, COReportsHelper helper, IPrintWriter writer)
		{
			
			if (this.PreviewOn)
			{
				_log.Debug("プレビューします");
				helper.Preview(DocumentName, FormFilePath, TempFilePath, condition.Copies, writer);
				return;
			}
			_log.Debug("印刷します");
			helper.Print(DocumentName, FormFilePath, condition.PrinterName, condition.Copies, writer);
		}

		#endregion

	}
}
