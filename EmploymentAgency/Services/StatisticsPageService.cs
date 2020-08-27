using System;
using System.Windows.Controls;

namespace EmploymentAgency.Services
{
    public class StatisticsPageService
    {
        public event Action<Page> OnPageChanged;

        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
