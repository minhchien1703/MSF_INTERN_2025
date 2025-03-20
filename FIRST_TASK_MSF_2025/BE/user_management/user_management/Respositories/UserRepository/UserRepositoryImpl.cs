using user_management.Data;
using user_management.Dto.RequestDto;
using user_management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using user_management.Dto.ResponseDto;
using System.Data;


namespace user_management.Respositories.UserRepository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<MessageResponseDto> DeleteUserById(int id)
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

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DeleteUserById @Id, @Message OUTPUT",
                    messageParam
                    );

                await transation.CommitAsync();

                return new MessageResponseDto
                {
                    Message = messageParam.Value.ToString()
                };
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FromSqlRaw("EXEC GetUserById @Id", new SqlParameter("@Id", id)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.FromSqlRaw("EXEC GetUsers").AsNoTracking().ToListAsync();
        }

        public async Task<MessageResponseDto> UpdateUserById(int id, UpdateUserRequestDto userRequest)
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
                    "EXEC UpdateUser @Id, @FirstName, @LastName, @Email, @WorkUnit, @ResponsibleUnit, @Status, @Message OUTPUT",
                        new SqlParameter("@Id", id),
                        new SqlParameter("@FirstName", userRequest.FirstName),
                        new SqlParameter("@LastName", userRequest.LastName),
                        new SqlParameter("@Email", userRequest.Email),
                        new SqlParameter("@WorkUnit", userRequest.WorkUnit),
                        new SqlParameter("@ResponsibleUnit", userRequest.ResponsibleUnit),
                        new SqlParameter("@Status", userRequest.Status),
                        messageParam
                    );

                await transation.CommitAsync();

                return new MessageResponseDto
                {
                    Message = messageParam.Value.ToString()
                };
            }
        }

        public async Task<MessageResponseDto> UpdateUserRole(int roleId, int userId)
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
                    "EXEC UpdateUserRole @UserId, @RoleId, @Message OUTPUT",
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@RoleId", roleId),
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
