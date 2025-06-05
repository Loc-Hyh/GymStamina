using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Customer;

public class CustomerUpdateRequest
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Enums.MembershipType MembershipType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

}