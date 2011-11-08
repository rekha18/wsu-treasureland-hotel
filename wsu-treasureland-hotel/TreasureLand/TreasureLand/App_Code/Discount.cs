using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreasureLand.App_Code
{
    public class Discount
    {
        public int ID;
        public string description;
        public bool isPercent;
        public double amountOfDiscount;
        public Discount() { }
        public Discount(int ID, string description, bool isPercent, double amountOfDiscount)
        {
            this.ID = ID;
            this.description = description;
            this.isPercent = isPercent;
            this.amountOfDiscount = amountOfDiscount;
        }

        public override string ToString()
        {
            if (this.isPercent == true)
            {
                return description + " " + amountOfDiscount + "%";
            }
            else
            return description + amountOfDiscount;
        }

    }
}