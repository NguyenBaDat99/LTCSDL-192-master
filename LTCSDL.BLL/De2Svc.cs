using Newtonsoft.Json;

using LTCSDL.BLL;
using LTCSDL.Common.Rsp;
using LTCSDL.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    public class De2Svc : GenericSvc<De2Rep, Orders>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        
        public List<object> proc_DanhSachDHTrongKhoang(string begin, string end)
        {
            return _rep.proc_DanhSachDHTrongKhoang(begin, end);
        }

        public List<object> proc_DanhSachChiTietDH(int id)
        {
            return _rep.proc_DanhSachChiTietDH(id);
        }

        public object LayDonHangTrongKhoang(DateTime begin, DateTime end)
        {
            return _rep.LayDonHangTrongKhoang(begin, end);
        }

        public object LayChiTietDonHang(int id)
        {
            return _rep.LayChiTietDonHang(id);
        }
        #endregion
    }
}
