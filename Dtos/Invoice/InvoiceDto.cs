using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Invoice;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public Guid? TrainerId { get; set; }
    public string? TrainerName { get; set; }
    public Guid ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public int DurationTime { get; set; }
    public int ActualTime { get; set; } //thuc te
    public decimal TotalPrice { get; set; }
    public Enums.PaymentMethod Method { get; set; }
    public DateTime PaymentDate { get; set; }
    public Guid CreateById {get;set;}
    public string? CreateByName { get; set; }
}