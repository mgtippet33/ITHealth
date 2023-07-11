namespace ITHealth.Web.API.Models.Jira;

public class GetEfficiencyStatisticsRequestModel
{
    public int UserId { get; set; }

    public DateTime StartDate { get; set; } = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

    public DateTime EndDate { get; set; }  = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 
        DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));
}