using TesteTecnicoDigiStart.Domain;

namespace TesteTecnicoDigiStart.Interface
{
    public interface IUsersRepository
    {
        void CreateNewUser(UserDTO model);

        bool CheckUserOnDB(string email);

        bool CheckUserOnDB(LoginDTO model);
    }
}
