using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Models;
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

        public bool AddApplicant(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber)
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

        public bool ContainsApplicant(int idApplicant)
        {
            return _context.Applicant.SingleOrDefault(a => a.IdApplicant == idApplicant) != null;
        }

        public bool ContainsBranch(int idOrganization, int idStreet, string nameHouse)
        {
            return _context.Branch.SingleOrDefault(b => b.IdOrganization == idOrganization && b.IdStreet == idStreet && b.NameHouse == nameHouse) != null;
        }

        public bool ContainsEmployer(int idEmployer)
        {
            return _context.Employer.SingleOrDefault(e => e.IdEmployer == idEmployer) != null;
        }

        public bool ContainsManager(int idManager)
        {
            return _context.Manager.SingleOrDefault(m => m.IdManager == idManager) != null;
        }

        public bool ContainsOrganization(string organizationName)
        {
            return _context.Organization.SingleOrDefault(o => o.OrganizationName == organizationName) != null;
        }

        public bool ContainsSkill(string skillName)
        {
            return _context.Skill.SingleOrDefault(s => s.SkillName == skillName) != null;
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

        public List<City> GetCities(int idCountry)
        {
            return _context.City.Where(c => c.IdCountry == idCountry).AsNoTracking().ToList();
        }

        public List<Country> GetCountries()
        {
            return _context.Country.AsNoTracking().ToList();
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

        public List<v_organizationWithoutPhoto> GetOrganizationsWithoutPhoto()
        {
            return _context.v_organizationWithoutPhoto.AsNoTracking().ToList();
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
