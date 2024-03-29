﻿using Backend.MovieManagement;
using Microsoft.IdentityModel.Tokens;
using PROG8060_Group.Helpers;
using PROG8060_Group.Models.DB;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PROG8060_Group.Models
{
    public class SessionManager
    {
        private IDbConnectionFactory _connectionFactory;

        public SessionManager(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public UserInfo Login(string username, string password)
        {
            try
            {
                bool ret = false; UserInfo userInfo = new UserInfo();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_login";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@iUsername";
                        pUsername.Value = username;
                        command.Parameters.Add(pUsername);

                        IDbDataParameter pPassword = command.CreateParameter();
                        pPassword.ParameterName = "@iPassword";
                        pPassword.Value = password;
                        command.Parameters.Add(pPassword);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);

                        IDbDataParameter pAdmin = command.CreateParameter();
                        pAdmin.ParameterName = "@oIsAdmin";
                        pAdmin.Direction = ParameterDirection.Output;
                        pAdmin.DbType = DbType.Int32;
                        pAdmin.Size = 50;
                        command.Parameters.Add(pAdmin);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                        userInfo.Name = username;
                        if (Convert.ToBoolean(pAdmin.Value))
                        {
                            userInfo.CanCreate = userInfo.CanDelete = userInfo.CanUpdate = true;
                        }
                        userInfo.CanRead = true;
                    }

                    if (!ret) throw new Exception();
                }

                if(userInfo != null)
                {
                    userInfo.Token = GenerateJwtToken(username);
                }
                return userInfo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to login. {ex.Message}");
            }
        }

        public bool Logout(string username)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_logout";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@iUsername";
                        pUsername.Value = username;
                        command.Parameters.Add(pUsername);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }

                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to logout. {ex.Message}");
            }
        }

        public bool AddUser(UserInfo userInfo)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_add_user";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@iUsername";
                        pUsername.Value = userInfo.Name;
                        command.Parameters.Add(pUsername);

                        IDbDataParameter pPassword = command.CreateParameter();
                        pPassword.ParameterName = "@iPassword";
                        pPassword.Value = userInfo.Password;
                        command.Parameters.Add(pPassword);

                        IDbDataParameter pEmail = command.CreateParameter();
                        pEmail.ParameterName = "@iEmail";
                        pEmail.Value = userInfo.Email;
                        command.Parameters.Add(pEmail);

                        IDbDataParameter pCanCreate = command.CreateParameter();
                        pCanCreate.ParameterName = "@iCanCreate";
                        pCanCreate.Value = userInfo.CanCreate;
                        command.Parameters.Add(pCanCreate);

                        IDbDataParameter pCanUpdate = command.CreateParameter();
                        pCanUpdate.ParameterName = "@iCanUpdate";
                        pCanUpdate.Value = userInfo.CanUpdate;
                        command.Parameters.Add(pCanUpdate);

                        IDbDataParameter pCanRead = command.CreateParameter();
                        pCanRead.ParameterName = "@iCanRead";
                        pCanRead.Value = userInfo.CanRead;
                        command.Parameters.Add(pCanRead);

                        IDbDataParameter pCanDelete = command.CreateParameter();
                        pCanDelete.ParameterName = "@iCanDelete";
                        pCanDelete.Value = userInfo.CanDelete;
                        command.Parameters.Add(pCanDelete);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }

                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add user. {ex.Message}");
            }
        }

        public bool UpdateUser(UserInfo userInfo)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_update_user";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@iUsername";
                        pUsername.Value = userInfo.Name;
                        command.Parameters.Add(pUsername);

                        IDbDataParameter pPassword = command.CreateParameter();
                        pPassword.ParameterName = "@iPassword";
                        pPassword.Value = userInfo.Password;
                        command.Parameters.Add(pPassword);

                        IDbDataParameter pEmail = command.CreateParameter();
                        pEmail.ParameterName = "@iEmail";
                        pEmail.Value = userInfo.Email;
                        command.Parameters.Add(pEmail);

                        IDbDataParameter pCanCreate = command.CreateParameter();
                        pCanCreate.ParameterName = "@iCanCreate";
                        pCanCreate.Value = userInfo.CanCreate;
                        command.Parameters.Add(pCanCreate);

                        IDbDataParameter pCanUpdate = command.CreateParameter();
                        pCanUpdate.ParameterName = "@iCanUpdate";
                        pCanUpdate.Value = userInfo.CanUpdate;
                        command.Parameters.Add(pCanUpdate);

                        IDbDataParameter pCanRead = command.CreateParameter();
                        pCanRead.ParameterName = "@iCanRead";
                        pCanRead.Value = userInfo.CanRead;
                        command.Parameters.Add(pCanRead);

                        IDbDataParameter pCanDelete = command.CreateParameter();
                        pCanDelete.ParameterName = "@iCanDelete";
                        pCanDelete.Value = userInfo.CanDelete;
                        command.Parameters.Add(pCanDelete);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }

                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update user. {ex.Message}");
            }
        }

        public bool DeleteUser(string username)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_delete_user";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@iUsername";
                        pUsername.Value = username;
                        command.Parameters.Add(pUsername);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }

                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete user {username}. {ex.Message}");
            }
        }

        public bool UpdateUserRole(string username, bool canCreate, bool canUpdate, bool canRead, bool canDelete)
        {
            try
            {
                bool ret = false;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_update_user_role";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@iUsername";
                        pUsername.Value = username;
                        command.Parameters.Add(pUsername);

                        IDbDataParameter pCanCreate = command.CreateParameter();
                        pCanCreate.ParameterName = "@iCanCreate";
                        pCanCreate.Value = canCreate;
                        command.Parameters.Add(pCanCreate);

                        IDbDataParameter pCanUpdate = command.CreateParameter();
                        pCanUpdate.ParameterName = "@iCanUpdate";
                        pCanUpdate.Value = canUpdate;
                        command.Parameters.Add(pCanUpdate);

                        IDbDataParameter pCanRead = command.CreateParameter();
                        pCanRead.ParameterName = "@iCanRead";
                        pCanRead.Value = canRead;
                        command.Parameters.Add(pCanRead);

                        IDbDataParameter pCanDelete = command.CreateParameter();
                        pCanDelete.ParameterName = "@iCanDelete";
                        pCanDelete.Value = canDelete;
                        command.Parameters.Add(pCanDelete);

                        IDbDataParameter pRet = command.CreateParameter();
                        pRet.ParameterName = "@oRet";
                        pRet.Direction = ParameterDirection.Output;
                        pRet.DbType = DbType.Int32;
                        pRet.Size = 50;
                        command.Parameters.Add(pRet);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        ret = Convert.ToBoolean(pRet.Value);
                    }

                    if (!ret) throw new Exception();
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update user. {ex.Message}");
            }
        }

        public UserInfo GetUser(string username)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_user";

                        IDbDataParameter pUsername = command.CreateParameter();
                        pUsername.ParameterName = "@userId";
                        pUsername.Value = username;
                        command.Parameters.Add(pUsername);

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                UserInfo userInfo = null;
                if(dataSet.Tables.Count == 1)
                {
                    userInfo = (from rw in dataSet.Tables[0].AsEnumerable()
                                select new UserInfo(Convert.ToString(rw["user_id"]),
                                                    Convert.ToString(rw["email"]),
                                                    Convert.ToBoolean(rw["enable_create"]),
                                                    Convert.ToBoolean(rw["enable_update"]),
                                                    Convert.ToBoolean(rw["enable_read"]),
                                                    Convert.ToBoolean(rw["enable_delete"]))
                                { }).First();
                }
                return userInfo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get user {username}. {ex.Message}");
            }
        }
    
        public UserInfo[] GetUsersAll()
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (var connection = _connectionFactory.CreateConnection())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_get_users_all";

                        connection.Open();
                        IDataAdapter adapter = _connectionFactory.CreateDataAdapter(command);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }
                }

                UserInfo[] userInfos = null;
                if (dataSet.Tables.Count == 1)
                {
                    userInfos = (from rw in dataSet.Tables[0].AsEnumerable()
                                 select new UserInfo(Convert.ToString(rw["user_id"]),
                                                      Convert.ToString(rw["email"]),
                                                      Convert.ToBoolean(rw["enable_create"]),
                                                      Convert.ToBoolean(rw["enable_update"]),
                                                      Convert.ToBoolean(rw["enable_read"]),
                                                      Convert.ToBoolean(rw["enable_delete"]),
                                                      Convert.ToBoolean(rw["enable_create"]) && Convert.ToBoolean(rw["enable_update"]) && Convert.ToBoolean(rw["enable_read"]) && Convert.ToBoolean(rw["enable_delete"]))
                                 { }).ToArray();
                }
                return userInfos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get movie (all).");
            }
        }

        private string GenerateJwtToken(string username)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}