using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.DTOs
{
	public class StaffDTO
	{
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid RoleId { get; set; } = Guid.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}
