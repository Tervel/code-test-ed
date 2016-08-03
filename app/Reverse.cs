using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace app {
    class Reverse {
        /// <summary>
        /// Reverses a given string
        /// </summary>
        /// <param name="toReverse"></param>
        /// <returns></returns>
        private static string reverseString(string toReverse) {
            return new string(toReverse.Reverse().ToArray());
        }

        /// <summary>
        /// Reverses a given string of words
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private static string reverseWords(string sentence) {
            var words = sentence.Split(' ');

            for(var i = 0; i < words.Length; ++i) {
                words[i] = reverseString(words[i]);
            }

            return string.Join(" ", words);
        }

        public static void MainOne(string[] args) {
            Debug.Print(reverseWords("Once upon a time in a far away place"));

//            // F# inspired solution (slightly more interesting)
//            Debug.Print(
//                "Once upon a time in a far away place"
//                .Split(' ') // Split to component parts
//                .ToList() // Stick in a list
//                .Aggregate("", (acc, word) => // accumulate each reversed word from the list and pass upto print
//                    acc + " " + new string(word.Reverse().ToArray()))
//            );
        }
    }
}
