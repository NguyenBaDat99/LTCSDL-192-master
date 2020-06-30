using System;
using System.Collections.Generic;
using System.Text;
using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using LTCSDL.Common.Req;
    using LTCSDL.Common.Rsp;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class De4Rep: GenericRep<NorthwindContext, Orders>
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

        public List<object> proc_DSProduct(NgayPhanTrangReq req)
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

                cmd.CommandText = "proc_DSProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ngay", req.ngay);
                cmd.Parameters.AddWithValue("@page", req.page);
                cmd.Parameters.AddWithValue("@size", req.size);

                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            ProductID = row["ProductID"],
                            ProductName = row["ProductName"],
                            SuppilerID = row["SupplierID"],
                            CategoryID = row["CategoryID"],
                            QuantityPerUnit = row["QuantityPerUnit"],
                            UnitPrice = row["UnitPrice"],
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

        public List<object> proc_DSOrder(SupPageReq req)
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

                cmd.CommandText = "proc_DSOrder";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@page", req.page);
                cmd.Parameters.AddWithValue("@size", req.size);
                cmd.Parameters.AddWithValue("@keyword", req.keyword);

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
                            EmployeeID = row["EmployeeID"],
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

        public object DSDonHangGiaoTrongNgay(NgayPhanTrangReq req)
        {
            var res = Context.Orders
                .Join(Context.Customers, a => a.CustomerId, b => b.CustomerId, (a, b) => new
                { 
                    a.OrderId,
                    a.OrderDate,
                    b.CompanyName,
                    a.ShipAddress
                }).Where(x => x.OrderDate.HasValue && x.OrderDate.Value.Date == req.ngay.Date).ToList();

            var offset = (req.page - 1) * req.size;
            var total = res.Count();
            int totalPage = (total % req.size) == 0 ? (int)(total / req.size) : (int)((total / req.size) + 1);
            var data = res.OrderBy(x => x.OrderId).Skip(offset).Take(req.size).ToList();

            var result = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = req.page,
                Size = req.size
            };
            return result;
        }

        public object SoLuongHangHoaCanGiaoTrongNgay(GiuaHaiNgayReq req)
        {
            var res = Context.Orders
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.OrderDate,
                    b.Quantity
                }).Where(x => x.OrderDate.HasValue && 
                x.OrderDate.Value.Date >= req.NgayBatDau.Date &&
                x.OrderDate.Value.Date <= req.NgayKetThuc.Date).ToList();

            var result = res.GroupBy(x => x.OrderDate)
               .Select(x => new
               {
                   x.First().OrderDate,
                   TongSoHangCanGiao = x.Sum(x => x.Quantity)
               }).ToList();
            return result;
        }

        #endregion
    }
}
