namespace MisaAsp.Common
{
    [Serializable]
    class InvalidValueException : Exception
    {
        public InvalidValueException() { }

        public InvalidValueException(string fieldName, string errorCode, string errorText, string name = "")
            : base(name)
        {
            FieldName = fieldName;
            ErrorCode = errorCode;
            ErrorText = errorText;
        }

        public string FieldName { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        
    }
}
