using AuthAPI.Data;
using AuthAPI.Helpers;
using AuthAPI.Models;
using AuthAPI.Repositories;
using AuthAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
  [ApiController]
	[Route("v1/management")]
	public class ManagerController : ControllerBase
	{
		private readonly UserRepository _repository;
		public ManagerController(UserRepository repository)
		{   
			_repository = repository;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "manager")]
    public ActionResult<dynamic> UpdateForManager(int id)
    {
      try
      {
        var user = _repository.GetUserById(id);
        user.Role = "manager";

        return Ok(new { message = "Permissões do usuário atualizadas com sucesso" });
      }
      catch (AppException e)
      {
        return BadRequest(new { message = e.Message });
      }
    }
	}
}
