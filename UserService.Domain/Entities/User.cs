namespace UserService.Domain.Entities;

public class User
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }
    
    public bool IsEmailConfirmed { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public string PasswordHash { get; set; } = string.Empty;
}