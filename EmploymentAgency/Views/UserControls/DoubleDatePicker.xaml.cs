using System;
using System.Windows;
using System.Windows.Controls;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DoubleDatePicker.xaml
    /// </summary>
    public partial class DoubleDatePicker : UserControl
    {
        public DateTime? MinSelectedDate
        {
            get { return (DateTime?)GetValue(MinSelectedDateProperty); }
            set { SetValue(MinSelectedDateProperty, value); }
        }

        public static readonly DependencyProperty MinSelectedDateProperty =
            DependencyProperty.Register("MinSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MinValue));


        public DateTime? BeginSelectedDate
        {
            get { return (DateTime?)GetValue(BeginSelectedDateProperty); }
            set { SetValue(BeginSelectedDateProperty, value); }
        }

        public static readonly DependencyProperty BeginSelectedDateProperty =
            DependencyProperty.Register("BeginSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MinValue));

        public DateTime? EndSelectedDate
        {
            get { return (DateTime?)GetValue(EndSelectedDateProperty); }
            set { SetValue(EndSelectedDateProperty, value); }
        }

        public static readonly DependencyProperty EndSelectedDateProperty =
            DependencyProperty.Register("EndSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MaxValue));

        public DateTime? MaxSelectedDate
        {
            get { return (DateTime?)GetValue(MaxSelectedDateProperty); }
            set { SetValue(MaxSelectedDateProperty, value); }
        }

        public static readonly DependencyProperty MaxSelectedDateProperty =
            DependencyProperty.Register("MaxSelectedDate", typeof(DateTime?), typeof(DoubleDatePicker), new PropertyMetadata((DateTime?)DateTime.MaxValue));

        public DoubleDatePicker()
        {
            InitializeComponent();
        }
    }
}
