using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP391.ChildGrowthTracking.Repository.DTO.AlertDTO;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IAlert
    {
        Task<List<AlertGetDTO>> GetAllAlerts();
        Task<AlertGetDTO?> GetAlertById(int alertId);
        Task<AlertGetDTO> CreateAlert(CreateAlertDTO dto);
        Task<AlertGetDTO?> UpdateAlert(int alertId, UpdateAlertDTO dto);
        Task<bool> DeleteAlert(int alertId);
        Task<int> CountAlerts();
    }
}