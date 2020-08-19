using DevExpress.Mvvm;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MyVacanciesViewModel : BindableBase
    {
        public MyVacanciesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {

        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddVacancy());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            //ChangeCityViewModel.SelectedIdCity = (int)SelectedIdCity;

            //WindowService.ShowWindow(new ChangeCity());
        });

        public ICommand Remove => new DelegateCommand(() =>
        {
            //if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            //    _executor.RemoveCity((int)SelectedIdCity);

            //    MessageBox.Show("Успешное удаление");

            //    Find();
            //}
        });

        public ICommand ToFind => new DelegateCommand(() =>
        {

        });
    }
}
