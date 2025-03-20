using Microsoft.Data.SqlClient;
using user_management.Data;
using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace user_management.Respositories.RoleService
{
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly ApplicationContext _context;

        public RoleRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<MessageResponseDto> DeleteRole(int roleId)
        {
            using (var transation = await _context.Database.BeginTransactionAsync())
            {
                var messageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC DeleteRoleById @Id, @Message OUTPUT", new SqlParameter("@Id", roleId), messageParam);

                await transation.CommitAsync();

                return new MessageResponseDto
                {
                    Message = messageParam.Value.ToString()
                };
            }
        }

        public async Task<Role> FindRoleById(int id)
        {
            return await _context.Roles.FromSqlRaw("EXEC GetRoleById @Id", new SqlParameter("@Id", id)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<MessageResponseDto> SaveRole(RoleRequestDto roleRequest)
        {
            using (var transation = await _context.Database.BeginTransactionAsync())
            {
                var messageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SaveRole @RoleName, @Message OUTPUT",
                    new SqlParameter("@RoleName", roleRequest.RoleName),
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
