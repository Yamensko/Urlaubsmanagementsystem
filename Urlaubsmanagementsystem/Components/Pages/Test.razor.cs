using Microsoft.EntityFrameworkCore;
using Urlaubsmanagementsystem.Models;

namespace Urlaubsmanagementsystem.Components.Pages
{
    public partial class Test
    {
        private List<LeaveRequest> ApprovedRequests = new();
        private List<DateTime> CalendarDays = new();
        private bool ShowPopup = false;
        private DateTime SelectedPopupDate;

        private int _selectedMonth = DateTime.Today.Month;
        private int _selectedYear = DateTime.Today.Year;

        private int SelectedMonth { get => _selectedMonth; set { _selectedMonth = value; GenerateCalendar(); } }
        private int SelectedYear { get => _selectedYear; set { _selectedYear = value; GenerateCalendar(); } }

        protected override async Task OnInitializedAsync()
        {
            // Wir laden alle genehmigten Anträge inklusive User-Daten
            ApprovedRequests = await DbContext.LeaveRequests
                .Include(x => x.User)
                .Where(x => x.Decision.Status == "Approved")
                .ToListAsync();
            GenerateCalendar();
        }

        private void GenerateCalendar()
        {
            var firstDay = new DateTime(SelectedYear, SelectedMonth, 1);
            int daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
            int offset = (int)firstDay.DayOfWeek;

            CalendarDays.Clear();
            for (int i = 0; i < offset; i++) CalendarDays.Add(DateTime.MinValue);
            for (int i = 1; i <= daysInMonth; i++) CalendarDays.Add(new DateTime(SelectedYear, SelectedMonth, i));
        }

        // FIX: DistinctBy stellt sicher, dass jeder User pro Tag nur 1x erscheint
        private List<LeaveRequest> GetRequestsForDay(DateTime date) =>
            ApprovedRequests
                .Where(x => date.Date >= x.StartDate.Date && date.Date <= x.EndDate.Date)
                .DistinctBy(x => x.UserId)
                .ToList();

        private int GetCountForDay(DateTime date) => GetRequestsForDay(date).Count;

        private void PreviousMonth()
        {
            if (SelectedMonth == 1) { SelectedMonth = 12; SelectedYear--; }
            else { SelectedMonth--; }
        }

        private void NextMonth()
        {
            if (SelectedMonth == 12) { SelectedMonth = 1; SelectedYear++; }
            else { SelectedMonth++; }
        }

        private void GoToToday() { 
            SelectedYear = DateTime.Today.Year; 
            SelectedMonth = DateTime.Today.Month; 
        }
        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        private bool IsToday(DateTime date) => date.Date == DateTime.Today;

        private string GetCssClass(int count)
        {
            if (count >= 5) return "status-critical";
            if (count >= 3) return "status-warning";
            if (count > 0) return "status-ok";
            return "";
        }

        private void OpenPopup(DateTime date) { SelectedPopupDate = date; ShowPopup = true; }
        private void ClosePopup() => ShowPopup = false;
    }
}
