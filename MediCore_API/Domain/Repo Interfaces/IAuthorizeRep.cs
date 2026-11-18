using MediCore_Library.Models.Entities;
using MediCore_Library.Models.ErrorHandle;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.Domain.Interfaces.IRepositories
{
    public interface IAuthorizeRep
    {
        public Task<Result> RegisterDoctor([FromBody] Register request);
        public Task<Result> RegisterPatient([FromBody] Register request);
        public Task<Result> RegisterNurse([FromBody] Register request);
        public Task<Result<LoginResponse>> Login([FromBody] Login request);
    }
}
