using System.Windows;
using System.Windows.Controls;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Chip.xaml
    /// </summary>
    public partial class Chip : UserControl
    {
        public string PhotoPath
        {
            get { return (string)GetValue(PhotoPathProperty); }
            set { SetValue(PhotoPathProperty, value); }
        }

        public static readonly DependencyProperty PhotoPathProperty =
            DependencyProperty.Register("PhotoPath", typeof(string), typeof(Chip), new PropertyMetadata(""));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Chip), new PropertyMetadata(""));

        public string TextPath
        {
            get { return (string)GetValue(TextPathProperty); }
            set { SetValue(TextPathProperty, value); }
        }

        public static readonly DependencyProperty TextPathProperty =
            DependencyProperty.Register("TextPath", typeof(string), typeof(Chip), new PropertyMetadata(""));

        public byte[] Photo
        {
            get { return (byte[])GetValue(PhotoProperty); }
            set { SetValue(PhotoProperty, value); }
        }

        public static readonly DependencyProperty PhotoProperty =
            DependencyProperty.Register("Photo", typeof(byte[]), typeof(Chip), new PropertyMetadata(null));

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(Chip), new PropertyMetadata(null));

        public Chip()
        {
            InitializeComponent();

            Text = TextPath.Length > 0 ? $"{Data.GetType().GetProperty(TextPath).GetValue(Data)}" : "";
            Photo = PhotoPath.Length > 0 ? (byte[])(Data.GetType().GetProperty(PhotoPath).GetValue(Data)) : null;
        }

        public Chip(string textPath, string photoPath, object data)
        {
            InitializeComponent();

            TextPath = textPath;
            PhotoPath = photoPath;
            Data = data;

            Text = TextPath.Length > 0 ? $"{Data.GetType().GetProperty(TextPath).GetValue(Data)}" : "";
            Photo = PhotoPath.Length > 0 ? (byte[])(Data.GetType().GetProperty(PhotoPath).GetValue(Data)) : null;
        }
    }
}
