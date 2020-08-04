using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

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

        public List<Country> GetCountries()
        {
            return _context.Country.AsNoTracking().ToList();
        }

        public List<Country> GetCountries(string countryName)
        {
            return _context.Country.Where(c =>
            (countryName.Length > 0 ? c.CountryName.ToLower().StartsWith(countryName.ToLower()) : true)).AsNoTracking().ToList();
        }

        public List<DrivingLicenseCategory> GetDrivingLicenseCategories()
        {
            return _context.DrivingLicenseCategory.AsNoTracking().ToList();
        }

        public List<Education> GetEducations()
        {
            return _context.Education.AsNoTracking().ToList();
        }

        public List<Gender> GetGenders()
        {
            return _context.Gender.AsNoTracking().ToList();
        }

        public int GetIdRole(string roleName)
        {
            return _context.Role.Single(r => r.RoleName == roleName).IdRole;
        }

        public List<Industry> GetIndustries()
        {
            return _context.Industry.AsNoTracking().ToList();
        }

        public List<LanguageProficiency> GetLanguageProficiencies()
        {
            return _context.LanguageProficiency.AsNoTracking().ToList();
        }

        public List<Language> GetLanguages()
        {
            return _context.Language.AsNoTracking().ToList();
        }

        public Organization GetOrganization(int idOrganization)
        {
            return _context.Organization.Single(o => o.IdOrganization == idOrganization);
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

        public List<Role> GetRoles()
        {
            return _context.Role.AsNoTracking().ToList();
        }

        public List<Skill> GetSkills()
        {
            return _context.Skill.AsNoTracking().ToList();
        }

        public List<Street> GetStreets(int idCity)
        {
            return _context.Street.Where(s => s.IdCity == idCity).AsNoTracking().ToList();
        }

        public List<SubIndustry> GetSubIndustries(int idIndustry)
        {
            return _context.SubIndustry.Where(s => s.IdIndustry == idIndustry).AsNoTracking().ToList();
        }

        public bool NecessaryToSupplementTheInformation(int idUser)
        {
            var results = new List<bool>();

            results.Add(ContainsApplicant(idUser));
            results.Add(ContainsEmployer(idUser));
            results.Add(ContainsManager(idUser));

            return results.All(r => r == false);
        }

        private void SetCommandTimeout()
        {
            var adapter = (IObjectContextAdapter)_context;
            var objectContext = adapter.ObjectContext;

            objectContext.CommandTimeout = _config.CommandTimeout;
        }
    }
}
