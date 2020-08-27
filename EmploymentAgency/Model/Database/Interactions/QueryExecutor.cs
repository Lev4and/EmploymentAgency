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

        public bool AddApplicant(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber, int idStreet, string nameHouse, string apartment, List<object> possessionSkills, List<object> possessionDrivingLicenseCategories, List<EducationalActivity> educationalActivities, List<KnowledgeLanguage> knowledgeLanguages, List<LaborActivity> laborActivities)
        {
            Applicant applicant = null;

            if(!ContainsApplicant(idUser))
            {
                applicant = _context.Applicant.Add(new Applicant
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
                _context.SaveChanges();

                AddPossessionSkills(applicant.IdApplicant, possessionSkills);
                AddPossessionDrivingLicenseCategories(applicant.IdApplicant, possessionDrivingLicenseCategories);
                AddEducationalActivities(applicant.IdApplicant, educationalActivities);
                AddKnowledgeLanguages(applicant.IdApplicant, knowledgeLanguages);
                AddLaborActivities(applicant.IdApplicant, laborActivities);

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

        public bool AddContract(int idEmploymentRequest)
        {
            if(!ContainsContract(idEmploymentRequest))
            {
                var contract = _context.Contract.Add(new Contract
                {
                    IdEmploymentRequest = idEmploymentRequest,
                    DateOfConclusion = DateTime.Now,
                    BreakDate = null
                });
                _context.SaveChanges();

                UpdateIsEmployedApplicant(contract.EmploymentRequest.SuitableVacancy.Request.Applicant.IdApplicant, true);

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

        public void AddEducationalActivities(int idApplicant, List<EducationalActivity> educationalActivities)
        {
            foreach (var educationalActivity in educationalActivities)
            {
                AddEducationalActivity(idApplicant, educationalActivity.IdEducation, educationalActivity.NameEducationalnstitution, educationalActivity.Address, educationalActivity.StartDate, educationalActivity.EndDate);
            }
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

        public bool AddEmploymentRequest(int idSuitableVacancy)
        {
            if(!ContainsEmploymentRequest(idSuitableVacancy))
            {
                _context.EmploymentRequest.Add(new EmploymentRequest
                {
                    IdSuitableVacancy = idSuitableVacancy,
                    IsSuitable = null
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

        public void AddKnowledgeLanguages(int idApplicant, List<KnowledgeLanguage> knowledgeLanguages)
        {
            foreach (var knowledgeLanguage in knowledgeLanguages)
            {
                AddKnowledgeLanguage(idApplicant, knowledgeLanguage.IdLanguage, knowledgeLanguage.IdLanguageProficiency);
            }
        }

        public void AddLaborActivities(int idApplicant, List<LaborActivity> laborActivities)
        {
            foreach (var laborActivity in laborActivities)
            {
                AddLaborActivity(idApplicant, laborActivity.OrganizationName, laborActivity.OrganizationAddress, laborActivity.ProfessionName, laborActivity.Activity, laborActivity.StartDate, laborActivity.EndDate);
            }
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

        public bool AddNecessarySkill(int idVacancy, int idSkill)
        {
            if(!ContainsNecessarySkill(idVacancy, idSkill))
            {
                _context.NecessarySkill.Add(new NecessarySkill
                {
                    IdVacancy = idVacancy,
                    IdSkill = idSkill
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void AddNecessarySkills(int idVacancy, List<object> necessarySkills)
        {
            foreach(var necessarySkill in necessarySkills)
            {
                AddNecessarySkill(idVacancy, Convert.ToInt32(necessarySkill));
            }
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

        public void AddPossessionDrivingLicenseCategories(int idApplicant, List<object> possessionDrivingLicenseCategories)
        {
            foreach (var possessionDrivingLicenseCategory in possessionDrivingLicenseCategories)
            {
                AddPossessionDrivingLicenseCategory(idApplicant, Convert.ToInt32((object)possessionDrivingLicenseCategory));
            }
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

        public void AddPossessionSkills(int idApplicant, List<object> possessionSkills)
        {
            foreach (var possessionSkill in possessionSkills)
            {
                AddPossessionSkill(idApplicant, Convert.ToInt32((object)possessionSkill));
            }
        }

        public bool AddPreferredEmploymentType(int idRequest, int idEmploymentType)
        {
            if(!ContainsPreferredEmploymentType(idRequest, idEmploymentType))
            {
                _context.PreferredEmploymentType.Add(new PreferredEmploymentType
                {
                    IdRequest = idRequest,
                    IdEmploymentType = idEmploymentType
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void AddPreferredEmploymentTypes(int idRequest, List<object> preferredEmploymentTypes)
        {
            foreach(var preferredEmploymentType in preferredEmploymentTypes)
            {
                AddPreferredEmploymentType(idRequest, Convert.ToInt32(preferredEmploymentType));
            }
        }

        public bool AddPreferredSchedule(int idRequest, int idSchedule)
        {
            if(!ContainsPreferredSchedule(idRequest, idSchedule))
            {
                _context.PreferredSchedule.Add(new PreferredSchedule
                {
                    IdRequest = idRequest,
                    IdSchedule = idSchedule
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void AddPreferredSchedules(int idRequest, List<object> preferredSchedules)
        {
            foreach(var preferredSchedule in preferredSchedules)
            {
                AddPreferredSchedule(idRequest, Convert.ToInt32(preferredSchedule));
            }
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

        public void AddRequest(int idApplicant, int idProfession, int idExperience, int? salary, string aboutMe, List<object> preferredEmploymentTypes, List<object> preferredSchedules)
        {
            var request = _context.Request.Add(new Request
            {
                IdApplicant = idApplicant,
                IdProfession = idProfession,
                IdExperience = idExperience,
                Salary = salary,
                AboutMe = aboutMe,
                DateOfRegistration = DateTime.Now,
                IdRequestStatus = GetIdRequestStatus("Не рассматривался")
            });
            _context.SaveChanges();

            AddPreferredEmploymentTypes(request.IdRequest, preferredEmploymentTypes);
            AddPreferredSchedules(request.IdRequest, preferredSchedules);
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

        public bool AddStreet(int idCity, string streetName)
        {
            if(!ContainsStreet(idCity, streetName))
            {
                _context.Street.Add(new Street
                {
                    IdCity = idCity,
                    StreetName = streetName,
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddSubIndustry(int idIndustry, string nameSubIndustry)
        {
            if(!ContainsSubIndustry(idIndustry, nameSubIndustry))
            {
                _context.SubIndustry.Add(new SubIndustry
                {
                    IdIndustry = idIndustry,
                    NameSubIndustry = nameSubIndustry
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool AddSuitableVacancy(int idRequest, int idManager, int idVacancy)
        {
            if(!ContainsSuitableVacancy(idRequest, idVacancy))
            {
                _context.SuitableVacancy.Add(new SuitableVacancy
                {
                    IdRequest = idRequest,
                    IdManager = idManager,
                    IdVacancy = idVacancy,
                    DiscoveryDate = DateTime.Now
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

        public bool AddUser(int idRole, string login, string password, out User user)
        {
            user = null;

            if(!ContainsUser(login))
            {
                user = _context.User.Add(new User
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

        public bool AddUser(int idRole, string login, string password, out v_user user)
        {
            user = null;

            if (!ContainsUser(login))
            {
                User userDefault = _context.User.Add(new User
                {
                    IdRole = idRole,
                    Login = login,
                    Password = password,
                    DateOfRegistration = DateTime.Now
                });
                _context.SaveChanges();

                user = GetUserExtendedInformation(userDefault.IdUser);

                return true;
            }

            return false;
        }

        public void AddVacancy(int idEmployer, int idProfession, int idEmploymentType, int idSchedule, int idExperience, string description, string duties, string requirements, string terms, int? salary, List<object> necessarySkills)
        {
            Vacancy vacancy = null;

            DateTime dateTime = DateTime.Now;

            vacancy = _context.Vacancy.Add(new Vacancy
            {
                IdEmployer = idEmployer,
                IdProfession = idProfession,
                IdEmploymentType = idEmploymentType,
                IdSchedule = idSchedule,
                IdExperience = idExperience,
                Description = description,
                Duties = duties,
                Requirements = requirements,
                Terms = terms,
                Salary = salary,
                DateOfRegistration = dateTime
            });
            _context.SaveChanges();

            AddNecessarySkills(vacancy.IdVacancy, necessarySkills);
        }

        public bool BreakContract(int idContract)
        {
            if(!IsBrokeContract(idContract))
            {
                var contract = GetContract(idContract);

                contract.BreakDate = DateTime.Now;

                _context.SaveChanges();

                UpdateIsEmployedApplicant(contract.EmploymentRequest.SuitableVacancy.Request.Applicant.IdApplicant, false);

                return true;
            }

            return false;
        }

        public void CloseVacancy(int idVacancy)
        {
            var vacancy = GetVacancy(idVacancy);

            vacancy.ClosingDate = DateTime.Now;

            _context.SaveChanges();
        }

        public void CompleteRemovalEducationActivities(int idApplicant)
        {
            var educationalActivities = GetEducationActivities(idApplicant);

            _context.EducationalActivity.RemoveRange(educationalActivities);
            _context.SaveChanges();
        }

        public void CompleteRemovalKnowledgeLanguages(int idApplicant)
        {
            var knowledgeLanguages = GetKnowledgeLanguages(idApplicant);

            _context.KnowledgeLanguage.RemoveRange(knowledgeLanguages);
            _context.SaveChanges();
        }

        public void CompleteRemovalLaborActivities(int idApplicant)
        {
            var laborActivities = GetLaborActivities(idApplicant);

            _context.LaborActivity.RemoveRange(laborActivities);
            _context.SaveChanges();
        }

        public void CompleteRemovalNecessarySkills(int idVacancy)
        {
            var necessarySkills = GetNecessarySkills(idVacancy);

            _context.NecessarySkill.RemoveRange(necessarySkills);
            _context.SaveChanges();
        }

        public void CompleteRemovalPossessionDrivingLicenseCategories(int idApplicant)
        {
            var possessionDrivingLicenseCategories = GetPossessionDrivingLicenseCategories(idApplicant);

            _context.PossessionDrivingLicenseCategory.RemoveRange(possessionDrivingLicenseCategories);
            _context.SaveChanges();
        }

        public void CompleteRemovalPossessionSkills(int idApplicant)
        {
            var possessionSkills = GetPossessionSkills(idApplicant);

            _context.PossessionSkill.RemoveRange(possessionSkills);
            _context.SaveChanges();
        }

        public void CompleteRemovalPreferredEmploymentTypes(int idRequest)
        {
            var preferredEmploymentTypes = GetPreferredEmploymentTypes(idRequest);

            _context.PreferredEmploymentType.RemoveRange(preferredEmploymentTypes);
            _context.SaveChanges();
        }

        public void CompleteRemovalPreferredSchedules(int idRequest)
        {
            var preferredSchedules = GetPreferredSchedules(idRequest);

            _context.PreferredSchedule.RemoveRange(preferredSchedules);
            _context.SaveChanges();
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

        public bool ContainsContract(int idEmploymentRequest)
        {
            return _context.Contract.SingleOrDefault(c => c.IdEmploymentRequest == idEmploymentRequest) != null;
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

        public bool ContainsEmploymentRequest(int idSuitableVacancy)
        {
            return _context.EmploymentRequest.SingleOrDefault(s => s.IdSuitableVacancy == idSuitableVacancy) != null;
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

        public bool ContainsNecessarySkill(int idVacancy, int idSkill)
        {
            return _context.NecessarySkill.SingleOrDefault(n =>
            n.IdVacancy == idVacancy &&
            n.IdSkill == idSkill) != null;
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

        public bool ContainsPreferredEmploymentType(int idRequest, int idEmploymentType)
        {
            return _context.PreferredEmploymentType.SingleOrDefault(p =>
            p.IdRequest == idRequest &&
            p.IdPreferredEmploymentType == idEmploymentType) != null;
        }

        public bool ContainsPreferredSchedule(int idRequest, int idSchedule)
        {
            return _context.PreferredSchedule.SingleOrDefault(p =>
            p.IdRequest == idRequest &&
            p.IdSchedule == idSchedule) != null;
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

        public bool ContainsStreet(int idCity, string streetName)
        {
            return _context.Street.SingleOrDefault(s =>
            s.IdCity == idCity &&
            s.StreetName == streetName) != null;
        }

        public bool ContainsSubIndustry(int idIndustry, string nameSubIndustry)
        {
            return _context.SubIndustry.SingleOrDefault(s =>
            s.IdIndustry == idIndustry &&
            s.NameSubIndustry == nameSubIndustry) != null;
        }

        public bool ContainsSuitableVacancy(int idRequest, int idVacancy)
        {
            return _context.SuitableVacancy.SingleOrDefault(s => s.IdRequest == idRequest && s.IdVacancy == idVacancy) != null;
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

        public bool DoesItHaveActiveContracts(int idApplicant)
        {
            return _context.v_contract.Where(c => c.IdApplicant == idApplicant && c.BreakDate == null).Count() > 0;
        }

        public Applicant GetApplicant(int idApplicant)
        {
            return _context.Applicant.SingleOrDefault(a => a.IdApplicant == idApplicant);
        }

        public v_applicant GetApplicantExtendedInformation(int idApplicant)
        {
            return _context.v_applicant.SingleOrDefault(a => a.IdApplicant == idApplicant);
        }

        public List<Applicant> GetApplicants()
        {
            return _context.Applicant.AsNoTracking().ToList();
        }

        public List<v_averageAgeDetailedReport> GetAverageAgeDetailedReport(Range<DateTime> rangeDateOfConclusionContract, List<object> selectedProfessionCategories, List<object> selectedProfessions)
        {
            var report = _context.v_averageAgeDetailedReport.Where(r =>
            r.Date >= rangeDateOfConclusionContract.BeginValue &&
            r.Date <= rangeDateOfConclusionContract.EndValue).AsNoTracking().ToList();

            return report.Where(r =>
            (selectedProfessionCategories.Count > 0 ? selectedProfessionCategories.Any(p => Convert.ToInt32(p) == r.IdProfessionCategory) : false) ||
            (selectedProfessions.Count > 0 ? selectedProfessions.Any(p => Convert.ToInt32(p) == r.IdProfession) : false)).OrderBy(r => r.Date).ToList();
        }

        public List<v_averageSalaryDetailedReport> GetAverageSalaryDetailedReport(Range<DateTime> rangeDateOfConclusionContract, List<object> selectedProfessionCategories, List<object> selectedProfessions)
        {
            var report = _context.v_averageSalaryDetailedReport.Where(r =>
            r.Date >= rangeDateOfConclusionContract.BeginValue &&
            r.Date <= rangeDateOfConclusionContract.EndValue).AsNoTracking().ToList();

            return report.Where(r =>
            (selectedProfessionCategories.Count > 0 ? selectedProfessionCategories.Any(p => Convert.ToInt32(p) == r.IdProfessionCategory) : false) ||
            (selectedProfessions.Count > 0 ? selectedProfessions.Any(p => Convert.ToInt32(p) == r.IdProfession) : false)).OrderBy(r => r.Date).ToList();
        }

        public List<v_bestManagersDetailedReport> GetBestManagersDetailedReport(DateTime? beginValueDateOfConsiderationRequest, DateTime? endValueDateOfConsiderationRequest, List<object> selectedFullNameManagers)
        {
            var report = _context.v_bestManagersDetailedReport.Where(r =>
            (beginValueDateOfConsiderationRequest != null ? r.Date >= beginValueDateOfConsiderationRequest : true) &&
            (endValueDateOfConsiderationRequest  != null ? r.Date <= endValueDateOfConsiderationRequest : true)).AsNoTracking().ToList();

            return report.Where(r =>
            (selectedFullNameManagers.Count > 0 ? selectedFullNameManagers.Any(f => Convert.ToInt32(f) == r.IdManager) : false)).OrderBy(r => r.Date).ToList();
        }

        public v_branch GetBranch(int idBranch)
        {
            return _context.v_branch.SingleOrDefault(b => b.IdBranch == idBranch);
        }

        public List<v_branchSimplifiedInformation> GetBranches(int idOrganization)
        {
            return _context.v_branchSimplifiedInformation.Where(b => b.IdOrganization == idOrganization).AsNoTracking().ToList();
        }

        public List<v_branch> GetBranches(int idIndustry, int idSubIndustry, int idCountry, int idCity, int idStreet, int idOrganization, string organizationName)
        {
            return _context.v_branch.Where(b =>
            (idIndustry != -1 ? b.IdIndustry == idIndustry : true) &&
            (idSubIndustry != -1 ? b.IdSubIndustry == idSubIndustry : true) &&
            (idCountry != -1 ? b.IdCountry == idCountry : true) &&
            (idCity != -1 ? b.IdCity == idCity : true) &&
            (idStreet != -1 ? b.IdStreet == idStreet : true) &&
            (idOrganization != -1 ? b.IdOrganization == idOrganization : true) &&
            (organizationName.Length > 0 ? b.OrganizationName.ToLower().Contains(organizationName.ToLower()) : true)).OrderBy(b => b.OrganizationName).AsNoTracking().ToList();
        }

        public List<City> GetCities(int idCountry)
        {
            return _context.City.Where(c => c.IdCountry == idCountry).OrderBy(c => c.CityName).AsNoTracking().ToList();
        }

        public List<v_city> GetCities()
        {
            return _context.v_city.OrderBy(c => c.CityName).AsNoTracking().ToList();
        }

        public List<v_city> GetCities(int idCountry, string cityName)
        {
            return _context.v_city.Where(c =>
            (idCountry != -1 ? c.IdCountry == idCountry : true) &&
            (cityName.Length > 0 ? c.CityName.ToLower().Contains(cityName.ToLower()) : true)).OrderBy(c => c.CityName).AsNoTracking().ToList();
        }

        public City GetCity(int idCity)
        {
            return _context.City.SingleOrDefault(c => c.IdCity == idCity);
        }

        public Contract GetContract(int idContract)
        {
            return _context.Contract.SingleOrDefault(c => c.IdContract == idContract);
        }

        public List<v_contract> GetContractsForApplicant(int idApplicant, string vacancyOrganizationName, Range<DateTime> rangeDateOfConclusion, DateTime? beginValueBreakDate, DateTime? endValueBreakDate, int? beginValueSalary, int? endValueSalary)
        {
            return _context.v_contract.Where(c =>
            c.IdApplicant == idApplicant &&
            (vacancyOrganizationName.Length > 0 ? c.VacancyOrganizationName.ToLower().Contains(vacancyOrganizationName.ToLower()) : true) &&
            c.DateOfConclusion >= rangeDateOfConclusion.BeginValue &&
            c.DateOfConclusion <= rangeDateOfConclusion.EndValue &&
            (beginValueBreakDate != null ? c.BreakDate >= beginValueBreakDate : true) &&
            (endValueBreakDate != null ? c.BreakDate <= endValueBreakDate : true) &&
            (c.VacancySalary != null ? c.VacancySalary >= beginValueSalary : true) &&
            (c.VacancySalary != null ? c.VacancySalary <= endValueSalary : true)).AsNoTracking().ToList();
        }

        public List<v_contract> GetContractsForEmployer(int idEmployer, string applicantFullName, Range<DateTime> rangeDateOfConclusion, DateTime? beginValueBreakDate, DateTime? endValueBreakDate, int? beginValueSalary, int? endValueSalary)
        {
            return _context.v_contract.Where(c =>
            c.IdEmployer == idEmployer &&
            (applicantFullName.Length > 0 ? c.ApplicantFullName.ToLower().Contains(applicantFullName.ToLower()) : true) &&
            c.DateOfConclusion >= rangeDateOfConclusion.BeginValue &&
            c.DateOfConclusion <= rangeDateOfConclusion.EndValue &&
            (beginValueBreakDate != null ? c.BreakDate >= beginValueBreakDate : true) &&
            (endValueBreakDate != null ? c.BreakDate <= endValueBreakDate : true) &&
            (c.VacancySalary != null ? c.VacancySalary >= beginValueSalary : true) &&
            (c.VacancySalary != null ? c.VacancySalary <= endValueSalary : true)).AsNoTracking().ToList();
        }

        public List<Country> GetCountries()
        {
            return _context.Country.OrderBy(c => c.CountryName).AsNoTracking().ToList();
        }

        public List<Country> GetCountries(string countryName)
        {
            return _context.Country.Where(c =>
            (countryName.Length > 0 ? c.CountryName.ToLower().Contains(countryName.ToLower()) : true)).OrderBy(c => c.CountryName).AsNoTracking().ToList();
        }

        public Country GetCountry(int idCountry)
        {
            return _context.Country.SingleOrDefault(c => c.IdCountry == idCountry);
        }

        public List<v_demandedProfessionsDetailedReport> GetDemandedProfessionsDetailedReports(Range<DateTime> rangeDateOfRegistrationVacancy, List<object> selectedProfessionCategories, List<object> selectedProfessions)
        {
            var report = _context.v_demandedProfessionsDetailedReport.Where(r =>
            r.Date >= rangeDateOfRegistrationVacancy.BeginValue &&
            r.Date <= rangeDateOfRegistrationVacancy.EndValue).AsNoTracking().ToList();

            return report.Where(r =>
            (selectedProfessionCategories.Count > 0 ? selectedProfessionCategories.Any(p => Convert.ToInt32(p) == r.IdProfessionCategory) : false) ||
            (selectedProfessions.Count > 0 ? selectedProfessions.Any(p => Convert.ToInt32(p) == r.IdProfession) : false)).OrderBy(r => r.Date).ToList();
        }

        public List<DrivingLicenseCategory> GetDrivingLicenseCategories()
        {
            return _context.DrivingLicenseCategory.OrderBy(d => d.DrivingLicenseCategoryName).AsNoTracking().ToList();
        }

        public List<DrivingLicenseCategory> GetDrivingLicenseCategories(string drivingLicenseCategoryName)
        {
            return _context.DrivingLicenseCategory.Where(d =>
            (drivingLicenseCategoryName.Length > 0 ? d.DrivingLicenseCategoryName.ToLower().Contains(drivingLicenseCategoryName.ToLower()) : true)).OrderBy(d => d.DrivingLicenseCategoryName).AsNoTracking().ToList();
        }

        public List<DrivingLicenseCategory> GetDrivingLicenseCategories(int idApplicant)
        {
            var selectedDrivingLicenseCategories = new List<DrivingLicenseCategory>();

            _context.PossessionDrivingLicenseCategory.Where(p => p.IdApplicant == idApplicant).AsNoTracking().ToList().ForEach(p =>
            selectedDrivingLicenseCategories.Add(p.DrivingLicenseCategory));

            return selectedDrivingLicenseCategories;
        }

        public DrivingLicenseCategory GetDrivingLicenseCategory(int idDrivingLicenseCategory)
        {
            return _context.DrivingLicenseCategory.SingleOrDefault(d => d.IdDrivingLicenseCategory == idDrivingLicenseCategory);
        }

        public Education GetEducation(int idEducation)
        {
            return _context.Education.SingleOrDefault(e => e.IdEducation == idEducation);
        }

        public List<EducationalActivity> GetEducationActivities(int idApplicant)
        {
            return _context.EducationalActivity.Where(e => e.IdApplicant == idApplicant).ToList();
        }

        public List<Education> GetEducations()
        {
            return _context.Education.OrderBy(e => e.EducationName).AsNoTracking().ToList();
        }

        public List<Education> GetEducations(string educationName)
        {
            return _context.Education.Where(e =>
            (educationName.Length > 0 ? e.EducationName.ToLower().Contains(educationName.ToLower()) : true)).OrderBy(e => e.EducationName).AsNoTracking().ToList();
        }

        public Employer GetEmployer(int idEmployer)
        {
            return _context.Employer.SingleOrDefault(e => e.IdEmployer == idEmployer);
        }

        public v_employer GetEmployerExtendedInformation(int idEmployer)
        {
            return _context.v_employer.SingleOrDefault(e => e.IdEmployer == idEmployer);
        }

        public EmploymentRequest GetEmploymentRequest(int idEmploymentRequest)
        {
            return _context.EmploymentRequest.SingleOrDefault(e => e.IdEmploymentRequest == idEmploymentRequest);
        }

        public EmploymentType GetEmploymentType(int idEmploymentType)
        {
            return _context.EmploymentType.SingleOrDefault(e => e.IdEmploymentType == idEmploymentType);
        }

        public List<EmploymentType> GetEmploymentTypes(string employmentTypeName)
        {
            return _context.EmploymentType.Where(e =>
            (employmentTypeName.Length > 0 ? e.EmploymentTypeName.ToLower().Contains(employmentTypeName.ToLower()) : true)).OrderBy(e => e.EmploymentTypeName).AsNoTracking().ToList();
        }

        public List<EmploymentType> GetEmploymentTypes()
        {
            return _context.EmploymentType.AsNoTracking().ToList();
        }

        public Experience GetExperience(int idExperience)
        {
            return _context.Experience.SingleOrDefault(e => e.IdExperience == idExperience);
        }

        public List<Experience> GetExperiences(string experienceName)
        {
            return _context.Experience.Where(e =>
            (experienceName.Length > 0 ? e.ExperienceName.ToLower().Contains(experienceName.ToLower()) : true)).OrderBy(e => e.ExperienceName).AsNoTracking().ToList();
        }

        public List<Experience> GetExperiences()
        {
            return _context.Experience.AsNoTracking().ToList();
        }

        public Gender GetGender(int idGender)
        {
            return _context.Gender.SingleOrDefault(g => g.IdGender == idGender);
        }

        public List<Gender> GetGenders()
        {
            return _context.Gender.OrderBy(g => g.GenderName).AsNoTracking().ToList();
        }

        public List<Gender> GetGenders(string genderName)
        {
            return _context.Gender.Where(g =>
            (genderName.Length > 0 ? g.GenderName.ToLower().Contains(genderName.ToLower()) : true)).OrderBy(g => g.GenderName).AsNoTracking().ToList();
        }

        public int GetIdRequestStatus(string requestStatusName)
        {
            return _context.RequestStatus.SingleOrDefault(r => r.RequestStatusName == requestStatusName).IdRequestStatus;
        }

        public int GetIdRole(string roleName)
        {
            return _context.Role.Single(r => r.RoleName == roleName).IdRole;
        }

        public List<v_inDemandSkillsDetailedReport> GetInDemandSkillsDetailedReports(Range<DateTime> rangeDateOfRegistrationVacancy, List<object> selectedSkills)
        {
            var report = _context.v_inDemandSkillsDetailedReport.Where(r =>
            r.Date >= rangeDateOfRegistrationVacancy.BeginValue &&
            r.Date <= rangeDateOfRegistrationVacancy.EndValue).AsNoTracking().ToList();

            return report.Where(r =>
            (selectedSkills.Count > 0 ? selectedSkills.Any(s => Convert.ToInt32(s) == r.IdSkill) : false)).OrderBy(r => r.Date).ToList();
        }

        public List<Industry> GetIndustries()
        {
            return _context.Industry.OrderBy(i => i.IndustryName).AsNoTracking().ToList();
        }

        public List<Industry> GetIndustries(string industryName)
        {
            return _context.Industry.Where(i =>
            (industryName.Length > 0 ? i.IndustryName.ToLower().Contains(industryName.ToLower()) : true)).OrderBy(i => i.IndustryName).AsNoTracking().ToList();
        }

        public Industry GetIndustry(int idIndustry)
        {
            return _context.Industry.SingleOrDefault(i => i.IdIndustry == idIndustry);
        }

        public List<KnowledgeLanguage> GetKnowledgeLanguages(int idApplicant)
        {
            return _context.KnowledgeLanguage.Where(k => k.IdApplicant == idApplicant).ToList();
        }

        public List<LaborActivity> GetLaborActivities(int idApplicant)
        {
            return _context.LaborActivity.Where(l => l.IdApplicant == idApplicant).ToList();
        }

        public Language GetLanguage(int idLanguage)
        {
            return _context.Language.SingleOrDefault(l => l.IdLanguage == idLanguage);
        }

        public List<LanguageProficiency> GetLanguageProficiencies()
        {
            return _context.LanguageProficiency.OrderBy(l => l.Designation).AsNoTracking().ToList();
        }

        public List<LanguageProficiency> GetLanguageProficiencies(string languageProficiencyName)
        {
            return _context.LanguageProficiency.Where(l =>
            (languageProficiencyName.Length > 0 ? l.LanguageProficiencyName.ToLower().Contains(languageProficiencyName.ToLower()) : true)).OrderBy(l => l.Designation).AsNoTracking().ToList();
        }

        public LanguageProficiency GetLanguageProficiency(int idLanguageProficiency)
        {
            return _context.LanguageProficiency.SingleOrDefault(l => l.IdLanguageProficiency == idLanguageProficiency);
        }

        public List<Language> GetLanguages()
        {
            return _context.Language.OrderBy(l => l.LanguageName).AsNoTracking().ToList();
        }

        public List<Language> GetLanguages(string languageName)
        {
            return _context.Language.Where(l =>
            (languageName.Length > 0 ? l.LanguageName.ToLower().Contains(languageName.ToLower()) : true)).OrderBy(l => l.LanguageName).AsNoTracking().ToList();
        }

        public Manager GetManager(int idManager)
        {
            return _context.Manager.SingleOrDefault(m => m.IdManager == idManager);
        }

        public v_manager GetManagerExtendedInformation(int idManager)
        {
            return _context.v_manager.SingleOrDefault(m => m.IdManager == idManager);
        }

        public List<v_manager> GetManagers()
        {
            return _context.v_manager.AsNoTracking().ToList();
        }

        public DateTime? GetMaxBreakDateMyContractsApplicant(int idApplicant)
        {
            return _context.v_contract.Where(c => c.IdApplicant == idApplicant).AsNoTracking().Max(c => c.BreakDate);
        }

        public DateTime? GetMaxBreakDateMyContractsEmployer(int idEmployer)
        {
            return _context.v_contract.Where(c => c.IdEmployer == idEmployer).AsNoTracking().Max(c => c.BreakDate);
        }

        public DateTime? GetMaxClosingDateMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Max(v => v.ClosingDate);
        }

        public DateTime? GetMaxClosingDateVacancy()
        {
            return _context.Vacancy.Max(v => v.ClosingDate);
        }

        public DateTime GetMaxDateOfConclusionContract()
        {
            var contracts = _context.Contract.AsNoTracking();

            return contracts.Count() > 0 ? contracts.Max(c => c.DateOfConclusion) : DateTime.Now;
        }

        public DateTime GetMaxDateOfConclusionMyContractsApplicant(int idApplicant)
        {
            var contracts = _context.v_contract.Where(c => c.IdApplicant == idApplicant).AsNoTracking();

            return contracts.Count() > 0 ? contracts.Max(c => c.DateOfConclusion) : DateTime.Now;
        }

        public DateTime GetMaxDateOfConclusionMyContractsEmployer(int idEmployer)
        {
            var contract = _context.v_contract.Where(c => c.IdEmployer == idEmployer).AsNoTracking();

            return contract.Count() > 0 ? contract.Max(c => c.DateOfConclusion) : DateTime.Now;
        }

        public DateTime? GetMaxDateOfConsiderationRequest()
        {
            return _context.Request.Max(r => r.DateOfConsideration);
        }

        public DateTime GetMaxDateOfRegistrationMyRequest(int idApplicant)
        {
            var request = _context.Request.Where(r => r.IdApplicant == idApplicant).AsNoTracking();

            return request.Count() > 0 ? request.Max(r => r.DateOfRegistration) : DateTime.Now;
        }

        public DateTime GetMaxDateOfRegistrationMyVacancy(int idEmployer)
        {
            var vacancies = _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking();

            return vacancies.Count() > 0 ? vacancies.Max(v => v.DateOfRegistration) : DateTime.Now;
        }

        public DateTime GetMaxDateOfRegistrationRequest()
        {
            var requests = _context.Request.AsNoTracking();

            return requests.Count() > 0 ? requests.Max(r => r.DateOfRegistration) : DateTime.Now;
        }

        public DateTime GetMaxDateOfRegistrationVacancy()
        {
            var vacancies = _context.Vacancy.AsNoTracking();

            return vacancies.Count() > 0 ? vacancies.Max(v => v.DateOfRegistration) : DateTime.Now;
        }

        public int? GetMaxNumberOfAcceptedApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Max(v => v.NumberOfAcceptedApplicants);
        }

        public int? GetMaxNumberOfApprovedApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Max(v => v.NumberOfApprovedApplicants);
        }

        public int? GetMaxNumberOfInterestedApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Max(v => v.NumberOfInterestedApplicants);
        }

        public int? GetMaxNumberOfPotentialApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Max(v => v.NumberOfPotentialApplicants);
        }

        public int? GetMaxSalaryMyContractsApplicant(int idApplicant)
        {
            return _context.v_contract.Where(c => c.IdApplicant == idApplicant).AsNoTracking().Max(c => c.VacancySalary);
        }

        public int? GetMaxSalaryMyContractsEmployer(int idEmployer)
        {
            return _context.v_contract.Where(c => c.IdEmployer == idEmployer).AsNoTracking().Max(c => c.VacancySalary);
        }

        public int? GetMaxSalaryMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Max(v => v.Salary);
        }

        public int? GetMaxSalaryVacancy()
        {
            return _context.Vacancy.Max(v => v.Salary);
        }

        public DateTime? GetMinBreakDateMyContractsApplicant(int idApplicant)
        {
            return _context.v_contract.Where(c => c.IdApplicant == idApplicant).AsNoTracking().Min(c => c.BreakDate);
        }

        public DateTime? GetMinBreakDateMyContractsEmployer(int idEmployer)
        {
            return _context.v_contract.Where(c => c.IdEmployer == idEmployer).AsNoTracking().Min(c => c.BreakDate);
        }

        public DateTime? GetMinClosingDateMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Min(v => v.ClosingDate);
        }

        public DateTime? GetMinClosingDateVacancy()
        {
            return _context.Vacancy.Min(v => v.ClosingDate);
        }

        public DateTime GetMinDateOfConclusionContract()
        {
            var contracts = _context.Contract.AsNoTracking();

            return contracts.Count() > 0 ? contracts.Min(c => c.DateOfConclusion) : DateTime.Now;
        }

        public DateTime GetMinDateOfConclusionMyContractsApplicant(int idApplicant)
        {
            var contracts = _context.v_contract.Where(c => c.IdApplicant == idApplicant).AsNoTracking();

            return contracts.Count() > 0 ? contracts.Min(c => c.DateOfConclusion) : DateTime.Now;
        }

        public DateTime GetMinDateOfConclusionMyContractsEmployer(int idEmployer)
        {
            var contracts = _context.v_contract.Where(c => c.IdEmployer == idEmployer).AsNoTracking();

            return contracts.Count() > 0 ? contracts.Min(c => c.DateOfConclusion) : DateTime.Now;
        }

        public DateTime? GetMinDateOfConsiderationRequest()
        {
            return _context.Request.Min(r => r.DateOfConsideration);
        }

        public DateTime GetMinDateOfRegistrationMyRequest(int idApplicant)
        {
            var requests = _context.Request.Where(r => r.IdApplicant == idApplicant).AsNoTracking();

            return requests.Count() > 0 ? requests.Min(r => r.DateOfRegistration) : DateTime.Now;
        }

        public DateTime GetMinDateOfRegistrationMyVacancy(int idEmployer)
        {
            var vacancies = _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking();

            return vacancies.Count() > 0 ? vacancies.Min(v => v.DateOfRegistration) : DateTime.Now;
        }

        public DateTime GetMinDateOfRegistrationRequest()
        {
            var requests = _context.Request.AsNoTracking();

            return requests.Count() > 0 ? requests.Min(r => r.DateOfRegistration) : DateTime.Now;
        }

        public DateTime GetMinDateOfRegistrationVacancy()
        {
            var vacancies = _context.Vacancy.AsNoTracking();

            return vacancies.Count() > 0 ? vacancies.Min(v => v.DateOfRegistration) : DateTime.Now;
        }

        public int? GetMinNumberOfAcceptedApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Min(v => v.NumberOfAcceptedApplicants);
        }

        public int? GetMinNumberOfApprovedApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Min(v => v.NumberOfApprovedApplicants);
        }

        public int? GetMinNumberOfInterestedApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Min(v => v.NumberOfInterestedApplicants);
        }

        public int? GetMinNumberOfPotentialApplicantsMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Min(v => v.NumberOfPotentialApplicants);
        }

        public int? GetMinSalaryMyContractsApplicant(int idApplicant)
        {
            return _context.v_contract.Where(c => c.IdApplicant == idApplicant).Min(c => c.VacancySalary);
        }

        public int? GetMinSalaryMyContractsEmployer(int idEmployer)
        {
            return _context.v_contract.Where(c => c.IdEmployer == idEmployer).Min(c => c.VacancySalary);
        }

        public int? GetMinSalaryMyVacancy(int idEmployer)
        {
            return _context.v_vacancy.Where(v => v.IdEmployer == idEmployer).AsNoTracking().Min(v => v.Salary);
        }

        public int? GetMinSalaryVacancy()
        {
            return _context.Vacancy.Min(v => v.Salary);
        }

        public List<v_request> GetMyRequests(int idApplicant, int idProfessionCategory, int idProfession, int idRequestStatus, string professionName, Range<DateTime> rangeDateOfRegistration)
        {
            return _context.v_request.Where(r =>
            r.IdApplicant == idApplicant &&
            (idProfessionCategory != -1 ? r.IdProfessionCategory == idProfessionCategory : true) &&
            (idProfession != -1 ? r.IdProfession == idProfession : true) &&
            (idRequestStatus != -1 ? r.IdRequestStatus == idRequestStatus : true) &&
            (professionName.Length > 0 ? r.ProfessionName.ToLower().Contains(professionName.ToLower()) : true) &&
            r.DateOfRegistration >= rangeDateOfRegistration.BeginValue &&
            r.DateOfRegistration <= rangeDateOfRegistration.EndValue).AsNoTracking().ToList();
        }

        public List<v_vacancy> GetMyVacancies(int idEmployer, int idProfessionCategory, int idProfession, string professionName, DateTime? beginValueClosingDate, DateTime? endValueClosingDate, DateTime beginValueDateOfRegistration, DateTime endValueDateOfRegistration, int? beginValueNumberOfAcceptedApplicants, int? endValueNumberOfAcceptedApplicants, int? beginValueNumberOfApprovedApplicants, int? endValueNumberOfApprovedApplicants, int? beginValueNumberOfInterestedApplicants, int? endValueNumberOfInterestedApplicants, int? beginValueNumberOfPotentialApplicants, int? endValueNumberOfPotentialApplicants, int? beginValueSalary, int? endValueSalary)
        {
            return _context.v_vacancy.Where(v =>
            v.IdEmployer == idEmployer &&
            (idProfessionCategory != -1 ? v.IdProfessionCategory == idProfessionCategory : true) &&
            (idProfession != -1 ? v.IdProfession == idProfession : true) &&
            (professionName.Length > 0 ? v.ProfessionName.ToLower().Contains(professionName.ToLower()) : true) &&
            (beginValueClosingDate != null ? v.ClosingDate >= beginValueClosingDate : true) &&
            (endValueClosingDate != null ? v.ClosingDate <= endValueClosingDate : true) &&
            v.DateOfRegistration >= beginValueDateOfRegistration &&
            v.DateOfRegistration <= endValueDateOfRegistration &&
            v.NumberOfAcceptedApplicants >= beginValueNumberOfAcceptedApplicants &&
            v.NumberOfAcceptedApplicants <= endValueNumberOfAcceptedApplicants &&
            v.NumberOfApprovedApplicants >= beginValueNumberOfApprovedApplicants &&
            v.NumberOfApprovedApplicants <= endValueNumberOfApprovedApplicants &&
            v.NumberOfInterestedApplicants >= beginValueNumberOfInterestedApplicants &&
            v.NumberOfInterestedApplicants <= endValueNumberOfInterestedApplicants &&
            v.NumberOfPotentialApplicants >= beginValueNumberOfPotentialApplicants &&
            v.NumberOfPotentialApplicants <= endValueNumberOfPotentialApplicants &&
            (v.Salary != null ?  v.Salary >= beginValueSalary : true) &&
            (v.Salary != null ? v.Salary <= endValueSalary : true)).AsNoTracking().ToList();
        }

        public List<NecessarySkill> GetNecessarySkills(int idVacancy)
        {
            return _context.NecessarySkill.Where(n => n.IdVacancy == idVacancy).ToList();
        }

        public List<NecessarySkill> GetNecessarySkills()
        {
            return _context.NecessarySkill.AsNoTracking().ToList();
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
            (organizationName.Length > 0 ? o.OrganizationName.ToLower().Contains(organizationName.ToLower()) : true)).OrderBy(o => o.OrganizationName).AsNoTracking().ToList();
        }

        public List<v_organizationWithoutPhoto> GetOrganizationsWithoutPhoto()
        {
            return _context.v_organizationWithoutPhoto.OrderBy(o => o.OrganizationName).AsNoTracking().ToList();
        }

        public List<PossessionDrivingLicenseCategory> GetPossessionDrivingLicenseCategories(int idApplicant)
        {
            return _context.PossessionDrivingLicenseCategory.Where(p => p.IdApplicant == idApplicant).ToList();
        }

        public List<PossessionSkill> GetPossessionSkills(int idApplicant)
        {
            return _context.PossessionSkill.Where(p => p.IdApplicant == idApplicant).ToList();
        }

        public List<v_suitableVacancy> GetPotentialApplicants(int idVacancy)
        {
            return _context.v_suitableVacancy.Where(s =>
            s.IdVacancy == idVacancy).AsNoTracking().ToList();
        }

        public List<PreferredEmploymentType> GetPreferredEmploymentTypes(int idRequest)
        {
            return _context.PreferredEmploymentType.Where(p => p.IdRequest == idRequest).ToList();
        }

        public List<PreferredSchedule> GetPreferredSchedules(int idRequest)
        {
            return _context.PreferredSchedule.Where(p => p.IdRequest == idRequest).ToList();
        }

        public Profession GetProfession(int idProfession)
        {
            return _context.Profession.SingleOrDefault(p => p.IdProfession == idProfession);
        }

        public List<ProfessionCategory> GetProfessionCategories(string nameProfessionCategory)
        {
            return _context.ProfessionCategory.Where(p =>
            (nameProfessionCategory.Length > 0 ? p.NameProfessionCategory.ToLower().Contains(nameProfessionCategory.ToLower()) : true)).OrderBy(p => p.NameProfessionCategory).AsNoTracking().ToList();
        }

        public List<ProfessionCategory> GetProfessionCategories()
        {
            return _context.ProfessionCategory.OrderBy(o => o.NameProfessionCategory).AsNoTracking().ToList();
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
            (professionName.Length > 0 ? p.ProfessionName.ToLower().Contains(professionName.ToLower()) : true)).OrderBy(p => p.ProfessionName).AsNoTracking().ToList();
        }

        public List<Profession> GetProfessions(int idProfessionCategory)
        {
            return _context.Profession.Where(p => p.IdProfessionCategory == idProfessionCategory).AsNoTracking().ToList();
        }

        public List<Profession> GetProfessions()
        {
            return _context.Profession.OrderBy(p => p.ProfessionName).AsNoTracking().ToList();
        }

        public List<v_employmentRequest> GetReceivedEmploymentRequests(int idVacancy)
        {
            return _context.v_employmentRequest.Where(e =>
            e.IdVacancy == idVacancy).AsNoTracking().ToList();
        }

        public Request GetRequest(int idRequest)
        {
            return _context.Request.SingleOrDefault(r => r.IdRequest == idRequest);
        }

        public v_request GetRequestExtendedInformation(int idRequest)
        {
            return _context.v_request.SingleOrDefault(r => r.IdRequest == idRequest);
        }

        public List<v_request> GetRequests(int idRequestStatus, string professionName, Range<DateTime> rangeDateOfRegistration, DateTime? beginValueDateOfConsideration, DateTime? endValueDateOfConsideration)
        {
            return _context.v_request.Where(r =>
            (idRequestStatus != -1 ? r.IdRequestStatus == idRequestStatus : true) &&
            (professionName.Length > 0 ? r.ProfessionName.ToLower().Contains(professionName.ToLower()) : true) &&
            r.DateOfRegistration >= rangeDateOfRegistration.BeginValue &&
            r.DateOfRegistration <= rangeDateOfRegistration.EndValue &&
            (beginValueDateOfConsideration != null ? r.DateOfConsideration >= beginValueDateOfConsideration : true) &&
            (endValueDateOfConsideration != null ? r.DateOfConsideration <= endValueDateOfConsideration : true)).AsNoTracking().ToList();
        }

        public RequestStatus GetRequestStatus(int idRequestStatus)
        {
            return _context.RequestStatus.SingleOrDefault(r => r.IdRequestStatus == idRequestStatus);
        }

        public List<RequestStatus> GetRequestStatuses(string requestStatusName)
        {
            return _context.RequestStatus.Where(r =>
            (requestStatusName.Length > 0 ? r.RequestStatusName.ToLower().Contains(requestStatusName.ToLower()) : true)).OrderBy(r => r.RequestStatusName).AsNoTracking().ToList();
        }

        public Role GetRole(int idRole)
        {
            return _context.Role.SingleOrDefault(r => r.IdRole == idRole);
        }

        public List<Role> GetRoles()
        {
            return _context.Role.OrderBy(r => r.RoleName).AsNoTracking().ToList();
        }

        public List<Role> GetRoles(string roleName)
        {
            return _context.Role.Where(r =>
            (roleName.Length > 0 ? r.RoleName.ToLower().Contains(roleName.ToLower()) : true)).OrderBy(r => r.RoleName).AsNoTracking().ToList();
        }

        public Schedule GetSchedule(int idSchedule)
        {
            return _context.Schedule.SingleOrDefault(s => s.IdSchedule == idSchedule);
        }

        public List<Schedule> GetSchedules(string scheduleName)
        {
            return _context.Schedule.Where(s =>
            (scheduleName.Length > 0 ? s.ScheduleName.ToLower().Contains(scheduleName.ToLower()) : true)).OrderBy(s => s.ScheduleName).AsNoTracking().ToList();
        }

        public List<Schedule> GetSchedules()
        {
            return _context.Schedule.AsNoTracking().ToList();
        }

        public List<v_employmentRequest> GetSentEmploymentRequests(int idRequest)
        {
            return _context.v_employmentRequest.Where(e =>
            e.IdRequest == idRequest).AsNoTracking().ToList();
        }

        public Skill GetSkill(int idSkill)
        {
            return _context.Skill.SingleOrDefault(s => s.IdSkill == idSkill);
        }

        public List<Skill> GetSkills(int idApplicant)
        {
            var selectedSkills = new List<Skill>();

            _context.PossessionSkill.Where(p => p.IdApplicant == idApplicant).AsNoTracking().ToList().ForEach(p =>
            selectedSkills.Add(p.Skill));

            return selectedSkills;
        }
        public List<Skill> GetSkills()
        {
            return _context.Skill.OrderBy(s => s.SkillName).AsNoTracking().ToList();
        }

        public List<Skill> GetSkills(string skillName)
        {
            return _context.Skill.Where(s =>
            (skillName.Length > 0 ? s.SkillName.ToLower().Contains(skillName.ToLower()) : true)).OrderBy(s => s.SkillName).AsNoTracking().ToList();
        }

        public Street GetStreet(int idStreet)
        {
            return _context.Street.SingleOrDefault(s => s.IdStreet == idStreet);
        }

        public v_street GetStreetExtendedInformation(int idStreet)
        {
            return _context.v_street.SingleOrDefault(s => s.IdStreet == idStreet);
        }

        public List<Street> GetStreets(int idCity)
        {
            return _context.Street.Where(s => s.IdCity == idCity).OrderBy(s => s.StreetName).AsNoTracking().ToList();
        }

        public List<v_street> GetStreets()
        {
            return _context.v_street.OrderBy(s => s.StreetName).AsNoTracking().ToList();
        }

        public List<v_street> GetStreets(int idCountry, int idCity, string streetName)
        {
            return _context.v_street.Where(s =>
            (idCountry != -1 ? s.IdCountry == idCountry : true) &&
            (idCity != -1 ? s.IdCity == idCity : true) &&
            (streetName.Length > 0 ? s.StreetName.ToLower().Contains(streetName.ToLower()) : true)).OrderBy(s => s.StreetName).AsNoTracking().ToList();
        }

        public List<SubIndustry> GetSubIndustries(int idIndustry)
        {
            return _context.SubIndustry.Where(s => s.IdIndustry == idIndustry).OrderBy(s => s.NameSubIndustry).AsNoTracking().ToList();
        }

        public List<v_subIndustry> GetSubIndustries(int idIndustry, string nameSubIndustry)
        {
            return _context.v_subIndustry.Where(s =>
            (idIndustry != -1 ? s.IdIndustry == idIndustry : true) &&
            (nameSubIndustry.Length > 0 ? s.NameSubIndustry.ToLower().Contains(nameSubIndustry.ToLower()) : true)).OrderBy(s => s.NameSubIndustry).AsNoTracking().ToList();
        }

        public SubIndustry GetSubIndustry(int idSubIndustry)
        {
            return _context.SubIndustry.SingleOrDefault(s => s.IdSubIndustry == idSubIndustry);
        }

        public v_subIndustry GetSubIndustryExtendedInformation(int idSubIndustry)
        {
            return _context.v_subIndustry.SingleOrDefault(s => s.IdSubIndustry == idSubIndustry);
        }

        public List<SuitableVacancy> GetSuitableVacancies(int idRequest)
        {
            return _context.SuitableVacancy.Where(s =>
            s.IdRequest == idRequest).AsNoTracking().ToList();
        }

        public List<SuitableVacancy> GetSuitableVacanciesTracking(int idRequest)
        {
            return _context.SuitableVacancy.Where(s =>
            s.IdRequest == idRequest).ToList();
        }

        public List<v_unemploymentReport> GetUnemploymentReports(Range<DateTime> rangeDateOfRegistrationRequest)
        {
            return _context.v_unemploymentReport.Where(r =>
            r.Date >= rangeDateOfRegistrationRequest.BeginValue &&
            r.Date <= rangeDateOfRegistrationRequest.EndValue).OrderBy(r => r.Date).AsNoTracking().ToList();
        }

        public User GetUser(int idUser)
        {
            return _context.User.SingleOrDefault(u => u.IdUser == idUser);
        }

        public v_user GetUserExtendedInformation(int idUser)
        {
            return _context.v_user.SingleOrDefault(u => u.IdUser == idUser);
        }

        public List<v_user> GetUsers(int idRole, string login)
        {
            return _context.v_user.Where(u =>
            (idRole != -1 ? u.IdRole == idRole : true) &&
            (login.Length > 0 ? u.Login.ToLower().Contains(login.ToLower()) : true)).OrderBy(u => u.Login).AsNoTracking().ToList();
        }

        public List<v_vacancy> GetVacancies(int idProfessionCategory, int idProfession, int idCountry, int idCity, int idStreet, string professionName, List<object> selectedExperiences, List<object> selectedSchedules, List<object> selectedEmploymentTypes, List<object> selectedSkills, Range<DateTime> rangeDateOfRegistration, DateTime? beginValueClosingDate, DateTime? endValueClosingDate, int? beginValueSalary, int? endValueSalary)
        {
            var necessarySkills = GetNecessarySkills();

            var vacancies = _context.v_vacancy.Where(v =>
            (idProfessionCategory != -1 ? v.IdProfessionCategory == idProfessionCategory : true) &&
            (idProfession != -1 ? v.IdProfession == idProfession : true) &&
            (idCountry != -1 ? v.IdCountry == idCountry : true) &&
            (idCity != -1 ? v.IdCity == idCity : true) &&
            (idStreet != -1 ? v.IdStreet == idStreet : true) &&
            (professionName.Length > 0 ? v.ProfessionName.ToLower().Contains(professionName.ToLower()) : true) &&
            v.DateOfRegistration >= rangeDateOfRegistration.BeginValue &&
            v.DateOfRegistration <= rangeDateOfRegistration.EndValue &&
            (beginValueClosingDate != null ? v.ClosingDate >= beginValueClosingDate : true) &&
            (endValueClosingDate != null ? v.ClosingDate <= endValueClosingDate : true) &&
            (v.Salary != null ? v.Salary >= beginValueSalary : true) && 
            (v.Salary != null ? v.Salary <= endValueSalary : true)).AsNoTracking().ToList();

            vacancies = vacancies.Where(v =>
            (selectedExperiences.Count > 0 ? selectedExperiences.Any(e => Convert.ToInt32(e) == v.IdExperience) : true) &&
            (selectedSchedules.Count > 0 ? selectedSchedules.Any(s => Convert.ToInt32(s) == v.IdSchedule) : true) &&
            (selectedEmploymentTypes.Count > 0 ? selectedEmploymentTypes.Any(e => Convert.ToInt32(e) == v.IdEmploymentType) : true) &&
            (selectedSkills.Count > 0 ? selectedSkills.All(s => necessarySkills.Any(n => n.IdVacancy == v.IdVacancy && n.IdSkill == Convert.ToInt32(s))) : true)).ToList();

            return vacancies;
        }

        public Vacancy GetVacancy(int idVacancy)
        {
            return _context.Vacancy.SingleOrDefault(v => v.IdVacancy == idVacancy);
        }

        public v_vacancy GetVacancyExtendedInformation(int idVacancy)
        {
            return _context.v_vacancy.SingleOrDefault(v => v.IdVacancy == idVacancy);
        }

        public bool IsBrokeContract(int idContract)
        {
            return GetContract(idContract).BreakDate != null;
        }

        public bool IsClosedVacancy(int idVacancy)
        {
            return GetVacancy(idVacancy).ClosingDate != null;
        }

        public bool IsRelatedToStaff(int idUser)
        {
            var user = GetUserExtendedInformation(idUser);

            return user.RoleName == "Администратор" || user.RoleName == "Менеджер" || user.RoleName == "Владелец";
        }

        public bool IsRequestConsidered(int idRequest)
        {
            return GetRequest(idRequest).DateOfConsideration != null;
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

        public void RemoveRequest(int idRequest)
        {
            RemoveSuitableVacancy(idRequest);

            var request = GetRequest(idRequest);

            _context.Request.Remove(request);
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

        public void RemoveStreet(int idStreet)
        {
            var street = GetStreet(idStreet);

            _context.Street.Remove(street);
            _context.SaveChanges();
        }

        public void RemoveSubIndustry(int idSubIndustry)
        {
            var subIndustry = GetSubIndustry(idSubIndustry);

            _context.SubIndustry.Remove(subIndustry);
            _context.SaveChanges();
        }

        public void RemoveSuitableVacancy(int idRequest)
        {
            var suitableVacancies = GetSuitableVacanciesTracking(idRequest);

            _context.SuitableVacancy.RemoveRange(suitableVacancies);
            _context.SaveChanges();
        }

        public void RemoveUser(int idUser)
        {
            var user = GetUser(idUser);

            _context.User.Remove(user);
            _context.SaveChanges();
        }

        public void RemoveVacancy(int idVacancy)
        {
            var vacancy = GetVacancy(idVacancy);

            _context.Vacancy.Remove(vacancy);
            _context.SaveChanges();
        }

        public void UpdateApplicant(int idApplicant, string name, string surname, string patronymic, byte[] photo, string phoneNumber, int idStreet, string nameHouse, string apartment, List<object> possessionSkills, List<object> possessionDrivingLicenseCategories, List<EducationalActivity> educationalActivities, List<KnowledgeLanguage> knowledgeLanguages, List<LaborActivity> laborActivities)
        {
            var applicant = GetApplicant(idApplicant);

            applicant.Name = name;
            applicant.Surname = surname;
            applicant.Patronymic = patronymic;
            applicant.Photo = photo;
            applicant.PhoneNumber = phoneNumber;
            applicant.IdStreet = idStreet;
            applicant.NameHouse = nameHouse;
            applicant.Apartment = apartment;

            _context.SaveChanges();

            UpdatePossessionSkills(idApplicant, possessionSkills);
            UpdatePossessionDrivingLicenseCategories(idApplicant, possessionDrivingLicenseCategories);
            UpdateEducationalActivities(idApplicant, educationalActivities);
            UpdateKnowledgeLanguages(idApplicant, knowledgeLanguages);
            UpdateLaborActivities(idApplicant, laborActivities);
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

        public void UpdateEducationalActivities(int idApplicant, List<EducationalActivity> educationalActivities)
        {
            CompleteRemovalEducationActivities(idApplicant);
            AddEducationalActivities(idApplicant, educationalActivities);
        }

        public void UpdateEmployer(int idEmployer, int idBranch, string name, string surname, string patronymic, byte[] photo, string phoneNumber)
        {
            var employer = GetEmployer(idEmployer);

            employer.IdBranch = idBranch;
            employer.Name = name;
            employer.Surname = surname;
            employer.Patronymic = patronymic;
            employer.Photo = photo;
            employer.PhoneNumber = phoneNumber;

            _context.SaveChanges();
        }

        public void UpdateEmploymentRequest(int idEmploymentRequest, bool? isSuitable)
        {
            var employmentRequest = GetEmploymentRequest(idEmploymentRequest);

            employmentRequest.IsSuitable = isSuitable;

            _context.SaveChanges();
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

        public bool UpdateIsEmployedApplicant(int idApplicant, bool isEmployed)
        {
            if(isEmployed == false)
            {
                if (!DoesItHaveActiveContracts(idApplicant))
                {
                    var applicant = GetApplicant(idApplicant);

                    applicant.IsEmployed = isEmployed;

                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var applicant = GetApplicant(idApplicant);

                applicant.IsEmployed = isEmployed;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void UpdateKnowledgeLanguages(int idApplicant, List<KnowledgeLanguage> knowledgeLanguages)
        {
            CompleteRemovalKnowledgeLanguages(idApplicant);
            AddKnowledgeLanguages(idApplicant, knowledgeLanguages);
        }

        public void UpdateLaborActivities(int idApplicant, List<LaborActivity> laborActivities)
        {
            CompleteRemovalLaborActivities(idApplicant);
            AddLaborActivities(idApplicant, laborActivities);
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

        public void UpdateManager(int idManager, string name, string surname, string patronymic, byte[] photo, string phoneNumber)
        {
            var manager = GetManager(idManager);

            manager.Name = name;
            manager.Surname = surname;
            manager.Patronymic = patronymic;
            manager.Photo = photo;
            manager.PhoneNumber = phoneNumber;

            _context.SaveChanges();
        }

        public void UpdateNecessarySkills(int idVacancy, List<object> necessarySkills)
        {
            CompleteRemovalNecessarySkills(idVacancy);
            AddNecessarySkills(idVacancy, necessarySkills);
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

        public void UpdatePossessionDrivingLicenseCategories(int idApplicant, List<object> possessionDrivingLicenseCategories)
        {
            CompleteRemovalPossessionDrivingLicenseCategories(idApplicant);
            AddPossessionDrivingLicenseCategories(idApplicant, possessionDrivingLicenseCategories);
        }

        public void UpdatePossessionSkills(int idApplicant, List<object> possessionSkills)
        {
            CompleteRemovalPossessionSkills(idApplicant);
            AddPossessionSkills(idApplicant, possessionSkills);
        }

        public void UpdatePreferredEmploymentTypes(int idRequest, List<object> preferredEmploymentTypes)
        {
            CompleteRemovalPreferredEmploymentTypes(idRequest);
            AddPreferredEmploymentTypes(idRequest, preferredEmploymentTypes);
        }

        public void UpdatePreferredSchedules(int idRequest, List<object> preferredSchedules)
        {
            CompleteRemovalPreferredSchedules(idRequest);
            AddPreferredSchedules(idRequest, preferredSchedules);
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

        public void UpdateRequest(int idRequest, int? salary, string aboutMe, List<object> preferredEmploymentTypes, List<object> preferredSchedules)
        {
            var request = GetRequest(idRequest);

            request.Salary = salary;
            request.AboutMe = aboutMe;

            _context.SaveChanges();

            UpdatePreferredEmploymentTypes(idRequest, preferredEmploymentTypes);
            UpdatePreferredSchedules(idRequest, preferredSchedules);
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

        public void UpdateStatusConsiderationRequest(int idRequest, string requestStatusName)
        {
            var request = GetRequest(idRequest);

            request.IdRequestStatus = GetIdRequestStatus(requestStatusName);
            request.DateOfConsideration = DateTime.Now;

            _context.SaveChanges();
        }

        public bool UpdateStreet(int idStreet, string streetName)
        {
            var street = GetStreet(idStreet);

            if (!ContainsStreet(street.IdCity, streetName))
            {
                street.StreetName = streetName;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateSubIndustry(int idSubIndustry, string nameSubIndustry)
        {
            var subIndustry = GetSubIndustry(idSubIndustry);

            if (!ContainsSubIndustry(subIndustry.IdIndustry, nameSubIndustry))
            {
                subIndustry.NameSubIndustry = nameSubIndustry;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateUser(int idUser, string login, string password)
        {
            var user = GetUser(idUser);

            if(!ContainsUser(login))
            {
                user.Login = login;
                user.Password = password;

                _context.SaveChanges();

                return true;
            }
            else
            {
                if(user.Login == login && user.Password != password)
                {
                    user.Password = password;

                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool UpdateUser(int idUser, string login, string password, out User user)
        {
            user = GetUser(idUser);

            if (!ContainsUser(login))
            {
                user.Login = login;
                user.Password = password;

                _context.SaveChanges();

                return true;
            }
            else
            {
                if (user.Login == login && user.Password != password)
                {
                    user.Password = password;

                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool UpdateUser(int idUser, string login, string password, out v_user user)
        {
            user = null;

            User userDefault = GetUser(idUser);

            if (!ContainsUser(login))
            {
                userDefault.Login = login;
                userDefault.Password = password;

                _context.SaveChanges();

                user = GetUserExtendedInformation(userDefault.IdUser);

                return true;
            }
            else
            {
                if (userDefault.Login == login && userDefault.Password != password)
                {
                    userDefault.Password = password;

                    _context.SaveChanges();

                    user = GetUserExtendedInformation(userDefault.IdUser);

                    return true;
                }
            }

            return false;
        }

        public void UpdateVacancy(int idVacancy, string description, string duties, string requirements, string terms, int? salary, List<object> necessarySkills)
        {
            var vacancy = GetVacancy(idVacancy);

            vacancy.Description = description;
            vacancy.Duties = duties;
            vacancy.Requirements = requirements;
            vacancy.Terms = terms;
            vacancy.Salary = salary;

            _context.SaveChanges();

            UpdateNecessarySkills(idVacancy, necessarySkills);
        }

        private void SetCommandTimeout()
        {
            var adapter = (IObjectContextAdapter)_context;
            var objectContext = adapter.ObjectContext;

            objectContext.CommandTimeout = _config.CommandTimeout;
        }
    }
}
