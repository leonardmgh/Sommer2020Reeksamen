using CsvHelper.Configuration;
using CVEViewerWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEViewerWPF.Helpers
{
    class CVEMap : ClassMap<CVE>
    {
        public CVEMap()
        {
            Map(m => m.Name);
            Map(m => m.Status);
            Map(m => m.Description);
            Map(m => m.References).TypeConverter<CollectionConverter>();
            Map(m => m.Phase);
            Map(m => m.Votes);
            Map(m => m.Comments).TypeConverter<CollectionConverter>();
        }
    }
}
