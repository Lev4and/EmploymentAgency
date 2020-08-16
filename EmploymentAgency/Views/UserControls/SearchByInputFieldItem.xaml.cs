using DevExpress.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public SolidColorBrush BackgroundBrush
        {
            get { return (SolidColorBrush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(SolidColorBrush), typeof(SearchByInputFieldItem), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public SolidColorBrush ForegroundBrush
        {
            get { return (SolidColorBrush)GetValue(ForegroundBrushProperty); }
            set { SetValue(ForegroundBrushProperty, value); }
        }

        public static readonly DependencyProperty ForegroundBrushProperty =
            DependencyProperty.Register("ForegroundBrush", typeof(SolidColorBrush), typeof(SearchByInputFieldItem), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public DelegateCommand Select { get; set; }

        public DelegateCommand Deselect { get; set; }

        public new ICommand OnMouseDown => new DelegateCommand(() =>
        {
            Click?.Invoke(this, new EventArgs());
        }, () => IsSelect);

        public new ICommand OnMouseMove => new DelegateCommand(() =>
        {
            MouseMove?.Invoke(this, new EventArgs());
        });

        public new ICommand OnMouseLeave => new DelegateCommand(() =>
        {
            MouseLeave?.Invoke(this, new EventArgs());
        });

        public event SearchByInputFieldItemHandler Click;

        public new event SearchByInputFieldItemHandler MouseMove;

        public new event SearchByInputFieldItemHandler MouseLeave;

        public delegate void SearchByInputFieldItemHandler(object sender, EventArgs e);

        public SearchByInputFieldItem()
        {
            InitializeComponent();

            Select = new DelegateCommand(() =>
            {
                IsSelect = true;

                BackgroundBrush = Application.Current.FindResource("SelectedSearchByInputFieldItemBackgroundBrush") as SolidColorBrush;
                ForegroundBrush = Application.Current.FindResource("SelectedSearchByInputFieldItemForegroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);

            Deselect = new DelegateCommand(() =>
            {
                IsSelect = false;

                BackgroundBrush = Application.Current.FindResource("DeselectedSearchByInputFieldItemBackgroundBrush") as SolidColorBrush; ;
                ForegroundBrush = Application.Current.FindResource("DeselectedSearchByInputFieldItemForegroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);
        }
    }
}
