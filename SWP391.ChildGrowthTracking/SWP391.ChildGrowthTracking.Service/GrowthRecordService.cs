using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.DTO.GrowthRecordDTO;
using SWP391.ChildGrowthTracking.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GrowthRecordService : IGrowthRecord
{
    private readonly Swp391ChildGrowthTrackingContext _context;

    public GrowthRecordService(Swp391ChildGrowthTrackingContext context)
    {
        _context = context;
    }

    // 🟢 Lấy tất cả GrowthRecords
    public async Task<IEnumerable<GrowthRecordDTO>> GetAll()
    {
        return await _context.GrowthRecords
            .Include(gr => gr.Children) // Lấy danh sách Child liên quan
            .Select(record => new GrowthRecordDTO
            {
                RecordId = record.RecordId,
                Month = record.Month,
                Weight = record.Weight,
                Height = record.Height,
                Bmi = record.Bmi,
                HeadCircumference = record.HeadCircumference,
                UpperArmCircumference = record.UpperArmCircumference,
                RecordedByUser = record.RecordedByUser,
                Notes = record.Notes,
                Old = record.Old,
                ChildId = record.Children.Select(c => c.ChildId).FirstOrDefault() // Lấy ID của Child đầu tiên
            })
            .ToListAsync();
    }

    // 🟢 Lấy GrowthRecord theo ID
    public async Task<GrowthRecordDTO> GetById(int id)
    {
        var record = await _context.GrowthRecords
            .Include(gr => gr.Children)
            .FirstOrDefaultAsync(gr => gr.RecordId == id);

        if (record == null) return null;

        return new GrowthRecordDTO
        {
            RecordId = record.RecordId,
            Month = record.Month,
            Weight = record.Weight,
            Height = record.Height,
            Bmi = record.Bmi,
            HeadCircumference = record.HeadCircumference,
            UpperArmCircumference = record.UpperArmCircumference,
            RecordedByUser = record.RecordedByUser,
            Notes = record.Notes,
            Old = record.Old,
            ChildId = record.Children.Select(c => c.ChildId).FirstOrDefault()
        };
    }

    // 🟢 Tạo GrowthRecord mới và gán vào Child
    public async Task<GrowthRecordDTO> Create(int childId, GrowthRecordDTO dto)
    {
        var child = await _context.Children.FindAsync(childId);
        if (child == null) throw new Exception("Child not found");

        var record = new GrowthRecord
        {
            Month = dto.Month,
            Weight = dto.Weight,
            Height = dto.Height,
            Bmi = dto.Bmi,
            HeadCircumference = dto.HeadCircumference,
            UpperArmCircumference = dto.UpperArmCircumference,
            RecordedByUser = dto.RecordedByUser,
            Notes = dto.Notes,
            Old = dto.Old
        };

        // Thêm GrowthRecord mới
        _context.GrowthRecords.Add(record);
        await _context.SaveChangesAsync();

        // Thêm vào danh sách Records của Child (Many-to-Many)
        child.Records.Add(record);
        await _context.SaveChangesAsync();

        dto.RecordId = record.RecordId;
        return dto;
    }

    // 🟢 Cập nhật GrowthRecord
    public async Task<bool> Update(int id, GrowthRecordDTO dto)
    {
        var record = await _context.GrowthRecords.FindAsync(id);
        if (record == null) return false;

        record.Month = dto.Month;
        record.Weight = dto.Weight;
        record.Height = dto.Height;
        record.Bmi = dto.Bmi;
        record.HeadCircumference = dto.HeadCircumference;
        record.UpperArmCircumference = dto.UpperArmCircumference;
        record.RecordedByUser = dto.RecordedByUser;
        record.Notes = dto.Notes;
        record.Old = dto.Old;

        _context.GrowthRecords.Update(record);
        await _context.SaveChangesAsync();
        return true;
    }

    // 🟢 Xóa GrowthRecord
    public async Task<bool> Delete(int id)
    {
        var record = await _context.GrowthRecords
            .Include(gr => gr.Children) // Load danh sách Child trước khi xóa
            .FirstOrDefaultAsync(gr => gr.RecordId == id);

        if (record == null) return false;

        // Xóa liên kết Many-to-Many trước khi xóa GrowthRecord
        record.Children.Clear();
        await _context.SaveChangesAsync();

        _context.GrowthRecords.Remove(record);
        await _context.SaveChangesAsync();
        return true;
    }
}
