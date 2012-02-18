using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TreasureLand.App_Code
{

    public class ShortTermInventoryDB
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings
                ["TreasureLandDB"].ConnectionString;
        }

        public static int AddTransaction(string ShortTermItemName, int ShortTermTotalQuantity, short DepartmentID)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "INSERT INTO ShortTermAsset (ShortTermItemName, ShortTermTotalQuantity, DepartmentID) " +
                        "VALUES(@ShortTermItemName, @ShortTermTotalQuantity, @DepartmentID)";
                SqlCommand connCommand = new SqlCommand(update, conn); 
                connCommand.Parameters.AddWithValue("@ShortTermItemName", ShortTermItemName);
                connCommand.Parameters.AddWithValue("@ShortTermTotalQuantity", ShortTermTotalQuantity);
                connCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);
      
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}