using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddSkillViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string SkillName { get; set; }
        
        public byte[] Photo { get; set; }

        public AddSkillViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            SkillName = "";

            Photo = null;
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(_executor.AddSkill(SkillName, Photo))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Навык с таким данными уже существует");
            }
        }, () => (SkillName != null ? SkillName.Length > 0 : false));

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
