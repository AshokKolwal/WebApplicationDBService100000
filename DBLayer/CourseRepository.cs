using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication10000.Models;

namespace WebApplication10000.DBLayer
{
    public class CourseRepository
    {
        private readonly string dbServer = "quadrantdb.database.windows.net";
        private readonly string dbName = "quadrantdb";
        private readonly string dbUsername = "Ashok";
        private readonly string dbPassword = "Vitrana@123456";
        private readonly string connString = "Server=tcp:quadrantdb.database.windows.net,1433;Initial Catalog=quadrantdb;Persist Security Info=False;User ID=Ashok;Password=Vitrana@123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public CourseRepository()
        {

        }
        public CourseRepository(IConfiguration configuration) {
           // connString = configuration.GetConnectionString(dbName).ToString();
        }


        public List<Course> GetAllCourses() 
        { 
           List<Course> courses = new List<Course>();
            //using (SqlConnection conn = GetConnection())
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Course;", conn);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = new Course();
                    course.CourseID = Convert.ToInt32(reader["CourseID"]);
                    course.CourseName = Convert.ToString(reader["CourseName"]);
                    course.Rating = Convert.ToDecimal(reader["Rating"]);
                    courses.Add(course);
                }
                conn.Close();
            }
            return courses;
        }

        private SqlConnection GetConnection() 
        { 
            SqlConnection conn = null;
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = dbServer;            
            sqlConnectionStringBuilder.UserID = dbUsername;
            sqlConnectionStringBuilder.Password = dbPassword;
            conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            
           return conn;
        }
    }
}
