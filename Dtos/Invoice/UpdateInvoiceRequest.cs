using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Invoice;

public class UpdateInvoiceRequest
{
    public Guid Id { get; set; }
    public Guid? TrainerId { get; set; }
    public Guid ServicesId { get; set; }
    public DateTime PaymentDate { get; set; }
}