namespace ToDo.API.Exceptions
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        string _message;
        public EntityNotFoundException()
        {
            _message = "Entity not found";
        }

        public EntityNotFoundException(string message)
        {
            _message = message;
        }

        public override string Message => _message;
    }
}