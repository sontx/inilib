using System.Text;

namespace INILib
{
    internal static class Config
    {
        public static readonly string[] COMMENT_CHARS = { ";", "#" };

        public static readonly string[] SECTION_CHARS = { "[", "]" };

        public static readonly string KEY_CHAR = "=";

        public static readonly bool IGNORE_CASE = true;

        public static readonly bool CREATE_IF_NOT_EXISTS = true;

        public static readonly Encoding TEXT_ENCODING = Encoding.UTF8;
    }
}
