using Microsoft.Extensions.Configuration;
using PayrollPartnerAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollPartnerAPI.DataAccessLayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlconnection;

        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlconnection = new SqlConnection(_configuration["ConnectionStrings:MySqlDBConnectionString"]);
        }

        public async Task<Response> Login(LoginModel model)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (sqlconnection.State != System.Data.ConnectionState.Open)
                {
                    await sqlconnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM crudoperation.userdetail 
                                    WHERE UserName=@UserName AND PassWord=@PassWord AND Role=@Role;";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, sqlconnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", model.userName);
                    sqlCommand.Parameters.AddWithValue("@PassWord", model.password);
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.Message = "Login Successfully";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Login Unsuccessfully";
                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }

        public async Task<Response> SignUp(LoginModel model)
        {
            Response response= new Response();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (sqlconnection.State != System.Data.ConnectionState.Open)
                {
                    await sqlconnection.OpenAsync();
                }

                if (!model.password.Equals(model.confirmPass))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }

                string SqlQuery = @"INSERT INTO crudoperation.userdetail 
                                    (UserName, PassWord, Role) VALUES 
                                    (@UserName, @PassWord, @Role)";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, sqlconnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", model.userName);
                    sqlCommand.Parameters.AddWithValue("@PassWord", model.password);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }
    }
}
