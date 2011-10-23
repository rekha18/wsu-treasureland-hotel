using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

[DataObject(true)]

public static class LocateGuestDB
{

    [DataObjectMethod(DataObjectMethodType.Select)]

    public static IEnumerable LocateGuest(string FirstName, string SurName, string Phone)
    {
        SqlConnection con = new SqlConnection(GetConnectionString());
        string sel = "SELECT GuestFirstName, GuestSurName, GuestPhone " +
        "FROM Guest " +
        "WHERE GuestFirstName = @GuestFirstName OR GuestSurName = @GuestSurName OR GuestPhone = @GuestPhone " +
        "ORDER BY GuestSurName";

        SqlCommand cmd =
        new SqlCommand(sel, con);
        cmd.Parameters.AddWithValue("GuestFirstName", FirstName);
        cmd.Parameters.AddWithValue("GuestSurName", SurName);
        cmd.Parameters.AddWithValue("GuestPhone", Phone);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr;

    }


    private static string GetConnectionString()
    {


        return ConfigurationManager.ConnectionStrings

        [

        "TreasureLandDB"].ConnectionString;

    }

}
