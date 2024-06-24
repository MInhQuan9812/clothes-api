using AutoMapper;
using clothes.api.Common;
using clothes.api.Common.Seedworks;
using clothes.api.Common.Settings;
using clothes.api.Dtos.User;
using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clothes.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IJwtExtension _jwtExtension;
        private readonly IRepository<User> _userRepo;
        private readonly IEfRepository<User, int> _userEfRepo;

        public UserController(IMapper mapper, IJwtExtension jwtExtension, IRepository<User> userRepo, IEfRepository<User, int> userEfRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _userEfRepo = userEfRepo;
            _jwtExtension = jwtExtension;
        }

        [HttpGet("getAllUser")]
        public IActionResult getAllUser()
        {
            var queryClause = _userEfRepo.GetQueryableNoTracking()
                                         .Where(x => !x.IsDeleted);
            return Ok(_mapper.Map<ICollection<UserDto>>(queryClause));
        }

        [HttpGet("getAllUserById")]
        public IActionResult getAllUserById(int id)
        {
            var queryClause = _userEfRepo.GetQueryableNoTracking()                                      
                                         .Where(x => !x.IsDeleted && x.Id==id);
            return Ok(_mapper.Map<ICollection<UserDto>>(queryClause));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto input)
        {
            var user = _userEfRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.UserName.Equals(input.UserName))
                ?? throw new ApplicationException("User does not exist");

            if (!BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                throw new ApplicationException("Password is incorrect");

            //_userRepo.SaveChanges();

            var token = _jwtExtension.GenerateToken(user.Id, user.Role);
            return Ok(new LoginResponeDto(token, _mapper.Map<UserDto>(user)));
        }



        [HttpPost("signUp")]
        public IActionResult SignUp([FromBody] SignUpDto input)
        {
            var userExists = _userEfRepo
                .GetQueryableNoTracking()
                .Any(x => x.UserName.Equals(input.UserName));

            if (userExists)
            {
                throw new ApplicationException("User already exists");
            }

            User newUser = new User()
            {
                UserName = input.UserName,
                FullName = input.FullName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                LastUpdate = DateTime.Now,
            };

            newUser = _userRepo.Insert(newUser);

            return Ok(_mapper.Map<UserDto>(newUser));
        }

        [HttpPut("updateInfo")]
        public IActionResult UpdateInfo([FromBody] UpdateInfoDto value)
        {
            var user = _userRepo
               .GetQueryable()
               .FirstOrDefault(x => x.Id == LoggingUserId)
                    ?? throw new ApplicationException("User does not exist");

            user.FullName = value.FullName;
            user.Email = value.Email;
            user.PhoneNumber = value.PhoneNumber;
            user.Gender = value.Gender;
            user.LastUpdate = DateTime.Now;
            user.Avatar = !string.IsNullOrEmpty(value.Avatar) ? value.Avatar : null;
            _userRepo.SaveChanges();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [AllowAnonymous]
        [HttpPut("resetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto value)
        {
            var user = _userRepo
               .GetQueryable()
               .FirstOrDefault(x => x.Id == 1)
                    ?? throw new ApplicationException("User does not exist");

            user.Password = BCrypt.Net.BCrypt.HashPassword(value.Password);
            _userRepo.SaveChanges();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userEfRepo
              .GetQueryableNoTracking()
              .FirstOrDefault(x => x.Id.Equals(id) && !x.IsDeleted)
               ?? throw new ApplicationException("User is not exist");

            _userRepo.Delete(user);
            return Ok();
        }
    }
}
