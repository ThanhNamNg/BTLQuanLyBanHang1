using QuanLyBanHang1.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang1
{
    public partial class fDangNhapLaAdmin : Form
    {
        public fDangNhapLaAdmin()
        {
            InitializeComponent();
            LoadDuLieuLenBang();
        }

        public void LoadDuLieuLenBang()
        {

           
            // load du lieu bang nhan vien
            string query1 = "exec layDuLieuBangNhanVien";
            dataGridViewNhanVien.DataSource = DataProvaider.Instance.ExcuteQuery(query1);

            // load du lieu bang nha cung cap
            string query2 = "EXEC layDuLieuBangNhaCungCap";
            dataGridViewNhaCungCap.DataSource = DataProvaider.Instance.ExcuteQuery(query2);

            // load du lieu bang mat hang
            string query3 = "EXEC layDuLieuBangMatHang";
            dataGridViewMatHang.DataSource = DataProvaider.Instance.ExcuteQuery(query3);

            // load du lieu len bang nhap hang

            string query4 = "EXEC duLieuNhapHang";
            dataGridViewHangTrongKho.DataSource = DataProvaider.Instance.ExcuteQuery(query4);
            

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }


        #region XuLybangNhanVien
        private void btn_Them_NhanVien_Click(object sender, EventArgs e)
        {
            if (ValidateDataNhanVien()&& ktCMNDTrung())
            {
                string tenNhanVien = textBoxTenNhanVien.Text;
                string cmnd = textBoxCMND.Text;
                string taiKhoan = textBoxTaiKhoan.Text;
                string matKhau = textBoxMatKhau.Text;
                int chucNang;
                if (cbChucNang.Text == "Admin")
                {
                    chucNang = 1;
                }
                else
                {
                    chucNang = 0;
                }
                string query = "execute ThemNhanVien @TenNV , @CMND , @TaiKhoan , @MatKhau , @ChucNang ";
                int kt = DataProvaider.Instance.ExcuteNonQuery(query, new object[] { tenNhanVien, cmnd, taiKhoan, matKhau, chucNang });

                if (kt == 0)
                {
                    MessageBox.Show("Thêm thất bại");
                }
                else
                {
                    MessageBox.Show("Thêm thành công");
                    lamMoiNhanVien();
                    lamTrongO();
                }
            }
            
          

        }

        private void btn_LamMoiNhanVien_Click(object sender, EventArgs e)
        {
            lamMoiNhanVien();
        }

        public void lamMoiNhanVien()
        {
            string query1 = "exec layDuLieuBangNhanVien";
            dataGridViewNhanVien.DataSource = DataProvaider.Instance.ExcuteQuery(query1);
        }


        public bool ValidateDataNhanVien()
        {
            // Kiểm tra dữ liệu cho textBoxTenNhanVien
            if (string.IsNullOrWhiteSpace(textBoxTenNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTenNhanVien.Focus();
                return false;
            }

            // Kiểm tra dữ liệu cho textBoxCMND
            if (string.IsNullOrWhiteSpace(textBoxCMND.Text))
            {
                MessageBox.Show("Vui lòng nhập số CMND.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCMND.Focus();
                return false;
            }
            // Kiểm tra dữ liệu cho textBoxTaiKhoan
            if (string.IsNullOrWhiteSpace(textBoxTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTaiKhoan.Focus();
                return false;
            }

            // Kiểm tra dữ liệu cho textBoxMatKhau
            if (string.IsNullOrWhiteSpace(textBoxMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxMatKhau.Focus();
                return false;
            }

            // Kiểm tra dữ liệu cho cbChucNang
            if (cbChucNang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chức năng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbChucNang.Focus();
                return false;
            }

           
            return true;

        }
        public bool ktCMNDTrung()
        {
            // Kiêm tra có trùng cmnd
            string cmnd = textBoxCMND.Text;

            string query = "SELECT *   FROM [dbo].[tblNhanVien]  WHERE [sCMND] = '" + cmnd + "';";
            int kt = -1;
            DataTable duLieu = DataProvaider.Instance.ExcuteQuery(query);
            kt = duLieu.Rows.Count;

            if (kt > 0)
            {
                MessageBox.Show("Vui lòng nhập số CMND không được trùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCMND.Focus();
                return false;
            }
            return true;
        }


        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (textBoxCMND.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số CMND hoặc chọn một nhân viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCMND.Focus();
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string cmnd = textBoxCMND.Text;
                    string query2 = "EXEC [dbo].[DeleteNhanVienByCMND] @CMND ";
                    int kq = DataProvaider.Instance.ExcuteNonQuery(query2, new object[] { cmnd });

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK);
                        lamMoiNhanVien();
                        lamTrongO();
                    }
                }
            }
        }

        private void dataGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            layDuLieuTuBangLenCacO();
        }

        public void layDuLieuTuBangLenCacO()
        {
            // Lấy chỉ số dòng được chọn
            int rowIndex = dataGridViewNhanVien.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewNhanVien.Rows[rowIndex];

                // Điền dữ liệu vào các TextBox và ComboBox tương ứng
                textBoxTenNhanVien.Text = row.Cells["Tên Nhân Viên"].Value.ToString();
                textBoxCMND.Text = row.Cells["Số CMND"].Value.ToString();
                textBoxTaiKhoan.Text = row.Cells["Tài khoản"].Value.ToString();
                textBoxMatKhau.Text = row.Cells["Mật Khẩu"].Value.ToString();

                // Thiết lập giá trị cho ComboBox cbChucNang
                bool isAdmin = Convert.ToBoolean(row.Cells["Chức năng"].Value);
                if (isAdmin)
                {
                    cbChucNang.SelectedItem = "Admin";
                }
                else
                {
                    cbChucNang.SelectedItem = "Nhân viên";
                }
            }
        }

        private void btn_LamTrongNV_Click(object sender, EventArgs e)
        {
            lamTrongO();
        }
        public void lamTrongO() {

            textBoxTenNhanVien.Text = ""; // Xóa dữ liệu trong TextBox tên nhân viên
            textBoxCMND.Text = "";         // Xóa dữ liệu trong TextBox số CMND
            textBoxTaiKhoan.Text = "";     // Xóa dữ liệu trong TextBox tài khoản
            textBoxMatKhau.Text = "";      // Xóa dữ liệu trong TextBox mật khẩu
            cbChucNang.SelectedIndex = -1; // Chọn lại mục đầu tiên trong ComboBox chức năng (nếu có)
        }


        private void btn_Sua_Click(object sender, EventArgs e)
        {
           if(ValidateDataNhanVien())
            {
                string tenNhanVien = textBoxTenNhanVien.Text;
                string cmnd = textBoxCMND.Text;
                string taiKhoan = textBoxTaiKhoan.Text;
                string matKhau = textBoxMatKhau.Text;
                int chucNang;
                if (cbChucNang.Text == "Admin")
                {
                    chucNang = 1;
                }
                else
                {
                    chucNang = 0;
                }
                string query = "EXEC [dbo].[UpdateNhanVienByCMND] @CMND , @TenNV , @TaiKhoan , @MatKhau , @ChucNang ";
                int kt = DataProvaider.Instance.ExcuteNonQuery(query, new object[] { cmnd, tenNhanVien, taiKhoan, matKhau, chucNang });

                if (kt == 0)
                {
                    MessageBox.Show("Sửa thất bại");
                }
                else
                {
                    MessageBox.Show("Sửa thành công");
                    lamMoiNhanVien();
                    lamTrongO();
                }
            }
        }

        #endregion

        #region XulyBnagNhaCungCap
        public bool ValidateNhaCungCap()
        {
            // Kiểm tra dữ liệu cho textBox_TenNCC
            if (string.IsNullOrWhiteSpace(textBox_TenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_TenNCC.Focus();
                return false;
            }

            // Kiểm tra dữ liệu cho textBox_SDT
            if (string.IsNullOrWhiteSpace(textBox_SDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_SDT.Focus();
                return false;
            }
            // Kiểm tra dữ liệu cho textBoxDiaChi
            if (string.IsNullOrWhiteSpace(textBoxDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxDiaChi.Focus();
                return false;
            }

            // Các kiểm tra khác nếu cần

            return true; // Trả về true nếu dữ liệu hợp lệ
        }

        public void lamMoiNhaCungCap()
        {
            string query2 = "EXEC layDuLieuBangNhaCungCap";
            dataGridViewNhaCungCap.DataSource = DataProvaider.Instance.ExcuteQuery(query2);
        }

        private void btn_ThemNCC_Click(object sender, EventArgs e)
        {
            if (ValidateNhaCungCap())
            {
                string tenNhaCC = textBox_TenNCC.Text;
                string diaChi = textBoxDiaChi.Text;
                string dienThoai = textBox_SDT.Text;

                string query = "EXEC sp_ThemNhaCungCap @TenNhaCC , @DiaChi , @DienThoai";
                int kt = DataProvaider.Instance.ExcuteNonQuery(query, new object[] { tenNhaCC, diaChi, dienThoai });

                if (kt == 0)
                {
                    MessageBox.Show("Thêm nhà cung cấp thất bại");
                }
                else
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công");
                    // Gọi hàm làm mới danh sách nhà cung cấp nếu cần
                    lamMoiNhaCungCap();
                    lamTRongNCC();
                }
            }
        }

        private void dataGridViewNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            layDuLieuNhaCungCapLenCacO();
        }


        string idNCC = "-1";
        public void layDuLieuNhaCungCapLenCacO()
        {
            int rowIndex = dataGridViewNhaCungCap.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewNhaCungCap.Rows[rowIndex];
                idNCC= row.Cells["ID"].Value.ToString();
                textBox_TenNCC.Text = row.Cells["Tên Nhà Cung Cấp"].Value.ToString();
                textBoxDiaChi.Text = row.Cells["Địa chỉ"].Value.ToString();
                textBox_SDT.Text = row.Cells["Số Điện Thoại"].Value.ToString();
            }
        }

        private void btn_SuaNCC_Click(object sender, EventArgs e)
        {
            if (ValidateNhaCungCap())
            {
                string tenNCC = textBox_TenNCC.Text;
                string diaChi = textBoxDiaChi.Text;
                string sdt = textBox_SDT.Text;

                // Viết câu truy vấn để gọi stored procedure sửa nhà cung cấp
                string query = "EXEC [dbo].[sp_SuaNhaCungCap] @MaNCC , @TenNhaCC , @DiaChi , @DienThoai";

                // Chuyển đổi số điện thoại thành kiểu nvarchar(12)
                // Đảm bảo số điện thoại có thể được truyền vào stored procedure
                string sdtFormatted = sdt.Length > 12 ? sdt.Substring(0, 12) : sdt;

                // Thực thi câu truy vấn và truyền tham số vào
                int kt = DataProvaider.Instance.ExcuteNonQuery(query, new object[] { idNCC, tenNCC, diaChi, sdtFormatted });

                // Kiểm tra kết quả của câu truy vấn
                if (kt == 0)
                {
                    MessageBox.Show("Sửa thất bại");
                }
                else
                {
                    MessageBox.Show("Sửa thành công");
                    lamMoiNhaCungCap();
                    lamTRongNCC();
                }
            }
        }
        private void btl_LamMoi_Click(object sender, EventArgs e)
        {
            lamMoiNhaCungCap();
        }

        private void Btn_XoaNCC_Click(object sender, EventArgs e)
        {
            if (idNCC != "-1") // Đảm bảo rằng đã chọn một nhà cung cấp để xóa
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Viết câu truy vấn để gọi stored procedure xóa nhà cung cấp
                    string query = "EXEC [dbo].[sp_XoaNhaCungCap] @MaNCC";

                    // Thực thi câu truy vấn và truyền tham số vào
                    int kt = DataProvaider.Instance.ExcuteNonQuery(query, new object[] { idNCC });

                    // Kiểm tra kết quả của câu truy vấn
                    if (kt == 0)
                    {
                        MessageBox.Show("Xóa thất bại");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thành công");
                        lamMoiNhaCungCap(); // Sau khi xóa, làm mới danh sách nhà cung cấp
                        lamTRongNCC();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btn_LamTrongNCC_Click(object sender, EventArgs e)
        {
            lamTRongNCC();
        }

        public void lamTRongNCC()
        {
            
                textBox_TenNCC.Text = "";   // Xóa dữ liệu trong TextBox tên nhà cung cấp
                textBoxDiaChi.Text = "";     // Xóa dữ liệu trong TextBox địa chỉ
                textBox_SDT.Text = "";       // Xóa dữ liệu trong TextBox số điện thoại
            

        }

        #endregion

        #region XuLyBangNhapHang

        private void button13_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }


}
