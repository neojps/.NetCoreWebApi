using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserMngt.Api.Resources;
using UserMngt.Api.Validations;
using UserMngt.Core.Models;
using UserMngt.Core.Services;

namespace UserMngt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        
        public UsersController(IUserService UserService, IMapper mapper)
        {
            this._mapper = mapper;
            this._UserService = UserService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAllUsers()
        {
            var Users = await _UserService.GetAll();
            var UserResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(Users);

            return Ok(UserResources);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserResource>> GetUserById(int id)
        {
            var User = await _UserService.GetUserById(id);
            var UserResource = _mapper.Map<User, UserResource>(User);

            return Ok(UserResource);
        }

        [HttpGet("login/{login}")]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetUserByLogin(string login)
        {
            var Users = await _UserService.GetUserByLogin(login);
            var UserResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(Users);

            return Ok(UserResources);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserResource>> CreateUser([FromBody] SaveUserResource saveUserResource)
        {
            var validator = new SaveUserResourceValidator();
            var validationResult = await validator.ValidateAsync(saveUserResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var UserToCreate = _mapper.Map<SaveUserResource, User>(saveUserResource);

            var newUser = await _UserService.CreateUser(UserToCreate);

            var User = await _UserService.GetUserById(newUser.Id);

            var UserResource = _mapper.Map<User, UserResource>(User);

            return Ok(UserResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResource>> UpdateUser(int id, [FromBody] SaveUserResource saveUserResource)
        {
            var validator = new SaveUserResourceValidator();
            var validationResult = await validator.ValidateAsync(saveUserResource);
            
            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok
            
            var UserToBeUpdate = await _UserService.GetUserById(id);

            if (UserToBeUpdate == null)
                return NotFound();

            var User = _mapper.Map<SaveUserResource, User>(saveUserResource);

            await _UserService.UpdateUser(UserToBeUpdate, User);

            var updatedUser = await _UserService.GetUserById(id);
            var updatedUserResource = _mapper.Map<User, UserResource>(updatedUser);
    
            return Ok(updatedUserResource);
        }

        [HttpPut("activate/{id}")]
        public async Task<ActionResult<UserResource>> ActivateUser(int id)
        {
            var UserToActivate = await _UserService.GetUserById(id);

            if (UserToActivate == null)
                return NotFound();

            await _UserService.ActivateUser(UserToActivate);

            var updatedUser = await _UserService.GetUserById(id);
            var updatedUserResource = _mapper.Map<User, UserResource>(updatedUser);
    
            return Ok(updatedUserResource);
        }

        [HttpPut("deactivate/{id}")]
        public async Task<ActionResult<UserResource>> DeactivateUser(int id)
        {
            var UserToDeactivate = await _UserService.GetUserById(id);

            if (UserToDeactivate == null)
                return NotFound();

            await _UserService.DeactivateUser(UserToDeactivate);

            var updatedUser = await _UserService.GetUserById(id);
            var updatedUserResource = _mapper.Map<User, UserResource>(updatedUser);
    
            return Ok(updatedUserResource);
        }

        [HttpGet("auth/{login}/{pass}")]
        public async Task<ActionResult<UserResource>> AuthenticateUser(string login, string pass)
        {
            var UserToAuth = await _UserService.GetUserByLogin(login);

            if (UserToAuth == null)
                return NotFound();

            if(UserToAuth.First().Pass.Equals(pass)){
                var UserResource = _mapper.Map<User, UserResource>(UserToAuth.First());
                return Ok(UserResource);
            }
            
            return StatusCode(401, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
                return BadRequest();
            
            var User = await _UserService.GetUserById(id);

            if (User == null)
                return NotFound();

            await _UserService.DeleteUser(User);

            return NoContent();
        }
    }
}