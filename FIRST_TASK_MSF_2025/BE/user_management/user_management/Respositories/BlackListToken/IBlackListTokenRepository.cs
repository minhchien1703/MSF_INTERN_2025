namespace user_management.Respositories.BlackListToken
{
    public interface IBlackListTokenRepository
    {
        Task AddToBlackList(string token, DateTime expiration);
        Task<bool> IsTokenBlackListed(string token);
    }
}
