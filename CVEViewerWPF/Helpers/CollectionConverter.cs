using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEViewerWPF.Helpers
{
    class CollectionConverter : DefaultTypeConverter
    {
        public override ObservableCollection<string> ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var subs = text.Split("|");
            ObservableCollection<string> data = new();
            foreach (var item in subs)
            {
                data.Add(item);
            }
            return data;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}
