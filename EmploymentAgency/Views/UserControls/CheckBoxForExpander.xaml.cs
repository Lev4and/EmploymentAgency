using DevExpress.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CheckBoxForExpander.xaml
    /// </summary>
    public partial class CheckBoxForExpander : UserControl
    {
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(CheckBoxForExpander), new PropertyMetadata(false, IsChecked_Changed));

        private static void IsChecked_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as CheckBoxForExpander;

            if(current != null)
            {
                if(current.CheckedChanged != null)
                {
                    EventArgs eventArgs = new EventArgs();

                    current.CheckedChanged(current, eventArgs);
                }
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CheckBoxForExpander), new PropertyMetadata(""));

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(CheckBoxForExpander), new PropertyMetadata(""));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(CheckBoxForExpander), new PropertyMetadata(""));

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(CheckBoxForExpander), new PropertyMetadata(null));

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(CheckBoxForExpander), new PropertyMetadata(null));

        public event CheckedChangedHandler CheckedChanged;

        public delegate void CheckedChangedHandler(object sender, EventArgs e); 

        public CheckBoxForExpander()
        {
            InitializeComponent();
        }

        public new ICommand Loaded => new DelegateCommand(() =>
        {
            Text = $"{Data.GetType().GetProperty(DisplayMemberPath).GetValue(Data)}";
            SelectedValue = Data.GetType().GetProperty(SelectedValuePath).GetValue(Data);
        });
    }
}
