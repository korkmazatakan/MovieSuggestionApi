﻿using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            
            if (registerResult.Success)
            {            
                var result = _authService.CreateAccessToken(registerResult.Data);
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }
        [HttpPut("update")]
        public ActionResult Update(UserForUpdateDto userForUpdateDto)
        {
            var updateResult = _authService.Update(userForUpdateDto);
            if (updateResult.Success)
            {            
                var result = _authService.CreateAccessToken(updateResult.Data);
                return Ok(result.Data);
            }
            return BadRequest(updateResult.Message);
        }
    }
}
