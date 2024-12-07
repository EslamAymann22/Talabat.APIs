using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Talabat.APIsProject.DTOs;
using Talabat.APIsProject.Errors;
using Talabat.APIsProject.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Services;

namespace Talabat.APIsProject.Controllers
{

    public class AccountController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager 
            , SignInManager<AppUser> signInManager
            ,ITokenService tokenService
            ,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                DisplayUser= model.DisplayName,
                PhoneNumber = model.PhoneNumber
            };

            var Result = await _userManager.CreateAsync(user,model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponses(400));

            var ReturnedDto = new UserDto()
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturnedDto);

        }
        [HttpPost("Login")]
        public async Task <ActionResult<UserDto>>Login(LoginDto model)
        {
            var User =await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponses(401));
            var Check = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (Check is null) return Unauthorized(new ApiResponses(401));

            var RetUser = new UserDto()
            {
                Email = model.Email,
                DisplayName = User.DisplayUser,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };
            return Ok(RetUser);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);

            var CurrentUser = new UserDto()
            {
                Email = Email,
                DisplayName = user.DisplayUser,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(CurrentUser);
        }

        [Authorize]
        [HttpGet("GetCurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            //var Email = await GetCurrentUser().email;

            //var Email = User.FindFirstValue(ClaimTypes.Email);
            //var user = await _userManager.FindByEmailAsync(Email);
                
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<Address,AddressDto>(user.Address);
            return Ok(MappedAddress);
        }

    }
}
