using AirMonit_Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace AirMonit_Service.Controllers
{

    public class AlarmsController : ApiController
    {

        //DESKTOP
         string connectionString = Properties.Settings.Default.connectionString;

        //LAPTOP
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\bruno\\Documents\\AirMonitDB.mdf;Integrated Security=True;Connect Timeout=30";

        [Route("alarms")]
        public List<Alarm> GetAllAlarms()
        {
            List<Alarm> lista = new List<Alarm>();

            using (var conn = new SqlConnection(connectionString))
            {

                string str = "SELECT * FROM Alarm";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.CommandType = CommandType.Text;

                conn.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            //I would also check for DB.Null here before reading the value.
                            string nameFromTable = objReader.GetString(objReader.GetOrdinal("name"));
                            int valueFromTable = objReader.GetInt32(objReader.GetOrdinal("value"));
                            string dateFromTable = objReader.GetString(objReader.GetOrdinal("date"));
                            string timeFromTable = objReader.GetString(objReader.GetOrdinal("time"));
                            string cityFromTable = objReader.GetString(objReader.GetOrdinal("city"));
                            string messageFromTable = objReader.GetString(objReader.GetOrdinal("message"));

                            lista.Add(new Alarm { Name = nameFromTable, Value = valueFromTable, Date = dateFromTable, Time = timeFromTable ,City = cityFromTable, Message = messageFromTable });
                        }
                    }
                }
                conn.Close();

                if (lista.Count() > 0)
                {
                    return lista;
                }
                return lista = null;
            }
        }


        [Route("alarms/{start}/{end}")]
        public List<Alarm> GetAlarmsBetweenDates(string start, string end)
        {
            List<Alarm> lista = new List<Alarm>();
            Console.WriteLine(start + end);
            using (var conn = new SqlConnection(connectionString))
            {

                string str = "SELECT * FROM Alarm WHERE DATE BETWEEN @start AND @end";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                cmd.CommandType = CommandType.Text;

                conn.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            //I would also check for DB.Null here before reading the value.
                            string nameFromTable = objReader.GetString(objReader.GetOrdinal("name"));
                            int valueFromTable = objReader.GetInt32(objReader.GetOrdinal("value"));
                            string dateFromTable = objReader.GetString(objReader.GetOrdinal("date"));
                            string cityFromTable = objReader.GetString(objReader.GetOrdinal("city"));
                            string messageFromTable = objReader.GetString(objReader.GetOrdinal("message"));

                            lista.Add(new Alarm { Name = nameFromTable, Value = valueFromTable, Date = dateFromTable, City = cityFromTable, Message = messageFromTable });
                        }
                    }
                }
                conn.Close();

                if (lista.Count() > 0)
                {
                    return lista;
                }
                return lista;
            }
        }
    }
}
