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
    public class De3Rep : GenericRep<NorthwindContext, Shippers>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Shippers Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ShipperId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ShipperId == id);
            m = base.Delete(m); //TODO
            return m.ShipperId;
        }

        public SingleRsp CreateShipper(Shippers shipper)
        {
            var res = new SingleRsp();
            using (var context = new NorthwindContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Shippers.Add(shipper);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        #endregion


        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>        

        public List<object> proc_ThemSuppiler(string name)
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

                cmd.CommandText = "proc_ThemSuppiler";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);

                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            SupplierID = row["SupplierID"],
                            CompanyName = row["CompanyName"],
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

        public List<object> proc_CapNhatSuppiler(SupplierReq req)
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

                cmd.CommandText = "proc_CapNhatSuppiler";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", req.ID);
                cmd.Parameters.AddWithValue("@CompanyName", req.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", req.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", req.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", req.Address);

                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            SupplierID = row["SupplierID"],
                            CompanyName = row["CompanyName"],
                            ContactName = row["ContactName"],
                            ContactTitle = row["ContactTitle"],
                            Address = row["Address"],
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

        public List<object> proc_TimSuppiler(SupPageReq req)
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

                cmd.CommandText = "proc_TimSuppiler";
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
                            STT = row["STT"],
                            SupplierID = row["SupplierID"],
                            CompanyName = row["CompanyName"],
                            ContactName = row["ContactName"],
                            ContactTitle = row["ContactTitle"],
                            Address = row["Address"],
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


        #endregion
    }
}
