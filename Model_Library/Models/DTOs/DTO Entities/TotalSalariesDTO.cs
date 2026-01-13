using System;
using System.Collections.Generic;
using System.Text;

namespace MediCore_Library.Models.DTOs.DTO_Entities
{
    public class TotalSalariesDTO
    {
        public double TotalDoctorSalaries { get; set; } = 0;
        public double TotalNurseSalaries { get; set; } = 0;
    }
}
