using System;
using System.Collections.Generic;
using System.Text;

namespace MediCore_Library.Models.ErrorHandle
{
    public readonly record struct Result<T>(T? Value, Error Error, bool IsSuccess)
    {
        public static Result<T> Success(T? value) => new(value, Error.None, true);
        public static Result<T> Failure(Error error) => new(default, error, false);
    }
}
