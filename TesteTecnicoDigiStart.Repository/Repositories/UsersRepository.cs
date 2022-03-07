using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TesteTecnicoDigiStart.Domain;
using TesteTecnicoDigiStart.Interface;

namespace TesteTecnicoDigiStart.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext DbContext;
        public UsersRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void CreateNewUser(UserDTO model)
        {
            model.password = ReturnMD5(model.password);

            var entity = new User()
            {
                Name = model.name,
                Email = model.email,
                Password = model.password,
                CreatedIn = DateTime.Now.ToString("dd/MM/yyyy"),
                LastAccess = DateTime.Now.ToString("dd/MM/yyyy"),
                UserRank = UserRank.Regular
            };

            DbContext.Users.Add(entity);
            DbContext.SaveChanges();
            return;
        }

        public bool CheckUserOnDB(string email)
        {
            var user = DbContext.Users.Where(x => x.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefault();

            if (user is null)
                return false;

            else
                return true;
        }
        public bool CheckUserOnDB(LoginDTO model)
        {
            var user = DbContext.Users.Where(x => x.Email.ToUpper().Equals(model.email.ToUpper())).FirstOrDefault();

            if (user is not null)
            {
                if (CompareMD5(user.Password, model.password))
                    return true;

                else
                    return false;
            }

            else
            {
                return false;
            }
        }

        private string ReturnMD5(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return ReturnHash(md5Hash, password);
            }
        }

        private string ReturnHash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool CompareMD5(string storedPassword, string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var md5Password = ReturnMD5(password);
                if (HashVerify(md5Hash, storedPassword, md5Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool HashVerify(MD5 md5Hash, string Md5Password, string password)
        {
            StringComparer compare = StringComparer.OrdinalIgnoreCase;

            if (0 == compare.Compare(Md5Password, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
