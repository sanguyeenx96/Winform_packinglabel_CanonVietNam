using packinglabel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packinglabel.Views
{
    public partial class Form_Lichsu : Form
    {
        public Form_Lichsu()
        {
            InitializeComponent();
        }

        private void LoadTableLichsuTrongNgay()
        {
            List<lichsus> TableList = Controllers.packinglabelController.Instance.LoadTableLichsu(DateTime.Now.ToString("yyyy-MM-dd"));
            dgvDulieu.DataSource = TableList;
            dgvDulieu.Columns["id"].Visible = false;
            dgvDulieu.Columns["time"].DisplayIndex = 0;
        }

        private void tabPage1_Enter_1(object sender, EventArgs e)
        {
            LoadTableLichsuTrongNgay();
        }

        private void dgvDulieu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgvDulieu.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null && cell.Value.ToString() == "OK")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            if (e.ColumnIndex == 11 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgvDulieu.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void tabPage2_Enter_1(object sender, EventArgs e)
        {
            List<string> tenThiTruongList = Controllers.packinglabelController.Instance.LoadTenThiTruongList();
            foreach (var item in tenThiTruongList)
            {
                cbTenthitruong.Items.Add(item.ToString());
            }
        }

        private string tenthitruong;
        private string tungay;
        private string denngay;

        private void btnLoc_Click_1(object sender, EventArgs e)
        {
            tenthitruong = cbTenthitruong.Text;
            tungay = dtpTungay.Value.ToString("yyyy-MM-dd");
            denngay = dtpDenngay.Value.ToString("yyyy-MM-dd");
            List<lichsus> TableList = Controllers.packinglabelController.Instance.LoadTableLichsu_Loc(tenthitruong, tungay, denngay);
            dgvLoc.DataSource = TableList;
            dgvLoc.Columns["id"].Visible = false;
            dgvLoc.Columns["date"].DisplayIndex = 0;
            dgvLoc.Columns["time"].DisplayIndex = 1;

            txtStatus.Text = "Dữ liệu thị trường " + cbTenthitruong.Text + " từ ngày " + dtpTungay.Value.ToString("dd/MM/yyyy") + " đến ngày " + dtpDenngay.Value.ToString("dd/MM/yyyy");
        }
    }
}