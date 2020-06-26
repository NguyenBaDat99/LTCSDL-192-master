using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class SupPageReq
    {
        public int page { get; set; }
        public int size { get; set; }
        public string keyword { get; set; }
    }
}
