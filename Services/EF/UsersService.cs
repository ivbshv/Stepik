using Stepik.Models;
using Stepik.Services;
using System.Data;
using System.Globalization;

namespace stepik.Services.EF;

public class UsersService : IUsersService
{
    public bool Add(User user)
    {
        try
        {
            using var dbContext = new ApplicationDbContext();
            dbContext.Users.Add(user);
            var rowsAffected = dbContext.SaveChanges();
            return rowsAffected == 0;
        }
        catch
        {
            return false;
        }
    }

    public string? FormatUserMetrics(int number)
    {
        if(number < 1000)
        {
            return number.ToString();
        }
        else
        {
            double value = number / 1000.0;
            string formatted = value.ToString("0.0", CultureInfo.InvariantCulture);
            formatted = formatted.Replace(".0", "");
            return formatted + "K";
        }
    }

    public User? Get(string fullName)
    {
        using var dbContext = new ApplicationDbContext();

        return dbContext.Users.FirstOrDefault(u => u.full_name == fullName);
    }

    public int GetTotalCount()
    {
        using var dbContext = new ApplicationDbContext();

        return (int)dbContext.Users.Count();
    }

    public DataSet GetUserRating()
    {
        throw new NotImplementedException();
    }

    public DataSet GetUserSocialInfo(string userName)
    {
        throw new NotImplementedException();
    }
}