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

namespace packinglabel.Views
{
    public partial class Form_Dangnhap_Canhbao : Form
    {
        private string value;
        private string info;

        public Form_Dangnhap_Canhbao(string valueKey, string valueInfo)
        {
            InitializeComponent();
            value = valueKey;
            info = valueInfo;
        }

        private string matkhauungdung;
        private string matkhauNG;

        private void Form_Dangnhap_Canhbao_Load(object sender, EventArgs e)
        {
            List<string> matkhaus = DataProvider.Instance.getMatkhau();
            matkhauungdung = matkhaus[0].ToString();
            matkhauNG = matkhaus[2].ToString();

            if (value == "setting")
            {
                txtStatus.Text = "ĐĂNG NHẬP";
                txtStatus.BackColor = Color.Teal;
                txtInfo.Text = info;
            }
            if (value == "NG")
            {
                txtStatus.Text = "NG";
                txtStatus.BackColor = Color.Red;
                txtInfo.Text = info;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string enteredPassword = txtInput.Text;
                if (value == "setting")
                {
                    if (enteredPassword == matkhauungdung)
                    {
                        this.Hide(); // Ẩn Form_NhapMatKhau
                        Form_Caidat formCaidat = new Form_Caidat();
                        //formCaidat.FormClosed += (s, args) => this.Close(); // Đóng chương trình khi Form_Caidat đóng
                        formCaidat.Show();
                    }
                    else
                    {
                        txtInput.Clear();
                        txtInput.Focus();
                        txtInfo.Text = "Sai mật khẩu!";
                        txtInfo.ForeColor = Color.Red;
                    }
                }
                if (value == "NG")
                {
                    if (enteredPassword == matkhauNG)
                    {
                        this.Hide();
                    }
                    else
                    {
                        txtInput.Clear();
                        txtInput.Focus();
                        txtInfo.Text = "Sai mật khẩu!";
                        txtInfo.ForeColor = Color.Red;
                    }
                }
            }
        }
    }
}