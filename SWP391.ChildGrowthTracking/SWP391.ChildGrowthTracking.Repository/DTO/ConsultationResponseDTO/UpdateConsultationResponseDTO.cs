﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository.DTO.ConsultationResponseDTO
{
    public class UpdateConsultationResponseDTO
    {
        public int? RequestId { get; set; }
        public int? DoctorId { get; set; }
      
        public string? Content { get; set; }
        public string? Attachments { get; set; }
        public bool? Status { get; set; }
        public string? Diagnosis { get; set; }
    }

}
