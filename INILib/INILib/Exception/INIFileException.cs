namespace INILib.Exception
{
    public abstract class INIFileException : System.Exception
    {
        public INIFileException() { }
        public INIFileException(string message) : base(message) { }
    }
}
