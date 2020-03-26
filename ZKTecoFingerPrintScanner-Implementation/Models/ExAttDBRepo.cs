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


        //check if the user r
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

        //insert the time,day and month for fingerprint attend to database


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


        //save all fingerprint to Fingertemplate in table  
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


        //get all class activate to verify fingerprint of student
        public List<ClassStatus> GetAllActivatedClasses()
        {
            List<ClassStatus> ListClasse = new List<ClassStatus>();

            String query = "SELECT * FROM class_activate";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String CRN_State = reader["CRN_State"].ToString();
                String Exa_Course_CRN = reader["Exa_Course_CRN"].ToString();



                ClassStatus u = new ClassStatus()
                {
                    CRN_State = CRN_State,
                    Exa_Course_CRN = Exa_Course_CRN
                };

                ListClasse.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return ListClasse;
        }



        //save only one fingerprint per user in one hour 
        internal bool CheckIsSavedthroughOneHour(int uni_ID)
        {
            var currentDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
            String query = "select * From fin_attend where Uni_ID=" + uni_ID + " and Exa_Attend_Time >'" + currentDatetime +"'";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Uni_ID = (int)reader["Uni_ID"];

                if (Uni_ID == uni_ID)
                    return true;
            }
            reader.Close();
            dbConn.Close();
            return false;
        }
    }
}
