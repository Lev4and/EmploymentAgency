using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Models;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewKnowledgeLanguage.xaml
    /// </summary>
    public partial class BlockViewKnowledgeLanguage : UserControl
    {
        public bool IsCanRemove
        {
            get { return (bool)GetValue(IsCanRemoveProperty); }
            set { SetValue(IsCanRemoveProperty, value); }
        }

        public static readonly DependencyProperty IsCanRemoveProperty =
            DependencyProperty.Register("IsCanRemove", typeof(bool), typeof(BlockViewKnowledgeLanguage), new PropertyMetadata(true));

        public Language Language
        {
            get { return (Language)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        public static readonly DependencyProperty LanguageProperty =
            DependencyProperty.Register("Language", typeof(Language), typeof(BlockViewKnowledgeLanguage), new PropertyMetadata(null));

        public LanguageProficiency LanguageProficiency
        {
            get { return (LanguageProficiency)GetValue(LanguageProficiencyProperty); }
            set { SetValue(LanguageProficiencyProperty, value); }
        }

        public static readonly DependencyProperty LanguageProficiencyProperty =
            DependencyProperty.Register("LanguageProficiency", typeof(LanguageProficiency), typeof(BlockViewKnowledgeLanguage), new PropertyMetadata(null));

        public ICommand Remove
        {
            get { return (ICommand)GetValue(RemoveProperty); }
            set { SetValue(RemoveProperty, value); }
        }

        public static readonly DependencyProperty RemoveProperty =
            DependencyProperty.Register("Remove", typeof(ICommand), typeof(BlockViewKnowledgeLanguage), new PropertyMetadata(null));

        public BlockViewKnowledgeLanguage()
        {
            InitializeComponent();
        }
    }
}
