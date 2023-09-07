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
using packinglabel.Controllers;
using System.IO;

namespace packinglabel
{
    public partial class Form_Caidat : Form
    {
        public Form_Caidat()
        {
            InitializeComponent();
        }

        private void tabPage_Danhsach_Enter(object sender, EventArgs e)
        {
            txtLabel.Text = "Danh sách thị trường";
            List<caidats> danhSachThiTruong = Controllers.packinglabelController.Instance.LoadTableCaidat();
            dgvDanhsachthitruong.DataSource = danhSachThiTruong;
            dgvDanhsachthitruong.Columns["id"].Visible = false;
        }

        //
        //
        //
        //Phần THÊM
        private void tabPage_Them_Enter(object sender, EventArgs e)
        {
            txtLabel.Text = "Thêm thị trường mới";
            txtBodycode.Enabled = false;
            txtProductcode1.Enabled = false;
            txtProductcode2.Enabled = false;
            txtPlaterating.Enabled = false;
            txtWarranty.Enabled = false;
            txtMaphoi.Enabled = false;
        }

        private void checkBoxBodycod_CheckedChanged(object sender, EventArgs e)
        {
            txtBodycode.Enabled = checkBoxBodycod.Checked;
        }

        private void checkBoxProductcode1_CheckedChanged(object sender, EventArgs e)
        {
            txtProductcode1.Enabled = checkBoxProductcode1.Checked;
        }

        private void checkBoxProductcode2_CheckedChanged(object sender, EventArgs e)
        {
            txtProductcode2.Enabled = checkBoxProductcode2.Checked;
        }

        private void checkBoxPlaterating_CheckedChanged(object sender, EventArgs e)
        {
            txtPlaterating.Enabled = checkBoxPlaterating.Checked;
        }

        private void checkBoxWarranty_CheckedChanged(object sender, EventArgs e)
        {
            txtWarranty.Enabled = checkBoxWarranty.Checked;
        }

        private void checkBoxMaphoi_CheckedChanged(object sender, EventArgs e)
        {
            txtMaphoi.Enabled = checkBoxMaphoi.Checked;
        }

        private void btnLuulai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenthitruong.Text))
            {
                MessageBox.Show("Vui lòng điền thông tin tên thị trường!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (AreEnabledTextBoxesEmpty())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin vào các ô được bật kiểm tra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; // Không thực hiện lưu nếu có ô trống.
            }
            // Tiếp tục thực hiện lưu dữ liệu nếu tất cả ô được bật đều có giá trị.
            string destination = txtTenthitruong.Text.Trim().ToUpper();
            string bodycode = txtBodycode.Text.Trim().ToUpper();
            string productcode1 = txtProductcode1.Text.Trim().ToUpper();
            string productcode2 = txtProductcode2.Text.Trim().ToUpper();
            string platerating = txtPlaterating.Text.Trim().ToUpper();
            string warranty = txtWarranty.Text.Trim().ToUpper();
            string maphoi = txtMaphoi.Text.Trim().ToUpper();
            bool result = save(destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi);
            if (result)
            {
                MessageBox.Show("Dữ liệu đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Gọi phương thức làm mới form
                RefreshForm();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool save(string destination, string bodycode, string productcode1, string productcode2, string platerating, string warranty, string maphoi)
        {
            return packinglabelController.Instance.Themdulieuthitruong(destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi);
        }

        private bool AreEnabledTextBoxesEmpty()
        {
            if ((checkBoxBodycod.Checked && string.IsNullOrWhiteSpace(txtBodycode.Text)) ||
                (checkBoxProductcode1.Checked && string.IsNullOrWhiteSpace(txtProductcode1.Text)) ||
                (checkBoxProductcode2.Checked && string.IsNullOrWhiteSpace(txtProductcode2.Text)) ||
                (checkBoxPlaterating.Checked && string.IsNullOrWhiteSpace(txtPlaterating.Text)) ||
                (checkBoxWarranty.Checked && string.IsNullOrWhiteSpace(txtWarranty.Text)) ||
                (checkBoxMaphoi.Checked && string.IsNullOrWhiteSpace(txtMaphoi.Text)))
            {
                return true;
            }
            return false;
        }

        private void RefreshForm()
        {
            txtLabel.Text = "Thêm thị trường mới";
            checkBoxBodycod.Checked = false;
            checkBoxProductcode1.Checked = false;
            checkBoxProductcode2.Checked = false;
            checkBoxPlaterating.Checked = false;
            checkBoxWarranty.Checked = false;
            checkBoxMaphoi.Checked = false;
            txtBodycode.Enabled = false;
            txtProductcode1.Enabled = false;
            txtProductcode2.Enabled = false;
            txtPlaterating.Enabled = false;
            txtWarranty.Enabled = false;
            txtMaphoi.Enabled = false;
            txtTenthitruong.Text = "";
            txtBodycode.Text = "";
            txtProductcode1.Text = "";
            txtProductcode2.Text = "";
            txtPlaterating.Text = "";
            txtWarranty.Text = "";
            txtMaphoi.Text = "";
            this.Invalidate(); // Hoặc this.Refresh();
        }

        private void btnHuybo_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        //
        //
        //
        //Phần SỬA
        private void tabPage_Sua_Enter(object sender, EventArgs e)
        {
            sua_cbTenthitruong.SelectedIndex = -1;
            sua_cbTenthitruong.Items.Clear();
            CleartxtFormSua();
            txtLabel.Text = "Sửa thông tin thị trường";
            List<string> tenThiTruongList = Controllers.packinglabelController.Instance.LoadTenThiTruongList();
            foreach (var item in tenThiTruongList)
            {
                sua_cbTenthitruong.Items.Add(item.ToString());
            }
        }

        private void CleartxtFormSua()
        {
            sua_txtBodycode.Text = "";
            sua_txtProductcode1.Text = "";
            sua_txtProductcode2.Text = "";
            sua_txtPlaterating.Text = "";
            sua_txtWarranty.Text = "";
            sua_txtMaphoi.Text = "";
        }

        private void sua_cbTenthitruong_SelectedValueChanged(object sender, EventArgs e)
        {
            CleartxtFormSua();
            List<caidats> thongTinThiTruong = new List<caidats>();
            thongTinThiTruong = Controllers.packinglabelController.Instance.LoadThongTinThiTruong(sua_cbTenthitruong.Text);
            foreach (caidats thongTin in thongTinThiTruong)
            {
                if (!string.IsNullOrEmpty(thongTin.MaBodyCode))
                {
                    sua_txtBodycode.Text = thongTin.MaBodyCode;
                }
                if (!string.IsNullOrEmpty(thongTin.MaProductCode1))
                {
                    sua_txtProductcode1.Text = thongTin.MaProductCode1;
                }
                if (!string.IsNullOrEmpty(thongTin.MaProductCode2))
                {
                    sua_txtProductcode2.Text = thongTin.MaProductCode2;
                }
                if (!string.IsNullOrEmpty(thongTin.MaPlateRatting))
                {
                    sua_txtPlaterating.Text = thongTin.MaPlateRatting;
                }
                if (!string.IsNullOrEmpty(thongTin.MaWarraty))
                {
                    sua_txtWarranty.Text = thongTin.MaWarraty;
                }
                if (!string.IsNullOrEmpty(thongTin.MaMaPhoi))
                {
                    sua_txtMaphoi.Text = thongTin.MaMaPhoi;
                }
            }
        }

        private void sua_btnLuulai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sua_cbTenthitruong.Text))
            {
                MessageBox.Show("Vui lòng chọn thị trường!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //if (sua_AreEnabledTextBoxesEmpty())
            //{
            //    MessageBox.Show("Vui lòng điền đầy đủ thông tin vào các ô được bật kiểm tra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return; // Không thực hiện lưu nếu có ô trống.
            //}
            // Tiếp tục thực hiện lưu dữ liệu nếu tất cả ô được bật đều có giá trị.
            string destination = sua_cbTenthitruong.Text.Trim().ToUpper();
            string bodycode = sua_txtBodycode.Text.Trim().ToUpper();
            string productcode1 = sua_txtProductcode1.Text.Trim().ToUpper();
            string productcode2 = sua_txtProductcode2.Text.Trim().ToUpper();
            string platerating = sua_txtPlaterating.Text.Trim().ToUpper();
            string warranty = sua_txtWarranty.Text.Trim().ToUpper();
            string maphoi = sua_txtMaphoi.Text.Trim().ToUpper();
            bool result = edit(destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi);
            if (result)
            {
                MessageBox.Show("Dữ liệu đã được sửa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sua_cbTenthitruong.SelectedIndex = -1;
                CleartxtFormSua();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private bool sua_AreEnabledTextBoxesEmpty()
        //{
        //    if ((sua_txtBodycode.Enabled && string.IsNullOrWhiteSpace(sua_txtBodycode.Text)) ||
        //        (sua_txtProductcode1.Enabled && string.IsNullOrWhiteSpace(sua_txtProductcode1.Text)) ||
        //        (sua_txtProductcode2.Enabled && string.IsNullOrWhiteSpace(sua_txtProductcode2.Text)) ||
        //        (sua_txtPlaterating.Enabled && string.IsNullOrWhiteSpace(sua_txtPlaterating.Text)) ||
        //        (sua_txtWarranty.Enabled && string.IsNullOrWhiteSpace(sua_txtWarranty.Text)) ||
        //        (sua_txtMaphoi.Enabled && string.IsNullOrWhiteSpace(sua_txtMaphoi.Text)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private bool edit(string destination, string bodycode, string productcode1, string productcode2, string platerating, string warranty, string maphoi)
        {
            return packinglabelController.Instance.Suadulieuthitruong(destination, bodycode, productcode1, productcode2, platerating, warranty, maphoi);
        }

        private void sua_btnHuybo_Click(object sender, EventArgs e)
        {
            sua_cbTenthitruong.SelectedIndex = -1;
            CleartxtFormSua();
        }

        //
        //
        //
        //Phần XOÁ
        private void tabPage_Xoa_Enter(object sender, EventArgs e)
        {
            txtLabel.Text = "Xoá thị trường";
            xoa_cbTenthitruong.SelectedIndex = -1;
            xoa_cbTenthitruong.Items.Clear();
            CleartxtFormXoa();
            xoa_cbTenthitruong_LOAD();
        }

        private void CleartxtFormXoa()
        {
            xoa_txtBodycode.Enabled = false;
            xoa_txtProductcode1.Enabled = false;
            xoa_txtProductcode2.Enabled = false;
            xoa_txtPlaterating.Enabled = false;
            xoa_txtWarranty.Enabled = false;
            xoa_txtMaphoi.Enabled = false;
            xoa_txtBodycode.Text = "";
            xoa_txtProductcode1.Text = "";
            xoa_txtProductcode2.Text = "";
            xoa_txtPlaterating.Text = "";
            xoa_txtWarranty.Text = "";
            xoa_txtMaphoi.Text = "";
        }

        private void xoa_cbTenthitruong_SelectedValueChanged(object sender, EventArgs e)
        {
            CleartxtFormXoa();
            List<caidats> thongTinThiTruong = new List<caidats>();
            thongTinThiTruong = Controllers.packinglabelController.Instance.LoadThongTinThiTruong(xoa_cbTenthitruong.Text);
            foreach (caidats thongTin in thongTinThiTruong)
            {
                if (!string.IsNullOrEmpty(thongTin.MaBodyCode))
                {
                    xoa_txtBodycode.Text = thongTin.MaBodyCode;
                }
                if (!string.IsNullOrEmpty(thongTin.MaProductCode1))
                {
                    xoa_txtProductcode1.Text = thongTin.MaProductCode1;
                }
                if (!string.IsNullOrEmpty(thongTin.MaProductCode2))
                {
                    xoa_txtProductcode2.Text = thongTin.MaProductCode2;
                }
                if (!string.IsNullOrEmpty(thongTin.MaPlateRatting))
                {
                    xoa_txtPlaterating.Text = thongTin.MaPlateRatting;
                }
                if (!string.IsNullOrEmpty(thongTin.MaWarraty))
                {
                    xoa_txtWarranty.Text = thongTin.MaWarraty;
                }
                if (!string.IsNullOrEmpty(thongTin.MaMaPhoi))
                {
                    xoa_txtMaphoi.Text = thongTin.MaMaPhoi;
                }
            }
        }

        private void xoa_cbTenthitruong_LOAD()
        {
            List<string> tenThiTruongList = Controllers.packinglabelController.Instance.LoadTenThiTruongList();
            foreach (var item in tenThiTruongList)
            {
                xoa_cbTenthitruong.Items.Add(item.ToString());
            }
        }

        private bool delete(string destination)
        {
            return packinglabelController.Instance.Xoadulieuthitruong(destination);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xoa_cbTenthitruong.Text))
            {
                MessageBox.Show("Vui lòng chọn thị trường!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string destination = xoa_cbTenthitruong.Text.ToUpper();
            bool result = delete(destination);
            if (result)
            {
                MessageBox.Show("Dữ liệu đã được xoá thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xoa_cbTenthitruong.SelectedIndex = -1;
                CleartxtFormXoa();
                xoa_cbTenthitruong.Items.Clear();
                xoa_cbTenthitruong_LOAD();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xoá dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        //
        //
        //Phần MẬT KHẨU
        private void tabPage_Matkhau_Enter(object sender, EventArgs e)
        {
            txtLabel.Text = "Cài đặt mật khẩu";
            loadMatkhaus();
        }

        private void loadMatkhaus()
        {
            List<string> matkhaus = DataProvider.Instance.getMatkhau();
            txtMatkhauungdung.Text = matkhaus[0].ToString();
            txtNgaythaydoi_Matkhauungdung.Text = matkhaus[1].ToString();
            txtMatkhauNG.Text = matkhaus[2].ToString();
            txtNgaythaydoi_MatkhauNG.Text = matkhaus[3].ToString();
        }

        private bool changePasswords(string matkhauungdung, string matkhauNG)
        {
            return packinglabelController.Instance.Doimatkhau(matkhauungdung, matkhauNG);
        }

        private void btn_Doimatkhau_Click(object sender, EventArgs e)
        {
            bool result = changePasswords(txtMatkhauungdung.Text, txtMatkhauNG.Text);
            if (result)
            {
                MessageBox.Show("Mật khẩu đã được thay đổi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadMatkhaus();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thay đổi mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadMatkhaus();
            }
        }

        private void dgvDanhsachthitruong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgvDanhsachthitruong.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;
                }
                if (string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}