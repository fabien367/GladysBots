using System;
using System.Collections.Generic;
using System.Text;

namespace Gladys.Models.ReferencesFirm
{
   public class ReferencesFirmModel
    {
        public int ID { get; set; }
        
        public string Classification { get; set; }

        public int OrderSolution { get; set; }
       
        public string Problem { get; set; }
        
        public string Solution { get; set; }
    }
}
