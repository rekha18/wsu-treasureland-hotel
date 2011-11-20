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
  
    public static class ExpenseDB
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings

            ["TreasureLandDB"].ConnectionString;

        }

        public static int AddTransaction(DateTime AccountingDate, decimal AccountingCost, int AccountingTypeID, string AccountingDescription)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "INSERT INTO ACCOUNTING (AccountingDate, AccountingTypeID, AccountingCost, AccountingDescription) " +
                        "VALUES(@AccountingDate, @AccountingTypeID, @AccountingCost, @AccountingDescription)";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@AccountingDate", AccountingDate);
                connCommand.Parameters.AddWithValue("@AccountingTypeID", AccountingTypeID);
                connCommand.Parameters.AddWithValue("@AccountingCost", AccountingCost);
                connCommand.Parameters.AddWithValue("@AccountingDescription", AccountingDescription);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }

}