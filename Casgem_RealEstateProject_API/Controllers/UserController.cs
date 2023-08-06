﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Casgem_RealEstateProject_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("GetAll")]
		public ActionResult<List<User>> Get()
		{
			return _userService.Get();
		}

		[HttpGet("GetById")]
		public ActionResult<User> Get(string id)
		{
			var user = _userService.Get(id);
			if (user == null)
			{
				return NotFound($"User with Id = {id} not found");
			}

			return user;
		}

		[HttpPost("Insert")]
		public ActionResult<User> Post([FromBody] User user)
		{
			user.Id = ObjectId.GenerateNewId().ToString();
			_userService.Create(user);

			return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
		}

		[HttpPut("Update")]
		public ActionResult Put(string id, [FromBody] User user)
		{
			var existingEssate = _userService.Get(id);
			

			_userService.Update(id, user);
			return NoContent();
		}

		[HttpDelete("Delete")]
		public ActionResult Delete(string id)
		{
			var essate = _userService.Get(id);
		
			_userService.Remove(essate.Id);
			return Ok();
		}

		[HttpGet("filter")]
		public ActionResult<List<User>> GetByFilter([FromQuery] string? userName = null)
		{
			var user = _userService.GetByFilter(userName);
			return Ok(user);
		}
	}
}
