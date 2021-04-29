﻿namespace ragther.Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }

        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }

        public Result(bool isSuccess)
        {
            Success = isSuccess;
        }
    }
}
