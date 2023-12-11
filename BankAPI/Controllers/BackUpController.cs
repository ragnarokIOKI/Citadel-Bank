using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using BankAPI.Models;
using System.Data;
using BankAPI.Models.BankAPI.Models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackUpController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public BackUpController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("BackupDatabase")]
        public async Task<ActionResult> DoBackup(string backupPath)
        {
            var pathParam = new SqlParameter("@BackupPath", backupPath ?? (object)DBNull.Value);
            var result = new SqlParameter("@Result", SqlDbType.NVarChar)
            {
                Size = 400,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC BackupDatabase @BackupPath, @Result OUT", pathParam, result);

            return Ok(result.Value.ToString());
        }

        [HttpGet("RestoreDatabase")]
        public async Task<ActionResult> RestoreBackup(string backupFilePath)
        {
            if (string.IsNullOrEmpty(backupFilePath))
            {
                return BadRequest("Путь до файла бэкапа не может быть пустым.");
            }

            string connectionString = "Data Source=DESKTOP-8J6N5LS;Initial Catalog=Bank_Database;Integrated Security=True;Trust Server Certificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string killConnectionsQuery = @"
                                            USE master;
                                            ALTER DATABASE Bank_Database
                                            SET SINGLE_USER
                                            WITH ROLLBACK IMMEDIATE;";
                SqlCommand killConnectionsCommand = new SqlCommand(killConnectionsQuery, connection);
                killConnectionsCommand.ExecuteNonQuery();

                string restoreQuery = @"
                                        RESTORE DATABASE Bank_Database
                                        FROM DISK = @BackupFilePath
                                        WITH REPLACE;";
                SqlCommand restoreCommand = new SqlCommand(restoreQuery, connection);
                restoreCommand.Parameters.Add(new SqlParameter("@BackupFilePath", backupFilePath));
                restoreCommand.ExecuteNonQuery();

                string setMultiUserQuery = @"
                                            USE master;
                                            ALTER DATABASE Bank_Database
                                            SET MULTI_USER;";
                SqlCommand setMultiUserCommand = new SqlCommand(setMultiUserQuery, connection);
                setMultiUserCommand.ExecuteNonQuery();

                return Ok("База данных восстановлена успешно.");
            }
        }

        [HttpGet("CalculateOverpayment")]
        public IActionResult GetOverpaymentResult(decimal creditAmount, decimal creditRate, DateTime dateDrawing, DateTime dateTermination)
        {
            var parameters = new
            {
                Credit_Amount = creditAmount,
                Credit_Rate = creditRate,
                Date_Drawing = dateDrawing,
                Date_Termination = dateTermination
            };

            var overpaymentResult = _context.Set<OverpaymentDTO>()
                .FromSqlRaw("SELECT * FROM [dbo].[Calculate_Overpayment] " +
                            "({0}, {1}, {2}, {3})", parameters.Credit_Amount, parameters.Credit_Rate,
                            parameters.Date_Drawing, parameters.Date_Termination)
                .AsNoTracking()
                .FirstOrDefault();

            if (overpaymentResult == null)
            {
                return NotFound();
            }

            return Ok(overpaymentResult);
        }
    }
}
