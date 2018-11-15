using System;
using System.Collections.Generic;
using System.Text;

namespace Gladys.Models.Firms
{
    public partial class Firm
    {
        public int ID { get; set; }
        
        public string FirmName { get; set; }

        public string ModelName { get; set; }
        
        public string TableName { get; set; }

        public bool IsDel { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
