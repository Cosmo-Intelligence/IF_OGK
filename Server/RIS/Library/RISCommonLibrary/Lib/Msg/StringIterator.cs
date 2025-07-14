using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using RISCommonLibrary.Lib.Utils;

namespace RISCommonLibrary.Lib.Msg
{
	public class StringIterator
	{
		private string _data;
		private string _current;
		/// <summary>
		/// 現在開始インデックス
		/// </summary>
		/// <remarks>
		/// 1オリジン
		/// </remarks>
		private int _currentIndex = 1;

		public string Data
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		public string Current
		{
			get
			{
				return _current;
			}
			set
			{
				_current = value;
			}
		}

		public bool EOF
		{
			get
			{
				return IsEOF();
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="srcString"></param>
		public StringIterator(string srcString)
		{
			_data = srcString;
		}

		public IEnumerator<string> GetEnumerator(int length)
		{
			if (string.IsNullOrEmpty(_data))
			{
				yield break;
			}
			_current = MBCSHelper.Copy(_data, _currentIndex, length);
			_currentIndex += length;
			yield return _current;
		}

		/// <summary>
		/// データを最後まで取得したかどうかを取得する
		/// </summary>
		/// <returns>データを最後まで取得していたらTrue</returns>
		public bool IsEOF()
		{
			return _currentIndex > MBCSHelper.GetSJISLength(_data);
		}

		/// <summary>
		/// 現在位置から指定された長さの文字列を取得する
		/// 取得した文字列はCurrentプロパティで参照する
		/// 取得したサイズ分だけ現在位置を進める
		/// </summary>
		/// <param name="length">取得するサイズ</param>
		/// <returns>実際に取得したサイズ</returns>
		public int GetNext(int length)
		{
			_current = MBCSHelper.Copy(_data, _currentIndex, length);
			int dataIndex = MBCSHelper.GetSJISLength(_current);
			_currentIndex += dataIndex;
			return dataIndex;
		}


	}
}
