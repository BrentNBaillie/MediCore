using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.ErrorHandle;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.Domain.Interfaces.IRepositories
{
    public interface IAddressRepo
    {
        public Task<Result<List<AddressDTO>>> GetAllAddresses();
        public Task<Result<AddressDTO>> GetAddress([FromRoute] Guid id);
        public Task<Result> PostAddress([FromBody] AddressDTO dto);
        public Task<Result> PatchAddress([FromBody] AddressDTO dto);
        public Task<Result> DeleteAddress([FromRoute] Guid id);
    }
}
