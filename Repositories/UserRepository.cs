using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Data;
using AuthAPI.Helpers;
using AuthAPI.Models;
using AuthAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Repositories
{
  public class UserRepository
  {
    private readonly DataContext _context;
    public UserRepository(DataContext context)
    {
      _context = context;
    }

    public async void Post(User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
    }

    public async void Put(User user)
    {
      _context.Entry(user).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }

    public async void Delete(User user)
    {
      _context.Users.Remove(user);
      await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<List<ListUsersViewModel>>> GetUsers()
    {
      var users = await _context
        .Users
        .Select(x => new ListUsersViewModel
        {
          Username = x.Username,
          Role = x.Role,
        })
        .ToListAsync();

        return users;
    }

    public User GetUserById(int id)
    {
      var user = _context.Users.Find(id);

      if (user == null)
        throw new AppException("O usuário informado não existe");
      else
        return user;
    }

    public void RegisterValidation(string username, string password)
    {
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("O campo senha é obrigatório");

      if (_context.Users.Any(x => x.Username == username))
        throw new AppException("Username já cadastrado");
    }

    public User UserAuthentication(string username, string password)
    {
      var user = _context.Users.Where(x => 
        x.Username == username && 
        x.Password == password
      ).FirstOrDefault();

      if (user == null)
        throw new AppException("Usuário ou senha inválidos");
      else
        return user;
    }

    public User GetUserLogged(System.Security.Principal.IPrincipal User)
    {
      var user = _context.Users.Where(x =>
        x.Username == User.Identity.Name
      ).FirstOrDefault();

      if (user == null)
        throw new AppException("Usuário não autenticado. Faça login novamente");
      else
        return user;
    }
  }
}