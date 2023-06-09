using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeraMega.Utils
{
    public class DebugHelper
    {
        /// <summary>
        /// Print a text on console withou caption
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        private static void Cmd(string message, string level = "INFO")
        {
            Console.WriteLine($"{DateTime.Now}    {level}: {message}");
        }
        /// <summary>
        /// Print a text on console with caption INFO
        /// </summary>
        /// <param name="message"></param>
        public static void CmdInfo(string message)
        {
            Cmd(message, "INFO");
        }
        /// <summary>
        /// Print a text on sonsole with caotion WARN and yellow color
        /// </summary> 
        /// <param name="message"></param>
        public static void CmdWarn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Cmd(message, "WARNING");
            Console.ResetColor();
        }
        /// <summary>
        /// Print a text on sonsole with caption and red color
        /// </summary>
        /// <param name="message"></param>
        public static void CmdError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Cmd(message, "ERRO");
            Console.ResetColor();
        }
    }
}
