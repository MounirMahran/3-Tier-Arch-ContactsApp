using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Collections.Specialized;
using System.Dynamic;
using System.Data;
using System.Reflection.Emit;

namespace ContactsApp_DataAccessLayer
{
    public class clsDataAccessLayer
    {
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName,
                                                      ref string Email, ref string Phone,
                                                      ref string Address, ref DateTime DateOfBirth,
                                                      ref int CountryID, ref string ImgPath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@ContactID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImgPath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImgPath = "";
                    }
                }
                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static int AddNewContact(string FirstName, string LastName,
                                                       string Email, string Phone,
                                                       string Address, DateTime DateOfBirth,
                                                       int CountryID, string ImgPath)
        {
            int ContactID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "INSERT INTO [dbo].[Contacts] ([FirstName], [LastName], [Email], [Phone], [Address], [DateOfBirth], [CountryID], [ImagePath])" +
                                    "VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);" +
                                    "SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImgPath != "")
                command.Parameters.AddWithValue("@ImagePath", ImgPath);

            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);




            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedIndex))
                {
                    ContactID = insertedIndex;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return ContactID;
        }


        public static bool UpdateContact(int ID, string FirstName, string LastName,
                                                       string Email, string Phone,
                                                       string Address, DateTime DateOfBirth,
                                                       int CountryID, string ImgPath)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE [dbo].[Contacts]
                               SET [FirstName] = @FirstName
                                  ,[LastName] = @LastName
                                  ,[Email] = @Email
                                  ,[Phone] = @Phone
                                  ,[Address] = @Address
                                  ,[CountryID] = @CountryID
                             WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImgPath != "")
                command.Parameters.AddWithValue("@ImagePath", ImgPath);

            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;

        }


        public static int DeleteContact(int ID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM [dbo].[Contacts]
                               WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected;
        }

        /*public static List<dynamic> ListContacts()
        {
            List<dynamic> Contacts = new List<dynamic>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Contacts;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dynamic contact = new ExpandoObject();

                    contact.ContactId = (int)reader["ContactID"];
                    contact.FirstName = (string)reader["FirstName"];
                    contact.LastName = (string)reader["LastName"];
                    contact.Email = (string)reader["Email"];
                    contact.Phone = (string)reader["Phone"];
                    contact.Address = (string)reader["Address"];
                    contact.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    contact.CountryId = (int)reader["CountryID"];
                    if (contact.ImgPath == System.DBNull.Value)
                    {
                        contact.ImgPath = "";
                    }
                    else
                    {
                        contact.ImgPath = (string)reader["ImagePath"];
                    }

                    Contacts.Add(contact);
                }
                reader.Close();
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                connection.Close();
                
            }

            return Contacts;


        }*/

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Contacts;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }


            return dt;
        }

        public static bool IsContactExist(int ContactID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found = 1 FROM Contacts WHERE ContactID = @ContactID;";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("ContactID", ContactID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    IsFound = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        //Countries
        public static bool GetCountryByID(int ID, ref string CountryName, ref int Code, ref int PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryName = (string)reader["CountryName"];

                    if (reader["Code"] != DBNull.Value)
                    {
                        if (int.TryParse(reader["Code"].ToString(), out int codeValue))
                        {
                            Code = codeValue;
                        }
                    }
                    
                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        if (int.TryParse(reader["PhoneCode"].ToString(), out int phoneCodeValue))
                        {
                            PhoneCode = phoneCodeValue;
                        }
                    }
                    
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);   
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetCountryByName(ref int ID, string CountryName, ref int Code, ref int PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @Name;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ID = (int)reader["CountryID"];
                    if (reader["Code"] != DBNull.Value)
                    {
                        Code = (int)reader["Code"];
                    }
                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        PhoneCode = (int)reader["PhoneCode"];
                    }
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }
        
        public static int AddNewCountry(string CountryName, int Code, int PhoneCode)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "INSERT INTO [dbo].[Countries] ([CountryName], [Code], [PhoneCode])" +
                "                   VALUES  (@CountryName, @Code, @PhoneCode)" +
                "           SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);
            if(Code == -1)
            {
                command.Parameters.AddWithValue("@Code", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Code", Code);

            }
            if (PhoneCode == -1)
            {
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            }

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedIndex))
                {
                    CountryID = insertedIndex;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            { 
                connection.Close(); 
            }

            return CountryID;
        }

        public static int UpdateCountry(int ID, string CountryName, int Code, int PhoneCode)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "UPDATE [dbo].[Countries]  SET [CountryName] = @CountryName, " +
                "                                          [Code] = @Code, [PhoneCode] = @PhoneCode" +
                " WHERE CountryID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (Code == -1)
            {
                command.Parameters.AddWithValue("@Code", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Code", Code);

            }
            if (PhoneCode == -1)
            {
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            }
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                object result = command.ExecuteNonQuery();

                if(result != null && int.TryParse(result.ToString(),out int updatedRows))
                {
                    rowsAffected = updatedRows;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }

        public static int DeleteCountry(int ID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "DELETE FROM [dbo].[Countries]  WHERE CountryID = @ID;";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                object result = command.ExecuteNonQuery();

                if(result != null && int.TryParse(result.ToString(), out int deletedRows))
                {
                    rowsAffected = deletedRows;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;

        }

        public static DataTable GetAllCountries()
        {
            DataTable table = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Countries;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    table.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return table;

        }

        public static bool IsCountryExist(int CountryID)
        {
            int isFound = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found = 1 FROM Countries WHERE CountryID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", CountryID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Index)) 
                {
                    isFound = Index;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound == 1;
        }

        public static bool IsCountryExist(string CountryName)
        {
            int isFound = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found = 1 FROM Countries WHERE CountryName = @Name;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", CountryName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Index))
                {
                    isFound = Index;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound == 1;
        }
    }
}
