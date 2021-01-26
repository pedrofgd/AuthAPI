using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Data;
using AuthAPI.Repositories;
using AuthAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
  [ApiController]
	[Route("v1/settings")]
	public class ProfileController : ControllerBase
	{
		private readonly DataContext _context;
		private readonly UserRepository _repository;
		public ProfileController(DataContext context, UserRepository repository)
		{   
			_context = context;
			_repository = repository;
		}

    [HttpPut]
    [Route("security")]
    [Authorize]
    public ActionResult<dynamic> ChangePassowrd([FromBody]ChangePasswordViewModel model)
    {
      var user = _repository.GetUserLogged(User);

      if (model.currentPassword == user.Password)
      {
        user.Password = model.newPassword;
        _repository.Put(user);
      }
      else
        return BadRequest(new { message = "A 'senha atual' informada está incorreta" });

      return Ok(new { message = "Senha atualizada" });
    }
	}
}
