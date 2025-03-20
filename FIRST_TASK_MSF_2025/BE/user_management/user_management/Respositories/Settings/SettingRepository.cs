using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using user_management.Data;
using user_management.Dto.ResponseDto;
using user_management.Models;
using System.Data;

namespace user_management.Respositories.Settings
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ApplicationContext _applicationContext;

        public SettingRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Setting>> GetSettings()
        {
            return await _applicationContext.Settings.FromSqlRaw("EXEC GetSettings").AsNoTracking().ToListAsync();
        }

        public async Task<MessageResponseDto> UpdateSetting(int id, string newValue)
        {
            using (var transation = await _applicationContext.Database.BeginTransactionAsync())
            {
                var messageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Output
                };

                await _applicationContext.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateSettingById @Id, @Value, @Message OUTPUT",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Value", newValue),
                    messageParam
                    );

                await transation.CommitAsync();

                return new MessageResponseDto
                {
                    Message = messageParam.Value.ToString()
                };
            }
        }
    }
}
