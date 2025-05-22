using DBEntities.Models;
public interface IUserDal
{
    public List<User> GetUsers(string? name = "");
    Task CreateUserAsync(User user); // רק יצירת משתמש
    Task CreateSupplierAsync(Supplier supplier); // רק יצירת ספק
    List<User> GetUsersWithSupplier(); // שליפת משתמשים
    User LogIn(string userName, string password);
}