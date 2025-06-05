using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Invoice;

public class CreateInvoiceRequest
{
    public Guid CustomerId { get; set; }
    public Guid? TrainerId { get; set; }
    public Guid ServicesId { get; set; }
    public Enums.PaymentMethod Method { get; set; }
}