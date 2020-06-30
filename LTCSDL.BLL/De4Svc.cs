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
    using LTCSDL.Common.Req;
    public class De4Svc : GenericSvc<De4Rep, Orders>
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

        public List<object> proc_DSProduct(NgayPhanTrangReq req)
        {
            return _rep.proc_DSProduct(req);
        }
        public List<object> proc_DSOrder(SupPageReq req)
        {
            return _rep.proc_DSOrder(req);
        }
        public object DSDonHangGiaoTrongNgay(NgayPhanTrangReq req)
        {
            return _rep.DSDonHangGiaoTrongNgay(req);
        }
        public object SoLuongHangHoaCanGiaoTrongNgay(GiuaHaiNgayReq req)
        {
            return _rep.SoLuongHangHoaCanGiaoTrongNgay(req);
        }

        #endregion
    }
}
