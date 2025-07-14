using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RISCommonLibrary.Lib.Utils
{
    /// <summary>
    /// ShiftJISの文字タイプ
    /// </summary>
    public enum TMbcsByteType { mbSingleByte, mbLeadByte, mbTrailByte };
    
    /// <summary>
    /// Shift_JISマルチバイトヘルパークラス
    /// </summary>
    public static class MBCSHelper
    {
		public static readonly Encoding ShiftJISEnc = Encoding.GetEncoding("Shift_JIS");

        #region Copy関数
        /// <summary>
        /// Copy関数
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pos"></param>
        /// <param name="count"></param>
		/// <param name="addFirstTrailByte">
		/// コピー対象範囲の最初の文字がTrailByteだったらインデックスを後ろにずらすか？
		/// </param>
		/// <param name="deleteLastLeadByte">
		/// コピー対象範囲の最後の文字がLeadByteだったらインデックスを前にずらすか？
		/// </param>
        /// <returns></returns>
		public static string Copy(string s, int pos, int count)
		{
			return Copy(s, pos, count, false, false);
		}

        /// <summary>
        /// Copy関数
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pos"></param>
        /// <param name="count"></param>
		/// <param name="addFirstTrailByte">
		/// コピー対象範囲の最初の文字がTrailByteだったらインデックスを後ろにずらすか？
		/// </param>
		/// <param name="deleteLastLeadByte">
		/// コピー対象範囲の最後の文字がLeadByteだったらインデックスを前にずらすか？
		/// </param>
        /// <returns></returns>
		public static string Copy(string s, int pos, int count, 
			bool addFirstTrailByte, bool deleteLastLeadByte)
        {
            // パラメータチェック（空文字）
			if (s == null || s == "")
			{
				return s;
			}

            // パラメータチェック（バイト数）
            int index = Math.Max(0, pos - 1);
            int byteCount = ShiftJISEnc.GetByteCount(s);
			if (index >= byteCount)
			{
				return "";
			}

            // バイト配列に変換
            byte[] src = ShiftJISEnc.GetBytes(s);
			if (addFirstTrailByte)
			{
				//pos指定の最初がmbTrailBytedaだったらインデックスを右にずらす
				if (ByteType(src, index + 1) == TMbcsByteType.mbTrailByte)
				{
					index++;
				}
			}

            // 長さは指定したものか、ソースの長さのどちらかを選択
            int len = Math.Min(count, src.Length - index);
			if (deleteLastLeadByte)
			{
				//最後のバイトがmbLeadByteだったら、削るためにインデックスを前にずらす
				if (ByteType(src, index + len) == TMbcsByteType.mbLeadByte)
				{
					if (len != 0)
					{
						len--;
					}
				}
			}

            // 転送先の配列作成
            byte[] dst = new byte[len];

            // コピー
            Array.Copy(src, index, dst, 0, len);

			// 2020.04.22 Add H.Taira@COSMO Start

			//文字列のbyte数が範囲外の場合文字をカットする
			string examComment = ShiftJISEnc.GetString(dst);
			string endText = null;
			while (ShiftJISEnc.GetByteCount(examComment) > len)
			{
				//末尾の文字取得
				endText = examComment.Substring(examComment.Length - 1, 1);

				//末尾が全角スペースかチェック
				if(!String.IsNullOrWhiteSpace(endText))
				{
				 //stringからchar配列を生成して一部の文字を書き換える
					char[] c = examComment.ToCharArray();
					//末尾に半角スペースセット
					c[examComment.Length - 1] = ' ';

					// string型に戻す
					examComment = new string(c);
				}
				else
				{
					//末尾の文字削除
					examComment = examComment.Substring(0, examComment.Length - 1);
				}
			}
			// 2020.04.22 Add H.Taira@COSMO End

			// 2020.04.22 Mod H.Taira@COSMO Start
			//return ShiftJISEnc.GetString(dst);
			return examComment;
			// 2020.04.22 Mod H.Taira@COSMO Start
		}
		#endregion

		#region Delete関数
		/// <summary>
		/// Delete関数
		/// </summary>
		/// <param name="s"></param>
		/// <param name="pos">削除開始位置</param>
		/// <param name="count">削除バイト数</param>
		/// <returns></returns>
		public static string Delete(string s, int pos, int count)
		{
			// パラメータチェック（空文字）
			if (s == null || s == "")
			{
				return s;
			}

			// パラメータチェック（バイト数）
			int index = Math.Max(0, pos -1);
			int byteCount = ShiftJISEnc.GetByteCount(s);
			if (index >= byteCount)
			{
				return "";
			}

			// バイト配列に変換
			byte[] src = ShiftJISEnc.GetBytes(s);

			// 長さは指定したものか、ソースの長さのどちらかを選択
			int len = Math.Max(0, index); //前半部分
			len += Math.Max(0, src.Length - (index + count)); //後半部分
			if (len < 1)
			{
				return "";
			}

			// 転送先の配列作成
			byte[] dst = new byte[len];
			// 前半コピー
			Array.Copy(src, 0, dst, 0, index);
			if (len == index)
			{
				return ShiftJISEnc.GetString(dst);
			}
			// 後半コピー
			int srcIdx = Math.Min(src.Length, index + count);
			int length = Math.Max(0, (src.Length) - srcIdx);
			Array.Copy(src, srcIdx, dst, index, length);

			return ShiftJISEnc.GetString(dst);
		}
		#endregion

        #region ByteType関数
        /// <summary>
        /// ByteType関数
        /// </summary>
        /// <remarks>
        /// 文字コードは以下のサイトを参考にしました。
        /// http://www5d.biglobe.ne.jp/~noocyte/Programming/CharCode.html#SJIS
        /// </remarks>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static TMbcsByteType ByteType(string s, int index)
        {
            return ByteType(ShiftJISEnc.GetBytes(s), index);
        }

        /// <summary>
        /// ByteType関数
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static TMbcsByteType ByteType(byte[] bytes, int index)
        {
            index = Math.Max(0, index - 1);

            // 範囲を超えた場合、SingleByteを返している。(Delphi 4)
			if (index >= bytes.Length)
			{
				return TMbcsByteType.mbSingleByte;
			}

            TMbcsByteType type = TMbcsByteType.mbSingleByte;
            for (int i = 0; i <= index; i++)
            {
                byte b = bytes[i];
                switch (type)
                {
                    case TMbcsByteType.mbSingleByte:
                    case TMbcsByteType.mbTrailByte:
                        if (0x81 <= b && b <= 0x9F || 0xE0 <= b && b <= 0xFC)
                            type = TMbcsByteType.mbLeadByte;
                        else
                            type = TMbcsByteType.mbSingleByte;
                        break;

                    case TMbcsByteType.mbLeadByte:
                        if (0x40 <= b && b <= 0x7E || 0x80 <= b && b <= 0xFC)
                            type = TMbcsByteType.mbTrailByte;
                        else
                            type = TMbcsByteType.mbSingleByte;
                        break;
                }
            }
            return type;
        }
        #endregion

		/// <summary>
		/// 文字列をシフトJISバイト配列として返す
		/// </summary>
		/// <param name="s">文字列</param>
		/// <returns>シフトJISバイト配列</returns>
		public static byte[] GetSJISByts(this string s)
		{
			return ShiftJISEnc.GetBytes(s);
		}

		/// <summary>
		/// 文字列のシフトJISバイト長を返す
		/// </summary>
		/// <param name="s">文字列</param>
		/// <returns>シフトJISバイト長</returns>
		public static int GetSJISLength(this string s)
		{
			return ShiftJISEnc.GetByteCount(s);
		}

		/// <summary>
		/// 文字列のシフトJIS文字数を返す
		/// </summary>
		/// <param name="s">文字列</param>
		/// <returns>シフトJIS文字数</returns>
		public static int GetSJISCharCount(this string s)
		{
			return ShiftJISEnc.GetCharCount(ShiftJISEnc.GetBytes(s));
		}

		/// <summary>
		/// シフトJISで指定の位置からは何文字にあたるか？
		/// </summary>
		/// <param name="s"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static int ShiftJisCountToCharCount(this string s, int index, int count)
		{
			return ShiftJISEnc.GetCharCount(ShiftJISEnc.GetBytes(s), index, count);
		}

		/// <summary>
		/// PadRightシフトJIS版
		/// </summary>
		/// <param name="s"></param>
		/// <param name="totalByteCount">バイト数</param>
		/// <returns></returns>
		public static string ShiftJisPadRight(this string s, int totalByteCount)
		{
			return ShiftJisPadRight(s, totalByteCount, ' ');
		}

		/// <summary>
		/// PadRightシフトJIS版
		/// </summary>
		/// <param name="s"></param>
		/// <param name="totalByteCount">PadRightシフトJIS版</param>
		/// <param name="paddingChar"></param>
		/// <returns></returns>
		public static string ShiftJisPadRight(this string s, int totalByteCount, char paddingChar)
		{
			byte[] byts = GetSJISByts(s);

			if (byts.Length >= totalByteCount)
			{
				return s;
			}
			byte[] paddingByts = ShiftJISEnc.GetBytes(new []{paddingChar});
			List<Byte> list = new List<byte>(byts);
			while (list.Count < totalByteCount)
			{
				list.Add(paddingByts[0]);
			}
			return ShiftJISEnc.GetString(list.ToArray());
		}

	}
}
