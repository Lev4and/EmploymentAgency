using System;
using System.Collections.ObjectModel;

namespace EmploymentAgency.Events
{
    public class DataEventArgs : EventArgs
    {
        public ObservableCollection<object> Data { get; private set; }

        public DataEventArgs(ObservableCollection<object> data) : base()
        {
            Data = data;
        }
    }
}
