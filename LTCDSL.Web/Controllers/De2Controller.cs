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
    public class De2Controller : ControllerBase
    {
        public De2Controller()
        {
            _svc = new De2Svc();
        }


        [HttpPost("get-all")]
        public IActionResult getAllOrders()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("proc_DanhSachDHTrongKhoang")]
        public IActionResult proc_DanhSachDHTrongKhoang([FromBody]BetweenDateReq date)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_DanhSachDHTrongKhoang(date.begin, date.end);
            return Ok(res);
        }

        [HttpPost("proc_DanhSachChiTietDH")]
        public IActionResult proc_DanhSachChiTietDH([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            var temp = _svc.proc_DanhSachChiTietDH(req.Id);
            res.Data = temp;
            return Ok(res);
        }

        [HttpPost("LayDonHangTrongKhoang")]
        public IActionResult LayDonHangTrongKhoang([FromBody]GiuaHaiNgayReq date)
        {
            var res = new SingleRsp();
            var temp = _svc.LayDonHangTrongKhoang(date.NgayBatDau, date.NgayKetThuc);
            res.Data = temp;
            return Ok(res);
        }

        [HttpPost("LayChiTietDonHang")]
        public IActionResult LayChiTietDonHang([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.LayChiTietDonHang(req.Id);
            return Ok(res);
        }

        private readonly De2Svc _svc;
    }
}