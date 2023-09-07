using packinglabel.Models;
using packinglabel.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using packinglabel.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace packinglabel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TextBox[] enabledTextBoxes = new TextBox[0];

        private void HideAndDisableAndClear()
        {
            txtBodycode.Enabled = false;
            txtProductcode1.Enabled = false;
            txtProductcode2.Enabled = false;
            txtPlaterating.Enabled = false;
            txtWarranty.Enabled = false;
            txtMaphoi.Enabled = false;

            txtDDBodycode.Text = "";
            txtDDProductcode1.Text = "";
            txtDDProductcode2.Text = "";
            txtDDPlaterating.Text = "";
            txtDDWarranty.Text = "";
            txtDDMaphoi.Text = "";

            statusBodycode.Text = "_ _";
            statusBodycode.BackColor = Color.White;
            statusBodycode.ForeColor = Color.Black;

            statusProductcode1.Text = "_ _";
            statusProductcode1.BackColor = Color.White;
            statusProductcode1.ForeColor = Color.Black;

            statusProductcode2.Text = "_ _";
            statusProductcode2.BackColor = Color.White;
            statusProductcode2.ForeColor = Color.Black;

            statusPlaterating.Text = "_ _";
            statusPlaterating.BackColor = Color.White;
            statusPlaterating.ForeColor = Color.Black;

            statusWarranty.Text = "_ _";
            statusWarranty.BackColor = Color.White;
            statusWarranty.ForeColor = Color.Black;

            statusMaphoi.Text = "_ _";
            statusMaphoi.BackColor = Color.White;
            statusMaphoi.ForeColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideAndDisableAndClear();
            List<string> tenThiTruongList = Controllers.packinglabelController.Instance.LoadTenThiTruongList();
            foreach (var item in tenThiTruongList)
            {
                cbThiTruong.Items.Add(item.ToString());
            }
            Loadsoluongtrongngay();
            LoadTableLichsuTrongNgay();
        }

        private void Loadsoluongtrongngay()
        {
            txtSoluongtrongngay.Text = packinglabelController.Instance.Load_soLuongtrongngay().ToString() + " " + "pcs";
            txtSoluongNGtrongngay.Text = packinglabelController.Instance.Load_soLuongNGtrongngay().ToString() + " " + "pcs";
        }

        private void LoadTableLichsuTrongNgay()
        {
            List<lichsus> TableList = Controllers.packinglabelController.Instance.LoadTableLichsu(DateTime.Now.ToString("yyyy-MM-dd"));
            dgvLichSuTrongNgay.DataSource = TableList;
            dgvLichSuTrongNgay.Columns["id"].Visible = false;
            dgvLichSuTrongNgay.Columns["time"].DisplayIndex = 0;
        }

        private List<caidats> thongTinThiTruong = new List<caidats>();

        private void cbThiTruong_SelectedValueChanged(object sender, EventArgs e)
        {
            cbThiTruong.Enabled = false;
            HideAndDisableAndClear();
            List<TextBox> enabledTextBoxList = new List<TextBox>();

            thongTinThiTruong = Controllers.packinglabelController.Instance.LoadThongTinThiTruong(cbThiTruong.Text);
            foreach (caidats thongTin in thongTinThiTruong)
            {
                if (!string.IsNullOrEmpty(thongTin.MaBodyCode))
                {
                    txtBodycode.Enabled = true;
                    statusBodycode.Text = "WAIT";
                    statusBodycode.BackColor = Color.Orange;
                    statusBodycode.ForeColor = Color.White;

                    txtDDBodycode.Text = thongTin.MaBodyCode;
                    enabledTextBoxList.Add(txtBodycode);
                }
                if (!string.IsNullOrEmpty(thongTin.MaProductCode1))
                {
                    txtProductcode1.Enabled = true;
                    statusProductcode1.Text = "WAIT";
                    statusProductcode1.BackColor = Color.Orange;
                    statusProductcode1.ForeColor = Color.White;

                    txtDDProductcode1.Text = thongTin.MaProductCode1;
                    enabledTextBoxList.Add(txtProductcode1);
                }
                if (!string.IsNullOrEmpty(thongTin.MaProductCode2))
                {
                    txtProductcode2.Enabled = true;
                    statusProductcode2.Text = "WAIT";
                    statusProductcode2.BackColor = Color.Orange;
                    statusProductcode2.ForeColor = Color.White;

                    txtDDProductcode2.Text = thongTin.MaProductCode2;
                    enabledTextBoxList.Add(txtProductcode2);
                }
                if (!string.IsNullOrEmpty(thongTin.MaPlateRatting))
                {
                    txtPlaterating.Enabled = true;
                    statusPlaterating.Text = "WAIT";
                    statusPlaterating.BackColor = Color.Orange;
                    statusPlaterating.ForeColor = Color.White;

                    txtDDPlaterating.Text = thongTin.MaPlateRatting;
                    enabledTextBoxList.Add(txtPlaterating);
                }
                if (!string.IsNullOrEmpty(thongTin.MaWarraty))
                {
                    txtWarranty.Enabled = true;
                    statusWarranty.Text = "WAIT";
                    statusWarranty.BackColor = Color.Orange;
                    statusWarranty.ForeColor = Color.White;

                    txtDDWarranty.Text = thongTin.MaWarraty;
                    enabledTextBoxList.Add(txtWarranty);
                }
                if (!string.IsNullOrEmpty(thongTin.MaMaPhoi))
                {
                    txtMaphoi.Enabled = true;
                    statusMaphoi.Text = "WAIT";
                    statusMaphoi.BackColor = Color.Orange;
                    statusMaphoi.ForeColor = Color.White;

                    txtDDMaphoi.Text = thongTin.MaMaPhoi;
                    enabledTextBoxList.Add(txtMaphoi);
                }
            }
            enabledTextBoxes = enabledTextBoxList.ToArray();
            foreach (TextBox textBox in enabledTextBoxes)
            {
                textBox.KeyDown -= TextBox_KeyDown;
                textBox.KeyDown += TextBox_KeyDown;
            }
            if (enabledTextBoxes.Length > 0)
            {
                enabledTextBoxes[0].Focus();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                txtStatus.Text = "WAIT";
                txtStatus.BackColor = Color.Orange;
                TextBox currentTextBox = (TextBox)sender;
                switch (currentTextBox.Name)
                {
                    case "txtBodycode":
                        if (checktrungbody(txtBodycode.Text))
                        {
                            statusBodycode.Text = "NG";
                            statusBodycode.BackColor = Color.Red;
                            statusBodycode.ForeColor = Color.White;
                            txtBodycode.Focus();
                            showNG("Mã Bodycode đã bị trùng trong CSDL!");
                            return;
                        }
                        if (txtBodycode.Text.StartsWith(txtDDBodycode.Text))
                        {
                            txtBodycode.Enabled = false;
                            statusBodycode.Text = "OK";
                            statusBodycode.BackColor = Color.Green;
                            statusBodycode.ForeColor = Color.White;
                            break;
                        }
                        else
                        {
                            statusBodycode.Text = "NG";
                            statusBodycode.BackColor = Color.Red;
                            statusBodycode.ForeColor = Color.White;
                            txtBodycode.Focus();
                            showNG("Mã Bodycode không đúng định dạng!");
                            return;
                        }

                    case "txtProductcode1":
                        if (txtProductcode1.Text.StartsWith(txtDDProductcode1.Text)
                            && txtProductcode1.Text.EndsWith(txtBodycode.Text))
                        {
                            txtProductcode1.Enabled = false;
                            statusProductcode1.Text = "OK";
                            statusProductcode1.BackColor = Color.Green;
                            statusProductcode1.ForeColor = Color.White;
                            break;
                        }
                        else
                        {
                            statusProductcode1.Text = "NG";
                            statusProductcode1.BackColor = Color.Red;
                            statusProductcode1.ForeColor = Color.White;
                            txtProductcode1.Focus();
                            showNG("Mã Productcode 1 không đúng định dạng!");
                            return;
                        }

                    case "txtProductcode2":
                        if (txtProductcode2.Text.StartsWith(txtDDProductcode2.Text)
                            && txtProductcode2.Text == txtProductcode1.Text)
                        {
                            txtProductcode2.Enabled = false;
                            statusProductcode2.Text = "OK";
                            statusProductcode2.BackColor = Color.Green;
                            statusProductcode2.ForeColor = Color.White;
                            break;
                        }
                        else
                        {
                            statusProductcode2.Text = "NG";
                            statusProductcode2.BackColor = Color.Red;
                            statusProductcode2.ForeColor = Color.White;
                            txtProductcode2.Focus();
                            showNG("Mã Productcode 2 không đúng định dạng!");
                            return;
                        }

                    case "txtPlaterating":
                        if (txtPlaterating.Text.StartsWith(txtDDPlaterating.Text)
                            && txtPlaterating.Text == txtBodycode.Text)
                        {
                            txtPlaterating.Enabled = false;
                            statusPlaterating.Text = "OK";
                            statusPlaterating.BackColor = Color.Green;
                            statusPlaterating.ForeColor = Color.White;
                            break;
                        }
                        else
                        {
                            statusPlaterating.Text = "NG";
                            statusPlaterating.BackColor = Color.Red;
                            statusPlaterating.ForeColor = Color.White;
                            txtPlaterating.Focus();
                            showNG("Mã Platerating không đúng định dạng!");
                            return;
                        }

                    case "txtWarranty":
                        if (txtWarranty.Text.StartsWith(txtDDWarranty.Text)
                            && txtWarranty.Text == txtPlaterating.Text
                            && txtWarranty.Text == txtBodycode.Text)
                        {
                            txtWarranty.Enabled = false;
                            statusWarranty.Text = "OK";
                            statusWarranty.BackColor = Color.Green;
                            statusWarranty.ForeColor = Color.White;
                            break;
                        }
                        else
                        {
                            statusWarranty.Text = "NG";
                            statusWarranty.BackColor = Color.Red;
                            statusWarranty.ForeColor = Color.White;
                            txtWarranty.Focus();
                            showNG("Mã Warranty không đúng định dạng!");
                            return;
                        }

                    case "txtMaphoi":
                        if (txtMaphoi.Text.StartsWith(txtDDMaphoi.Text))
                        {
                            txtMaphoi.Enabled = false;
                            statusMaphoi.Text = "OK";
                            statusMaphoi.BackColor = Color.Green;
                            statusMaphoi.ForeColor = Color.White;
                            break;
                        }
                        else
                        {
                            statusMaphoi.Text = "NG";
                            statusMaphoi.BackColor = Color.Red;
                            statusMaphoi.ForeColor = Color.White;
                            txtMaphoi.Focus();
                            showNG("Mã phôi không đúng định dạng!");
                            return;
                        }
                    default:
                        break;
                }
                // Tìm vị trí của TextBox trong mảng enabledTextBoxes
                int currentIndex = Array.IndexOf(enabledTextBoxes, currentTextBox);
                if (currentIndex >= 0 && currentIndex < enabledTextBoxes.Length - 1)
                {
                    enabledTextBoxes[currentIndex + 1].Focus();
                }
                else if (currentIndex == enabledTextBoxes.Length - 1)
                {
                    bool result = saveOK("OK", cbThiTruong.Text, txtBodycode.Text, txtProductcode1.Text,
                        txtProductcode2.Text, txtPlaterating.Text, txtWarranty.Text, txtMaphoi.Text,
                        DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"));
                    if (result)
                    {
                        txtStatus.Text = "PASS";
                        txtStatus.BackColor = Color.Green;
                        foreach (TextBox textBox in enabledTextBoxes)
                        {
                            textBox.Clear();
                        }
                        foreach (TextBox textBox in enabledTextBoxes)
                        {
                            textBox.Enabled = true;
                        }
                        if (enabledTextBoxes.Length > 0)
                        {
                            enabledTextBoxes[0].Focus();
                        }
                        foreach (caidats thongTin in thongTinThiTruong)
                        {
                            if (!string.IsNullOrEmpty(thongTin.MaBodyCode))
                            {
                                statusBodycode.Text = "WAIT";
                                statusBodycode.BackColor = Color.Orange;
                                statusBodycode.ForeColor = Color.White;
                            }
                            if (!string.IsNullOrEmpty(thongTin.MaProductCode1))
                            {
                                statusProductcode1.Text = "WAIT";
                                statusProductcode1.BackColor = Color.Orange;
                                statusProductcode1.ForeColor = Color.White;
                            }
                            if (!string.IsNullOrEmpty(thongTin.MaProductCode2))
                            {
                                statusProductcode2.Text = "WAIT";
                                statusProductcode2.BackColor = Color.Orange;
                                statusProductcode2.ForeColor = Color.White;
                            }
                            if (!string.IsNullOrEmpty(thongTin.MaPlateRatting))
                            {
                                statusPlaterating.Text = "WAIT";
                                statusPlaterating.BackColor = Color.Orange;
                                statusPlaterating.ForeColor = Color.White;
                            }
                            if (!string.IsNullOrEmpty(thongTin.MaWarraty))
                            {
                                statusWarranty.Text = "WAIT";
                                statusWarranty.BackColor = Color.Orange;
                                statusWarranty.ForeColor = Color.White;
                            }
                            if (!string.IsNullOrEmpty(thongTin.MaMaPhoi))
                            {
                                statusMaphoi.Text = "WAIT";
                                statusMaphoi.BackColor = Color.Orange;
                                statusMaphoi.ForeColor = Color.White;
                            }
                        }
                        Loadsoluongtrongngay();
                        LoadTableLichsuTrongNgay();
                    }
                    else
                    {
                        showNG("Không lưu được dữ liệu!");
                    }
                }
            }
        }

        public string valueKey;
        public string valueInfo;

        private void showNG(string valueInfo)
        {
            bool result = saveNG("NG", cbThiTruong.Text, txtBodycode.Text, txtProductcode1.Text,
                        txtProductcode2.Text, txtPlaterating.Text, txtWarranty.Text, txtMaphoi.Text,
                        DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), valueInfo);
            if (result)
            {
                LoadTableLichsuTrongNgay();
                txtStatus.Text = "NG";
                txtStatus.BackColor = Color.Red;
                valueKey = "NG";
                Form_Dangnhap_Canhbao form = new Form_Dangnhap_Canhbao(valueKey, valueInfo);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không kết nối được với máy chủ!");
            }
            Loadsoluongtrongngay();
        }

        private bool checktrungbody(string bodycode)
        {
            return packinglabelController.Instance.checkBody(bodycode);
        }

        private bool saveOK(string status, string destination, string bodycode, string productcode1,
            string productcode2, string platerating, string warranty, string maphoi, string date, string time)
        {
            return packinglabelController.Instance.saveLogOK(status, destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi, date, time);
        }

        private bool saveNG(string status, string destination, string bodycode, string productcode1,
            string productcode2, string platerating, string warranty, string maphoi, string date, string time, string info)
        {
            return packinglabelController.Instance.saveLogNG(status, destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi, date, time, info);
        }

        private void btnDoiThiTruong_Click(object sender, EventArgs e)
        {
            HideAndDisableAndClear();
            cbThiTruong.Enabled = true;
        }

        private void btnCaiDatThiTruong_Click(object sender, EventArgs e)
        {
            valueKey = "setting";
            valueInfo = "Nhập mật khẩu để đăng nhập";
            Form_Dangnhap_Canhbao form = new Form_Dangnhap_Canhbao(valueKey, valueInfo);
            form.ShowDialog();
        }

        private void btnTraCuuLichSu_Click(object sender, EventArgs e)
        {
            Form_Lichsu form = new Form_Lichsu();
            form.ShowDialog();
        }

        private void dgvLichSuTrongNgay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgvLichSuTrongNgay.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
                DataGridViewCell cell = dgvLichSuTrongNgay.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }
    }
}