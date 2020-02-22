using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKTecoFingerPrintScanner_Implementation.Models
{
    public class ExUniDBRepo
    {
        #region Db Initilizaion
        private const String SERVER = "sql248.main-hosting.eu";
        private const String DATABASE = "u842190477_uni";
        private const String UID = "u842190477_Admin1";
        private const String PASSWORD = "Aa123456";
        private static MySqlConnection dbConn;
        public ExUniDBRepo()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connString = builder.ToString();

            builder = null;

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

        }
        #endregion

        public List<Student> GetAllStudents()
        {
            List<Student> ListStudent = new List<Student>();

            String query = "select * from uni_student us ";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Stu_ID = (int)reader["Stu_ID"];
                String Stu_First_Name = reader["Stu_First_Name"].ToString();
                String Stu_Middle_Name = reader["Stu_Middle_Name"].ToString();
                String Stu_Last_Name = reader["Stu_Last_Name"].ToString();
                int Stu_National_Num = (int)reader["Stu_National_Num"];


                Student u = new Student()
                {
                    Stu_First_Name = Stu_First_Name,
                    Stu_ID = Stu_ID,
                    Stu_Last_Name = Stu_Last_Name,
                    Stu_Middle_Name = Stu_Middle_Name,
                    Stu_National_Num = Stu_National_Num
                };

                ListStudent.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return ListStudent;
        }


    }
}
