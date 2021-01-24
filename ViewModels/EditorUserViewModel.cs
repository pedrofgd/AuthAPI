using System.ComponentModel.DataAnnotations;

namespace AuthAPI.ViewModels
{
  public class EditorUserViewModel
  {
    [MinLength(3, ErrorMessage = "O username deve ter mais do que 3 caracteres")]
    [MaxLength(30, ErrorMessage = "O username deve ter menos do que 30 caracteres")]
    public string Username { get; set; }

    [MinLength(2, ErrorMessage = "O nome deve ter mais do que 2 caracteres")]
    [MaxLength(30, ErrorMessage = "O nome deve ter menos do que 30 caracteres")]
    public string Name { get; set; }

    [MinLength(6, ErrorMessage = "A senha deve ter mais do que 6 caracteres")]
    [MaxLength(30, ErrorMessage = "A senha deve ter menos do que 30 caracteres")]
    public string Password { get; set; }
  }
}