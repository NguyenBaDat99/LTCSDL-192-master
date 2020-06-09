using System;
using System.Collections.Generic;
using System.Text;
using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class De2Rep: GenericRep<NorthwindContext, Orders>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Orders Read(int id)
        {
            var res = All.FirstOrDefault(p => p.OrderId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.OrderId == id);
            m = base.Delete(m); //TODO
            return m.OrderId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>        

        public List<object> proc_DanhSachDHTrongKhoang(string begin, string end)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                var cmd = cnn.CreateCommand();

                cmd.CommandText = "proc_DanhSachDHTrongKhoang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@begin", begin);
                cmd.Parameters.AddWithValue("@end", end);


                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            OrderID = row["OrderID"],
                            CustomerID = row["CustomerID"],
                            OrderDate = row["OrderDate"],
                            RequiredDate = row["RequiredDate"]
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception ex)
            {
                res = null;
            }

            return res;
        }

        public List<object> proc_DanhSachChiTietDH(int id)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                var cmd = cnn.CreateCommand();

                cmd.CommandText = "proc_DanhSachChiTietDH";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);


                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            OrderID = row["OrderID"],
                            ProductID = row["ProductID"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"]
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception ex)
            {
                res = null;
            }

            return res;
        }

        public object LayDonHangTrongKhoang(DateTime begin, DateTime end)
        {
            var res = Context.Orders.Where(x => x.OrderDate.HasValue &&
            x.OrderDate.Value.Date >= begin.Date && 
            x.OrderDate.Value.Date <= end.Date).ToList();
            
            return res;
        }

        public object LayChiTietDonHang(int id)
        {
            var res = Context.OrderDetails.Where(x => x.OrderId == id).ToList();

            return res;
        }

        #endregion

    }
}
