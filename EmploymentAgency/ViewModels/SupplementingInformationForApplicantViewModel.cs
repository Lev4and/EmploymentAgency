using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SupplementingInformationForApplicantViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private ConfigurationUser _config;
        private QueryExecutor _executor;

        public int? SelectedIdGender { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime MinValueDateOfBirth { get; set; }

        public DateTime MaxValueDateOfBirth { get; set; }

        public ObservableCollection<Gender> Genders { get; set; }

        public ObservableCollection<object> SelectedSkills { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public SupplementingInformationForApplicantViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _config = ConfigurationUser.GetConfiguration();
            _executor = new QueryExecutor();

            SelectedIdGender = null;

            Name = "";
            Surname = "";
            Patronymic = "";
            PhoneNumber = "";

            Photo = null;

            MinValueDateOfBirth = new DateTime(1900, 1, 1);
            MaxValueDateOfBirth = DateTime.Now.Date.AddYears(-18);

            DateOfBirth = MaxValueDateOfBirth;

            Genders = CollectionConverter<Gender>.ConvertToObservableCollection(_executor.GetGenders());
            Skills = CollectionConverter<object>.ConvertToObservableCollection(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));
        });

        public ICommand AddInformation => new DelegateCommand(() =>
        {
            if (_executor.AddApplicant(_config.IdUser, Name, Surname, Patronymic, (int)SelectedIdGender, Photo, DateOfBirth, PhoneNumber))
            {
                MessageBox.Show("Успешное добавление информации");
            }
            else
            {
                MessageBox.Show("Информация о данном пользователе уже существует");
            }
        }, () => SelectedIdGender != null &&
                 (Name != null ? Name.Length > 0 : false) &&
                 (Surname != null ? Surname.Length > 0 : false) &&
                 (Patronymic != null ? Patronymic.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false));

        public ICommand AddPhoto => new DelegateCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.jpeg, *.png, *webp)|*.bmp;*.jpg;*.jpeg;*.png;*.webp";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Photo = FileConverter.GetBytesFromFile(openFileDialog.FileName);
            }
        }, () => Photo == null);

        public ICommand RemovePhoto => new DelegateCommand(() =>
        {
            Photo = null;
        }, () => Photo != null);
    }
}
