﻿using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class GendersViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdGender { get; set; }

        public string GenderName { get; set; }

        public ObservableCollection<object> Genders { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddGender());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeGenderViewModel.SelectedIdGender = (int)SelectedIdGender;

            WindowService.ShowWindow(new ChangeGender());
        }, () => SelectedIdGender != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveGender((int)SelectedIdGender);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdGender != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdGender = null;

            GenderName = "";

            Find();
        }

        private void Find()
        {
            Genders = new ObservableCollection<object>(CollectionConverter<Gender>.ConvertToObjectList(_executor.GetGenders(GenderName)));
        }
    }
}
