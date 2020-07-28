using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Models;
using System;
using System.Collections.Generic;
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

        public bool ContainsApplicant(int idApplicant)
        {
            return _context.Applicant.SingleOrDefault(a => a.IdApplicant == idApplicant) != null;
        }

        public bool ContainsEmployer(int idEmployer)
        {
            return _context.Employer.SingleOrDefault(e => e.IdEmployer == idEmployer) != null;
        }

        public bool ContainsManager(int idManager)
        {
            return _context.Manager.SingleOrDefault(m => m.IdManager == idManager) != null;
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

        public List<Gender> GetGenders()
        {
            return _context.Gender.AsNoTracking().ToList();
        }

        public int GetIdRole(string roleName)
        {
            return _context.Role.Single(r => r.RoleName == roleName).IdRole;
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
