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
    public class LongTermAssetDB
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings
            ["TreasureLandDB"].ConnectionString;
        }

        public static int AddTransaction(string LongTermAssetName, string LongTermAssetLocation, decimal LongTermAssetCost, bool LongTermAssetInUse, DateTime LongTermAssetPurchaseDate)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "INSERT INTO LongTermAsset (LongTermAssetName, LongTermAssetLocation, LongTermAssetCost, LongTermAssetInUse, LongTermAssetPurchaseDate) " +
                        "VALUES(@LongTermAssetName, @LongTermAssetLocation, @LongTermAssetCost, @LongTermAssetInUse, @LongTermAssetPurchaseDate)";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@LongTermAssetName", LongTermAssetName);
                connCommand.Parameters.AddWithValue("@LongTermAssetLocation", LongTermAssetLocation);
                connCommand.Parameters.AddWithValue("@LongTermAssetCost", LongTermAssetCost);
                connCommand.Parameters.AddWithValue("@LongTermAssetInUse", LongTermAssetInUse);
                connCommand.Parameters.AddWithValue("@LongTermAssetPurchaseDate", LongTermAssetPurchaseDate);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     }
}