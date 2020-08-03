using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Commands;
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
    /// Логика взаимодействия для BlockViewLaborActivities.xaml
    /// </summary>
    public partial class BlockViewLaborActivities : UserControl
    {
        public bool IsPeriodEnded
        {
            get { return (bool)GetValue(IsPeriodEndedProperty); }
            set { SetValue(IsPeriodEndedProperty, value); }
        }

        public static readonly DependencyProperty IsPeriodEndedProperty =
            DependencyProperty.Register("IsPeriodEnded", typeof(bool), typeof(BlockViewLaborActivities), new PropertyMetadata(false, IsPeriodEnded_Changed));

        private static void IsPeriodEnded_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewLaborActivities;

            if(current != null)
            {
                if(current.IsPeriodEnded == false)
                {
                    current.EndDatePeriod = null;
                }
            }
        }

        public string OrganizationName
        {
            get { return (string)GetValue(OrganizationNameProperty); }
            set { SetValue(OrganizationNameProperty, value); }
        }

        public static readonly DependencyProperty OrganizationNameProperty =
            DependencyProperty.Register("OrganizationName", typeof(string), typeof(BlockViewLaborActivities), new PropertyMetadata(""));

        public string OrganizationAddress
        {
            get { return (string)GetValue(OrganizationAddressProperty); }
            set { SetValue(OrganizationAddressProperty, value); }
        }

        public static readonly DependencyProperty OrganizationAddressProperty =
            DependencyProperty.Register("OrganizationAddress", typeof(string), typeof(BlockViewLaborActivities), new PropertyMetadata(""));

        public string ProfessionName
        {
            get { return (string)GetValue(ProfessionNameProperty); }
            set { SetValue(ProfessionNameProperty, value); }
        }

        public static readonly DependencyProperty ProfessionNameProperty =
            DependencyProperty.Register("ProfessionName", typeof(string), typeof(BlockViewLaborActivities), new PropertyMetadata(""));

        public string Activity
        {
            get { return (string)GetValue(ActivityProperty); }
            set { SetValue(ActivityProperty, value); }
        }

        public static readonly DependencyProperty ActivityProperty =
            DependencyProperty.Register("Activity", typeof(string), typeof(BlockViewLaborActivities), new PropertyMetadata(""));

        public DateTime MinDatePeriod
        {
            get { return (DateTime)GetValue(MinDatePeriodProperty); }
            set { SetValue(MinDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty MinDatePeriodProperty =
            DependencyProperty.Register("MinDatePeriod", typeof(DateTime), typeof(BlockViewLaborActivities), new PropertyMetadata(new DateTime(1900, 1, 1)));

        public DateTime BeginDatePeriod
        {
            get { return (DateTime)GetValue(BeginDatePeriodProperty); }
            set { SetValue(BeginDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty BeginDatePeriodProperty =
            DependencyProperty.Register("BeginDatePeriod", typeof(DateTime), typeof(BlockViewLaborActivities), new PropertyMetadata(DateTime.Now));

        public DateTime? EndDatePeriod
        {
            get { return (DateTime?)GetValue(EndDatePeriodProperty); }
            set { SetValue(EndDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty EndDatePeriodProperty =
            DependencyProperty.Register("EndDatePeriod", typeof(DateTime?), typeof(BlockViewLaborActivities), new PropertyMetadata(null));

        public DateTime? MaxDatePeriod
        {
            get { return (DateTime?)GetValue(MaxDatePeriodProperty); }
            set { SetValue(MaxDatePeriodProperty, value); }
        }

        public static readonly DependencyProperty MaxDatePeriodProperty =
            DependencyProperty.Register("MaxDatePeriod", typeof(DateTime?), typeof(BlockViewLaborActivities), new PropertyMetadata(null));

        public ObservableCollection<LaborActivity> SelectedLaborActivities
        {
            get { return (ObservableCollection<LaborActivity>)GetValue(SelectedLaborActivitiesProperty); }
            set { SetValue(SelectedLaborActivitiesProperty, value); }
        }

        public static readonly DependencyProperty SelectedLaborActivitiesProperty =
            DependencyProperty.Register("SelectedLaborActivities", typeof(ObservableCollection<LaborActivity>), typeof(BlockViewLaborActivities), new PropertyMetadata(null, SelectedLaborActivities_Changed));

        private static void SelectedLaborActivities_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewLaborActivities;

            if(current != null)
            {
                if(current.Items != null && current.SelectedLaborActivities != null)
                {
                    if(current.SelectedLaborActivities.Count > current.Items.Count)
                    {
                        current.GenerateItems();
                    }
                }
            }
        }

        public ObservableCollection<BlockViewLaborActivity> Items
        {
            get { return (ObservableCollection<BlockViewLaborActivity>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BlockViewLaborActivity>), typeof(BlockViewLaborActivities), new PropertyMetadata(null));

        public BlockViewLaborActivities()
        {
            InitializeComponent();

            Items = new ObservableCollection<BlockViewLaborActivity>();
        }

        public new ICommand Loaded => new DelegateCommand(() =>
        {

        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (Items.Any(i => i.StartDate == BeginDatePeriod && i.EndDate == (IsPeriodEnded == true ? EndDatePeriod : null) && i.OrganizationName == OrganizationName && i.OrganizationAddress == OrganizationAddress && i.Activity == Activity) == false)
            {
                RenderItem();
            }
            else
            {
                MessageBox.Show("Нельзя добавлять повторяющиеся значения");
            }
        }, () => (IsPeriodEnded == true ? (EndDatePeriod != null ? BeginDatePeriod <= EndDatePeriod : false) : true) &&
                 (OrganizationName != null ? OrganizationName.Length > 0 : false) &&
                 (OrganizationAddress != null ? OrganizationAddress.Length > 0 : false) &&
                 (ProfessionName != null ? ProfessionName.Length > 0 : false) &&
                 (Activity != null ? Activity.Length > 0 : false));

        public ICommand Remove => new Command((obj) =>
        {
            var item = obj as BlockViewLaborActivity;

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
            SelectedLaborActivities.OrderByDescending(i => i.StartDate);

            for(int i = 0; i < SelectedLaborActivities.Count; i++)
            {
                var item = new BlockViewLaborActivity();
                item.StartDate = SelectedLaborActivities[i].StartDate;
                item.EndDate = SelectedLaborActivities[i].EndDate;
                item.OrganizationName = SelectedLaborActivities[i].OrganizationName;
                item.OrganizationAddress = SelectedLaborActivities[i].OrganizationAddress;
                item.ProfessionName = SelectedLaborActivities[i].ProfessionName;
                item.Activity = SelectedLaborActivities[i].Activity;
                item.VerticalAlignment = VerticalAlignment.Top;
                item.MinWidth = grid.ActualWidth - 20;
                item.MaxWidth = grid.ActualWidth - 20;
                item.Width = grid.ActualWidth - 20;
                item.Margin = new Thickness(10, 10 + (210 * i), 10, 0);
                item.Remove = Remove;
                grid.Children.Add(item);

                Items.Add(item);
            }
        }

        private void RenderItem()
        {
            var item = new BlockViewLaborActivity();
            item.StartDate = BeginDatePeriod;
            item.EndDate = IsPeriodEnded ? EndDatePeriod : null;
            item.OrganizationName = OrganizationName;
            item.OrganizationAddress = OrganizationAddress;
            item.ProfessionName = ProfessionName;
            item.Activity = Activity;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.MinWidth = grid.ActualWidth - 20;
            item.MaxWidth = grid.ActualWidth - 20;
            item.Width = grid.ActualWidth - 20;
            item.Margin = new Thickness(10, 10 + (210 * Items.Count), 10, 0);
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
                Items[i].Margin = new Thickness(10, 10 + (210 * i), 10, 0);
            }
        }

        private void UpdateListSelectedValues()
        {
            SelectedLaborActivities = new ObservableCollection<LaborActivity>();

            Items = Items.OrderByDescending(i => i.StartDate).ToObservableCollection();
            Items.ForEach(i =>
            {
                SelectedLaborActivities.Add(new LaborActivity
                {
                    OrganizationName = OrganizationName,
                    OrganizationAddress = OrganizationAddress,
                    ProfessionName = ProfessionName,
                    Activity = Activity,
                    StartDate = BeginDatePeriod,
                    EndDate = EndDatePeriod
                });
            });
        }
    }
}
