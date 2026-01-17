using Estrrado_SchoolManagementSystem.Models;
using Microsoft.Data.SqlClient;

namespace Estrrado_SchoolManagementSystem.Repository
{

    public interface IRegistrationRepository
    {
        int AddUser(RegistrationModel user);
    }
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly string _connectionString;
        public RegistrationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int AddUser(RegistrationModel user)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            using SqlTransaction tx = conn.BeginTransaction();

            try
            {
                string userSql = @"
            INSERT INTO Users
            (FullName, Email, Password, ConfirmPassword, Phone, Class, DateOfBirth, Address)
            OUTPUT INSERTED.Id
            VALUES
            (@FullName, @Email, @Password, @ConfirmPassword, @Phone, @Class, @DateOfBirth, @Address)";

                int studentId;

                using (SqlCommand cmd = new SqlCommand(userSql, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Class", user.Class);
                    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);

                    studentId = (int)cmd.ExecuteScalar();
                }

                // INSERT QUALIFICATIONS
                if (user.Qualifications != null && user.Qualifications.Count > 0)
                {
                    string qualSql = @"
                INSERT INTO Qualifications
                (StudentId, Course, University, Year, Percentage)
                VALUES
                (@StudentId, @Course, @University, @Year, @Percentage)";

                    foreach (var q in user.Qualifications)
                    {
                        using SqlCommand qCmd = new SqlCommand(qualSql, conn, tx);
                        qCmd.Parameters.AddWithValue("@StudentId", studentId);
                        qCmd.Parameters.AddWithValue("@Course", q.Course);
                        qCmd.Parameters.AddWithValue("@University", q.University);
                        qCmd.Parameters.AddWithValue("@Year", q.Year);
                        qCmd.Parameters.AddWithValue("@Percentage", q.Percentage);

                        qCmd.ExecuteNonQuery();
                    }
                }

                tx.Commit();
                return studentId;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }



    }
}
