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
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class UnemploymentReportViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string[] ArrayDates { get; set; }

        public DateTime BeginValueDateOfRegistrationRequest { get; set; }

        public DateTime EndValueDateOfRegistrationRequest { get; set; }

        public DateTime MaxValueDateOfRegistrationRequest { get; set; }

        public DateTime MinValueDateOfRegistrationRequest { get; set; }

        public SeriesCollection LineSeries { get; set; }

        public SeriesCollection ColumnSeries { get; set; }

        public SeriesCollection PieSeries { get; set; }

        public List<string> ListDates { get; set; }

        public ObservableCollection<v_unemploymentReport> Report { get; set; }

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
            ListDates = new List<string>();

            MinValueDateOfRegistrationRequest = _executor.GetMinDateOfRegistrationRequest();
            MaxValueDateOfRegistrationRequest = _executor.GetMaxDateOfRegistrationRequest();

            BeginValueDateOfRegistrationRequest = MinValueDateOfRegistrationRequest;
            EndValueDateOfRegistrationRequest = MaxValueDateOfRegistrationRequest;

            Show();
        }

        private void GenerateReport()
        {
            Report = new ObservableCollection<v_unemploymentReport>(_executor.GetUnemploymentReports(
                new Model.Logic.Range<DateTime>(BeginValueDateOfRegistrationRequest.Date.Add(new TimeSpan(0, 0, 0, 0, 0)), EndValueDateOfRegistrationRequest.Date.Add(new TimeSpan(0, 23, 59, 59, 999)))));
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
            ColumnSeries = new SeriesCollection();
            PieSeries = new SeriesCollection();

            LineSeries.Add(new LineSeries { Title = "Общее количество безработных", Values = new ChartValues<double>(GetCounts()) });
            ColumnSeries.Add(new ColumnSeries { Title = "Количество безработных (за день)", Values = new ChartValues<double>(GetRates()) });

            PieSeries.Add(new PieSeries { Title = "Трудоустроенные", Values = new ChartValues<double>() { (double)Report.FirstOrDefault(r => r.Date == Report.Max(re => re.Date)).Count } });
            PieSeries.Add(new PieSeries { Title = "Нетрудоустроенные", Values = new ChartValues<double>() { (double)_executor.GetApplicants().Where(u => u.User.DateOfRegistration.Date <= Report.Max(re => re.Date)).Count() - (double)Report.FirstOrDefault(r => r.Date == Report.Max(re => re.Date)).Count } });
        }

        private IEnumerable<double> GetCounts()
        {
            var values = new List<double>();

            Report.ForEach(v =>
            {
                values.Add(v.Count != null ? (double)v.Count : double.NaN);
            });

            return values;
        }

        public IEnumerable<double> GetRates()
        {
            var values = new List<double>();

            Report.ForEach(v =>
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
