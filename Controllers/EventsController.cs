using AirMonit_Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirMonit_Service.Controllers
{
    public class EventsController : ApiController
    {
        //DESKTOP
         string connectionString = Properties.Settings.Default.connectionString;

        //LAPTOP
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\bruno\\Documents\\AirMonitDB.mdf;Integrated Security=True;Connect Timeout=30";

        [Route("events")]
        [HttpPost]
        public IHttpActionResult PostEvent(Event e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string str = "Insert into Event values (@user_id, @event, @temperature, @city, @date, @time)";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.Parameters.AddWithValue("user_id", e.User_id);
            cmd.Parameters.AddWithValue("event", e.Eventh);
            cmd.Parameters.AddWithValue("temperature",e.Temperature);
            cmd.Parameters.AddWithValue("city", e.City);
            cmd.Parameters.AddWithValue("date", e.Date);
            cmd.Parameters.AddWithValue("time", e.Time);

            int nRows = cmd.ExecuteNonQuery();
            conn.Close();

            if (nRows > 0)
            {
                return Ok(e);
            }
            return NotFound();
        }


        [Route("events")]
        public List<Event> GetAllEvents()
        {
            List<Event> lista = new List<Event>();

            using (var conn = new SqlConnection(connectionString))
            {

                string str = "SELECT * FROM Event";
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
                            string user_idFromTable = objReader.GetString(objReader.GetOrdinal("user_id"));
                            string eventFromTable = objReader.GetString(objReader.GetOrdinal("event"));
                            float tempFromTable = (float) objReader.GetOrdinal("date");
                            string cityFromTable = objReader.GetString(objReader.GetOrdinal("city"));
                            string dateFromTable = objReader.GetString(objReader.GetOrdinal("date"));
                            string timeFromTable = objReader.GetString(objReader.GetOrdinal("time"));

                            lista.Add(new Event { User_id = user_idFromTable, Eventh = eventFromTable, Temperature = tempFromTable, City = cityFromTable, Date = dateFromTable, Time = timeFromTable});
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
