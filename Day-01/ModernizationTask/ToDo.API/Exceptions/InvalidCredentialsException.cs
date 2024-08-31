namespace ToDo.API.Exceptions
{
    [Serializable]
    internal class InvalidCredentialsException : Exception
    {
        string _message;
        public InvalidCredentialsException()
        {
            _message = "Username or password is incorrect";
        }

        public override string Message => _message;
    }
}