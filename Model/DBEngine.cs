﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FleetManagement.Model
{
    internal class DBEngine
    {
        public static string CnnStr = "datasource=localhost;port=3306;username=root;database=fleetmanagementdb;password=password;";

        public static DataTable GetTable(string SQL)
        {
            MySqlConnection Cnn = new MySqlConnection(CnnStr);
            MySqlCommand Cmd = new MySqlCommand(SQL, Cnn);
            MySqlDataAdapter Adp = new MySqlDataAdapter();
            DataTable dTable = new DataTable();
            Cnn.Open();
            Adp.SelectCommand = Cmd;
            Adp.Fill(dTable);
            Cnn.Close();
            return dTable;
        }
        public static void Execute(string SQL)
        {
            MySqlConnection Cnn = new MySqlConnection(CnnStr);
            MySqlCommand Cmd = new MySqlCommand(SQL, Cnn);
            Cnn.Open();
            Cmd.ExecuteNonQuery();
            Cnn.Close();
        }
    }
}
