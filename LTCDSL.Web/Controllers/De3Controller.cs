using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using System.Collections.Generic;
    //using BLL.Req;
    using Common.Rsp;
    using Microsoft.VisualBasic;

    [Route("api/[controller]")]
    [ApiController]
    public class De3Controller : ControllerBase
    {
        public De3Controller()
        {
            _svc = new De3Svc();
        }


        [HttpPost("get-all")]
        public IActionResult getAllSuppliers()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("proc_ThemSuppiler")]
        public IActionResult proc_ThemSuppiler([FromBody]string name)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_ThemSuppiler(name);
            return Ok(res);
        }

        [HttpPost("proc_CapNhatSuppiler")]
        public IActionResult proc_CapNhatSuppiler([FromBody]SupplierReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_CapNhatSuppiler(req);
            return Ok(res);
        }

        [HttpPost("proc_TimSuppiler")]
        public IActionResult proc_TimSuppiler([FromBody]SupPageReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_TimSuppiler(req);
            return Ok(res);
        }

        [HttpPost("create-Shipper")]
        public IActionResult CreateShipper([FromBody]ShipperReq req)
        {
            var res = _svc.CreateShipper(req);
            return Ok(res);
        }

        private readonly De3Svc _svc;
    }
}