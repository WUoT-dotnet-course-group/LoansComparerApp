﻿using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract
{
    public interface IUserService
    {
        Task<bool> UserExistsByEmail(string userEmail);
        Task<AuthDTO> Authenticate(string userEmail);
    }
}