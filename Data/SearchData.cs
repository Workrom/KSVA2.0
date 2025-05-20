using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSVA2._0_WPF.Data
{
    public class SearchData
    {
        public bool UseName { get; set; }
        public string Name { get; set; }

        public bool UseExpertise { get; set; }
        public string Expertise { get; set; }

        public bool UseSubject { get; set; }
        public string Subject { get; set; }

        public bool UsePrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
