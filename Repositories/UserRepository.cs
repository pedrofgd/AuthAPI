using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Data;
using AuthAPI.Helpers;
using AuthAPI.Models;
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

    public void RegisterValidation(string username, string password)
    {
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("O campo senha é obrigatório");

      if (_context.Users.Any(x => x.Username == username))
        throw new AppException("Username já cadastrado");
    }
  }
}