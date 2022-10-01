using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Data.Dto;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        public DataContext _context;
        public AccountController( DataContext context , ITokenService tokenService){
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserDtotoken>> Register(UserDto user){
            if(await UserExists(user.UserName)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var register = new AppUser{
                Name = user.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                PasswordSalt = hmac.Key
            };
            _context.users.Add(register);
            await _context.SaveChangesAsync();
            return new UserDtotoken{
                Name = register.Name,
                Token = _tokenService.CreateToken(register)
            };
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserDtotoken>>Login(LoginDto user){
            var login = await _context.users.
            SingleOrDefaultAsync(x => x.Name == user.username);

            using var hmac = new HMACSHA512(login.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != login.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDtotoken{
                Name = login.Name,
                Token = _tokenService.CreateToken(login)
            }; 
        }

        private async Task<bool> UserExists(string username){
            return await _context.users.AnyAsync(x => x.Name == username.ToLower());
        }
    }   
}