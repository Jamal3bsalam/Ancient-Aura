using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification
{
    public class SpecParameters
    {
        public string? sort { get; set; }
        public int? pageSize { get; set; } = 6;
        public int? pageIndex { get; set; } = 1;

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
    }
}
