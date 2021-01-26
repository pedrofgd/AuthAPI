using AuthAPI.Repositories;
using AuthAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
  [ApiController]
	[Route("v1/settings")]
	public class ProfileController : ControllerBase
	{
		private readonly UserRepository _repository;
		public ProfileController(UserRepository repository)
		{   
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

    [HttpPut("profile")]
    [Authorize]
    public ActionResult<dynamic> UpdateProfile([FromBody]EditProfileViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = _repository.GetUserLogged(User);

        user.Username = model.Username;
        user.Name = model.Name;
        _repository.Put(user);

        return Ok(new 
        { 
          message = "Usuário atualizado com sucesso",
          data = model
        });
      }
      else
      {
        return BadRequest(new { message = "Verifique as informações e tente novamente" });
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<dynamic> DeleteAccount(int id)
    {
      var user = _repository.GetUserLogged(User);

      if (id != user.Id)
        return BadRequest(new { message = "Esse usuário não tem autorização para excluir essa conta" });

      _repository.Delete(user);
      return Ok(new { message = "Conta deletada com sucesso" });
    }
	}
}
