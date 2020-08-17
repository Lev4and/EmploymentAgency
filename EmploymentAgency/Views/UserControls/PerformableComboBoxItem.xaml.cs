using DevExpress.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PerformableComboBoxItem.xaml
    /// </summary>
    public partial class PerformableComboBoxItem : UserControl, ISelectable
    {
        public bool IsCanSelect
        {
            get { return (bool)GetValue(IsCanSelectProperty); }
            set { SetValue(IsCanSelectProperty, value); }
        }

        public static readonly DependencyProperty IsCanSelectProperty =
            DependencyProperty.Register("IsCanSelect", typeof(bool), typeof(PerformableComboBoxItem), new PropertyMetadata(true));

        public bool IsSelect
        {
            get { return (bool)GetValue(IsSelectProperty); }
            set { SetValue(IsSelectProperty, value); }
        }

        public static readonly DependencyProperty IsSelectProperty =
            DependencyProperty.Register("IsSelect", typeof(bool), typeof(PerformableComboBoxItem), new PropertyMetadata(false));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(PerformableComboBoxItem), new PropertyMetadata(""));

        public string ResultSearch
        {
            get { return (string)GetValue(ResultSearchProperty); }
            set { SetValue(ResultSearchProperty, value); }
        }

        public static readonly DependencyProperty ResultSearchProperty =
            DependencyProperty.Register("ResultSearch", typeof(string), typeof(PerformableComboBoxItem), new PropertyMetadata(""));

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(PerformableComboBoxItem), new PropertyMetadata(null));

        public SolidColorBrush BackgroundBrush
        {
            get { return (SolidColorBrush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(SolidColorBrush), typeof(PerformableComboBoxItem), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public SolidColorBrush ForegroundBrush
        {
            get { return (SolidColorBrush)GetValue(ForegroundBrushProperty); }
            set { SetValue(ForegroundBrushProperty, value); }
        }

        public static readonly DependencyProperty ForegroundBrushProperty =
            DependencyProperty.Register("ForegroundBrush", typeof(SolidColorBrush), typeof(PerformableComboBoxItem), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

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

        public event PerformableComboBoxItemHandler Click;

        public new event PerformableComboBoxItemHandler MouseMove;

        public new event PerformableComboBoxItemHandler MouseLeave;

        public delegate void PerformableComboBoxItemHandler(object sender, EventArgs e);

        public PerformableComboBoxItem(object data, string displayMemberPath)
        {
            InitializeComponent();

            Data = data;
            DisplayMemberPath = displayMemberPath;

            Select = new DelegateCommand(() =>
            {
                IsSelect = true;

                BackgroundBrush = Application.Current.FindResource("SelectedPerformableComboBoxItemBackgroundBrush") as SolidColorBrush;
                ForegroundBrush = Application.Current.FindResource("SelectedPerformableComboBoxItemForegroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);

            Deselect = new DelegateCommand(() =>
            {
                IsSelect = false;

                BackgroundBrush = Application.Current.FindResource("DeselectedPerformableComboBoxItemBackgroundBrush") as SolidColorBrush; ;
                ForegroundBrush = Application.Current.FindResource("DeselectedPerformableComboBoxItemForegroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);
        }

        public new ICommand Loaded => new DelegateCommand(() =>
        {
            ResultSearch = $"{Data.GetType().GetProperty(DisplayMemberPath).GetValue(Data)}";
        });
    }
}
