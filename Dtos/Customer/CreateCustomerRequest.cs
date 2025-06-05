using GymManagementProject.Const;
using GymManagementProject.Entity;

namespace GymManagementProject.Dtos.Customer;

public class CreateCustomerRequest
{
    
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Enums.MembershipType MembershipType { get; set; } // ngay, thang, quy, nam
}