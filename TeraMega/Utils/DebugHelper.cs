using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeraMega.Utils
{
    public class DebugHelper
    {
        private static void Cmd(string message, string level = "INFO")
        {
            Console.WriteLine($"{DateTime.Now}    {level}: {message}");
        }
        public static void CmdInfo(string message)
        {
            Cmd(message, "INFO");
        }
        public static void CmdWarn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Cmd(message, "WARN");
            Console.ResetColor();
        }
        public static void CmdError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Cmd(message, "ERRO");
            Console.ResetColor();
        }
    }
}
