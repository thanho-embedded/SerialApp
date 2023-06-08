using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeraMega.Utils
{
    public static class FileExtension
    {
        /// <summary>
        /// Read all data of .ini file
        /// </summary>
        /// <param name="iniFile"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> ReadIniFile(this string iniFile)
        {
            Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();

            if (!File.Exists(iniFile))
            {
                DebugHelper.CmdWarn($"The '{iniFile}' doesn't exists."); // File doesn't exists
            }
            else
            {
                try
                {
                    using (StreamReader reader = new StreamReader(iniFile))
                    {
                        string section = null;
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine()?.Trim();
                            if (!string.IsNullOrEmpty(line))
                            {
                                if (line.StartsWith(";")) continue;                 // Skip comment

                                if (line.StartsWith("[") && line.EndsWith("]"))     // Section
                                {
                                    section = line.Substring(1, line.Length - 2);
                                    dict[section] = new Dictionary<string, string>();
                                }
                                else if (section != null && line.Contains("="))     // Setting
                                {
                                    int index = line.IndexOf('=');
                                    if (index != -1)
                                    {
                                        string key = line.Substring(0, index).Trim();
                                        string value = line.Substring(index + 1).Trim();
                                        dict[section][key] = value;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { DebugHelper.CmdError($"Failed to get file {iniFile}. Ex -> {ex.Message}"); }
            }
            return dict;
        }
    }
}
