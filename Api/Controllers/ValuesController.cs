using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {

        //database stuff
        private const String SERVER = "sql248.main-hosting.eu";
        private const String DATABASE = "u842190477_Exattend";
        private const String UID = "u842190477_Admin";
        private const String PASSWORD = "Aa123456";
        private static MySqlConnection dbConn;
        public static void InitializeDB()
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

        void x() {
            GetEmployees();
        }

        public static List<Employee> GetEmployees()
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
                int Emp_Salary = (int)reader["Emp_Salary"];
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
    }
}
