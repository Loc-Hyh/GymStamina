namespace GymManagementProject.Dtos.Customer;

public class SendEmailToAllCustomers
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsHtml { get; set; } = true;
}