using MediCore_API.Data;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Entities;

namespace MediCore_API.Interfaces
{
	public interface IModelMapper
	{
		public TDestination Map<TSource, TDestination>(TSource source);
		public Task<MedicalRecord> MapAsync(MedicalRecordDTO dto, MediCoreContext context);
	}
}
