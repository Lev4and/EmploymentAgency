using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SearchByInputFieldItem.xaml
    /// </summary>
    public partial class SearchByInputFieldItem : UserControl, ISelectable
    {
        public bool IsCanSelect
        {
            get { return (bool)GetValue(IsCanSelectProperty); }
            set { SetValue(IsCanSelectProperty, value); }
        }

        public static readonly DependencyProperty IsCanSelectProperty =
            DependencyProperty.Register("IsCanSelect", typeof(bool), typeof(SearchByInputFieldItem), new PropertyMetadata(true));

        public bool IsSelect
        {
            get { return (bool)GetValue(IsSelectProperty); }
            set { SetValue(IsSelectProperty, value); }
        }

        public static readonly DependencyProperty IsSelectProperty =
            DependencyProperty.Register("IsSelect", typeof(bool), typeof(SearchByInputFieldItem), new PropertyMetadata(false));

        public string ResultSearch
        {
            get { return (string)GetValue(ResultSearchProperty); }
            set { SetValue(ResultSearchProperty, value); }
        }

        public static readonly DependencyProperty ResultSearchProperty =
            DependencyProperty.Register("ResultSearch", typeof(string), typeof(SearchByInputFieldItem), new PropertyMetadata(""));

        public SolidColorBrush MonochromeBrush
        {
            get { return (SolidColorBrush)GetValue(MonochromeBrushProperty); }
            set { SetValue(MonochromeBrushProperty, value); }
        }

        public static readonly DependencyProperty MonochromeBrushProperty =
            DependencyProperty.Register("MonochromeBrush", typeof(SolidColorBrush), typeof(SearchByInputFieldItem), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public DelegateCommand Select { get; set; }

        public DelegateCommand Deselect { get; set; }

        public SearchByInputFieldItem()
        {
            InitializeComponent();

            Select = new DelegateCommand(() =>
            {
                IsSelect = true;

                MonochromeBrush = new SolidColorBrush(Colors.Blue);
            }, () => IsCanSelect);

            Deselect = new DelegateCommand(() =>
            {
                IsSelect = false;

                MonochromeBrush = new SolidColorBrush(Colors.White);
            }, () => IsCanSelect);
        }
    }
}
