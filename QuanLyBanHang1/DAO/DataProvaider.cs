using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang1.DAO
{
    public class DataProvaider
    {
        private static DataProvaider instance;
        private string connectionString = ConfigurationManager.ConnectionStrings["QLBH"].ConnectionString;

        public static DataProvaider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvaider();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }



        // chay select du lieu bthg
        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
    
            DataTable dataTable = new DataTable();

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                SqlCommand command= new SqlCommand(query, connection);
                if(parameter != null )
                {
                    string[] para = query.Split(' ');
                    int i = 0;
                    foreach(string item in para ) {
                        if(item.Contains("@")) {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                connection.Close();
            }
            return dataTable;
        }


        // khi insert update delete tra ve so dong thanh cong 
        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
          
            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] para = query.Split(' ');
                    int i = 0;
                    foreach (string item in para)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }


        // tra ve cot dau tien trong dong ket qua
        public object ExcuteScaLar(string query, object[] parameter = null)
        {
            object data =0;

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] para = query.Split(' ');
                    int i = 0;
                    foreach (string item in para)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }

    }
}
