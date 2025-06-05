using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Customer;

namespace GymManagementProject.Services.Customer
{
    public interface ICustomerService
    {
        Task<Guid> Create(CreateCustomerRequest request);
        Task<Guid> Update(CustomerUpdateRequest request);
        Task<List<CustomerDto>> GetAll();
        Task Delete(Guid id);
        Task <PageView<CustomerDto>> SearchCustomer(SearchCustom request);
        Task SendEmailAllCustomer(SendEmailToAllCustomers request);
    }
}
