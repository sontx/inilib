using System;

namespace INILib
{
    public static class Utils
    {
        private const string NEW_LINE_REVERSE = "/r/n";

        public static string MultilineToSingleLine(string multiline)
        {
            if (string.IsNullOrEmpty(multiline))
                return multiline;
            return multiline.Replace(Environment.NewLine, NEW_LINE_REVERSE).Replace("\n", NEW_LINE_REVERSE);
        }

        public static string SinglineToMultiline(string singleline)
        {
            return singleline.Replace(NEW_LINE_REVERSE, Environment.NewLine);
        }
    }
}
