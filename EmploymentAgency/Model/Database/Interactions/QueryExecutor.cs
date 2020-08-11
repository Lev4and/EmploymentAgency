using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;

namespace EmploymentAgency.Model.Database.Interactions
{
    public class QueryExecutor : IInteraction
    {
        private ConfigurationDatabase _config;
        private EmploymentAgencyContext _context;

        public QueryExecutor()
        {
            _config = ConfigurationDatabase.GetConfiguration();
            _context = new EmploymentAgencyContext(_config.ConnectionString);

            SetCommandTimeout();
        }

        public object MessageBox { get; internal set; }

        public bool AddApplicant(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber, int idStreet, string nameHouse, string apartment)
        {
            if(!ContainsApplicant(idUser))
            {
                _context.Applicant.Add(new Applicant
                {
                    IdApplicant = idUser,
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    IdGender = idGender,
                    Photo = photo,
                    DateOfBirth = dateOfBirth,
                    PhoneNumber = phoneNumber,
                    IdStreet = idStreet,
                    NameHouse = nameHouse,
                    Apartment = apartment,
                    IsEmployed = false
                });

                return true;
            }

            return false;
        }

        public bool AddBranch(int idOrganization, int idStreet, string nameHouse, string phoneNumber)
        {
            if(!ContainsBranch(idOrganization, idStreet, nameHouse))
            {
                _context.Branch.Add(new Branch
                {
                    IdOrganization = idOrganization,
                    IdStreet = idStreet,
                    NameHouse = nameHouse,
                    PhoneNumber = phoneNumber
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddCity(int idCountry, string cityName)
        {
            if(!ContainsCity(idCountry, cityName))
            {
                _context.City.Add(new City
                {
                    IdCountry = idCountry,
                    CityName = cityName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddCountry(string countryName, byte[] flag)
        {
            if(!ContainsCountry(countryName))
            {
                _context.Country.Add(new Country
                {
                    CountryName = countryName,
                    Flag = flag
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddDrivingLicenseCategory(string drivingLicenseCategoryName)
        {
            if(!ContainsDrivingLicenseCategory(drivingLicenseCategoryName))
            {
                _context.DrivingLicenseCategory.Add(new DrivingLicenseCategory
                {
                    DrivingLicenseCategoryName = drivingLicenseCategoryName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddEducation(string educationName)
        {
            if(!ContainsEducation(educationName))
            {
                _context.Education.Add(new Education
                {
                    EducationName = educationName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddEducationalActivity(int idApplicant, int idEducation, string nameEducationalnstitution, string address, DateTime startDate, DateTime? endDate)
        {
            if(!ContainsEducationalActivity(idApplicant, idEducation, nameEducationalnstitution, address, startDate, endDate))
            {
                _context.EducationalActivity.Add(new EducationalActivity
                {
                    IdApplicant = idApplicant,
                    IdEducation = idEducation,
                    NameEducationalnstitution = nameEducationalnstitution,
                    Address = address,
                    StartDate = startDate,
                    EndDate = endDate
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddEmployer(int idUser, int idBranch, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber)
        {
            if(!ContainsEmployer(idUser))
            {
                _context.Employer.Add(new Employer
                {
                    IdEmployer = idUser,
                    IdBranch = idBranch,
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    IdGender = idGender,
                    Photo = photo,
                    DateOfBirth = dateOfBirth,
                    PhoneNumber = phoneNumber
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddEmploymentType(string employmentTypeName)
        {
            if(!ContainsEmploymentType(employmentTypeName))
            {
                _context.EmploymentType.Add(new EmploymentType
                {
                    EmploymentTypeName = employmentTypeName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddExperience(string experienceName)
        {
            if(!ContainsExperience(experienceName))
            {
                _context.Experience.Add(new Experience
                {
                    ExperienceName = experienceName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddGender(string genderName)
        {
            if(!ContainsGender(genderName))
            {
                _context.Gender.Add(new Gender
                {
                    GenderName = genderName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddIndustry(string industryName)
        {
            if(!ContainsIndustry(industryName))
            {
                _context.Industry.Add(new Industry
                {
                    IndustryName = industryName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddKnowledgeLanguage(int idApplicant, int idLanguage, int idLanguageProficiency)
        {
            if(!ContainsKnowledgeLanguage(idApplicant, idLanguage, idLanguageProficiency))
            {
                _context.KnowledgeLanguage.Add(new KnowledgeLanguage
                {
                    IdApplicant = idApplicant,
                    IdLanguage = idLanguage,
                    IdLanguageProficiency = idLanguageProficiency
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddLaborActivity(int idApplicant, string organizationName, string organizationAddress, string professionName, string activity, DateTime startDate, DateTime? endDate)
        {
            if(!ContainsLaborActivity(idApplicant, organizationName, organizationAddress, professionName, activity, startDate, endDate))
            {
                _context.LaborActivity.Add(new LaborActivity
                {
                    IdApplicant = idApplicant,
                    OrganizationName = organizationName,
                    OrganizationAddress = organizationAddress,
                    ProfessionName = professionName,
                    Activity = activity,
                    StartDate = startDate,
                    EndDate = endDate
                });

                return true;
            }

            return false;
        }

        public bool AddLanguage(string languageName)
        {
            if(!ContainsLanguage(languageName))
            {
                _context.Language.Add(new Language
                {
                    LanguageName = languageName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddLanguageProficiency(string designation, string languageProficiencyName)
        {
            if(!ContainsLanguageProficiency(designation, languageProficiencyName))
            {
                _context.LanguageProficiency.Add(new LanguageProficiency
                {
                    Designation = designation,
                    LanguageProficiencyName = languageProficiencyName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddManager(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber)
        {
            if(!ContainsManager(idUser))
            {
                _context.Manager.Add(new Manager
                {
                    IdManager = idUser,
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    IdGender = idGender,
                    Photo = photo,
                    DateOfBirth = dateOfBirth,
                    PhoneNumber = phoneNumber
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo)
        {
            if(!ContainsOrganization(organizationName))
            {
                DateTime now = DateTime.Now;

                _context.Organization.Add(new Organization
                {
                    IdSubIndustry = idSubIndustry,
                    OrganizationName = organizationName,
                    Photo = photo,
                    DateOfRegistration = now
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo, out Organization organization)
        {
            organization = null;

            if (!ContainsOrganization(organizationName))
            {
                DateTime now = DateTime.Now;

                organization = _context.Organization.Add(new Organization
                {
                    IdSubIndustry = idSubIndustry,
                    OrganizationName = organizationName,
                    Photo = photo,
                    DateOfRegistration = now
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddPossessionDrivingLicenseCategory(int idApplicant, int idDrivingLicenseCategory)
        {
            if(!ContainsPossessionDrivingLicenseCategory(idApplicant, idDrivingLicenseCategory))
            {
                _context.PossessionDrivingLicenseCategory.Add(new PossessionDrivingLicenseCategory
                {
                    IdApplicant = idApplicant,
                    IdDrivingLicenseCategory = idDrivingLicenseCategory
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddPossessionSkill(int idApplicant, int idSkill)
        {
            if(!ContainsPossessionSkill(idApplicant, idSkill))
            {
                _context.PossessionSkill.Add(new PossessionSkill
                {
                    IdApplicant = idApplicant,
                    IdSkill = idSkill
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddProfession(int idProfessionCategory, string professionName)
        {
            if(!ContainsProfession(professionName))
            {
                _context.Profession.Add(new Profession
                {
                    IdProfessionCategory = idProfessionCategory,
                    ProfessionName = professionName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddProfessionCategory(string nameProfessionCategory)
        {
            if(!ContainsProfessionCategory(nameProfessionCategory))
            {
                _context.ProfessionCategory.Add(new ProfessionCategory
                {
                    NameProfessionCategory = nameProfessionCategory
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddRequestStatus(string requestStatusName)
        {
            if(!ContainsRequestStatus(requestStatusName))
            {
                _context.RequestStatus.Add(new RequestStatus
                {
                    RequestStatusName = requestStatusName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddRole(string roleName)
        {
            if(!ContainsRole(roleName))
            {
                _context.Role.Add(new Role
                {
                    RoleName = roleName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddSchedule(string scheduleName)
        {
            if(!ContainsSchedule(scheduleName))
            {
                _context.Schedule.Add(new Schedule
                {
                    ScheduleName = scheduleName
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddSkill(string skillName, byte[] photo)
        {
            if(!ContainsSkill(skillName))
            {
                _context.Skill.Add(new Skill
                {
                    SkillName = skillName,
                    Photo = photo
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddUser(int idRole, string login, string password)
        {
            if(!ContainsUser(login))
            {
                _context.User.Add(new User
                {
                    IdRole = idRole,
                    Login = login,
                    Password = password,
                    DateOfRegistration = DateTime.Now
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsApplicant(int idApplicant)
        {
            return _context.Applicant.SingleOrDefault(a => a.IdApplicant == idApplicant) != null;
        }

        public bool ContainsBranch(int idOrganization, int idStreet, string nameHouse)
        {
            return _context.Branch.SingleOrDefault(b => b.IdOrganization == idOrganization && b.IdStreet == idStreet && b.NameHouse == nameHouse) != null;
        }

        public bool ContainsCity(int idCountry, string cityName)
        {
            return _context.City.SingleOrDefault(c => c.IdCountry == idCountry && c.CityName == cityName) != null;
        }

        public bool ContainsCountry(string countryName)
        {
            return _context.Country.SingleOrDefault(c => c.CountryName == countryName) != null;
        }

        public bool ContainsDrivingLicenseCategory(string drivingLicenseCategoryName)
        {
            return _context.DrivingLicenseCategory.SingleOrDefault(d => d.DrivingLicenseCategoryName == drivingLicenseCategoryName) != null;
        }

        public bool ContainsEducation(string educationName)
        {
            return _context.Education.SingleOrDefault(e => e.EducationName == educationName) != null;
        }

        public bool ContainsEducationalActivity(int idApplicant, int idEducation, string nameEducationalnstitution, string address, DateTime startDate, DateTime? endDate)
        {
            return _context.EducationalActivity.SingleOrDefault(e =>
            e.IdApplicant == idApplicant && 
            e.IdEducation == idEducation &&
            e.NameEducationalnstitution == nameEducationalnstitution &&
            e.Address == address &&
            e.StartDate == startDate &&
            e.EndDate == endDate) != null;
        }

        public bool ContainsEmployer(int idEmployer)
        {
            return _context.Employer.SingleOrDefault(e => e.IdEmployer == idEmployer) != null;
        }

        public bool ContainsEmploymentType(string employmentTypeName)
        {
            return _context.EmploymentType.SingleOrDefault(e => e.EmploymentTypeName == employmentTypeName) != null;
        }

        public bool ContainsExperience(string experienceName)
        {
            return _context.Experience.SingleOrDefault(e => e.ExperienceName == experienceName) != null;
        }

        public bool ContainsGender(string genderName)
        {
            return _context.Gender.SingleOrDefault(g => g.GenderName == genderName) != null;
        }

        public bool ContainsIndustry(string industryName)
        {
            return _context.Industry.SingleOrDefault(i => i.IndustryName == industryName) != null;
        }

        public bool ContainsKnowledgeLanguage(int idApplicant, int idLanguage, int idLanguageProficiency)
        {
            return _context.KnowledgeLanguage.SingleOrDefault(k =>
            k.IdApplicant == idApplicant &&
            k.IdLanguage == idLanguage &&
            k.IdLanguageProficiency == idLanguageProficiency) != null;
        }

        public bool ContainsLaborActivity(int idApplicant, string organizationName, string organizationAddress, string professionName, string activity, DateTime startDate, DateTime? endDate)
        {
            return _context.LaborActivity.SingleOrDefault(l =>
            l.IdApplicant == idApplicant &&
            l.OrganizationName == organizationName &&
            l.OrganizationAddress == organizationAddress &&
            l.ProfessionName == professionName &&
            l.Activity.Contains(activity) &&
            l.StartDate == startDate &&
            l.EndDate == endDate) != null;
        }

        public bool ContainsLanguage(string languageName)
        {
            return _context.Language.SingleOrDefault(l => l.LanguageName == languageName) != null;
        }

        public bool ContainsLanguageProficiency(string designation, string languageProficiencyName)
        {
            return _context.LanguageProficiency.SingleOrDefault(l =>
            l.Designation == designation &&
            l.LanguageProficiencyName == languageProficiencyName) != null;
        }

        public bool ContainsManager(int idManager)
        {
            return _context.Manager.SingleOrDefault(m => m.IdManager == idManager) != null;
        }

        public bool ContainsOrganization(string organizationName)
        {
            return _context.Organization.SingleOrDefault(o => o.OrganizationName == organizationName) != null;
        }

        public bool ContainsPossessionDrivingLicenseCategory(int idApplicant, int idDrivingLicenseCategory)
        {
            return _context.PossessionDrivingLicenseCategory.SingleOrDefault(p => p.IdApplicant == idApplicant && p.IdDrivingLicenseCategory == idDrivingLicenseCategory) != null;
        }

        public bool ContainsPossessionSkill(int idApplicant, int idSkill)
        {
            return _context.PossessionSkill.SingleOrDefault(p => p.IdApplicant == idApplicant && p.IdSkill == idSkill) != null;
        }

        public bool ContainsProfession(string professionName)
        {
            return _context.Profession.FirstOrDefault(p => p.ProfessionName == professionName) != null;
        }

        public bool ContainsProfessionCategory(string nameProfessionCategory)
        {
            return _context.ProfessionCategory.SingleOrDefault(p => p.NameProfessionCategory == nameProfessionCategory) != null;
        }

        public bool ContainsRequestStatus(string requestStatusName)
        {
            return _context.RequestStatus.SingleOrDefault(r => r.RequestStatusName == requestStatusName) != null;
        }

        public bool ContainsRole(string roleName)
        {
            return _context.Role.SingleOrDefault(r => r.RoleName == roleName) != null;
        }

        public bool ContainsSchedule(string scheduleName)
        {
            return _context.Schedule.SingleOrDefault(s => s.ScheduleName == scheduleName) != null;
        }

        public bool ContainsSkill(string skillName)
        {
            return _context.Skill.SingleOrDefault(s => s.SkillName == skillName) != null;
        }

        public bool ContainsUser(string login)
        {
            return _context.User.SingleOrDefault(u => u.Login == login) != null;
        }

        public bool CorrectDataUser(string login, string password)
        {
            return _context.User.SingleOrDefault(u => u.Login == login && u.Password == password) != null;
        }

        public bool CorrectDataUser(string login, string password, out v_user user)
        {
            user = _context.v_user.SingleOrDefault(u => u.Login == login && u.Password == password);

            return user != null;
        }

        public v_branch GetBranch(int idBranch)
        {
            return _context.v_branch.SingleOrDefault(b => b.IdBranch == idBranch);
        }

        public List<v_branchSimplifiedInformation> GetBranches(int idOrganization)
        {
            return _context.v_branchSimplifiedInformation.Where(b => b.IdOrganization == idOrganization).AsNoTracking().ToList();
        }

        public List<v_branch> GetBranches(int idIndustry, int idSubIndustry, int idCountry, int idCity, int idStreet, int idOrganization)
        {
            return _context.v_branch.Where(b =>
            (idIndustry != -1 ? b.IdIndustry == idIndustry : true) &&
            (idSubIndustry != -1 ? b.IdSubIndustry == idSubIndustry : true) &&
            (idCountry != -1 ? b.IdCountry == idCountry : true) &&
            (idCity != -1 ? b.IdCity == idCity : true) &&
            (idStreet != -1 ? b.IdStreet == idStreet : true) &&
            (idOrganization != -1 ? b.IdOrganization == idOrganization : true)).AsNoTracking().ToList();
        }

        public List<City> GetCities(int idCountry)
        {
            return _context.City.Where(c => c.IdCountry == idCountry).AsNoTracking().ToList();
        }

        public List<v_city> GetCities()
        {
            return _context.v_city.AsNoTracking().ToList();
        }

        public List<v_city> GetCities(int idCountry, string cityName)
        {
            return _context.v_city.Where(c =>
            (idCountry != -1 ? c.IdCountry == idCountry : true) &&
            (cityName.Length > 0 ? c.CityName.ToLower().StartsWith(cityName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public City GetCity(int idCity)
        {
            return _context.City.SingleOrDefault(c => c.IdCity == idCity);
        }

        public List<Country> GetCountries()
        {
            return _context.Country.AsNoTracking().ToList();
        }

        public List<Country> GetCountries(string countryName)
        {
            return _context.Country.Where(c =>
            (countryName.Length > 0 ? c.CountryName.ToLower().StartsWith(countryName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Country GetCountry(int idCountry)
        {
            return _context.Country.SingleOrDefault(c => c.IdCountry == idCountry);
        }

        public List<DrivingLicenseCategory> GetDrivingLicenseCategories()
        {
            return _context.DrivingLicenseCategory.AsNoTracking().ToList();
        }

        public List<DrivingLicenseCategory> GetDrivingLicenseCategories(string drivingLicenseCategoryName)
        {
            return _context.DrivingLicenseCategory.Where(d =>
            (drivingLicenseCategoryName.Length > 0 ? d.DrivingLicenseCategoryName.ToLower().StartsWith(drivingLicenseCategoryName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public DrivingLicenseCategory GetDrivingLicenseCategory(int idDrivingLicenseCategory)
        {
            return _context.DrivingLicenseCategory.SingleOrDefault(d => d.IdDrivingLicenseCategory == idDrivingLicenseCategory);
        }

        public Education GetEducation(int idEducation)
        {
            return _context.Education.SingleOrDefault(e => e.IdEducation == idEducation);
        }

        public List<Education> GetEducations()
        {
            return _context.Education.AsNoTracking().ToList();
        }

        public List<Education> GetEducations(string educationName)
        {
            return _context.Education.Where(e =>
            (educationName.Length > 0 ? e.EducationName.ToLower().StartsWith(educationName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public EmploymentType GetEmploymentType(int idEmploymentType)
        {
            return _context.EmploymentType.SingleOrDefault(e => e.IdEmploymentType == idEmploymentType);
        }

        public List<EmploymentType> GetEmploymentTypes(string employmentTypeName)
        {
            return _context.EmploymentType.Where(e =>
            (employmentTypeName.Length > 0 ? e.EmploymentTypeName.ToLower().StartsWith(employmentTypeName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Experience GetExperience(int idExperience)
        {
            return _context.Experience.SingleOrDefault(e => e.IdExperience == idExperience);
        }

        public List<Experience> GetExperiences(string experienceName)
        {
            return _context.Experience.Where(e =>
            (experienceName.Length > 0 ? e.ExperienceName.ToLower().StartsWith(experienceName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Gender GetGender(int idGender)
        {
            return _context.Gender.SingleOrDefault(g => g.IdGender == idGender);
        }

        public List<Gender> GetGenders()
        {
            return _context.Gender.AsNoTracking().ToList();
        }

        public List<Gender> GetGenders(string genderName)
        {
            return _context.Gender.Where(g =>
            (genderName.Length > 0 ? g.GenderName.ToLower().StartsWith(genderName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public int GetIdRole(string roleName)
        {
            return _context.Role.Single(r => r.RoleName == roleName).IdRole;
        }

        public List<Industry> GetIndustries()
        {
            return _context.Industry.AsNoTracking().ToList();
        }

        public List<Industry> GetIndustries(string industryName)
        {
            return _context.Industry.Where(i =>
            (industryName.Length > 0 ? i.IndustryName.ToLower().StartsWith(industryName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Industry GetIndustry(int idIndustry)
        {
            return _context.Industry.SingleOrDefault(i => i.IdIndustry == idIndustry);
        }

        public Language GetLanguage(int idLanguage)
        {
            return _context.Language.SingleOrDefault(l => l.IdLanguage == idLanguage);
        }

        public List<LanguageProficiency> GetLanguageProficiencies()
        {
            return _context.LanguageProficiency.AsNoTracking().ToList();
        }

        public List<LanguageProficiency> GetLanguageProficiencies(string languageProficiencyName)
        {
            return _context.LanguageProficiency.Where(l =>
            (languageProficiencyName.Length > 0 ? l.LanguageProficiencyName.ToLower().StartsWith(languageProficiencyName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public LanguageProficiency GetLanguageProficiency(int idLanguageProficiency)
        {
            return _context.LanguageProficiency.SingleOrDefault(l => l.IdLanguageProficiency == idLanguageProficiency);
        }

        public List<Language> GetLanguages()
        {
            return _context.Language.AsNoTracking().ToList();
        }

        public List<Language> GetLanguages(string languageName)
        {
            return _context.Language.Where(l =>
            (languageName.Length > 0 ? l.LanguageName.ToLower().StartsWith(languageName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Organization GetOrganization(int idOrganization)
        {
            return _context.Organization.Single(o => o.IdOrganization == idOrganization);
        }

        public v_organization GetOrganizationExtendedInformation(int idOrganization)
        {
            return _context.v_organization.SingleOrDefault(o => o.IdOrganization == idOrganization);
        }
        public List<v_organization> GetOrganizations(int idIndustry, int idSubIndustry, string organizationName)
        {
            return _context.v_organization.Where(o =>
            (idIndustry != -1 ? o.IdIndustry == idIndustry : true) &&
            (idSubIndustry != -1 ? o.IdSubIndustry == idSubIndustry : true) &&
            (organizationName.Length > 0 ? o.OrganizationName.ToLower().StartsWith(organizationName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public List<v_organizationWithoutPhoto> GetOrganizationsWithoutPhoto()
        {
            return _context.v_organizationWithoutPhoto.AsNoTracking().ToList();
        }

        public Profession GetProfession(int idProfession)
        {
            return _context.Profession.SingleOrDefault(p => p.IdProfession == idProfession);
        }

        public List<ProfessionCategory> GetProfessionCategories(string nameProfessionCategory)
        {
            return _context.ProfessionCategory.Where(p =>
            (nameProfessionCategory.Length > 0 ? p.NameProfessionCategory.ToLower().StartsWith(nameProfessionCategory.ToLower()) : true)).AsNoTracking().ToList();
        }

        public List<ProfessionCategory> GetProfessionCategories()
        {
            return _context.ProfessionCategory.AsNoTracking().ToList();
        }

        public ProfessionCategory GetProfessionCategory(int idProfessionCategory)
        {
            return _context.ProfessionCategory.SingleOrDefault(p => p.IdProfessionCategory == idProfessionCategory);
        }

        public v_profession GetProfessionExtendedInformation(int idProfession)
        {
            return _context.v_profession.SingleOrDefault(p => p.IdProfession == idProfession);
        }

        public List<v_profession> GetProfessions(int idProfessionCategory, string professionName)
        {
            return _context.v_profession.Where(p =>
            (idProfessionCategory != -1 ? p.IdProfessionCategory == idProfessionCategory : true) &&
            (professionName.Length > 0 ? p.ProfessionName.ToLower().StartsWith(professionName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public RequestStatus GetRequestStatus(int idRequestStatus)
        {
            return _context.RequestStatus.SingleOrDefault(r => r.IdRequestStatus == idRequestStatus);
        }

        public List<RequestStatus> GetRequestStatuses(string requestStatusName)
        {
            return _context.RequestStatus.Where(r =>
            (requestStatusName.Length > 0 ? r.RequestStatusName.ToLower().StartsWith(requestStatusName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Role GetRole(int idRole)
        {
            return _context.Role.SingleOrDefault(r => r.IdRole == idRole);
        }

        public List<Role> GetRoles()
        {
            return _context.Role.AsNoTracking().ToList();
        }

        public List<Role> GetRoles(string roleName)
        {
            return _context.Role.Where(r =>
            (roleName.Length > 0 ? r.RoleName.ToLower().StartsWith(roleName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Schedule GetSchedule(int idSchedule)
        {
            return _context.Schedule.SingleOrDefault(s => s.IdSchedule == idSchedule);
        }

        public List<Schedule> GetSchedules(string scheduleName)
        {
            return _context.Schedule.Where(s =>
            (scheduleName.Length > 0 ? s.ScheduleName.ToLower().StartsWith(scheduleName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public Skill GetSkill(int idSkill)
        {
            return _context.Skill.SingleOrDefault(s => s.IdSkill == idSkill);
        }

        public List<Skill> GetSkills()
        {
            return _context.Skill.AsNoTracking().ToList();
        }

        public List<Skill> GetSkills(string skillName)
        {
            return _context.Skill.Where(s =>
            (skillName.Length > 0 ? s.SkillName.ToLower().StartsWith(skillName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public List<Street> GetStreets(int idCity)
        {
            return _context.Street.Where(s => s.IdCity == idCity).AsNoTracking().ToList();
        }

        public List<v_street> GetStreets()
        {
            return _context.v_street.AsNoTracking().ToList();
        }

        public List<v_street> GetStreets(int idCountry, int idCity, string streetName)
        {
            return _context.v_street.Where(s =>
            (idCountry != -1 ? s.IdCountry == idCountry : true) &&
            (idCity != -1 ? s.IdCity == idCity : true) &&
            (streetName.Length > 0 ? s.StreetName.ToLower().StartsWith(streetName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public List<SubIndustry> GetSubIndustries(int idIndustry)
        {
            return _context.SubIndustry.Where(s => s.IdIndustry == idIndustry).AsNoTracking().ToList();
        }

        public List<v_subIndustry> GetSubIndustries(int idIndustry, string nameSubIndustry)
        {
            return _context.v_subIndustry.Where(s =>
            (idIndustry != -1 ? s.IdIndustry == idIndustry : true) &&
            (nameSubIndustry.Length > 0 ? s.NameSubIndustry.ToLower().StartsWith(nameSubIndustry.ToLower()) : true)).AsNoTracking().ToList();
        }

        public List<v_user> GetUsers(int idRole, string login)
        {
            return _context.v_user.Where(u =>
            (idRole != -1 ? u.IdRole == idRole : true) &&
            (login.Length > 0 ? u.Login.ToLower().StartsWith(login.ToLower()) : true)).AsNoTracking().ToList();
        }

        public bool NecessaryToSupplementTheInformation(int idUser)
        {
            var results = new List<bool>();

            results.Add(ContainsApplicant(idUser));
            results.Add(ContainsEmployer(idUser));
            results.Add(ContainsManager(idUser));

            return results.All(r => r == false);
        }

        public void RemoveBranch(int idBranch)
        {
            var branch = _context.Branch.SingleOrDefault(b => b.IdBranch == idBranch);

            _context.Branch.Remove(branch);
            _context.SaveChanges();
        }

        public void RemoveCity(int idCity)
        {
            var city = _context.City.SingleOrDefault(c => c.IdCity == idCity);

            _context.City.Remove(city);
            _context.SaveChanges();
        }

        public void RemoveCountry(int idCountry)
        {
            var country = GetCountry(idCountry);

            _context.Country.Remove(country);
            _context.SaveChanges();
        }

        public void RemoveDrivingLicenseCategory(int idDrivingLicenseCategory)
        {
            var drivingLicenseCategory = GetDrivingLicenseCategory(idDrivingLicenseCategory);

            _context.DrivingLicenseCategory.Remove(drivingLicenseCategory);
            _context.SaveChanges();
        }

        public void RemoveEducation(int idEducation)
        {
            var education = GetEducation(idEducation);

            _context.Education.Remove(education);
            _context.SaveChanges();
        }

        public void RemoveEmploymentType(int idEmploymentType)
        {
            var employmentType = GetEmploymentType(idEmploymentType);

            _context.EmploymentType.Remove(employmentType);
            _context.SaveChanges();
        }

        public void RemoveExperience(int idExperience)
        {
            var experience = GetExperience(idExperience);

            _context.Experience.Remove(experience);
            _context.SaveChanges();
        }

        public void RemoveGender(int idGender)
        {
            var gender = GetGender(idGender);

            _context.Gender.Remove(gender);
            _context.SaveChanges();
        }

        public void RemoveIndustry(int idIndustry)
        {
            var industry = GetIndustry(idIndustry);

            _context.Industry.Remove(industry);
            _context.SaveChanges();
        }

        public void RemoveLanguage(int idLanguage)
        {
            var language = GetLanguage(idLanguage);

            _context.Language.Remove(language);
            _context.SaveChanges();
        }

        public void RemoveLanguageProficiency(int idLanguageProficiency)
        {
            var languageProficiency = GetLanguageProficiency(idLanguageProficiency);

            _context.LanguageProficiency.Remove(languageProficiency);
            _context.SaveChanges();
        }

        public void RemoveOrganization(int idOrganization)
        {
            var organization = GetOrganization(idOrganization);

            _context.Organization.Remove(organization);
            _context.SaveChanges();
        }

        public void RemoveProfession(int idProfession)
        {
            var profession = GetProfession(idProfession);

            _context.Profession.Remove(profession);
            _context.SaveChanges();
        }

        public void RemoveProfessionCategory(int idProfessionCategory)
        {
            var professionCategory = GetProfessionCategory(idProfessionCategory);

            _context.ProfessionCategory.Remove(professionCategory);
            _context.SaveChanges();
        }

        public void RemoveRequestStatus(int idRequestStatus)
        {
            var requestStatus = GetRequestStatus(idRequestStatus);

            _context.RequestStatus.Remove(requestStatus);
            _context.SaveChanges();
        }

        public void RemoveRole(int idRole)
        {
            var role = GetRole(idRole);

            _context.Role.Remove(role);
            _context.SaveChanges();
        }

        public void RemoveSchedule(int idSchedule)
        {
            var schedule = GetSchedule(idSchedule);

            _context.Schedule.Remove(schedule);
            _context.SaveChanges();
        }

        public void RemoveSkill(int idSkill)
        {
            var skill = GetSkill(idSkill);

            _context.Skill.Remove(skill);
            _context.SaveChanges();
        }

        public void UpdateBranch(int idBranch, string phoneNumber)
        {
            var branch = _context.Branch.SingleOrDefault(b => b.IdBranch == idBranch);

            branch.PhoneNumber = phoneNumber;

            _context.SaveChanges();
        }

        public bool UpdateCity(int idCountry, int idCity, string cityName)
        {
            if(!ContainsCity(idCountry, cityName))
            {
                var city = _context.City.SingleOrDefault(c => c.IdCity == idCity);

                city.CityName = cityName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateCountry(int idCountry, string countryName, byte[] flag)
        {
            var country = GetCountry(idCountry);

            if(country.CountryName != countryName)
            {
                if(ContainsCountry(countryName))
                {
                    return false;
                }
            }

            country.CountryName = countryName;
            country.Flag = flag;

            _context.SaveChanges();

            return true;
        }

        public bool UpdateDrivingLicenseCategory(int idDrivingLicenseCategory, string drivingLicenseCategoryName)
        {
            if(!ContainsDrivingLicenseCategory(drivingLicenseCategoryName))
            {
                var drivingLicenseCategory = GetDrivingLicenseCategory(idDrivingLicenseCategory);

                drivingLicenseCategory.DrivingLicenseCategoryName = drivingLicenseCategoryName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateEducation(int idEducation, string educationName)
        {
            if(!ContainsEducation(educationName))
            {
                var education = GetEducation(idEducation);

                education.EducationName = educationName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateEmploymentType(int idEmploymentType, string employmentTypeName)
        {
            if(!ContainsEmploymentType(employmentTypeName))
            {
                var employmentType = GetEmploymentType(idEmploymentType);

                employmentType.EmploymentTypeName = employmentTypeName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateExperience(int idExperience, string experienceName)
        {
            if(!ContainsExperience(experienceName))
            {
                var experience = GetExperience(idExperience);

                experience.ExperienceName = experienceName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateGender(int idGender, string genderName)
        {
            if(!ContainsGender(genderName))
            {
                var gender = GetGender(idGender);

                gender.GenderName = genderName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateIndustry(int idIndustry, string industryName)
        {
            if(!ContainsIndustry(industryName))
            {
                var industry = GetIndustry(idIndustry);

                industry.IndustryName = industryName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateLanguage(int idLanguage, string languageName)
        {
            if(!ContainsLanguage(languageName))
            {
                var language = GetLanguage(idLanguage);

                language.LanguageName = languageName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateLanguageProficiency(int idLanguageProficiency, string designation, string languageProficiencyName)
        {
            if (!ContainsLanguageProficiency(designation, languageProficiencyName))
            {
                var languageProficiency = GetLanguageProficiency(idLanguageProficiency);

                languageProficiency.Designation = designation;
                languageProficiency.LanguageProficiencyName = languageProficiencyName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateOrganization(int idOrganization, string organizationName, byte[] photo, DateTime? closingDate)
        {
            var organization = GetOrganization(idOrganization);

            if (!ContainsOrganization(organizationName))
            {
                organization.OrganizationName = organizationName;
                organization.Photo = photo;
                organization.ClosingDate = closingDate;

                _context.SaveChanges();

                return true;
            }
            else
            {
                if((organization.Photo != photo || organization.ClosingDate != closingDate) && organization.OrganizationName == organizationName)
                {
                    organization.Photo = photo;
                    organization.ClosingDate = closingDate;

                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool UpdateProfession(int idProfession, string professionName)
        {
            if(!ContainsProfession(professionName))
            {
                var profession = GetProfession(idProfession);

                profession.ProfessionName = professionName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateProfessionCategory(int idProfessionCategory, string nameProfessionCategory)
        {
            if(!ContainsProfessionCategory(nameProfessionCategory))
            {
                var professionCategory = GetProfessionCategory(idProfessionCategory);

                professionCategory.NameProfessionCategory = nameProfessionCategory;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateRequestStatus(int idRequestStatus, string requestStatusName)
        {
            if(!ContainsRequestStatus(requestStatusName))
            {
                var requestStatus = GetRequestStatus(idRequestStatus);

                requestStatus.RequestStatusName = requestStatusName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateRole(int idRole, string roleName)
        {
            if(!ContainsRole(roleName))
            {
                var role = GetRole(idRole);

                role.RoleName = roleName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateSchedule(int idSchedule, string scheduleName)
        {
            if(!ContainsSchedule(scheduleName))
            {
                var schedule = GetSchedule(idSchedule);

                schedule.ScheduleName = scheduleName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateSkill(int idSkill, string skillName, byte[] photo)
        {
            var skill = GetSkill(idSkill);

            if(!ContainsSkill(skillName))
            {
                skill.SkillName = skillName;
                skill.Photo = photo;

                _context.SaveChanges();

                return true;
            }
            else
            {
                if(skill.Photo != photo && skill.SkillName == skillName)
                {
                    skill.Photo = photo;

                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        private void SetCommandTimeout()
        {
            var adapter = (IObjectContextAdapter)_context;
            var objectContext = adapter.ObjectContext;

            objectContext.CommandTimeout = _config.CommandTimeout;
        }
    }
}
