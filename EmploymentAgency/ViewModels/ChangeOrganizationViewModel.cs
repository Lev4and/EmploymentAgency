using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeOrganizationViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private v_organization _organization;

        public static int SelectedIdOrganization;


        public string OrganizationName { get; set; }

        public string IndustryName { get; set; }

        public string NameSubIndustry { get; set; }

        public byte[] Photo { get; set; }

        public DateTime? MinValueClosingDate { get; set; }

        public DateTime? MaxValueClosingDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        public ChangeOrganizationViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _organization = _executor.GetOrganizationExtendedInformation(SelectedIdOrganization);

            IndustryName = _organization.IndustryName;
            NameSubIndustry = _organization.NameSubIndustry;
            OrganizationName = _organization.OrganizationName;

            MinValueClosingDate = new DateTime(1900, 1, 1);
            MaxValueClosingDate = DateTime.Now;

            Photo = _organization.Photo;
            ClosingDate = _organization.ClosingDate;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateOrganization(SelectedIdOrganization, OrganizationName, Photo, ClosingDate))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Организация с такими данными уже существует");
            }
        }, () => (OrganizationName != null ? OrganizationName.Length > 0 : false));
    }
}
