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

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {
            _svc = new CategoriesSvc();
        }

        [HttpPost("get-by-id")]
        public IActionResult getCategoryById([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }

        [HttpPost("get-all")]
        public IActionResult getAllCategories()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("proc_DoanhThuNhanVienTrongNgay")]
        public IActionResult proc_DoanhThuNhanVienTrongNgay([FromBody]string date)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_DoanhThuNhanVienTrongNgay(date);
            return Ok(res);
        }

        [HttpPost("proc_DoanhThuNhanVienTrongKhoang")]
        public IActionResult proc_DoanhThuNhanVienTrongKhoang([FromBody]BetweenDateReq date)
        {
            var res = new SingleRsp();
            res.Data = _svc.proc_DoanhThuNhanVienTrongKhoang(date.NgayBatDau, date.NgayKetThuc);
            return Ok(res);
        }

        [HttpPost("LayDoanhThuTrongNgay")]
        public IActionResult LayDoanhThuTrongNgay([FromBody]DateTime date)
        {
            var res = new SingleRsp();
            res.Data = _svc.LayDoanhThuTrongNgay(date);
            return Ok(res);
        }

        private readonly CategoriesSvc _svc;
    }
}