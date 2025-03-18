using AzureFunctions.Models;
using Microsoft.Extensions.Logging;
using UserService.Domain.Entities;
using UserService.Infrastructure.Context;

namespace AzureFunctions.Services;

public class SaveToSqlService(ApplicationDbContext dbContext)
{
    public async Task SaveToSqlDatabase(UserData userData, ILogger logger)
    {
        var user = new User
        {
            Email = userData.Email,
            Name = userData.Name
        };
        
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Saved user data to SQL Database: {userData.Email}");
    }
}