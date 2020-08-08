using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeCountryViewModel : BindableBase
    {
        private byte[] _flag;

        private QueryExecutor _executor;
        private Country _country;

        public static int SelectedIdCountry { get; set; }


        public bool IsCanAddFlag { get; set; }

        public string CountryName { get; set; }

        public byte[] Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;

                IsCanAddFlag = _flag != null ? false : true;
            }
        }

        public ChangeCountryViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _country = _executor.GetCountry(SelectedIdCountry);

            IsCanAddFlag = true;

            CountryName = _country.CountryName;

            Flag = _country.Flag;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            _executor.UpdateCountry(SelectedIdCountry, Flag);

            MessageBox.Show("Успешное изменение");
        });

        public ICommand AddPhoto => new DelegateCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.jpeg, *.png, *webp)|*.bmp;*.jpg;*.jpeg;*.png;*.webp";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Flag = FileConverter.GetBytesFromFile(openFileDialog.FileName);
            }
        }, () => IsCanAddFlag == true);

        public ICommand RemovePhoto => new DelegateCommand(() =>
        {
            Flag = null;
        }, () => IsCanAddFlag == false);
    }
}
