using Converters;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Commands;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewEducationalActivities.xaml
    /// </summary>
    public partial class BlockViewEducationalActivities : UserControl
    {
        private QueryExecutor _executor;

        public bool IsPeriodEnded
        {
            get { return (bool)GetValue(IsPeriodEndedProperty); }
            set { SetValue(IsPeriodEndedProperty, value); }
        }

        public static readonly DependencyProperty IsPeriodEndedProperty =
            DependencyProperty.Register("IsPeriodEnded", typeof(bool), typeof(BlockViewEducationalActivities), new PropertyMetadata(false, IsPeriodEnded_Changed));

        private static void IsPeriodEnded_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewEducationalActivities;

            if(current != null)
            {
                if(current.IsPeriodEnded == false)
                {
                    current.EndDatePeriod = null;
                }
            }
        }

        public int? SelectedIdEducation
        {
            get { return (int?)GetValue(SelectedIdEducationProperty); }
            set { SetValue(SelectedIdEducationProperty, value); }
        }

        public static readonly DependencyProperty SelectedIdEducationProperty =
            DependencyProperty.Register("SelectedIdEducation", typeof(int?), typeof(BlockViewEducationalActivities), new PropertyMetadata(null));

        public string NameEducationalnstitution
        {
            get { return (string)GetValue(NameEducationalnstitutionProperty); }
            set { SetValue(NameEducationalnstitutionProperty, value); }
        }

        public static readonly DependencyProperty NameEducationalnstitutionProperty =
            DependencyProperty.Register("NameEducationalnstitution", typeof(string), typeof(BlockViewEducationalActivities), new PropertyMetadata(""));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(BlockViewEducationalActivities), new PropertyMetadata(""));

        public DateTime MinDatePeriod
        {
            get { return (DateTime)GetValue(MinDatePeriodProperty); }
            set { SetValue(MinDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty MinDatePeriodProperty =
            DependencyProperty.Register("MinDatePeriod", typeof(DateTime), typeof(BlockViewEducationalActivities), new PropertyMetadata(new DateTime(1900, 1, 1)));

        public DateTime BeginDatePeriod
        {
            get { return (DateTime)GetValue(BeginDatePeriodProperty); }
            set { SetValue(BeginDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty BeginDatePeriodProperty =
            DependencyProperty.Register("BeginDatePeriod", typeof(DateTime), typeof(BlockViewEducationalActivities), new PropertyMetadata(DateTime.Now));

        public DateTime? EndDatePeriod
        {
            get { return (DateTime?)GetValue(EndDatePeriodProperty); }
            set { SetValue(EndDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty EndDatePeriodProperty =
            DependencyProperty.Register("EndDatePeriod", typeof(DateTime?), typeof(BlockViewEducationalActivities), new PropertyMetadata(null));

        public DateTime? MaxDatePeriod
        {
            get { return (DateTime?)GetValue(MaxDatePeriodProperty); }
            set { SetValue(MaxDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty MaxDatePeriodProperty =
            DependencyProperty.Register("MaxDatePeriod", typeof(DateTime?), typeof(BlockViewEducationalActivities), new PropertyMetadata(null));

        public ObservableCollection<Education> Educations
        {
            get { return (ObservableCollection<Education>)GetValue(EducationsProperty); }
            set { SetValue(EducationsProperty, value); }
        }

        public static readonly DependencyProperty EducationsProperty =
            DependencyProperty.Register("Educations", typeof(ObservableCollection<Education>), typeof(BlockViewEducationalActivities), new PropertyMetadata(null));

        public ObservableCollection<EducationalActivity> SelectedEducationActivities
        {
            get { return (ObservableCollection<EducationalActivity>)GetValue(SelectedEducationActivitiesProperty); }
            set { SetValue(SelectedEducationActivitiesProperty, value); }
        }

        public static readonly DependencyProperty SelectedEducationActivitiesProperty =
            DependencyProperty.Register("SelectedEducationActivities", typeof(ObservableCollection<EducationalActivity>), typeof(BlockViewEducationalActivities), new PropertyMetadata(null, SelectedEducationActivities_Cahnged));

        private static void SelectedEducationActivities_Cahnged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewEducationalActivities;

            if(current != null)
            {
                if(current.Items != null && current.SelectedEducationActivities != null)
                {
                    if(current.SelectedEducationActivities.Count > current.Items.Count)
                    {
                        current.GenerateItems();
                    }
                }
            }
        }

        public ObservableCollection<BlockViewEducationalActivity> Items
        {
            get { return (ObservableCollection<BlockViewEducationalActivity>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BlockViewEducationalActivity>), typeof(BlockViewEducationalActivities), new PropertyMetadata(null));

        public BlockViewEducationalActivities()
        {
            InitializeComponent();

            _executor = new QueryExecutor();

            Items = new ObservableCollection<BlockViewEducationalActivity>();

            Educations = CollectionConverter<Education>.ConvertToObservableCollection(_executor.GetEducations());
        }

        public new ICommand Loaded => new DelegateCommand(() =>
        {

        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(Items.Any(i => i.BeginDate == BeginDatePeriod && i.EndDate == (IsPeriodEnded == true ? EndDatePeriod : null) && i.NameEducationalnstitution == NameEducationalnstitution && i.Address == Address && i.Education.IdEducation == (int)SelectedIdEducation) == false)
            {
                RenderItem();
            }
            else
            {
                MessageBox.Show("Нельзя добавлять повторяющиеся значения");
            }    
        }, () => (IsPeriodEnded == true ? (EndDatePeriod != null ? BeginDatePeriod <= EndDatePeriod : false) : true) &&
                 SelectedIdEducation != null &&
                 (NameEducationalnstitution != null ? NameEducationalnstitution.Length > 0 : false) &&
                 (Address != null ? Address.Length > 0 : false));

        public ICommand Remove => new Command((obj) =>
        {
            var item = obj as BlockViewEducationalActivity;

            if (Items.Contains(item))
            {
                grid.Children.Remove(item);

                Items.Remove(item);

                MoveElements();

                UpdateListSelectedValues();
            }
        });

        private void GenerateItems()
        {
            SelectedEducationActivities.OrderByDescending(i => i.StartDate);

            for(int i = 0; i < SelectedEducationActivities.Count; i++)
            {
                var item = new BlockViewEducationalActivity();
                item.Education = SelectedEducationActivities[i].Education;
                item.BeginDate = SelectedEducationActivities[i].StartDate;
                item.EndDate = SelectedEducationActivities[i].EndDate;
                item.NameEducationalnstitution = SelectedEducationActivities[i].NameEducationalnstitution;
                item.Address = SelectedEducationActivities[i].Address;
                item.VerticalAlignment = VerticalAlignment.Top;
                item.MinWidth = grid.ActualWidth - 20;
                item.MaxWidth = grid.ActualWidth - 20;
                item.Width = grid.ActualWidth - 20;
                item.Margin = new Thickness(10, 10 + (110 * i), 10, 0);
                item.Remove = Remove;
                grid.Children.Add(item);

                Items.Add(item);
            }
        }

        private void RenderItem()
        {
            var item = new BlockViewEducationalActivity();
            item.Education = Educations.Single(e => e.IdEducation == (int)SelectedIdEducation);
            item.BeginDate = BeginDatePeriod;
            item.EndDate = IsPeriodEnded ? EndDatePeriod : null;
            item.NameEducationalnstitution = NameEducationalnstitution;
            item.Address = Address;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.MinWidth = grid.ActualWidth - 20;
            item.MaxWidth = grid.ActualWidth - 20;
            item.Width = grid.ActualWidth - 20;
            item.Margin = new Thickness(10, 10 + (110 * Items.Count), 10, 0);
            item.Remove = Remove;
            grid.Children.Add(item);

            Items.Add(item);

            UpdateListSelectedValues();
            MoveElements();
        }

        private void MoveElements()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Margin = new Thickness(10, 10 + (110 * i), 10, 0);
            }
        }

        private void UpdateListSelectedValues()
        {
            SelectedEducationActivities = new ObservableCollection<EducationalActivity>();

            Items = Items.OrderByDescending(i => i.BeginDate).ToObservableCollection();
            Items.ForEach(i =>
            {
                SelectedEducationActivities.Add(new EducationalActivity
                {
                    IdEducation = i.Education.IdEducation,
                    NameEducationalnstitution = i.NameEducationalnstitution,
                    Address = i.Address,
                    StartDate = i.BeginDate,
                    EndDate = i.EndDate
                });
            });
        }
    }
}
