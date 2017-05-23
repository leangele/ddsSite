using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ddcSite.Models
{
    public class CompleteOrder
    {
        public Order OrderMaster { get; set; }
        public List<DetailOrder> OrderDetail { get; set; }
        public List<Attachment> ListAtaAttachments { get; set; }
    }
}