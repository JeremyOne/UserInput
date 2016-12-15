using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    public static class StringExtentions {

        /// <summary>
        /// Takes a CSV string/line and splits it into an array of strings, supports escape characters (like quotes)
        /// </summary>
        /// <param name="StringToSplit">The CSV to split</param>
        /// <param name="SplitChar">The delimiter (usually a comma)</param>
        /// <param name="EscapeChar">The character used to enclose values (usually a quote)</param>
        /// <returns>A list of strings</returns>
        public static string[] SmartSplit(this string StringToSplit, char SplitChar, char EscapeChar) {
            List<string> SplitList = new List<string>();
            bool InEscape = false;
            string CurrentSplitItem = "";

            foreach (char c in StringToSplit) {
                if (c == EscapeChar) {
                    //enter escape mode
                    if (InEscape) {
                        InEscape = false;
                    } else {
                        InEscape = true;
                    }

                }
                if ((c == SplitChar) & InEscape == false) {
                    SplitList.Add(CurrentSplitItem.Trim().Trim(EscapeChar));
                    CurrentSplitItem = "";
                } else {
                    CurrentSplitItem += c;
                }
            }

            if (!string.IsNullOrEmpty(CurrentSplitItem)) {
                SplitList.Add(CurrentSplitItem.Trim().Trim(EscapeChar));
            }

            return SplitList.ToArray();
        }




    }
}
