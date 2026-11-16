using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediCore_Library.Models.ErrorHandle
{
    public readonly record struct Error(ErrorCodes ErrorCode, string Description)
    {
        public static readonly Error None = new(ErrorCodes.None, string.Empty);
        public static Error NotFound(string description) => new(ErrorCodes.NotFound, description);
        public static Error BadRequest(string description) => new(ErrorCodes.BadRequest, description);
        public static Error Validation(string description) => new(ErrorCodes.Validation, description);
        public static Error Failure(string description) => new(ErrorCodes.Failure, description);
        public static Error Unauthorized(string description) => new(ErrorCodes.Unauthorized, description);
    }
}
