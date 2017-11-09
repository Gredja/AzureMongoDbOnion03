﻿using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Application.Services.Auntification
{
    public class Aunification : ControllerBase, IAunification
    {
        private readonly IDbService _dbService;

        public Aunification(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<User> TryLogin(User user)
        {
            var users = await _dbService.GetAllUsers();
            return users.Where(x => x.Email == user.Email).FirstOrDefault(x => x.Password == user.Password);
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
