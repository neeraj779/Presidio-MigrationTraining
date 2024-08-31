namespace ToDo.API.Exceptions
{
    [Serializable]
    internal class UsernameTakenException : Exception
    {
        string _message;
        public UsernameTakenException()
        {
            _message = "Username is already taken. Please choose another one.";
        }

        public override string Message => _message;
    }
}