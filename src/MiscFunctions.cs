using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kerbal_Mod_Manager
{
    class MiscFunctions
    {
        public static void ProcessDirectory(string startLocation, bool includeFirst)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                ProcessDirectory(directory, false);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }

            if (includeFirst)
            {
                try
                {
                    Directory.Delete(startLocation, false);
                }
                catch
                { }
            }
        }

        public static bool PartialMatch(string localName, string onlineName)
        {
            string cleanS1 = CleanString(CleanModName(localName)).Replace("mod", "");
            string cleanS2 = CleanString(onlineName).Replace("mod", "");

            if (cleanS1.Contains(cleanS2) || cleanS2.Contains(cleanS1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string CleanModName(string originalName)
        {
            return CleanString(originalName);
        }

        public static string CleanString(string originalString)
        {
            return originalString
                .ToLower()
                    .Replace("0", "")
                    .Replace("1", "")
                    .Replace("2", "")
                    .Replace("3", "")
                    .Replace("4", "")
                    .Replace("5", "")
                    .Replace("6", "")
                    .Replace("7", "")
                    .Replace("8", "")
                    .Replace("9", "")
                    .Replace(".zip", "")
                    .Replace(".txt", "")
                    .Replace("-", "")
                    .Replace("+", "")
                    .Replace("_", "")
                    .Replace(" ", "")
                    .Replace(".", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("(", "")
                    .Replace("}", "")
                    .Replace("&#x27;", "");
        }

        public static string RemoveLetters(string originalString)
        {
            string returnString = "";
            char[] charArray = originalString.Replace(" ", "").ToLower().ToCharArray();
            List<char> allowedList = new List<char>();

            allowedList.Add('1');
            allowedList.Add('2');
            allowedList.Add('3');
            allowedList.Add('4');
            allowedList.Add('5');
            allowedList.Add('6');
            allowedList.Add('7');
            allowedList.Add('8');
            allowedList.Add('9');
            allowedList.Add('0');

            returnString += "#";
            bool pointMade = true;

            foreach (char c in charArray)
            {
                if (allowedList.Contains(c))
                {
                    returnString += c;
                    pointMade = false;
                }
                else if (!pointMade)
                {
                    returnString += ".";
                    pointMade = true;
                }
            }

            returnString += "#";
            return returnString.Replace(".#", "").Replace("#.", "").Replace("#", "").Replace("..", ".").Replace("..", ".").Replace("..", ".");
        }

        public static string ExtractSection(string s, char[] endChars)
        {
            string _s = "##" + s;
            char[] startCharList = new char[] { '#', '#' };
            return MiscFunctions.ExtractSection(_s, endChars, startCharList);
        }

        public static string ExtractSection(string s, char[] endChars, char[] startChars)
        {
            if (startChars == null || startChars.Length == 0)
            {
                s = "##" + s;
                startChars = new char[] { '#', '#' };
            }

            string returnString = "";
            bool foundChars = false;
            char[] charArray = s.ToCharArray();
            int checkChar = 0;

            foreach (char c in charArray)
            {
                if (startChars[checkChar] == c)
                {
                    checkChar++;
                }
                else
                {
                    checkChar = 0;
                }

                if (foundChars && endChars.Contains(c))
                {
                    break;
                }
                else if (foundChars)
                {
                    returnString += c;
                }

                if (startChars.Length == checkChar)
                {
                    foundChars = true;
                    checkChar = 0;
                }
            }

            return returnString;
        }
    }
}
