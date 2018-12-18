using System;
using System.Collections.Generic;
using System.Text;

namespace Connect_Four
{
    class Trace
    {
        public static bool ON = false;

        public static void println(String str) {
            if (ON) {
                Console.WriteLine(str);
            }
        }
    }
}
