
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using AirMonit_Service.Models;
using System.Globalization;

namespace AirMonit_Service.Controllers
{
    public class ParametersController : ApiController
    {
        //DESKTOP
        string connectionString = Properties.Settings.Default.connectionString;

        //LAPTOP
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\bruno\\Documents\\AirMonitDB.mdf;Integrated Security=True;Connect Timeout=30";

        //TODO: FAZER METODOS TODOS PARA A APP ADMIN E AINDA OS ALARMES E EVENTOS

        //GetHourlySummarizedInformationAllCities
        [Route("parameters/{parameter}/{date}/{city}")]
        public IHttpActionResult GetHourlySummarizedInformationAllCities(string parameter, string date, string city)
        {
            string str = "";
            SqlCommand cmd;
            List<Hour> listaHourly = new List<Hour>();
            float avgHour = new float();
            int sumValues = new int();
            int numValues = new int();
            int min = new int();
            int max = new int();
            int aux = new int();

            for (int i = 0; i < 24; i++)
            {
                avgHour = 0;
                sumValues = 0;
                numValues = 0;
                min = 0;
                max = 0;
                aux = 0;
            }



            using (var conn = new SqlConnection(connectionString))
            {
                List<Parameter> lista = new List<Parameter>();

                int hour = 0;
                if (city == "all")
                {
                    str = "SELECT * FROM Parameter WHERE name=@name AND date=@date";
                    cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("name", parameter);
                    cmd.Parameters.AddWithValue("date", date);
                }
                else
                {
                    str = "SELECT * FROM Parameter WHERE name=@name AND date=@date AND city=@city";
                    cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("name", parameter);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("city", city);
                }



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


                            lista.Add(new Parameter { Name = nameFromTable, Date = dateFromTable, Time = timeFromTable, City = cityFromTable, Value = valueFromTable });

                        }
                    }

                    foreach (Parameter item in lista)
                    {
                        string[] formattedTime = item.Time.Split(':');
                        hour = int.Parse(formattedTime[0]);
                        for (int i = 0; i < 24; i++)
                        {
                            if (hour == i)
                            {
                                aux = item.Value;
                                numValues++;
                                sumValues += aux;

                                if (numValues == 1)
                                {
                                    min = aux;
                                    max = aux;
                                }
                                else
                                {
                                    if (aux < min)
                                    {
                                        min = aux;

                                    }
                                    else if (aux > max)
                                    {
                                        max = aux;
                                    }
                                }

                            }
                        }

                    }
                    if (numValues != 0)
                    {
                        avgHour = sumValues / numValues;
                        listaHourly.Add(new Hour { Avg = avgHour, HourNr = hour, Max = max, Min = min });
                    }
                }
                conn.Close();

                if (listaHourly.Count() > 0)
                {
                    return Ok(listaHourly);
                }
                return NotFound();
            }
        }

        [Route("parameters/{parameter}/{date1}/{date2}/{city}")]
        public IHttpActionResult GetDailySummarizedInformationAllCities(string parameter, string date1, string date2, string city)
        {
            string str = "";
            SqlCommand cmd;
            List<WeekDay> listaDaily = new List<WeekDay>();
            float avgHour = new float();
            int sumValues = new int();
            int numValues = new int();
            int min = new int();
            int max = new int();
            int aux = new int();

            for (int i = 0; i < 24; i++)
            {
                avgHour = 0;
                sumValues = 0;
                numValues = 0;
                min = 0;
                max = 0;
                aux = 0;
            }



            using (var conn = new SqlConnection(connectionString))
            {
                List<Parameter> lista = new List<Parameter>();

                int day = 0;
                if (city == "all")
                {
                    str = "SELECT * FROM Parameter WHERE name=@name AND (date BETWEEN @date1 AND @date2)";
                    cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("name", parameter);
                    cmd.Parameters.AddWithValue("date1", date1);
                    cmd.Parameters.AddWithValue("date2", date2);
                }
                else
                {
                    str = "SELECT * FROM Parameter WHERE name=@name AND (date BETWEEN @date1 AND @date2) AND city=@city";
                    cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("name", parameter);
                    cmd.Parameters.AddWithValue("date1", date1);
                    cmd.Parameters.AddWithValue("date2", date2);
                    cmd.Parameters.AddWithValue("city", city);
                }



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


                            lista.Add(new Parameter { Name = nameFromTable, Date = dateFromTable, Time = timeFromTable, City = cityFromTable, Value = valueFromTable });

                        }
                    }

                    foreach (Parameter item in lista)
                    {
                        string[] formattedDate = item.Date.Split('-');
                        day = int.Parse(formattedDate[0]);
                        for (int i = 0; i < 31; i++)
                        {
                            if (day == i)
                            {
                                aux = item.Value;
                                numValues++;
                                sumValues += aux;

                                if (numValues == 1)
                                {
                                    min = aux;
                                    max = aux;
                                }
                                else
                                {
                                    if (aux < min)
                                    {
                                        min = aux;

                                    }
                                    else if (aux > max)
                                    {
                                        max = aux;
                                    }
                                }
                                

                            }
                            
                        }
                        
                    }
                    if (numValues != 0)
                    {
                        avgHour = sumValues / numValues;
                        listaDaily.Add(new WeekDay { Avg = avgHour, DayNr = day, Max = max, Min = min });
                    }


                }
                conn.Close();

                if (listaDaily.Count() > 0)
                {
                    return Ok(listaDaily);
                }
                return NotFound();
            }
        }

        [Route("parameters")]
        public List<Parameter> GetAllParameters()
        {
            List<Parameter> lista = new List<Parameter>();

            using (var conn = new SqlConnection(connectionString))
            {

                string str = "SELECT * FROM Parameter";
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

                            lista.Add(new Parameter { Name = nameFromTable, Value = valueFromTable, Date = dateFromTable, Time = timeFromTable, City = cityFromTable });
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

        [Route("parameters/{parameter}")]
        public List<Parameter> GetAllParametersByName(string parameter)
        {
            List<Parameter> lista = new List<Parameter>();

            using (var conn = new SqlConnection(connectionString))
            {

                string str = "SELECT * FROM Parameter WHERE name=@name";

                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("name", parameter);
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

                            lista.Add(new Parameter { Name = nameFromTable, Value = valueFromTable, Date = dateFromTable, Time = timeFromTable, City = cityFromTable });
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

        [Route("cities/{cityvalue}/{parameter}")]
        public List<Parameter> GetAllParametersByCity(string cityvalue, string parameter)
        {
            List<Parameter> lista = new List<Parameter>();

            using (var conn = new SqlConnection(connectionString))
            {
                string str = "SELECT * FROM Parameter WHERE city=@city AND name=@parameter";

                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("city", cityvalue);
                cmd.Parameters.AddWithValue("parameter", parameter);
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

                            lista.Add(new Parameter { Name = nameFromTable, Value = valueFromTable, Date = dateFromTable, City = cityFromTable });
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


        [Route("cities")]
        public List<string> GetAllCities()
        {
            List<string> lista = new List<string>();

            using (var conn = new SqlConnection(connectionString))
            {

                string str = "SELECT DISTINCT city FROM Parameter";
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
                            string cityFromTable = objReader.GetString(objReader.GetOrdinal("city"));

                            lista.Add(cityFromTable);
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
    }
}
