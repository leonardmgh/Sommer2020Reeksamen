using CsvHelper.Configuration.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEViewer.Models
{
    public enum Status
    {
        Canidate,
        Entry
    }
    class CVE : BindableBase
    {
        private String _name;
        private Status _status;
        private String _description;
        private readonly ObservableCollection<string> _references;
        private String _phase;
        private String _votes;
        private readonly ObservableCollection<String> _comments;

        public CVE()
        {
            _references = new();
            _comments = new();
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public Status Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public ObservableCollection<string> References
        {
            get { return _references; }
            set
            {
                _references.Clear();
                foreach (var item in value)
                {
                    _references.Add(item);
                }
            }
        }

        public String Phase
        {
            get { return _phase; }
            set { SetProperty(ref _phase, value); }
        }

        public String Votes
        {
            get { return _votes; }
            set { SetProperty(ref _votes, value); }
        }

        public ObservableCollection<String> Comments
        {
            get { return _comments; }
            set
            {
                _comments.Clear();
                foreach (var item in value)
                {
                    _comments.Add(item);
                }
            }
        }
    }
}
