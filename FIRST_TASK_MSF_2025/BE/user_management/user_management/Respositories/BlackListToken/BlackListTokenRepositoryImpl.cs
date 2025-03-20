
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using user_management.Data;
using System.Data;

namespace user_management.Respositories.BlackListToken
{
    public class BlackListTokenRepositoryImpl : IBlackListTokenRepository
    {
        private readonly ApplicationContext _applicationContext;

        public BlackListTokenRepositoryImpl(ApplicationContext context)
        {
            _applicationContext = context;
        } 

        public async Task AddToBlackList(string token, DateTime expiration)
        {
            using (var transation = await _applicationContext.Database.BeginTransactionAsync())
            {
                await _applicationContext.Database.ExecuteSqlRawAsync(
                    "EXEC SaveTokenToBlackList @Token, @Expiration", 
                    new SqlParameter("@Token", token),
                    new SqlParameter("@Expiration", expiration)
                    );

                await transation.CommitAsync();
            }
        }

        public async Task<bool> IsTokenBlackListed(string token)
        {
            var existsParam = new SqlParameter
            {
                ParameterName = "@Exists",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            await _applicationContext.Database.ExecuteSqlRawAsync("EXEC CheckExistsToken @Token, @Exists OUTPUT", new SqlParameter("@Token", token), existsParam);

            return (bool)existsParam.Value;
        }
    }
}
