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

    public class De3Svc : GenericSvc<De3Rep, Shippers>
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

        public List<object> proc_ThemSuppiler(string name)
        {
            return _rep.proc_ThemSuppiler(name);
        }

        public List<object> proc_CapNhatSuppiler(SupplierReq req)
        {
            return _rep.proc_CapNhatSuppiler(req);
        }

        public List<object> proc_TimSuppiler(SupPageReq req)
        {
            return _rep.proc_TimSuppiler(req);
        }

        public SingleRsp CreateShipper(ShipperReq req)
        {
            var res = new SingleRsp();
            Shippers shipper = new Shippers();
            shipper.CompanyName = req.CompanyName;
            shipper.Phone = req.Phone;                     

            res = _rep.CreateShipper(shipper);

            return res;
        }

        #endregion
    }
}
