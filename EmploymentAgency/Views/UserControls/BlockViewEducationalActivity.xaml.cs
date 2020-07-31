using EmploymentAgency.Model.Database.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewEducationalActivity.xaml
    /// </summary>
    public partial class BlockViewEducationalActivity : UserControl
    {
        public bool IsCanRemove
        {
            get { return (bool)GetValue(IsCanRemoveProperty); }
            set { SetValue(IsCanRemoveProperty, value); }
        }

        public static readonly DependencyProperty IsCanRemoveProperty =
            DependencyProperty.Register("IsCanRemove", typeof(bool), typeof(BlockViewEducationalActivity), new PropertyMetadata(true));

        public string NameEducationalnstitution
        {
            get { return (string)GetValue(NameEducationalnstitutionProperty); }
            set { SetValue(NameEducationalnstitutionProperty, value); }
        }

        public static readonly DependencyProperty NameEducationalnstitutionProperty =
            DependencyProperty.Register("NameEducationalnstitution", typeof(string), typeof(BlockViewEducationalActivity), new PropertyMetadata(""));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(BlockViewEducationalActivity), new PropertyMetadata(""));

        public DateTime BeginDate
        {
            get { return (DateTime)GetValue(BeginDateProperty); }
            set { SetValue(BeginDateProperty, value); }
        }

        public static readonly DependencyProperty BeginDateProperty =
            DependencyProperty.Register("BeginDate", typeof(DateTime), typeof(BlockViewEducationalActivity), new PropertyMetadata(DateTime.Now));

        public DateTime? EndDate
        {
            get { return (DateTime?)GetValue(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        public static readonly DependencyProperty EndDateProperty =
            DependencyProperty.Register("EndDate", typeof(DateTime?), typeof(BlockViewEducationalActivity), new PropertyMetadata(null));

        public Education Education
        {
            get { return (Education)GetValue(EducationProperty); }
            set { SetValue(EducationProperty, value); }
        }

        public static readonly DependencyProperty EducationProperty =
            DependencyProperty.Register("Education", typeof(Education), typeof(BlockViewEducationalActivity), new PropertyMetadata(null));

        public ICommand Remove
        {
            get { return (ICommand)GetValue(RemoveProperty); }
            set { SetValue(RemoveProperty, value); }
        }

        public static readonly DependencyProperty RemoveProperty =
            DependencyProperty.Register("Remove", typeof(ICommand), typeof(BlockViewEducationalActivity), new PropertyMetadata(null));

        public BlockViewEducationalActivity()
        {
            InitializeComponent();
        }
    }
}
