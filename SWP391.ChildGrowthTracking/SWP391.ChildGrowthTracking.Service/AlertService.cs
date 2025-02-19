using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.DTO.AlertDTO;
using SWP391.ChildGrowthTracking.Repository.Model;

namespace SWP391.ChildGrowthTracking.Repository.Services
{
    public class AlertService : IAlert
    {
        private readonly Swp391ChildGrowthTrackingContext _context;

        public AlertService(Swp391ChildGrowthTrackingContext context)
        {
            _context = context;
        }

        // Get all alerts
        public async Task<List<AlertGetDTO>> GetAllAlerts()
        {
            return await _context.Alerts
                .Select(a => new AlertGetDTO
                {
                    AlertId = a.AlertId,
                    ChildId = a.ChildId,
                    AlertType = a.AlertType,
                    AlertDate = a.AlertDate,
                    Message = a.Message,
                    IsRead = a.IsRead
                })
                .ToListAsync();
        }

        // Get a single alert by ID
        public async Task<AlertGetDTO?> GetAlertById(int alertId)
        {
            var alert = await _context.Alerts
                .Include(a => a.Child)
                .Where(a => a.AlertId == alertId)
                .Select(a => new AlertGetDTO
                {
                    AlertId = a.AlertId,
                    ChildId = a.ChildId,
                    AlertType = a.AlertType,
                    AlertDate = a.AlertDate,
                    Message = a.Message,
                    IsRead = a.IsRead
                })
                .FirstOrDefaultAsync();

            return alert;
        }

        // Create a new alert
        public async Task<AlertGetDTO> CreateAlert(CreateAlertDTO dto)
        {
            var newAlert = new Alert
            {
                ChildId = dto.ChildId,
                AlertType = dto.AlertType,
                AlertDate = dto.AlertDate ?? DateTime.UtcNow,
                Message = dto.Message,
                IsRead = dto.IsRead ?? false
            };

            _context.Alerts.Add(newAlert);
            await _context.SaveChangesAsync();

            return await GetAlertById(newAlert.AlertId)
                ?? throw new Exception("Error retrieving the newly created alert.");
        }

        // Update an alert
        public async Task<AlertGetDTO?> UpdateAlert(int alertId, UpdateAlertDTO dto)
        {
            var alert = await _context.Alerts.FindAsync(alertId);
            if (alert == null) return null;

            alert.ChildId = dto.ChildId ?? alert.ChildId;
            alert.AlertType = dto.AlertType ?? alert.AlertType;
            alert.AlertDate = dto.AlertDate ?? alert.AlertDate;
            alert.Message = dto.Message ?? alert.Message;
            alert.IsRead = dto.IsRead ?? alert.IsRead;

            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();

            return await GetAlertById(alertId);
        }

        // Delete an alert
        public async Task<bool> DeleteAlert(int alertId)
        {
            var alert = await _context.Alerts.FindAsync(alertId);
            if (alert == null) return false;

            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountAlerts()
        {
            return await _context.Alerts.CountAsync();
        }
    }
}
