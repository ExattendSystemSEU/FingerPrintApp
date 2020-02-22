﻿using MySql.Data.MySqlClient;
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
                // int Stu_National_Num = (int)reader["Stu_National_Num"];


                Student u = new Student()
                {
                    Stu_First_Name = Stu_First_Name,
                    Stu_ID = Stu_ID,
                    Stu_Last_Name = Stu_Last_Name,
                    Stu_Middle_Name = Stu_Middle_Name,
                  //  Stu_National_Num = Stu_National_Num
                };

                ListStudent.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return ListStudent;
        }


        public List<Employee> GetAllEmployees()
        {
            List<Employee> ListEmployee = new List<Employee>();

            String query = "select * From uni_employee";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Emp_ID = (int)reader["Emp_ID"];
                //String Emp_Password = reader["Emp_Password"].ToString();
                //  int Emp_National_Num = (int)reader["Emp_National_Num"];
                String Emp_First_Name = reader["Emp_First_Name"].ToString();
                String Emp_Middle_Name = reader["Emp_Middle_Name"].ToString();
                String Emp_Last_Name = reader["Emp_Last_Name"].ToString();
                String Emp_Email = reader["Emp_Email"].ToString();
                String Emp_Gender = reader["Emp_Gender"].ToString();
                //float Emp_Salary = (float)reader["Emp_Salary"];
                //int Pos_ID = (int)reader["Pos_ID"];


                Employee u = new Employee()
                {
                    Emp_ID = Emp_ID,
                    //Emp_Password = Emp_Password,
                    Emp_First_Name = Emp_First_Name,
                    Emp_Email = Emp_Email,
                    Emp_Gender = Emp_Gender,
                    Emp_Last_Name = Emp_Last_Name,
                    Emp_Middle_Name = Emp_Middle_Name,
                    //  Emp_National_Num = Emp_National_Num,
                    //Emp_Salary = Emp_Salary,
                    //Pos_ID = Pos_ID
                };

                ListEmployee.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return ListEmployee;
        }

        public Student GetStudent(string stu_ID)
        {
            Student student = new Student();

            String query = "select * from uni_student WHERE Stu_ID=" + stu_ID;

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Stu_ID = (int)reader["Stu_ID"];
                String Stu_First_Name = reader["Stu_First_Name"].ToString();
                String Stu_Middle_Name = reader["Stu_Middle_Name"].ToString();
                String Stu_Last_Name = reader["Stu_Last_Name"].ToString();
              //  int Stu_National_Num = (int)reader["Stu_National_Num"];



                student.Stu_First_Name = Stu_First_Name;
                student.Stu_ID = Stu_ID;
                student.Stu_Last_Name = Stu_Last_Name;
                student.Stu_Middle_Name = Stu_Middle_Name;
              //  student.Stu_National_Num = Stu_National_Num;

            }

            reader.Close();

            dbConn.Close();

            return student;
        }

    }
}