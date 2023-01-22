namespace Harpoon.Application.Backend.Authentication
{
    public interface IAuthProvider
    {
        bool TrySignIn(string passwordHash, string password);
        void SignOut();
        bool CheckPassword(string passwordHash, string password);
        string GetPasswordHash(string password);
    }
}