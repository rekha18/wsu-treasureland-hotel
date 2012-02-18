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
    public class ShortTermAssetDB
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings
            ["TreasureLandDB"].ConnectionString;
        }

        public static int AddTransaction(int DepartmentID, string ShortTermItemName, int ShortTermTotalQuantity)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "INSERT INTO ShortTermAsset (DepartmentID, ShortTermItemName, ShortTermTotalQuantity) " +
                        "VALUES(@DepartmentID, @ShortTermItemName, @ShortTermItemQuantity)";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                connCommand.Parameters.AddWithValue("@ShortTermItemName", ShortTermItemName);
                connCommand.Parameters.AddWithValue("@ShortTermTotalQuantity", ShortTermTotalQuantity);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}