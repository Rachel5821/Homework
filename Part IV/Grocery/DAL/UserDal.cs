using DBEntities;
using DBEntities.Models;
using IDAL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public class UserDal : IUserDal
    {
        public List<User> GetUsers(string? name = "")
        {
            try
            {
                using D_Context context = new D_Context();
                if (string.IsNullOrEmpty(name))
                {
                    return context.Users.ToList(); // פשוט מחזירים את כל המשתמשים
                }
                else
                {
                    return context.Users
                        .Where(a => a.UserName.Contains(name)) // מחפשים משתמשים לפי שם
                        .ToList();
                }
            }
            catch
            {
                return new List<User>(); // אם יש שגיאה מחזירים רשימה ריקה
            }
        }
        public async Task<int> AddUserAsync(User user)
        {
            using D_Context context = new D_Context();
            context.Users.Add(user);  // הוספת המשתמש למסד הנתונים
            await context.SaveChangesAsync();
            return user.Id;  // מחזירים את ה-Id של המשתמש שנוסף
        }
        // יצירת משתמש
        public async Task CreateUserAsync(User user)
        {
            using D_Context context = new D_Context();
            await context.Users.AddAsync(user);  // הוספת המשתמש למסד הנתונים
            await context.SaveChangesAsync();
        }
        // יצירת ספק
        public async Task CreateSupplierAsync(Supplier supplier)
        {
            using D_Context context = new D_Context();
            await context.Suppliers.AddAsync(supplier);  // הוספת הספק למסד הנתונים
            await context.SaveChangesAsync();
        }
        // שליפת כל המשתמשים
        public List<User> GetUsersWithSupplier()
        {
            using D_Context context = new D_Context();
            return context.Users.ToList();  // שליפת המשתמשים בלבד
        }
        public User LogIn(string userName, string password)
        {
            using D_Context ctx = new D_Context();
            return ctx.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            //if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            //{
            //    return ctx.Users
            //              .Where(u => u.UserName == userName && u.Password == password)
            //              .FirstOrDefault();
            //}
            //else
            //{
            //    return null;
            //}
        }
    }
}