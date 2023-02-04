using AutoMapper;
using ECommerice.Api.DTOs;
using ECommerice.Api.Helpers;
using ECommerice.Api.Helpers.Extensions;
using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, 
                                SignInManager<AppUser> signInManager,
                                ITokenService tokenService,
                                IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            //if (user.UserType != "user") return Unauthorized(new ApiResponse(401));
            if (user.IsDeleted == true) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDTO
            {
                Email = user.Email,
                UserData = user,
                Displayname = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("adminLogin")]
        public async Task<ActionResult<AdminUserDTO>> AdminLogin(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));
            if (user.UserType != "admin") return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new AdminUserDTO
            {
                UserData = user,
                Displayname = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO user)
        {
            if (EmailExsist(user.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] {"Email already taken"}
                });
            }

            var addressDto = _mapper.Map<AddressDTO, Address > (user.Address);

            var userToAdd = new AppUser
            {
                DisplayName = user.Displayname,
                Email = user.Email,
                UserName = user.Email,
                UserType = "user",
                PhoneNumber = user.PhoneNumber,
                Address = addressDto,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            var result = await _userManager.CreateAsync(userToAdd, user.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDTO
            {
                Displayname = userToAdd.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(userToAdd)
            };
        }

        [Authorize]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value;
            //var email = User.FindFirstValue(ClaimTypes.Email);
            
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);

            return new UserDTO
            {
                Email = user.Email,
                Displayname = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailExsist")]
        public async Task<ActionResult<bool>> EmailExsist([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("getUserAddress")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindByUserClaimPrincipleWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        [Authorize]
        [HttpPut("updateUserAddress")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
        {
            var user = await _userManager.FindByUserClaimPrincipleWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDTO, Address>(address);
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Problem updaeting user address");

            return Ok(_mapper.Map<Address, AddressDTO>(user.Address));

        }

        [Authorize]
        [HttpGet("getMyProfile")]
        public async Task<ActionResult<UserProfileDTO>> GetMyProfile()
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            var userAddress = _mapper.Map<Address,AddressDTO>(user.Address);
            
            return new UserProfileDTO
            {
                Id = user.Id,
                Email = user.Email,
                Displayname = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Address= userAddress
            };
        }

        [Authorize]
        [HttpPost("updateMyProfile")]
        public async Task<IActionResult> UpdateMyProfile(UserProfileDTO user)
        {
            //var _user = await _userManager.FindByIdAsync(user.Id.ToString());
            var _user = await _userManager.FindByUserClaimPrincipleWithAddressAsync(HttpContext.User);

            if (_user != null)
            {
                _user.UserName = user.Email;
                _user.Email = user.Email;
                _user.DisplayName = user.Displayname;
                _user.PhoneNumber = user.PhoneNumber;
                _user.Address = _mapper.Map<AddressDTO, Address>(user.Address);

                if (user.OldPassword == null || user.NewPassword == null)
                {
                    var userUpdated = await _userManager.UpdateAsync(_user);
                    if (userUpdated.Succeeded)
                        return Ok(user);
                }

                if (user.OldPassword == "" || user.NewPassword == "")
                {
                    var userUpdated = await _userManager.UpdateAsync(_user);
                    if (userUpdated.Succeeded)
                        return Ok(user);
                }
                else
                {
                    var result = await _userManager.ChangePasswordAsync(_user, user.OldPassword, user.NewPassword);
                    if (result.Succeeded)
                        return Ok();
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("getUsers")]
        public async Task<ActionResult<UsersDTO>> GetUsers(CommonSearchDTO commonSearch)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            if(commonSearch.SearchText == null || commonSearch.SearchText == "")
            {
                var users = _userManager.Users.Where(u => u.UserType == "user").ToList();
                var usersToReturn = _mapper.Map<List<AppUser>, List<UsersDTO>>(users)
                    .Skip((commonSearch.PageNo - 1) * commonSearch.pageSize).Take(commonSearch.pageSize);

                return Ok(
                    new
                    {
                        Data = usersToReturn,
                        Count = users.Count()
                    });
            }
            else
            {
                var users = _userManager.Users.Where(u => u.UserType == "user" && u.DisplayName == commonSearch.SearchText).ToList();
                var usersToReturn = _mapper.Map<List<AppUser>, List<UsersDTO>>(users)
                         .Skip((commonSearch.PageNo - 1) * commonSearch.pageSize).Take(commonSearch.pageSize);
                return Ok(new
                    {
                        Data = usersToReturn,
                        Count = users.Count()
                    });
            }
           

           
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult<UsersDTO>> DeleteUser(string id)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            var _user = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();
            _user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(_user);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }
    }
}
