using Microsoft.EntityFrameworkCore;
using Library_management_system.Models.Database;
using Library_management_system.Models.Database.Tables;
using Library_management_system.DTOs;
using CustomExceptionsNS = Library_management_system.Models.Exceptions;
using Library_management_system.Models.database;
using Library_management_system.Enums;
using BCrypt.Net;



namespace Library_management_system.Services
{
    public class UserServices
    {
        private readonly AppDbContext? _context;
        private readonly JwtServices? _jwtServices;


        public UserServices(AppDbContext context, JwtServices jwtServices)
        {
            _context = context;
            _jwtServices = jwtServices;
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterUserDTO dto)
        {
            if (!Enum.IsDefined(typeof(UserTypes), dto.UserType))
            {
                throw new CustomExceptionsNS.CustomExceptions("Invalid user type.", 400);
            }
            var email = dto.Email.ToLower();
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                throw new CustomExceptionsNS.CustomExceptions("Email is Already Registered", 400);
            }

            var user = new Users
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UserType = dto.UserType
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return new RegisterResponseDTO
            {
                UserID = user.Id,
                Username = user.Username,
                Email = user.Email,
                UserType = user.UserType
            };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
        {
            var email = dto.Email.ToLower();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);




            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new CustomExceptionsNS.CustomExceptions("User or password invalid", 400);

            }
            else
            {


                var token = _jwtServices.GenerateToken(user);


                return new LoginResponseDTO
                {
                    UserID = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    UserType = user.UserType,
                    Token = token

                };
            }
        }
    }

    



}
