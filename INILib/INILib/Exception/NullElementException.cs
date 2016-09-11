namespace INILib.Exception
{
    public class NullElementException : INIFileException
    {
        public NullElementException() { }
        public NullElementException(string message) : base(message) { }
    }
}
