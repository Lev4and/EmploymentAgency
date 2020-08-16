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

        public new bool IsLoaded
        {
            get { return (bool)GetValue(IsLoadedProperty); }
            set { SetValue(IsLoadedProperty, value); }
        }

        public static readonly DependencyProperty IsLoadedProperty =
            DependencyProperty.Register("IsLoaded", typeof(bool), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(false));

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

        public ObservableCollection<KnowledgeLanguage> SelectedKnowledgeLanguages
        {
            get { return (ObservableCollection<KnowledgeLanguage>)GetValue(SelectedKnowledgeLanguagesProperty); }
            set { SetValue(SelectedKnowledgeLanguagesProperty, value); }
        }

        public static readonly DependencyProperty SelectedKnowledgeLanguagesProperty =
            DependencyProperty.Register("SelectedKnowledgeLanguages", typeof(ObservableCollection<KnowledgeLanguage>), typeof(BlockViewKnowledgeLanguages), new PropertyMetadata(null, SelectedKnowledgeLanguages_Changed));

        private static void SelectedKnowledgeLanguages_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewKnowledgeLanguages;

            if(current != null)
            {
                if(current.Items != null && current.SelectedKnowledgeLanguages != null)
                {
                    if(current.SelectedKnowledgeLanguages.Count > current.Items.Count)
                    {
                        if (current.IsLoaded == false)
                            current.Loading();

                        current.GenerateItems();
                    }
                }
            }
        }

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

            Items = new ObservableCollection<BlockViewKnowledgeLanguage>();
        }

        public new ICommand Loaded => new DelegateCommand(() =>
        {
            Loading();

            IsLoaded = true;
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
                Grid.Children.Remove(item);

                Items.Remove(item);

                MoveElements();
                UpdateListSelectedValues();
            }
        });

        private void Loading()
        {
            _executor = new QueryExecutor();

            Languages = new ObservableCollection<Language>(_executor.GetLanguages());
            LanguageProficiencies =  new ObservableCollection<LanguageProficiency>(_executor.GetLanguageProficiencies());
        }

        private void GenerateItems()
        {
            for(int i = 0; i < SelectedKnowledgeLanguages.Count; i++)
            {
                var item = new BlockViewKnowledgeLanguage();
                item.Language = Languages.Single(l => l.IdLanguage == (int)SelectedKnowledgeLanguages[i].IdLanguage);
                item.LanguageProficiency = LanguageProficiencies.Single(l => l.IdLanguageProficiency == (int)SelectedKnowledgeLanguages[i].IdLanguageProficiency);
                item.VerticalAlignment = VerticalAlignment.Top;
                item.MinWidth = Grid.ActualWidth - 20;
                item.MaxWidth = Grid.ActualWidth - 20;
                item.Width = Grid.ActualWidth - 20;
                item.Margin = new Thickness(10, 10 + (60 * i), 10, 0);
                item.Remove = Remove;
                Grid.Children.Add(item);

                Items.Add(item);
            }
        }

        private void RenderItem()
        {
            var item = new BlockViewKnowledgeLanguage();
            item.Language = Languages.Single(l => l.IdLanguage == (int)SelectedIdLanguage);
            item.LanguageProficiency = LanguageProficiencies.Single(l => l.IdLanguageProficiency == (int)SelectedIdLanguageProficiency);
            item.VerticalAlignment = VerticalAlignment.Top;
            item.MinWidth = Grid.ActualWidth - 20;
            item.MaxWidth = Grid.ActualWidth - 20;
            item.Width = Grid.ActualWidth - 20;
            item.Margin = new Thickness(10, 10 + (60 * Items.Count), 10, 0);
            item.Remove = Remove;
            Grid.Children.Add(item);

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
            SelectedKnowledgeLanguages = new ObservableCollection<KnowledgeLanguage>();

            Items.ForEach(i =>
            {
                SelectedKnowledgeLanguages.Add(new KnowledgeLanguage
                {
                    IdLanguage = i.Language.IdLanguage,
                    IdLanguageProficiency = i.LanguageProficiency.IdLanguageProficiency
                });
            });
        }
    }
}
