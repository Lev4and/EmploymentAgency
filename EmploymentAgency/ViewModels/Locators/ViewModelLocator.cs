using EmploymentAgency.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmploymentAgency.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        
        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();

        public AuthorizationViewModel AuthorizationViewModel => _provider.GetRequiredService<AuthorizationViewModel>();

        public SettingsViewModel SettingsViewModel => _provider.GetRequiredService<SettingsViewModel>();

        public SupplementingInformationForManagerViewModel SupplementingInformationForManagerViewModel => _provider.GetRequiredService<SupplementingInformationForManagerViewModel>();

        public AddOrganizationViewModel AddOrganizationViewModel => _provider.GetRequiredService<AddOrganizationViewModel>();

        public SupplementingInformationForEmployerViewModel SupplementingInformationForEmployerViewModel => _provider.GetRequiredService<SupplementingInformationForEmployerViewModel>();

        public AddSkillViewModel AddSkillViewModel => _provider.GetRequiredService<AddSkillViewModel>();

        public SupplementingInformationForApplicantViewModel SupplementingInformationForApplicantViewModel => _provider.GetRequiredService<SupplementingInformationForApplicantViewModel>();

        public RegistrationViewModel RegistrationViewModel => _provider.GetRequiredService<RegistrationViewModel>();

        public MenuViewModel MenuViewModel => _provider.GetRequiredService<MenuViewModel>();

        public OrganizationsViewModel OrganizationsViewModel => _provider.GetRequiredService<OrganizationsViewModel>();

        public BranchesViewModel BranchesViewModel => _provider.GetRequiredService<BranchesViewModel>();

        public CountriesViewModel CountriesViewModel => _provider.GetRequiredService<CountriesViewModel>();

        public CitiesViewModel CitiesViewModel => _provider.GetRequiredService<CitiesViewModel>();

        public StreetsViewModel StreetsViewModel => _provider.GetRequiredService<StreetsViewModel>();

        public SkillsViewModel SkillsViewModel => _provider.GetRequiredService<SkillsViewModel>();

        public DrivingLicenseCategoriesViewModel DrivingLicenseCategoriesViewModel => _provider.GetRequiredService<DrivingLicenseCategoriesViewModel>();

        public EducationsViewModel EducationsViewModel => _provider.GetRequiredService<EducationsViewModel>();

        public EmploymentTypesViewModel EmploymentTypesViewModel => _provider.GetRequiredService<EmploymentTypesViewModel>();

        public ExperiencesViewModel ExperiencesViewModel => _provider.GetRequiredService<ExperiencesViewModel>();

        public LanguagesViewModel LanguagesViewModel => _provider.GetRequiredService<LanguagesViewModel>();

        public ProfessionCategoriesViewModel ProfessionCategoriesViewModel => _provider.GetRequiredService<ProfessionCategoriesViewModel>();

        public LanguageProficienciesViewModel LanguageProficienciesViewModel => _provider.GetRequiredService<LanguageProficienciesViewModel>();

        public GendersViewModel GendersViewModel => _provider.GetRequiredService<GendersViewModel>();

        public IndustriesViewModel IndustriesViewModel => _provider.GetRequiredService<IndustriesViewModel>();

        public SubIndustriesViewModel SubIndustriesViewModel => _provider.GetRequiredService<SubIndustriesViewModel>();

        public ProfessionsViewModel ProfessionsViewModel => _provider.GetRequiredService<ProfessionsViewModel>();

        public RequestStatusesViewModel RequestStatusesViewModel => _provider.GetRequiredService<RequestStatusesViewModel>();

        public RolesViewModel RolesViewModel => _provider.GetRequiredService<RolesViewModel>();

        public SchedulesViewModel SchedulesViewModel => _provider.GetRequiredService<SchedulesViewModel>();

        public UsersViewModel UsersViewModel => _provider.GetRequiredService<UsersViewModel>();

        public AddBranchViewModel AddBranchViewModel => _provider.GetRequiredService<AddBranchViewModel>();

        public ChangeBranchViewModel ChangeBranchViewModel => _provider.GetRequiredService<ChangeBranchViewModel>();

        public AddCityViewModel AddCityViewModel => _provider.GetRequiredService<AddCityViewModel>();

        public AddCountryViewModel AddCountryViewModel => _provider.GetRequiredService<AddCountryViewModel>();

        public ChangeCountryViewModel ChangeCountryViewModel => _provider.GetRequiredService<ChangeCountryViewModel>();

        public ChangeCityViewModel ChangeCityViewModel => _provider.GetRequiredService<ChangeCityViewModel>();

        public AddDrivingLicenseCategoryViewModel AddDrivingLicenseCategoryViewModel => _provider.GetRequiredService<AddDrivingLicenseCategoryViewModel>();

        public ChangeDrivingLicenseCategoryViewModel ChangeDrivingLicenseCategoryViewModel => _provider.GetRequiredService<ChangeDrivingLicenseCategoryViewModel>();

        public AddEducationViewModel AddEducationViewModel => _provider.GetRequiredService<AddEducationViewModel>();

        public ChangeEducationViewModel ChangeEducationViewModel => _provider.GetRequiredService<ChangeEducationViewModel>();

        public AddEmploymentTypeViewModel AddEmploymentTypeViewModel => _provider.GetRequiredService<AddEmploymentTypeViewModel>();

        public ChangeEmploymentTypeViewModel ChangeEmploymentTypeViewModel => _provider.GetRequiredService<ChangeEmploymentTypeViewModel>();

        public AddExperienceViewModel AddExperienceViewModel => _provider.GetRequiredService<AddExperienceViewModel>();

        public ChangeExperienceViewModel ChangeExperienceViewModel => _provider.GetRequiredService<ChangeExperienceViewModel>();

        public AddGenderViewModel AddGenderViewModel => _provider.GetRequiredService<AddGenderViewModel>();

        public ChangeGenderViewModel ChangeGenderViewModel => _provider.GetRequiredService<ChangeGenderViewModel>();

        public AddIndustryViewModel AddIndustryViewModel => _provider.GetRequiredService<AddIndustryViewModel>();

        public ChangeIndustryViewModel ChangeIndustryViewModel => _provider.GetRequiredService<ChangeIndustryViewModel>();

        public AddLanguageViewModel AddLanguageViewModel => _provider.GetRequiredService<AddLanguageViewModel>();

        public ChangeLanguageViewModel ChangeLanguageViewModel => _provider.GetRequiredService<ChangeLanguageViewModel>();

        public AddLanguageProficiencyViewModel AddLanguageProficiencyViewModel => _provider.GetRequiredService<AddLanguageProficiencyViewModel>();

        public ChangeLanguageProficiencyViewModel ChangeLanguageProficiencyViewModel => _provider.GetRequiredService<ChangeLanguageProficiencyViewModel>();

        public ChangeOrganizationViewModel ChangeOrganizationViewModel => _provider.GetRequiredService<ChangeOrganizationViewModel>();

        public AddProfessionViewModel AddProfessionViewModel => _provider.GetRequiredService<AddProfessionViewModel>();

        public ChangeProfessionViewModel ChangeProfessionViewModel => _provider.GetRequiredService<ChangeProfessionViewModel>();

        public AddProfessionCategoryViewModel AddProfessionCategoryViewModel => _provider.GetRequiredService<AddProfessionCategoryViewModel>();

        public ChangeProfessionCategoryViewModel ChangeProfessionCategoryViewModel => _provider.GetRequiredService<ChangeProfessionCategoryViewModel>();

        public AddRequestStatusViewModel AddRequestStatusViewModel => _provider.GetRequiredService<AddRequestStatusViewModel>();

        public ChangeRequestStatusViewModel ChangeRequestStatusViewModel => _provider.GetRequiredService<ChangeRequestStatusViewModel>();

        public AddRoleViewModel AddRoleViewModel => _provider.GetRequiredService<AddRoleViewModel>();

        public ChangeRoleViewModel ChangeRoleViewModel => _provider.GetRequiredService<ChangeRoleViewModel>();

        public AddScheduleViewModel AddScheduleViewModel => _provider.GetRequiredService<AddScheduleViewModel>();

        public ChangeScheduleViewModel ChangeScheduleViewModel => _provider.GetRequiredService<ChangeScheduleViewModel>();

        public ChangeSkillViewModel ChangeSkillViewModel => _provider.GetRequiredService<ChangeSkillViewModel>();

        public AddStreetViewModel AddStreetViewModel => _provider.GetRequiredService<AddStreetViewModel>();

        public ChangeStreetViewModel ChangeStreetViewModel => _provider.GetRequiredService<ChangeStreetViewModel>();

        public AddSubIndustryViewModel AddSubIndustryViewModel => _provider.GetRequiredService<AddSubIndustryViewModel>();

        public ChangeSubIndustryViewModel ChangeSubIndustryViewModel => _provider.GetRequiredService<ChangeSubIndustryViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SupplementingInformationForManagerViewModel>();
            services.AddTransient<AddOrganizationViewModel>();
            services.AddTransient<SupplementingInformationForEmployerViewModel>();
            services.AddTransient<AddSkillViewModel>();
            services.AddTransient<SupplementingInformationForApplicantViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<OrganizationsViewModel>();
            services.AddTransient<BranchesViewModel>();
            services.AddTransient<CountriesViewModel>();
            services.AddTransient<CitiesViewModel>();
            services.AddTransient<StreetsViewModel>();
            services.AddTransient<SkillsViewModel>();
            services.AddTransient<DrivingLicenseCategoriesViewModel>();
            services.AddTransient<EducationsViewModel>();
            services.AddTransient<EmploymentTypesViewModel>();
            services.AddTransient<ExperiencesViewModel>();
            services.AddTransient<LanguagesViewModel>();
            services.AddTransient<ProfessionCategoriesViewModel>();
            services.AddTransient<LanguageProficienciesViewModel>();
            services.AddTransient<GendersViewModel>();
            services.AddTransient<IndustriesViewModel>();
            services.AddTransient<SubIndustriesViewModel>();
            services.AddTransient<ProfessionsViewModel>();
            services.AddTransient<RequestStatusesViewModel>();
            services.AddTransient<RolesViewModel>();
            services.AddTransient<SchedulesViewModel>();
            services.AddTransient<UsersViewModel>();
            services.AddTransient<AddBranchViewModel>();
            services.AddTransient<ChangeBranchViewModel>();
            services.AddTransient<AddCityViewModel>();
            services.AddTransient<AddCountryViewModel>();
            services.AddTransient<ChangeCountryViewModel>();
            services.AddTransient<ChangeCityViewModel>();
            services.AddTransient<AddDrivingLicenseCategoryViewModel>();
            services.AddTransient<ChangeDrivingLicenseCategoryViewModel>();
            services.AddTransient<AddEducationViewModel>();
            services.AddTransient<ChangeEducationViewModel>();
            services.AddTransient<AddEmploymentTypeViewModel>();
            services.AddTransient<ChangeEmploymentTypeViewModel>();
            services.AddTransient<AddExperienceViewModel>();
            services.AddTransient<ChangeExperienceViewModel>();
            services.AddTransient<AddGenderViewModel>();
            services.AddTransient<ChangeGenderViewModel>();
            services.AddTransient<AddIndustryViewModel>();
            services.AddTransient<ChangeIndustryViewModel>();
            services.AddTransient<AddLanguageViewModel>();
            services.AddTransient<ChangeLanguageViewModel>();
            services.AddTransient<AddLanguageProficiencyViewModel>();
            services.AddTransient<ChangeLanguageProficiencyViewModel>();
            services.AddTransient<ChangeOrganizationViewModel>();
            services.AddTransient<AddProfessionViewModel>();
            services.AddTransient<ChangeProfessionViewModel>();
            services.AddTransient<AddProfessionCategoryViewModel>();
            services.AddTransient<ChangeProfessionCategoryViewModel>();
            services.AddTransient<AddRequestStatusViewModel>();
            services.AddTransient<ChangeRequestStatusViewModel>();
            services.AddTransient<AddRoleViewModel>();
            services.AddTransient<ChangeRoleViewModel>();
            services.AddTransient<AddScheduleViewModel>();
            services.AddTransient<ChangeScheduleViewModel>();
            services.AddTransient<ChangeSkillViewModel>();
            services.AddTransient<AddStreetViewModel>();
            services.AddTransient<ChangeStreetViewModel>();
            services.AddTransient<AddSubIndustryViewModel>();
            services.AddTransient<ChangeSubIndustryViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<MenuPageService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
                _provider.GetRequiredService(item.ServiceType);
        }
    }
}
