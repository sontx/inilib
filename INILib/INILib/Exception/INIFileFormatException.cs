namespace INILib.Exception
{
    public class INIFileFormatException : INIFileException
    {
        public INIFileFormatException() { }
        public INIFileFormatException(string message) : base(message) { }
    }
}
