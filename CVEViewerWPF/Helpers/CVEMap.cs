using CsvHelper.Configuration;
using CVEViewerWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Map(m => m.References).TypeConverter<CollectionConverter>().Optional();
            Map(m => m.Phase).Optional();
            Map(m => m.Votes).Optional();
            Map(m => m.Comments).TypeConverter<CollectionConverter>().Optional();
        }
    }
}
