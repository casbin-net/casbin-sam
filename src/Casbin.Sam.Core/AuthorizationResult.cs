namespace Casbin.Sam.Core
{
    public readonly struct SamAuthorizationResult
    {
        public SamAuthorizationResult(bool result) : this(result, 0)
        {
            Result = result;
        }

        public SamAuthorizationResult(bool result, int code)
        {
            Result = result;
            Code = code;
        }

        public bool Result { get; }

        private int Code { get; }
    }
}
