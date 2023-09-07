using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using Microsoft.SqlServer.Server;
using packinglabel.Controllers;
using packinglabel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace packinglabel.Controllers
{
    public class packinglabelController
    {
        private static packinglabelController instance;

        public static packinglabelController Instance
        {
            get { if (instance == null) instance = new packinglabelController(); return packinglabelController.instance; }
            private set { packinglabelController.instance = value; }
        }

        private packinglabelController()
        { }

        public List<lichsus> LoadTableLichsu(string ngay)
        {
            List<lichsus> tablelist = new List<lichsus>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Lichsu where date ='" + ngay + "' ORDER BY Id DESC");
            foreach (DataRow item in data.Rows)
            {
                lichsus nhap = new lichsus(item);
                tablelist.Add(nhap);
            }
            return tablelist;
        }

        public List<caidats> LoadTableCaidat()
        {
            List<caidats> tablelist = new List<caidats>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Caidat ORDER BY Destination ");
            foreach (DataRow item in data.Rows)
            {
                caidats nhap = new caidats(item);
                tablelist.Add(nhap);
            }
            return tablelist;
        }

        public List<string> LoadTenThiTruongList()
        {
            List<string> tenThiTruongList = new List<string>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Caidat ORDER BY Destination ");

            foreach (DataRow row in data.Rows)
            {
                string tenThiTruong = row["Destination"].ToString();
                tenThiTruongList.Add(tenThiTruong);
            }
            return tenThiTruongList;
        }

        public List<caidats> LoadThongTinThiTruong(string destination)
        {
            List<caidats> thongtin = new List<caidats>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Caidat where Destination = '" + destination + "' ");
            if (data.Rows.Count > 0)
            {
                DataRow firstRow = data.Rows[0];
                caidats thongTin = new caidats(firstRow);
                thongtin.Add(thongTin);
            }
            return thongtin;
        }

        public bool Themdulieuthitruong(string destination, string bodycode, string productcode1,
            string productcode2, string platerating, string warranty, string maphoi)
        {
            string checkQuery = string.Format("SELECT COUNT(*) FROM [dbo].[Caidat] WHERE Destination = N'{0}'", destination);
            int existingDestinationCount = (int)DataProvider.Instance.ExecuteScalar(checkQuery);
            // Nếu 'destination' đã tồn tại, không lưu và trả về false.
            if (existingDestinationCount > 0)
            {
                MessageBox.Show("Thị trường đã tồn tại trong cơ sở dữ liệu!");
                return false;
            }
            string query = string.Format("INSERT INTO [dbo].[Caidat] " +
                "(Destination, maBodycode, maProductcode1,maProductcode2,maPlaterating,maWarranty,maMaphoi)" +
                " VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}',N'{5}',N'{6}')",
                destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool Suadulieuthitruong(string destination, string bodycode, string productcode1,
            string productcode2, string platerating, string warranty, string maphoi)
        {
            string query = string.Format("update [dbo].[Caidat] " +
                "set maBodycode='{1}',maProductcode1='{2}',maProductcode2='{3}',maPlaterating='{4}'" +
                ",maWarranty='{5}',maMaphoi='{6}' " +
                "WHERE Destination ='{0}'",
                destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool Xoadulieuthitruong(string destination)
        {
            string query = string.Format("delete [dbo].[Caidat] WHERE Destination='" + destination + "'");
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool Doimatkhau(string matkhauungdung, string matkhauNG)
        {
            List<string> matkhaus = DataProvider.Instance.getMatkhau();
            matkhaus[0] = matkhauungdung;
            matkhaus[1] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            matkhaus[2] = matkhauNG;
            matkhaus[3] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string basepath = Application.StartupPath;
            string txtpath = basepath + @"/password.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(txtpath))
                {
                    string matkhauLine = string.Join(";", matkhaus);
                    sw.WriteLine(matkhauLine);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public int Load_soLuongtrongngay()
        {
            string query = string.Format("SELECT COUNT(*) FROM dbo.Lichsu WHERE Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
            int soluong = (int)DataProvider.Instance.ExecuteScalar(query);
            return soluong;
        }

        public int Load_soLuongNGtrongngay()
        {
            string query = string.Format("SELECT COUNT(*) FROM dbo.Lichsu WHERE Status='NG' and Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
            int soluong = (int)DataProvider.Instance.ExecuteScalar(query);
            return soluong;
        }

        public bool checkBody(string bodycode)
        {
            string query = string.Format("SELECT COUNT(*) FROM dbo.Lichsu WHERE Bodycode='" + bodycode + "'");
            int result = (int)DataProvider.Instance.ExecuteScalar(query);
            return (result > 0);
        }

        public bool saveLogOK(string status, string destination, string bodycode, string productcode1, string productcode2,
            string platerating, string warranty, string maphoi, string date, string time)
        {
            string query = string.Format("INSERT INTO [dbo].[Lichsu] " +
                "(Status,Destination, Bodycode, Productcode1, Productcode2,Platerating, Warranty,Maphoi,Date,Time) " +
                "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}')",
                status, destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi, date, time);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool saveLogNG(string status, string destination, string bodycode, string productcode1, string productcode2,
            string platerating, string warranty, string maphoi, string date, string time, string info)
        {
            string query = string.Format("INSERT INTO [dbo].[Lichsu] " +
                "(Status,Destination, Bodycode, Productcode1, Productcode2,Platerating, Warranty,Maphoi,Date,Time,Info) " +
                "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}')",
                status, destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi, date, time, info);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public List<lichsus> LoadTableLichsu_Loc(string tenthitruong, string tungay, string denngay)
        {
            List<lichsus> tablelist = new List<lichsus>();
            string query = "SELECT * FROM dbo.Lichsu WHERE Destination = '" + tenthitruong + "' AND Date >= '" + tungay + "' AND Date <= '" + denngay + "' ORDER BY Id DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                lichsus nhap = new lichsus(item);
                tablelist.Add(nhap);
            }
            return tablelist;
        }
    }
}