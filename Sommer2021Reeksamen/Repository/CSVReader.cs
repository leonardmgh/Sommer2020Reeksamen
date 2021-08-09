using CsvHelper;
using CsvHelper.Configuration;
using CVEViewer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEViewer.Repository
{
    class CSVReader
    {
        public static bool ReadFile(string path, out ObservableCollection<CVE> data)
        {
            using TextReader reader = new StreamReader(path);
            //Read the first 9 lines
            for (int i = 0; i < 10; i++){
                reader.ReadLine();
            }
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<CVEMap>();
            data = (ObservableCollection<CVE>)csvReader.GetRecords<CVE>();
            return true;
        }
    }
}
