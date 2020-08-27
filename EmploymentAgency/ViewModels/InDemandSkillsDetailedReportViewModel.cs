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
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class InDemandSkillsDetailedReportViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string[] ArrayDates { get; set; }

        public DateTime BeginValueDateOfRegistrationVacancy { get; set; }

        public DateTime EndValueDateOfRegistrationVacancy { get; set; }

        public DateTime MaxValueDateOfRegistrationVacancy { get; set; }

        public DateTime MinValueDateOfRegistrationVacancy { get; set; }

        public Func<double, string> YFormatterForRowSeries { get; set; }

        public SeriesCollection LineSeries { get; set; }

        public SeriesCollection LineSeries2 { get; set; }

        public SeriesCollection RowSeries { get; set; }

        public SeriesCollection PieSeries { get; set; }

        public List<string> ListDates { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public ObservableCollection<object> SelectedSkills { get; set; }

        public ObservableCollection<v_inDemandSkillsDetailedReport> Report { get; set; }

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
            YFormatterForRowSeries = value => "Название навыка";

            ListDates = new List<string>();

            MinValueDateOfRegistrationVacancy = _executor.GetMinDateOfRegistrationVacancy();
            MaxValueDateOfRegistrationVacancy = _executor.GetMaxDateOfRegistrationVacancy();

            BeginValueDateOfRegistrationVacancy = MinValueDateOfRegistrationVacancy;
            EndValueDateOfRegistrationVacancy = MaxValueDateOfRegistrationVacancy;

            SelectedSkills = new ObservableCollection<object>();

            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));

            Show();
        }

        private void GenerateReport()
        {
            Report = new ObservableCollection<v_inDemandSkillsDetailedReport>(_executor.GetInDemandSkillsDetailedReports(
                new Model.Logic.Range<DateTime>(BeginValueDateOfRegistrationVacancy.Date.Add(new TimeSpan(0, 0, 0, 0, 0)), EndValueDateOfRegistrationVacancy.Date.Add(new TimeSpan(0, 23, 59, 59, 999))),
                SelectedSkills.ToList()));
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
                if (LineSeries.FirstOrDefault(s => s.Title == value.SkillName) == null)
                    LineSeries.Add(new LineSeries { Title = value.SkillName, Values = new ChartValues<double>(GetCounts(value.IdSkill)) });
            }

            foreach (var value in Report)
            {
                if (LineSeries2.FirstOrDefault(s => s.Title == value.SkillName) == null)
                    LineSeries2.Add(new LineSeries { Title = value.SkillName, Values = new ChartValues<double>(GetRates(value.IdSkill)) });
            }

            foreach (var value in Report.Where(r => r.Date == Report.Max(re => re.Date) && r.Count > 0).OrderByDescending(r => r.Count).Take(15).ToList())
            {
                RowSeries.Add(new RowSeries { Title = value.SkillName, Values = new ChartValues<double>() { value.Count != null ? (double)value.Count : 0 } });
            }

            foreach (var value in Report.Where(r => r.Date == Report.Max(re => re.Date) && r.Count > 0).OrderByDescending(r => r.Count).Take(15).ToList())
            {
                PieSeries.Add(new PieSeries { Title = value.SkillName, Values = new ChartValues<double>() { value.Count != null ? (double)value.Count : 0 } });
            }
        }

        private IEnumerable<double> GetCounts(int idSkill)
        {
            var values = new List<double>();

            Report.Where(r => r.IdSkill == idSkill).ForEach(v =>
            {
                values.Add(v.Count != null ? (double)v.Count : double.NaN);
            });

            return values;
        }

        public IEnumerable<double> GetRates(int idSkill)
        {
            var values = new List<double>();

            Report.Where(r => r.IdSkill == idSkill).ForEach(v =>
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
