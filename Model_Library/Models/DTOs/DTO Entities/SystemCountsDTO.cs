using System;
using System.Collections.Generic;
using System.Text;

namespace MediCore_Library.Models.DTOs.DTO_Entities
{
    public class SystemCountsDTO
    {
        public int Addresses { get; set; } = 0;
        public int Appointments { get; set; } = 0;
        public int Users { get; set; } = 0;
        public int Bills { get; set; } = 0;
        public int Chats { get; set; } = 0;
        public int Doctors { get; set; } = 0;
        public int Feedbacks { get; set; } = 0;
        public int MedicalRecords { get; set; } = 0;
        public int Medicines { get; set; } = 0;
        public int Nurses { get; set; } = 0;
        public int Patients { get; set; } = 0;
        public int Prescriptions { get; set; } = 0;
        public int Schedules { get; set; } = 0;
    }
}
