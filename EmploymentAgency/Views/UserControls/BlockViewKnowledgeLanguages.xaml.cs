using Converters;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Commands;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewKnowledgeLanguages.xaml
    /// </summary>
    public partial class BlockViewKnowledgeLanguages : UserControl
    {
        private QueryExecutor _executor;

        public int? SelectedIdLanguage
        {
            get { return (int?)GetValue(SelectedIdLanguageProperty); }
            set { SetValue(SelectedIdLanguageProperty, value); }
        }

        public static readonly DependencyProperty SelectedIdLanguageProperty =
            DependencyProperty.Register("SelectedIdLanguage", typeof(int?), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public int? SelectedIdLanguageProficiency
        {
            get { return (int?)GetValue(SelectedIdLanguageProficiencyProperty); }
            set { SetValue(SelectedIdLanguageProficiencyProperty, value); }
        }

        public static readonly DependencyProperty SelectedIdLanguageProficiencyProperty =
            DependencyProperty.Register("SelectedIdLanguageProficiency", typeof(int?), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public ObservableCollection<Language> Languages
        {
            get { return (ObservableCollection<Language>)GetValue(LanguagesProperty); }
            set { SetValue(LanguagesProperty, value); }
        }

        public static readonly DependencyProperty LanguagesProperty =
            DependencyProperty.Register("Languages", typeof(ObservableCollection<Language>), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public ObservableCollection<Language> SelectedLanguages
        {
            get { return (ObservableCollection<Language>)GetValue(SelectedLanguagesProperty); }
            set { SetValue(SelectedLanguagesProperty, value); }
        }

        public static readonly DependencyProperty SelectedLanguagesProperty =
            DependencyProperty.Register("SelectedLanguages", typeof(ObservableCollection<Language>), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public ObservableCollection<LanguageProficiency> SelectedLanguageProficiencies
        {
            get { return (ObservableCollection<LanguageProficiency>)GetValue(SelectedLanguageProficienciesProperty); }
            set { SetValue(SelectedLanguageProficienciesProperty, value); }
        }

        public static readonly DependencyProperty SelectedLanguageProficienciesProperty =
            DependencyProperty.Register("SelectedLanguageProficiencies", typeof(ObservableCollection<LanguageProficiency>), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public ObservableCollection<LanguageProficiency> LanguageProficiencies
        {
            get { return (ObservableCollection<LanguageProficiency>)GetValue(LanguageProficienciesProperty); }
            set { SetValue(LanguageProficienciesProperty, value); }
        }

        public static readonly DependencyProperty LanguageProficienciesProperty =
            DependencyProperty.Register("LanguageProficiencies", typeof(ObservableCollection<LanguageProficiency>), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public ObservableCollection<BlockViewKnowledgeLanguage> Items
        {
            get { return (ObservableCollection<BlockViewKnowledgeLanguage>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BlockViewKnowledgeLanguage>), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null));

        public BlockViewKnowledgeLanguages()
        {
            InitializeComponent();
        }

        public new ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Items = new ObservableCollection<BlockViewKnowledgeLanguage>();

            Languages = CollectionConverter<Language>.ConvertToObservableCollection(_executor.GetLanguages());
            LanguageProficiencies = CollectionConverter<LanguageProficiency>.ConvertToObservableCollection(_executor.GetLanguageProficiencies());
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(Items.Any(i => i.Language.IdLanguage == (int)SelectedIdLanguage) == false)
            {
                RenderItem();
            }
            else
            {
                MessageBox.Show("Нельзя добавлять повторяющиеся значения");
            }
        }, () => SelectedIdLanguage != null && SelectedIdLanguageProficiency != null);

        public ICommand Remove => new Command((obj) =>
        {
            var item = obj as BlockViewKnowledgeLanguage;

            if (Items.Contains(item))
            {
                grid.Children.Remove(item);

                Items.Remove(item);

                MoveElements();

                UpdateListSelectedValues();
            }
        });

        private void RenderItem()
        {
            var item = new BlockViewKnowledgeLanguage();
            item.Language = Languages.Single(l => l.IdLanguage == (int)SelectedIdLanguage);
            item.LanguageProficiency = LanguageProficiencies.Single(l => l.IdLanguageProficiency == (int)SelectedIdLanguageProficiency);
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Margin = new Thickness(10, 10 + (60 * Items.Count), 10, 0);
            item.Remove = Remove;
            grid.Children.Add(item);

            Items.Add(item);

            UpdateListSelectedValues();
        }

        private void MoveElements()
        {
            for(int i = 0; i < Items.Count; i++)
            {
                Items[i].Margin = new Thickness(10, 10 + (60 * i), 10, 0);
            }
        }

        private void UpdateListSelectedValues()
        {
            SelectedLanguages = new ObservableCollection<Language>();
            SelectedLanguageProficiencies = new ObservableCollection<LanguageProficiency>();

            Items.ForEach(i =>
            {
                SelectedLanguages.Add(i.Language);
                SelectedLanguageProficiencies.Add(i.LanguageProficiency);
            });
        }
    }
}
