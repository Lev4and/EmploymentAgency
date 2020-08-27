using DevExpress.Mvvm;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class StatisticsViewModel : BindableBase
    {
        private readonly StatisticsPageService _statisticsPageService;

        public Page PageSource { get; set; }

        public StatisticsViewModel(StatisticsPageService statisticsPageService)
        {
            _statisticsPageService = statisticsPageService;
            _statisticsPageService.OnPageChanged += (page) => { PageSource = page; };

            _statisticsPageService = statisticsPageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new AverageAgeDetailedReport());
        });

        public ICommand AverageAgeDetailedReport => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new AverageAgeDetailedReport());
        }, () => (PageSource != null ? PageSource.GetType().Name != "AverageAgeDetailedReport" : true));

        public ICommand AverageSalaryDetailedReport => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new AverageSalaryDetailedReport());
        }, () => (PageSource != null ? PageSource.GetType().Name != "AverageSalaryDetailedReport" : true));

        public ICommand BestManagersDetailedReport => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new BestManagersDetailedReport());
        }, () => (PageSource != null ? PageSource.GetType().Name != "BestManagersDetailedReport" : true));

        public ICommand DemandedProfessionsDetailedReport => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new DemandedProfessionsDetailedReport());
        }, () => (PageSource != null ? PageSource.GetType().Name != "DemandedProfessionsDetailedReport" : true));

        public ICommand InDemandSkillsDetailedReport => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new InDemandSkillsDetailedReport());
        }, () => (PageSource != null ? PageSource.GetType().Name != "InDemandSkillsDetailedReport" : true));

        public ICommand UnemploymentReport => new DelegateCommand(() =>
        {
            _statisticsPageService.ChangePage(new UnemploymentReport());
        }, () => (PageSource != null ? PageSource.GetType().Name != "UnemploymentReport" : true));
    }
}
