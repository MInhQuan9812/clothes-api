using AutoMapper;
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
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IJwtExtension _jwtExtension;
        private readonly IRepository<User> _userRepo;
        private readonly IEfRepository<User, int> _userEfRepo;

        public UserController(IMapper mapper, IJwtExtension jwtExtension,IRepository<User> userRepo, IEfRepository<User,int> userEfRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _userEfRepo = userEfRepo;
            _jwtExtension = jwtExtension;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto input)
        {
            var user = _userEfRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Email.Equals(input.Email))
                ?? throw new ApplicationException("User does not exist");

            if (!BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                throw new ApplicationException("Password is incorrect");

            //_userRepo.SaveChanges();

            var token = _jwtExtension.GenerateToken(user.Id, user.Role);
            return Ok(new LoginResponeDto(token, _mapper.Map<UserDto>(user)));
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public IActionResult SignUp([FromBody] SignUpDto input)
        {
            var user = _userRepo.Insert(new User()
            {
                UserName=input.UserName,
                FullName = input.FullName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
            });

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
