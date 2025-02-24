using SWP391.ChildGrowthTracking.Repository.DTO.GrowthRecordDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IGrowthRecord
    {
        Task<IEnumerable<GrowthRecordDTO>> GetAll();
        Task<GrowthRecordDTO> GetById(int id);
        Task<GrowthRecordDTO> Create(int childId, GrowthRecordDTO dto);
        Task<bool> Update(int id, GrowthRecordDTO dto);
        Task<bool> Delete(int id);
    }
}
