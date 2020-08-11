using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeSkillViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Skill _skill;

        public static int SelectedIdSkill { get; set; }


        public string SkillName { get; set; }

        public byte[] Photo { get; set; }

        public ChangeSkillViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _skill = _executor.GetSkill(SelectedIdSkill);

            SkillName = _skill.SkillName;

            Photo = _skill.Photo;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateSkill(SelectedIdSkill, SkillName, Photo))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Навык с такими данными уже существует");
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
