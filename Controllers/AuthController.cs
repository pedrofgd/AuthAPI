using System;
using AuthAPI.Data;
using AuthAPI.Helpers;
using AuthAPI.Models;
using AuthAPI.Repositories;
using AuthAPI.Services;
using AuthAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
  [ApiController]
	[Route("v1/account")]
	public class AuthController : ControllerBase
	{
		private readonly DataContext _context;
		private readonly UserRepository _repository;
		public AuthController(DataContext context, UserRepository repository)
		{   
			_context = context;
			_repository = repository;
		}

		[HttpPost]
		[Route("register")]
		[AllowAnonymous]
		public ActionResult<dynamic> CreateUser([FromBody]EditorUserViewModel model)
		{
			try
			{
				_repository.RegisterValidation(model.Username, model.Password);

				var user = new User();
				user.Username = model.Username;
				user.Name = model.Name;
				user.Password = model.Password;
				user.CreateDate = DateTime.Now;
				user.Role = "user"; /* Usuário padrão */

				/* Persiste no db */
				_repository.Post(user);

				/* Oculta a senha */
				model.Password = "";

				return Ok(new { message = "Usuário criado", data = model });
			}
			catch (AppException e)
			{
				return BadRequest(new { message = e.Message });
			}
		}

		[HttpPost]
		[Route("authenticate")]
		[AllowAnonymous]
		public ActionResult<dynamic> Login([FromBody]LoginUserViewModel model)
		{
			try
			{
				var user = _repository.UserAuthentication(model.Username, model.Password);
				var token = TokenService.GenerateToken(user);

				return new {
					Username = user.Username,
					Name = user.Name,
					token = token
				};
			}
			catch (AppException e)
			{
				return BadRequest(new { message = e.Message });
			}
		}
	}
}
