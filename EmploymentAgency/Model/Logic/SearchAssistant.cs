using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentAgency.Model.Logic
{
    public class SearchAssistant
    {
        public enum SearchType { StartsWith, Contains, EndsWith, AllCriteria }

        public static Func<bool> GetSearchAction(string value, string searchLine, SearchType searchType)
        {
            switch(searchType)
            {
                case SearchType.StartsWith:
                    return () => value.ToLower().StartsWith(searchLine.ToLower());
                case SearchType.Contains:
                    return () => value.ToLower().Contains(searchLine.ToLower());
                case SearchType.EndsWith:
                    return () => value.ToLower().EndsWith(searchLine.ToLower());
                case SearchType.AllCriteria:
                    return () => value.ToLower().StartsWith(searchLine.ToLower()) || value.ToLower().Contains(searchLine.ToLower()) || value.ToLower().EndsWith(searchLine.ToLower());
            }

            return () => true;
        }
    }
}
