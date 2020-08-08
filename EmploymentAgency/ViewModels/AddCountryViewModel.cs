using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddCountryViewModel : BindableBase
    {
        private byte[] _flag;

        private QueryExecutor _executor;

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

        public AddCountryViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            IsCanAddFlag = true;

            Flag = null;
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddCountry(CountryName, Flag))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Страна с такими данными уже существует");
            }
        }, () => (CountryName != null ? CountryName.Length > 0 : false));

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
