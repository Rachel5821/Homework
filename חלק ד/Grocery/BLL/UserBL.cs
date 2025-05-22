using DBEntities.Models;
using IBL;
using DTO;
using IDAL;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UserBL : IUserBL
    {
        private readonly IUserDal _userDal;
        private readonly MapperConfiguration configUserConverter;

        public UserBL(IUserDal userDal)
        {
            _userDal = userDal;
            configUserConverter = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(x => x.Id, s => s.MapFrom(p => p.Id))
                    .ReverseMap()
                    .ForMember(x => x.Id, s => s.MapFrom(p => p.Id))
            );
        }

        public List<UserDTO> GETUser(string name = "")
        {
            var users = _userDal.GetUsers();
            List<UserDTO> convertedList = new List<UserDTO>();
            users.ForEach(u => convertedList.Add(convertUser(u)));
            return convertedList;
        }

        public UserDTO LogIn(string userName, string password)
        {
            var users = _userDal.GetUsers();
            var user = users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user == null)
            {
                return null;
            }

            //string tokenData = $"{user.Id}:{user.UserName}:{user.IsManager}:{DateTime.Now.AddHours(1).Ticks}";
            //string tokenString = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenData));
            //var userDto = convertUser(user);
            //userDto.Token = tokenString;
            //userDto.Password = null;

            UserDTO userDto = new UserDTO(); // שיניתי את השם מ-user ל-userDto
            userDto.UserName = userName;
            userDto.Password = password;
            userDto.Id=user.Id;
            return userDto;
        }

       



        public async Task CreateUserAsync(UserDTO userDto)
        {
            if (userDto.IsManager == false)
            {
                // יצירת משתמש חדש
                var newUser = new User
                {
                    UserName = userDto.UserName,
                    IsManager = userDto.IsManager,
                    Password = userDto.Password,

                };

                // יצירת ספק חדש
                var newSupplier = new Supplier
                {
                    ManufacturerName = userDto.ManufacturerName,
                    PhoneNumber = userDto.PhoneNumber,
                    RepresentativeName = userDto.RepresentativeName
                };

                // הוספת המשתמש והספק למסד הנתונים
                await _userDal.CreateUserAsync(newUser);
                await _userDal.CreateSupplierAsync(newSupplier);
            }
            else
            {
                // אם המשתמש הוא מנהל, רק יוצרים את המשתמש
                var newUser = new User
                {
                    UserName = userDto.UserName,
                    IsManager = userDto.IsManager
                };

                await _userDal.CreateUserAsync(newUser);
            }
        }

        private UserDTO convertUser(User source)
        {
            var mapper = new Mapper(configUserConverter);
            UserDTO user = mapper.Map<UserDTO>(source);
            return user;
        }
    }
}
