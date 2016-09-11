namespace INILib.Exception
{
    public class DuplicateElementException : INIFileException
    {
        public DuplicateElementException() { }
        public DuplicateElementException(string message) : base(message) { }
    }
}
