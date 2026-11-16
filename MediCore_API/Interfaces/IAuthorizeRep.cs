using MediCore_Library.Models.Entities;
using MediCore_Library.Models.ErrorHandle;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.Interfaces
{
    public interface IAuthorizeRep
    {
        public Task<Result<Doctor>> RegisterDoctor([FromBody] Register request);
        public Task<Result<Patient>> RegisterPatient([FromBody] Register request);
        public Task<Result<Nurse>> RegisterNurse([FromBody] Register request);
        public Task<Result<LoginResponse>> Login([FromBody] Login request);
    }
}
