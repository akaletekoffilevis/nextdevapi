namespace NextDevAsp.Api;

public class TeamMember
{
   public int Id { get; set; }
    public string? Initial
    {
        get
        {
            if (FirstName is not null && LastName is not null)
                return FirstName.Substring(0, 1) + "" + LastName.Substring(0, 1);
            return "NA";
        }
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName
    {
        get => $"{FirstName} {LastName}";
    }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? GroupName { get; set; }
    public bool Status { get; set; }
}


