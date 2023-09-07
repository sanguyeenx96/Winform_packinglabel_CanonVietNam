using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace packinglabel.Models
{
    public class lichsus
    {
        public lichsus(int id, string status, string destination, string bodycode, string productcode1, string productcode2,
            string plateratting, string warraty, string maphoi, string date, string time, string info)
        {
            this.Id = id;
            this.Status = status;
            this.Destination = destination;
            this.BodyCode = bodycode;
            this.ProductCode1 = productcode1;
            this.ProductCode2 = productcode2;
            this.PlateRatting = plateratting;
            this.Warraty = warraty;
            this.MaPhoi = maphoi;
            this.Date = date;
            this.Time = time;
            this.Info = info;
        }

        public lichsus(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.Status = row["Status"].ToString();
            this.Destination = row["Destination"].ToString();
            this.BodyCode = row["Bodycode"].ToString();
            this.ProductCode1 = row["Productcode1"].ToString();
            this.ProductCode2 = row["Productcode2"].ToString();
            this.PlateRatting = row["Platerating"].ToString();
            this.Warraty = row["Warranty"].ToString();
            this.MaPhoi = row["Maphoi"].ToString();
            //this.Date = row["Date"].ToString();
            if (DateTime.TryParse(row["Date"].ToString(), out DateTime dateValue))
            {
                this.Date = dateValue.Date.ToString("dd/MM/yyyy");
            }
            this.Time = row["Time"].ToString();
            this.Info = row["Info"].ToString();
        }

        private int id;
        private string status;
        private string destination;
        private string bodycode;
        private string productcode1;
        private string productcode2;
        private string plateratting;
        private string warraty;
        private string maphoi;
        private string date;
        private string time;
        private string info;

        public int Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }
        public string Destination { get => destination; set => destination = value; }
        public string BodyCode { get => bodycode; set => bodycode = value; }
        public string ProductCode1 { get => productcode1; set => productcode1 = value; }
        public string ProductCode2 { get => productcode2; set => productcode2 = value; }
        public string PlateRatting { get => plateratting; set => plateratting = value; }
        public string Warraty { get => warraty; set => warraty = value; }
        public string MaPhoi { get => maphoi; set => maphoi = value; }
        public string Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }
        public string Info { get => info; set => info = value; }
    }
}