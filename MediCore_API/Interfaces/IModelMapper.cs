using MediCore_API.Data;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;

namespace MediCore_API.Interfaces
{
	public interface IModelMapper
	{
		public TDestination Map<TSource, TDestination>(TSource source);
		public Task<MedicalRecord> MedRecMapAsync(MedicalRecordDTO dto, MediCoreContext context);
	}
}
