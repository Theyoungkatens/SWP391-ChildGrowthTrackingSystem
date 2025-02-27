﻿using SWP391.ChildGrowthTracking.Repository.DTO;
using SWP391.ChildGrowthTracking.Repository.DTO.DoctorDTO;
using SWP391.ChildGrowthTracking.Repository.DTO.UseraccountDTO;
using SWP391.ChildGrowthTracking.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP391.ChildGrowthTracking.Repository
{
    public interface IUseraccount
    {
        Task<List<GetAllUserDTO>> GetAllUsers();
        Task<Useraccount> Authenticate(string username, string password);
        Task<GetAllUserDTO> RegisterUser(RegisterDTO request);
        Task<GetAllUserDTO> UpdateUserAsync(int userId, UpdateUserDTO request);
        Task<GetAllUserDTO> GetUserById(int userId);
        Task<bool> BanUser(int userId);
        Task<bool> RemoveUser(int userId);
        Task<CreateUserDoctorDTO> CreateDoctorAsync(CreateUserDoctorDTO request);
        Task<List<GetAllUserDTO>> GetAllCustomers();

    }
}