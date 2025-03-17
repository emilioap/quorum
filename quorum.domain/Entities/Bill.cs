using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quorum.domain.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PrimarySponsorId { get; set; }
    }
}
