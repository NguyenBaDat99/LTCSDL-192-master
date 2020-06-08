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

    public class CategoriesRep : GenericRep<NorthwindContext, Categories>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Categories Read(int id)
        {
            var res = All.FirstOrDefault(p => p.CategoryId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.CategoryId == id);
            m = base.Delete(m); //TODO
            return m.CategoryId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public List<object> proc_DoanhThuNhanVienTrongNgay(string date)
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

                cmd.CommandText = "proc_DoanhThuNhanVienTrongNgay";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ngay", date);

                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            EmployeeID = row["EmployeeID"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            Sum = row["DoanhThu"]
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

        public List<object> proc_DoanhThuNhanVienTrongKhoang(string batdau, string ketthuc)
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

                cmd.CommandText = "proc_DoanhThuNhanVienTrongKhoang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@batdau", batdau);
                cmd.Parameters.AddWithValue("@ketthuc", ketthuc);


                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            EmployeeID = row["EmployeeID"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            Sum = row["DoanhThu"]
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

        public object LayDoanhThuTrongNgay(DateTime date)
        {
            var res = Context.Employees
                .Join(Context.Orders, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
                {
                    a.EmployeeId,
                    a.FirstName,
                    a.LastName,
                    b.OrderId,
                    b.OrderDate
                })
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.EmployeeId,
                    a.FirstName,
                    a.LastName,
                    a.OrderDate,
                    b.UnitPrice,
                    b.Quantity,
                    b.Discount,
                    DoanhThu = b.UnitPrice * b.Quantity * (1 - ((decimal)b.Discount))
                }).Where(x => x.OrderDate.HasValue && x.OrderDate.Value.Date == date.Date).ToList();
            var data = res.GroupBy(x => x.EmployeeId)
                .Select(x => new
            {
                    x.First().EmployeeId,
                    x.First().FirstName,
                    x.First().LastName,
                    TongDoanhThu = x.Sum(x => x.DoanhThu)
            }).ToList();



            return data;
        }

        #endregion
    }
}
