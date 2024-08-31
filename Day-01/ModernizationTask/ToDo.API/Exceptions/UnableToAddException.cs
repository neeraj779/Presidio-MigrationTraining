namespace ToDo.API.Exceptions
{
    [Serializable]
    internal class UnableToAddException : Exception
    {
        string _message;
        public UnableToAddException()
        {
            _message = "Unable to add entity";
        }

        public override string Message => _message;

    }
}