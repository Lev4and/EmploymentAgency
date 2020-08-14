using Converters;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewImage.xaml
    /// </summary>
    public partial class BlockViewImage : UserControl
    {
        public byte[] Image
        {
            get { return (byte[])GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(byte[]), typeof(BlockViewImage), new PropertyMetadata(null));

        public ICommand AddImage => new DelegateCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.jpeg, *.png, *webp)|*.bmp;*.jpg;*.jpeg;*.png;*.webp";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Image = FileConverter.GetBytesFromFile(openFileDialog.FileName);
            }
        }, () => Image == null);

        public ICommand RemoveImage => new DelegateCommand(() =>
        {
            Image = null;
        }, () => Image != null);

        public BlockViewImage()
        {
            InitializeComponent();
        }
    }
}
