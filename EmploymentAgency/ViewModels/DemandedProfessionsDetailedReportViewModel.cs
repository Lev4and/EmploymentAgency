﻿using Converters;
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
    public class DemandedProfessionsDetailedReportViewModel : BindableBase
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

        public ObservableCollection<object> ProfessionCategories { get; set; }

        public ObservableCollection<object> SelectedProfessionCategories { get; set; }

        public ObservableCollection<object> Professions { get; set; }

        public ObservableCollection<object> SelectedProfessions { get; set; }

        public ObservableCollection<v_demandedProfessionsDetailedReport> Report { get; set; }

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
            YFormatterForRowSeries = value => "Название профессии";

            ListDates = new List<string>();

            MinValueDateOfRegistrationVacancy = _executor.GetMinDateOfRegistrationVacancy();
            MaxValueDateOfRegistrationVacancy = _executor.GetMaxDateOfRegistrationVacancy();

            BeginValueDateOfRegistrationVacancy = MinValueDateOfRegistrationVacancy;
            EndValueDateOfRegistrationVacancy = MaxValueDateOfRegistrationVacancy;

            SelectedProfessionCategories = new ObservableCollection<object>();
            SelectedProfessions = new ObservableCollection<object>();

            ProfessionCategories = new ObservableCollection<object>(CollectionConverter<ProfessionCategory>.ConvertToObjectList(_executor.GetProfessionCategories()));
            Professions = new ObservableCollection<object>(CollectionConverter<Profession>.ConvertToObjectList(_executor.GetProfessions()));

            Show();
        }

        private void GenerateReport()
        {
            Report = new ObservableCollection<v_demandedProfessionsDetailedReport>(_executor.GetDemandedProfessionsDetailedReports(
                new Model.Logic.Range<DateTime>(BeginValueDateOfRegistrationVacancy.Date.Add(new TimeSpan(0, 0, 0, 0, 0)), EndValueDateOfRegistrationVacancy.Date.Add(new TimeSpan(0, 23, 59, 59, 999))),
                SelectedProfessionCategories.ToList(),
                SelectedProfessions.ToList()));
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
                if (LineSeries.FirstOrDefault(s => s.Title == value.ProfessionName) == null)
                    LineSeries.Add(new LineSeries { Title = value.ProfessionName, Values = new ChartValues<double>(GetCounts(value.IdProfession)) });
            }

            foreach (var value in Report)
            {
                if (LineSeries2.FirstOrDefault(s => s.Title == value.ProfessionName) == null)
                    LineSeries2.Add(new LineSeries { Title = value.ProfessionName, Values = new ChartValues<double>(GetRates(value.IdProfession)) });
            }

            foreach (var value in Report.Where(r => r.Date == Report.Max(re => re.Date) && r.Count > 0).OrderByDescending(r => r.Count).Take(15).ToList())
            {
                RowSeries.Add(new RowSeries { Title = value.ProfessionName, Values = new ChartValues<double>() { value.Count != null ? (double)value.Count : 0 } });
            }

            foreach (var value in Report.Where(r => r.Date == Report.Max(re => re.Date) && r.Count > 0).OrderByDescending(r => r.Count).Take(15).ToList())
            {
                PieSeries.Add(new PieSeries { Title = value.ProfessionName, Values = new ChartValues<double>() { value.Count != null ? (double)value.Count : 0 } });
            }
        }

        private IEnumerable<double> GetCounts(int idProfession)
        {
            var values = new List<double>();

            Report.Where(r => r.IdProfession == idProfession).ForEach(v =>
            {
                values.Add(v.Count != null ? (double)v.Count : double.NaN);
            });

            return values;
        }

        private IEnumerable<double> GetRates(int idProfession)
        {
            var values = new List<double>();

            Report.Where(r => r.IdProfession == idProfession).ForEach(v =>
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
