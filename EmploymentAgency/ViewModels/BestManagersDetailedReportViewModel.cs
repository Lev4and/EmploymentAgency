using Converters;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class BestManagersDetailedReportViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string[] ArrayDates { get; set; }

        public DateTime? BeginValueDateOfConsiderationRequest { get; set; }

        public DateTime? EndValueDateOfConsiderationRequest { get; set; }

        public DateTime? MaxValueDateOfConsiderationRequest { get; set; }

        public DateTime? MinValueDateOfConsiderationRequest { get; set; }

        public Func<double, string> YFormatterForRowSeries { get; set; }

        public SeriesCollection LineSeries { get; set; }

        public SeriesCollection LineSeries2 { get; set; }

        public SeriesCollection RowSeries { get; set; }

        public SeriesCollection PieSeries { get; set; }

        public List<string> ListDates { get; set; }

        public ObservableCollection<object> FullNameManagers { get; set; }

        public ObservableCollection<object> SelectedFullNameManagers { get; set; }

        public ObservableCollection<v_bestManagersDetailedReport> Report { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand ShowCharts => new DelegateCommand(() =>
        {
            Show();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        });

        private void ResetToDefault()
        {
            YFormatterForRowSeries = value => "ФИО менеджера";

            ListDates = new List<string>();

            MinValueDateOfConsiderationRequest = _executor.GetMinDateOfConsiderationRequest();
            MaxValueDateOfConsiderationRequest = _executor.GetMaxDateOfConsiderationRequest();

            BeginValueDateOfConsiderationRequest = MinValueDateOfConsiderationRequest;
            EndValueDateOfConsiderationRequest = MaxValueDateOfConsiderationRequest;

            SelectedFullNameManagers = new ObservableCollection<object>();

            FullNameManagers = new ObservableCollection<object>(CollectionConverter<v_manager>.ConvertToObjectList(_executor.GetManagers()));

            Show();
        }

        private void GenerateReport()
        {
            Report = new ObservableCollection<v_bestManagersDetailedReport>(_executor.GetBestManagersDetailedReport(
                (BeginValueDateOfConsiderationRequest != null ? ((DateTime)BeginValueDateOfConsiderationRequest).Date.Add(new TimeSpan(0, 0, 0, 0, 0)) : BeginValueDateOfConsiderationRequest),
                (EndValueDateOfConsiderationRequest != null ?  ((DateTime)EndValueDateOfConsiderationRequest).Date.Add(new TimeSpan(0, 23, 59, 59, 999)) : EndValueDateOfConsiderationRequest),
                SelectedFullNameManagers.ToList()));
        }

        private void ClearLists()
        {
            ListDates.Clear();
        }

        private void GenerateListData()
        {
            Report.ForEach(r =>
            {
                if (!ListDates.Contains(((DateTime)r.Date).ToString("dd-MM-yyyy")))
                    ListDates.Add(((DateTime)r.Date).ToString("dd-MM-yyyy"));
            });

            ArrayDates = ListDates.ToArray();
        }

        private void GenerateSeries()
        {
            LineSeries = new SeriesCollection();
            LineSeries2 = new SeriesCollection();
            RowSeries = new SeriesCollection();
            PieSeries = new SeriesCollection();

            foreach (var value in Report)
            {
                if (LineSeries.FirstOrDefault(s => s.Title == value.FullNameManager) == null)
                    LineSeries.Add(new LineSeries { Title = value.FullNameManager, Values = new ChartValues<double>(GetCounts(value.IdManager)) });
            }

            foreach (var value in Report)
            {
                if (LineSeries2.FirstOrDefault(s => s.Title == value.FullNameManager) == null)
                    LineSeries2.Add(new LineSeries { Title = value.FullNameManager, Values = new ChartValues<double>(GetRates(value.IdManager)) });
            }

            foreach (var value in Report.Where(r => r.Date == Report.Max(re => re.Date) && r.Count > 0).OrderByDescending(r => r.Count).Take(15).ToList())
            {
                RowSeries.Add(new RowSeries { Title = value.FullNameManager, Values = new ChartValues<double>() { value.Count != null ? (double)value.Count : 0 } });
            }

            foreach (var value in Report.Where(r => r.Date == Report.Max(re => re.Date) && r.Count > 0).OrderByDescending(r => r.Count).Take(15).ToList())
            {
                PieSeries.Add(new PieSeries { Title = value.FullNameManager, Values = new ChartValues<double>() { value.Count != null ? (double)value.Count : 0 } });
            }
        }

        private IEnumerable<double> GetCounts(int idManager)
        {
            var values = new List<double>();

            Report.Where(r => r.IdManager == idManager).ForEach(v =>
            {
                values.Add(v.Count != null ? (double)v.Count : double.NaN);
            });

            return values;
        }

        public IEnumerable<double> GetRates(int idManager)
        {
            var values = new List<double>();

            Report.Where(r => r.IdManager == idManager).ForEach(v =>
            {
                values.Add(v.Rate != null ? (double)v.Rate : double.NaN);
            });

            return values;
        }

        private void Show()
        {
            ClearLists();
            GenerateReport();
            GenerateListData();
            GenerateSeries();
        }
    }
}
