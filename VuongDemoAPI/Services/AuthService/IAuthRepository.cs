namespace VuongDemoAPI.Services.AuthService
{
    public interface IAuthRepository
    {
        Task<Response<int>> Register(User user, string password);
        Task<Response<string>> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}
