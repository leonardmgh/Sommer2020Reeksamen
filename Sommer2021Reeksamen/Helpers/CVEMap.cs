using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CVEViewer.Helpers;
using CVEViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEViewer.Models
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
