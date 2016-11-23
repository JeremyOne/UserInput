using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    public static class IEnumerableExtentions {
        
        /// <summary>
        /// Attempts to write a 1-or-2 dimensional IEnumerable to a CSV line or table for human readability
        /// </summary>
        /// <param name="ArrayToWrite">The object to convert</param>
        /// <param name="TableMode">If true each 1-level object will be a Row in the output instead of a Column</param>
        /// <returns>A CSV string</returns>
        public static string ToCsv(this IEnumerable ArrayToWrite, bool TableMode) {
            string csv = "";
            bool headerWritten = false;

            if (TableMode) {
                foreach (var i in ArrayToWrite) {

                    if (headerWritten == false) {
                        csv += UserInput.Data.ObjectToCSVHeaderString(i, true);
                        headerWritten = true;
                    }

                    csv += UserInput.Data.ObjectToCSVString(i) + "\r\n";
                }

            } else {
                csv = UserInput.Data.ObjectToCSVString(ArrayToWrite);
            }

            return csv;
        }

    }
}
