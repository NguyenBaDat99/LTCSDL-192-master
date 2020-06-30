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
    public class De4Controller : ControllerBase
    {
        public De4Controller()
        {
            _svc = new De4Svc();
        }

        [HttpPost("get-all")]
        public IActionResult getAllSuppliers()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("proc_DSProduct")]
        public IActionResult proc_DSProduct([FromBody]NgayPhanTrangReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_DSProduct(req);
            return Ok(res);
        }

        [HttpPost("proc_DSOrder")]
        public IActionResult proc_DSOrder([FromBody] SupPageReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_DSOrder(req);
            return Ok(res);
        }
        [HttpPost("DSDonHangGiaoTrongNgay")]
        public IActionResult DSDonHangGiaoTrongNgay([FromBody] NgayPhanTrangReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.DSDonHangGiaoTrongNgay(req);
            return Ok(res);
        }
        [HttpPost("SoLuongHangHoaCanGiaoTrongNgay")]
        public IActionResult SoLuongHangHoaCanGiaoTrongNgay([FromBody] GiuaHaiNgayReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.SoLuongHangHoaCanGiaoTrongNgay(req);
            return Ok(res);
        }

        private readonly De4Svc _svc;

    }
}
