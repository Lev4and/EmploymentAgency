using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewLaborActivity.xaml
    /// </summary>
    public partial class BlockViewLaborActivity : UserControl
    {
        public bool IsCanRemove
        {
            get { return (bool)GetValue(IsCanRemoveProperty); }
            set { SetValue(IsCanRemoveProperty, value); }
        }

        public static readonly DependencyProperty IsCanRemoveProperty =
            DependencyProperty.Register("IsCanRemove", typeof(bool), typeof(BlockViewLaborActivity), new PropertyMetadata(true));

        public string OrganizationName
        {
            get { return (string)GetValue(OrganizationNameProperty); }
            set { SetValue(OrganizationNameProperty, value); }
        }

        public static readonly DependencyProperty OrganizationNameProperty =
            DependencyProperty.Register("OrganizationName", typeof(string), typeof(BlockViewLaborActivity), new PropertyMetadata(""));

        public string OrganizationAddress
        {
            get { return (string)GetValue(OrganizationAddressProperty); }
            set { SetValue(OrganizationAddressProperty, value); }
        }

        public static readonly DependencyProperty OrganizationAddressProperty =
            DependencyProperty.Register("OrganizationAddress", typeof(string), typeof(BlockViewLaborActivity), new PropertyMetadata(""));

        public string ProfessionName
        {
            get { return (string)GetValue(ProfessionNameProperty); }
            set { SetValue(ProfessionNameProperty, value); }
        }

        public static readonly DependencyProperty ProfessionNameProperty =
            DependencyProperty.Register("ProfessionName", typeof(string), typeof(BlockViewLaborActivity), new PropertyMetadata(""));

        public string Activity
        {
            get { return (string)GetValue(ActivityProperty); }
            set { SetValue(ActivityProperty, value); }
        }

        public static readonly DependencyProperty ActivityProperty =
            DependencyProperty.Register("Activity", typeof(string), typeof(BlockViewLaborActivity), new PropertyMetadata(""));

        public DateTime StartDate
        {
            get { return (DateTime)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime), typeof(BlockViewLaborActivity), new PropertyMetadata(DateTime.Now));

        public DateTime? EndDate
        {
            get { return (DateTime?)GetValue(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        public static readonly DependencyProperty EndDateProperty =
            DependencyProperty.Register("EndDate", typeof(DateTime?), typeof(BlockViewLaborActivity), new PropertyMetadata(null));

        public ICommand Remove
        {
            get { return (ICommand)GetValue(RemoveProperty); }
            set { SetValue(RemoveProperty, value); }
        }

        public static readonly DependencyProperty RemoveProperty =
            DependencyProperty.Register("Remove", typeof(ICommand), typeof(BlockViewLaborActivity), new PropertyMetadata(null));

        public BlockViewLaborActivity()
        {
            InitializeComponent();
        }
    }
}
