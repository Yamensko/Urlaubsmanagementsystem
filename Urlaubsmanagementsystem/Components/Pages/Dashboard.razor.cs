using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Schedule;
using Urlaubsmanagementsystem.Models;
using Urlaubsmanagementsystem.Data;

namespace Urlaubsmanagementsystem.Components.Pages
{
    public partial class Dashboard
    {
        [Inject]
        public DatabaseConnection DbContext { get; set; } = default!;


        private List<LeaveRequest> ApprovedRequests = new();
        private SfSchedule<LeaveRequest>? ScheduleRef;

        protected override async Task OnInitializedAsync()
        {
            ApprovedRequests = await DbContext.LeaveRequests
                .Include(x => x.User)
                .Where(x => x.DecisionId == (byte)Decision.DecisionStatus.Approved)
                .ToListAsync();

            await InvokeAsync(async () =>
            {
                StateHasChanged();

                if (ScheduleRef != null)
                    await ScheduleRef.RefreshAsync();
            });
        }

        private void OnCellDoubleClick(CellClickEventArgs args)
        {
            // Das verhindert, dass sich das Standard-Fenster zum Erstellen von Terminen öffnet
            args.Cancel = true;
        }
        private void OnEventDoubleClick(EventClickArgs<LeaveRequest> args)
        {
            //  das Öffnen des Editor-Fensters beim Doppelklick auf einen Urlaubseintrag
            args.Cancel = false;
        }
        private int GetCountForDay(DateTime date)
        {
            return ApprovedRequests.Count(x =>
                date.Date >= x.StartDate.Date &&
                date.Date <= x.EndDate.Date);
        }

        private void OnRenderCell(RenderCellEventArgs args)
        {
            if (args.ElementType != ElementType.MonthCells)
                return;

            // ✅ FIX: initialize CssClasses
            if (args.CssClasses == null)
                args.CssClasses = new List<string>();

            var count = GetCountForDay(args.Date);

            if (count >= 5)
                args.CssClasses.Add("full-day");
            else if (count >= 3)
                args.CssClasses.Add("medium-day");
            else if (count > 0)
                args.CssClasses.Add("low-day");

        // DEBUG OUTPUT (for now)
        Console.WriteLine($"{args.Date}: {count}");
        }
    }
}