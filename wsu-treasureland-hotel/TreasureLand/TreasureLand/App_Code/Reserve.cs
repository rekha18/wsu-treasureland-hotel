using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreasureLand.App_Code
{

    public class Reserve
    {
        public Int16 GuestID;
        public Int16 roomID;
        public int returnView;
        public string firstName;
        public string surName;
        public string phone;
        public int numAdults;
        public int numChild;
        public int Discount;
        public string reserveDate;
        public int daysStaying;
        public int view;
        public int reservationID;

        public Reserve Clone()
        {
            Reserve clone = new Reserve();
            clone.GuestID = GuestID;
            clone.roomID = roomID;
            clone.returnView = returnView;
            clone.firstName = firstName == null ? String.Empty : (string)firstName.Clone();
            clone.surName = surName == null ? String.Empty : (string)surName.Clone();
            clone.phone = phone == null ? String.Empty : (string)phone.Clone();
            clone.numAdults = numAdults;
            clone.numChild = numChild;
            clone.Discount = Discount;
            clone.reserveDate = reserveDate == null ? String.Empty : (string)reserveDate.Clone();
            clone.daysStaying = daysStaying;
            clone.view = view;
            clone.reservationID = reservationID;
            return clone;
        }
    }
}