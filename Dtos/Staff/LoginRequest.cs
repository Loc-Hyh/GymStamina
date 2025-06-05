using System.ComponentModel.DataAnnotations;

namespace GymManagementProject.Dtos.Staff;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }    
    //[Required]
    //public string Email { get; set; }
}