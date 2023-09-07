using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace packinglabel.Models
{
    public class caidats
    {
        public caidats(int id, string destination, string mabodycode, string maproductcode1, string maproductcode2,
             string maplateratting, string mawarraty, string mamaphoi)
        {
            this.ID = id;
            this.Destination = destination;
            this.MaBodyCode = mabodycode;
            this.MaProductCode1 = maproductcode1;
            this.MaProductCode2 = maproductcode2;
            this.MaPlateRatting = maplateratting;
            this.MaWarraty = mawarraty;
            this.MaMaPhoi = mamaphoi;
        }

        public caidats(DataRow row)
        {
            this.ID = (int)row["Id"];
            this.Destination = row["Destination"].ToString();
            this.MaBodyCode = row["maBodycode"].ToString();
            this.MaProductCode1 = row["maProductcode1"].ToString();
            this.MaProductCode2 = row["maProductcode2"].ToString();
            this.MaPlateRatting = row["maPlaterating"].ToString();
            this.MaWarraty = row["maWarranty"].ToString();
            this.MaMaPhoi = row["maMaphoi"].ToString();
        }

        private int id;
        private string madestination;
        private string mabodycode;
        private string maproductcode1;
        private string maproductcode2;
        private string maplateratting;
        private string mawarraty;
        private string mamaphoi;

        public int ID { get => id; set => id = value; }
        public string Destination { get => madestination; set => madestination = value; }
        public string MaBodyCode { get => mabodycode; set => mabodycode = value; }
        public string MaProductCode1 { get => maproductcode1; set => maproductcode1 = value; }
        public string MaProductCode2 { get => maproductcode2; set => maproductcode2 = value; }
        public string MaPlateRatting { get => maplateratting; set => maplateratting = value; }
        public string MaWarraty { get => mawarraty; set => mawarraty = value; }
        public string MaMaPhoi { get => mamaphoi; set => mamaphoi = value; }
    }
}