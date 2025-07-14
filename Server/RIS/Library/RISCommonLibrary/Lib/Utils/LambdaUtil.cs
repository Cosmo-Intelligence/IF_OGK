//以下からコピー
//http://www.atmarkit.co.jp/fdotnet/dotnettips/986dynamiclinq/dynamiclinq.html

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace RISCommonLibrary.Lib.Utils
{
	public static class LambdaUtil
	{
		/// <summary>
		/// String.ToLowerメソッド。
		/// </summary>
		private static readonly MethodInfo ToLower = 
    typeof(string).GetMethod("ToLower", Type.EmptyTypes);

		/// <summary>
		/// String.Containsメソッド。
		/// </summary>
		private static readonly MethodInfo Contains = 
    typeof(string).GetMethod("Contains");

		/// <summary>
		/// ラムダ式におけるパラメータを作成する。
		/// </summary>
		/// <param name="paramName">パラメータ名</param>
		/// <returns>パラメータ式</returns>
		public static ParameterExpression GetStringParameterExpression(string paramName)
		{
			return Expression.Parameter(typeof(string), paramName);
		}

		/// <summary>
		/// ラムダ式におけるパラメータ式の配列を作成する。
		/// </summary>
		/// <param name="parameters">複数のパラメータ式</param>
		/// <returns>パラメータ式の配列</returns>
		public static ParameterExpression[] GetParameterExpressions(params ParameterExpression[] parameters)
		{
			return parameters;
		}

		/// <summary>
		/// 「x.Contains("keyword")」を条件OR演算子（||）で連結しながら式を作成する。
		/// </summary>
		/// <param name="parameter">ラムダ式の左にあるパラメータ</param>
		/// <param name="keyword">検索キーワード</param>
		/// <param name="curBody">現在の「x.Contains("keyword")」の表現文。初回はnullを指定。2回目以降は前回の戻り値を指定。</param>
		/// <returns>「x.Contains("keyword")」を||演算子で連結した式</returns>
		public static Expression GetContainsExpression(Expression parameter, string keyword, Expression curBody)
		{
			var keywordValue = Expression.Constant(keyword, typeof(string));
			var newBody = Expression.Call(
			  Expression.Call(parameter, ToLower),
			  Contains,
			  Expression.Call(keywordValue, ToLower));
			if (curBody != null)
			{
				return Expression.OrElse(curBody, newBody);
			}
			return newBody;
		}

		/// <summary>
		/// 動的に作成したラムダ式から式ツリー型オブジェクトを取得する。
		/// </summary>
		/// <param name="parameters">パラメータ式の配列（＝ラムダ式の左辺）</param>
		/// <param name="body">式／文（＝ラムダ式の右辺）</param>
		/// <returns>式ツリー型オブジェクト</returns>
		public static Expression<Func<string, bool>> GetLambdaExpressionWhere(ParameterExpression[] parameters, Expression body)
		{
			return Expression.Lambda<Func<string, bool>>(body, parameters);
		}
	}
}
