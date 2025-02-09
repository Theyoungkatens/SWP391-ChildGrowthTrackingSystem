using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IUseraccount
    {
        Task<List<Useraccount>> GetAllUsers(GetAllDTO request);
    }
}