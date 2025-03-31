namespace UserService.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
    
    public bool IsEmailConfirmed { get; set; }
    
    public bool IsActive { get; set; } = true;
}