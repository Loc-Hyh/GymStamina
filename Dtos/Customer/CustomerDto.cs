using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Customer;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Enums.MembershipType MembershipType { get; set; } // bình thường, thân thiết, vip
}