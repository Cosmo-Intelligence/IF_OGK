using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Waiting.Common.Utils
{
    /// <summary>
    /// 文字列を正規表現で評価するクラス
    /// </summary>
    class MatchStringFinder
    {
        /// <summary>
        /// この条件の名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 検索する正規表現条件
        /// </summary>
        public string FindPattern { get; set; }
        /// <summary>
        /// 取得する(正規表現中に含まれる)グループ名
        /// </summary>
        public string NameBlock { get; set; }
        /// <summary>
        /// 取得した値
        /// </summary>
        public string Value { get; private set; }
        /// <summary>
        /// 評価した行
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// 合致評価
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool Match(string line)
        {
            Match m = Regex.Match(line, FindPattern, 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (!m.Success)
            {
                return false;
            }
            Line = line;
            Value = m.Groups[NameBlock].Value;//NameBlockが該当しない場合でもstring.Emptyが帰る
            return true;
        }
    }
}
