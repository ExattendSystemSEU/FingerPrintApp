using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKTecoFingerPrintScanner_Implementation.Models
{
    public class ExAttDBRepo
    {
        #region Db Initilizaion
        private const String SERVER = "sql248.main-hosting.eu";
        private const String DATABASE = "u842190477_Exattend";
        private const String UID = "u842190477_Admin";
        private const String PASSWORD = "Aa123456";
        private static MySqlConnection dbConn;
        public ExAttDBRepo()
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

        public List<Employee> GetAllEmployees()
        {
            List<Employee> ListEmployee = new List<Employee>();

            String query = "select * From employee";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Emp_ID = (int)reader["Emp_ID"];
                String Emp_Password = reader["Emp_Password"].ToString();
                int Emp_National_Num = (int)reader["Emp_National_Num"];
                String Emp_First_Name = reader["Emp_First_Name"].ToString();
                String Emp_Middle_Name = reader["Emp_Middle_Name"].ToString();
                String Emp_Last_Name = reader["Emp_Last_Name"].ToString();
                String Emp_Email = reader["Emp_Email"].ToString();
                String Emp_Gender = reader["Emp_Gender"].ToString();
                float Emp_Salary = (float)reader["Emp_Salary"];
                int Pos_ID = (int)reader["Pos_ID"];


                Employee u = new Employee()
                {
                    Emp_ID = Emp_ID,
                    Emp_Password = Emp_Password,
                    Emp_First_Name = Emp_First_Name,
                    Emp_Email = Emp_Email,
                    Emp_Gender = Emp_Gender,
                    Emp_Last_Name = Emp_Last_Name,
                    Emp_Middle_Name = Emp_Middle_Name,
                    Emp_National_Num = Emp_National_Num,
                    Emp_Salary = Emp_Salary,
                    Pos_ID = Pos_ID
                };

                ListEmployee.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return ListEmployee;
        }


        public List<Finger> GetAllFingers()
        {
            List<Finger> ListEmployee = new List<Finger>();

            String query = "select * From fingerprint";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Uni_ID = (int)reader["Uni_ID"];
                String FingerTemplate = reader["FingerTemplate"].ToString();



                Finger u = new Finger()
                {
                    Uni_ID = Uni_ID,
                    FingerTemplate = FingerTemplate
                };

                ListEmployee.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return ListEmployee;
        }

        public Finger IsRegisterdBefor(string uni_ID)
        {
            Finger fingerData = new Finger();

            String query = "select * From fingerprint where Uni_ID=" + uni_ID;

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Uni_ID = (int)reader["Uni_ID"];
                String FingerTemplate = reader["FingerTemplate"].ToString();

                fingerData.Uni_ID = Uni_ID;
                fingerData.FingerTemplate = FingerTemplate;



            }
            reader.Close();
            dbConn.Close();
            return fingerData;
        }


        public int inserAttendanceLogToDB(int Uni_ID)
        {
            var currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlConnection connection = null;

            MySqlConnection dbConn;
            String query = @"INSERT INTO u842190477_Exattend.fin_attend (Exa_Attend_Time, Uni_ID)VALUES('" + currentDateTime + "'," + Uni_ID + ");";
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connString = builder.ToString();

            builder = null;

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection = connection;
            dbConn.Close();

            return 1;
        }

        public int inserFingerPrintToDB(string SoicailID, string FIngerTemplate)
        {

            MySqlConnection connection = null;
            try
            {


                dbConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"INSERT INTO u842190477_Exattend.fingerprint(Uni_ID, Fin_Code, FingerTemplate)VALUES(" + SoicailID + ", " + 0 + ", '" + FIngerTemplate + "'); ";

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return 1;

        }

    }
}
