using DataAccess.ProductNetFrameWork.DAO;
using DataAccess.ProductNetFrameWork.DBHelper;
using DataAccess.ProductNetFrameWork.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DAOImpl
{
    public class OrderDAOImpl : IOrderDAO
    {
        public List<Order> GetListOrder(string MaDonHang, DateTime fromDate, DateTime toDate)
        {
            var list = new List<Order>();
            try
            {
                // B1 : Mở connection
                var conn = DBConnection.GetSqlConnection();
                // Bước 2 : Dùng SQLCOMMAND

                //var cmd1 = new SqlCommand("Select * from ", conn);
                // cmd1.CommandType = System.Data.CommandType.Text;

                var cmd = new SqlCommand("SP_Order_GetList", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // B3 : Truyền tham số 
                cmd.Parameters.AddWithValue("@_MaDH", MaDonHang);
                cmd.Parameters.AddWithValue("@_FromDate", fromDate);
                cmd.Parameters.AddWithValue("@_ToDate", toDate);

                // B4 : đọc dữ liệu trong Database

                var sqlDataReader = cmd.ExecuteReader();

                // B 5: TRẢ VỀ DỮ LIỆU
                list = DataReaderMapToList<Order>(sqlDataReader);

                //while (sqlDataReader.Read())
                //{
                //    var order = new Order();
                //    var orderId = sqlDataReader["OrderID"] != DBNull.Value ? Convert.ToInt32(sqlDataReader["OrderID"].ToString()) : 0;
                //    order.OrderID = orderId;

                //    list.Add(order);
                //}

                // B 6:
                conn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }

        public int OrderCreate(Order order)
        {
            throw new NotImplementedException();
        }

        public List<T> DataReaderMapToList<T>(SqlDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
