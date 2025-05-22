using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace IBL
{
    public interface IUserBL
    {
        // שליפת כל המשתמשים (עם או בלי חיפוש לפי שם)
        List<UserDTO> GETUser(string name = "");
        // יצירת משתמש חדש (ולפעמים גם ספק)
        Task CreateUserAsync(UserDTO userDto);
        UserDTO LogIn(string userName, string password);
        

    }
}