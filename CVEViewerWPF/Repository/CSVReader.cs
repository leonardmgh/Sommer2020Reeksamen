using CsvHelper;
using CsvHelper.Configuration;
using CVEViewerWPF.Helpers;
using CVEViewerWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEViewerWPF.Repository
{
    class CSVReader
    {
        public static bool ReadFile(string path, out ObservableCollection<CVE> data)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using TextReader reader = new StreamReader(path);
            //Read the first 9 lines
            for (int i = 0; i < 10; i++)
            {
                reader.ReadLine();
            }
            data = null;
            try
            {
                using var csvReader = new CsvReader(reader, config);
                csvReader.Context.RegisterClassMap<CVEMap>();
                data = new ObservableCollection<CVE>(csvReader.GetRecords<CVE>());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }
    }
}
