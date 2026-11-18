using MediCore_API.Domain.Interfaces.IRepositories;
using MediCore_Library.Models.DTOs.DTO_Entities;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminRepo adminRepo) : ControllerBase
    {
        [HttpGet("count")]
        public async Task<ActionResult<SystemCountsDTO>> GetAllCounts()
        {
            

            return Ok();
        }

        [HttpGet("salaries")]
        public async Task<ActionResult<TotalSalariesDTO>> GetSalarySums()
        {
            
            return Ok();
        }

        [HttpGet("user-count")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            
            return Ok();
        }
    }
}
