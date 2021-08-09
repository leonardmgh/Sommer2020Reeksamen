using CVEViewerWPF.Models;
using CVEViewerWPF.Repository;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CVEViewerWPF.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly ObservableCollection<CVE> _cves = new();
        private CVE _currentCVE;
        private int _currentIndex = -1;

        public ObservableCollection<CVE> CVES
        {
            get { return _cves; }
            set
            {
                _cves.Clear();
                foreach (var item in value)
                {
                    _cves.Add(item);
                }
            }
        }

        public CVE CurrentCVE
        {
            get { return _currentCVE; }
            set { SetProperty(ref _currentCVE, value); }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { SetProperty(ref _currentIndex, value); }
        }

        private string _filePath;

        private DelegateCommand _openFileCommand;
        public DelegateCommand OpenFileCommand => _openFileCommand ??= new DelegateCommand(() =>
        {
            var dialog = new OpenFileDialog
            {
                Filter = "CSV documents|*.csv|All Files|*.*",
                DefaultExt = "csv"
            };

            if (_filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(_filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                _filePath = dialog.FileName;
                try
                {
                    CSVReader.ReadFile(_filePath, out ObservableCollection<CVE> tmp);
                    CVES = tmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Kunne ikke åbne fil", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });


    }

}
