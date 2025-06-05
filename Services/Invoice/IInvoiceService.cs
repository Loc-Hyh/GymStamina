using AutoMapper;
using GymManagementProject.Dtos.Invoice;
using GymManagementProject.Entity;
using GymManagementProject.Repository;

namespace GymManagementProject.Services.Invoice
{
    public interface IInvoiceService
    {
        Task<Guid> Create(CreateInvoiceRequest request);
        Task<List<InvoiceDto>> GetAll();
        Task<List<InvoiceDto>> GetByCustomerId(Guid CustomerId);
        Task<Guid> Update(UpdateInvoiceRequest request);
        Task Delete(Guid id);
        Task<decimal> GetTotalRevenue();
        Task<decimal> GetMonthlyRevenue(int month, int year);
    }
}
