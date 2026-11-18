using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_Library.Models.ErrorHandle;
using MediCore_API.Domain.Interfaces.IRepositories;
using MediCore_API.Domain.Interfaces.Iservices;
using MediCore_API.Infrastructure.Data;

namespace MediCore_API.Infrastructure.Repositories
{
    public class AddressRepo(MediCoreContext context, IMapper mapper, IModelValidation validate) : IAddressRepo
    {
        private readonly AutoMapper.IConfigurationProvider mapConfigProv = mapper.ConfigurationProvider;

        public async Task<Result<List<AddressDTO>>> GetAllAddresses()
        {
            var addresses = await context.Addresses
                .ProjectTo<AddressDTO>(mapConfigProv)
                .ToListAsync();
            return (addresses.Count > 0) ? Result<List<AddressDTO>>.Success(addresses) : Result<List<AddressDTO>>.Failure(Error.NotFound($"No addresses found."));
        }

        public async Task<Result<AddressDTO>> GetAddress([FromRoute] Guid id)
        {
            var address = await context.Addresses
                .ProjectTo<AddressDTO>(mapConfigProv)
                .FirstOrDefaultAsync(a => a.Id == id);
            return (address is not null) ? Result<AddressDTO>.Success(address) : Result<AddressDTO>.Failure(Error.NotFound($"Address not found."));
        }

        public async Task<Result> PostAddress([FromBody] AddressDTO dto)
        {
            if (!validate.AddressIsValid(dto)) return Result.Failure(Error.BadRequest($"Address data not valid."));
            await context.Addresses.AddAsync(mapper.Map<Address>(dto));
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> PatchAddress([FromBody] AddressDTO dto)
        {
            if (dto is null) return Result.Failure(Error.BadRequest($"Address data not valid."));
            var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (address is null) return Result.Failure(Error.NotFound($"Address not found."));

            if (!string.IsNullOrEmpty(dto.Street)) address.Street = dto.Street;
            if (!string.IsNullOrEmpty(dto.City)) address.City = dto.City;
            if (!string.IsNullOrEmpty(dto.ProvinceOrState)) address.ProvinceOrState = dto.ProvinceOrState;
            if (!string.IsNullOrEmpty(dto.Country)) address.Country = dto.Country;
            if (!string.IsNullOrEmpty(dto.PostalCode)) address.PostalCode = dto.PostalCode;

            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAddress([FromRoute] Guid id)
        {
            var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (address is null) return Result.Failure(Error.NotFound($"Address not found."));
            context.Addresses.Remove(address);
            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
