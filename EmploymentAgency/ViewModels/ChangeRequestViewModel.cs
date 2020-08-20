using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeRequestViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_request _request;

        public static int SelectedIdRequest { get; set; }


        public int? Salary { get; set; }

        public string NameProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public string ExperienceName { get; set; }

        public string AboutMe { get; set; }

        public ObservableCollection<object> EmploymentTypes { get; set; }

        public ObservableCollection<object> SelectedEmploymentTypes { get; set; }

        public ObservableCollection<object> Schedules { get; set; }

        public ObservableCollection<object> SelectedSchedules { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            _request = _executor.GetRequestExtendedInformation(SelectedIdRequest);

            Salary = _request.Salary;

            NameProfessionCategory = _request.NameProfessionCategory;
            ProfessionName = _request.ProfessionName;
            ExperienceName = _request.ExperienceName;
            AboutMe = _request.AboutMe;

            SelectedEmploymentTypes = new ObservableCollection<object>(CollectionConverter<PreferredEmploymentType>.ConvertToObjectList(_executor.GetPreferredEmploymentTypes(SelectedIdRequest)));
            SelectedSchedules = new ObservableCollection<object>(CollectionConverter<PreferredSchedule>.ConvertToObjectList(_executor.GetPreferredSchedules(SelectedIdRequest)));

            EmploymentTypes = new ObservableCollection<object>(CollectionConverter<EmploymentType>.ConvertToObjectList(_executor.GetEmploymentTypes()));
            Schedules = new ObservableCollection<object>(CollectionConverter<Schedule>.ConvertToObjectList(_executor.GetSchedules()));
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if(!_executor.IsRequestConsidered(SelectedIdRequest))
            {
                _executor.UpdateRequest(SelectedIdRequest, Salary, AboutMe, SelectedEmploymentTypes.ToList(), SelectedSchedules.ToList());

                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Нельзя менять данные рассмотренного запроса");
            }
        });
    }
}
