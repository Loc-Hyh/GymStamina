using AutoMapper;
using GymManagementProject.Dtos.Branch;
using GymManagementProject.Dtos.Customer;
using GymManagementProject.Dtos.Equipment;
using GymManagementProject.Dtos.Invoice;
using GymManagementProject.Dtos.Schedule;
using GymManagementProject.Dtos.Service;
using GymManagementProject.Dtos.Staff;
using GymManagementProject.Dtos.Trainer;
using GymManagementProject.Entity;

namespace GymManagementProject.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Branch
            CreateMap<BranchCreateRequest, BranchEntity>();
            CreateMap<BranchUpdateRequest, BranchEntity>();
            CreateMap<BranchEntity, BranchDto>()
                .ForMember(dest => dest.ManagementName, opt => opt.MapFrom(src => src.Staff != null ? src.Staff.Name: ""));
            //Staff
            CreateMap<StaffCreateRequest, StaffEntity>();
            CreateMap<StaffUpdateRequest, StaffEntity>();
            CreateMap<StaffEntity, StaffDto>()
                .ForMember(dest => dest.BranchName, o => o.MapFrom(src => src.Branch != null ? src.Branch.BranchName : ""));
            //Customer
            CreateMap<CreateCustomerRequest, CustomerEntity>();
            CreateMap<CustomerUpdateRequest, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerDto>();
            //Equipment
            CreateMap<CreateEquipmentRequest, EquipmentEntity>();
            CreateMap<UpdateEquipmentRequest, EquipmentEntity>();
            CreateMap<EquipmentEntity, EquipmentDto>()
                .ForMember(dest => dest.BranchId, o => o.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.BranchName, o=>o.MapFrom(src => src.Branch != null ? src.Branch.BranchName : ""))
                .ForMember(dest => dest.ImageUrl, o => o.MapFrom(src => src.ImageUrl));
            //Invoice
            CreateMap<CreateInvoiceRequest, InvoiceEntity>()
                .ForMember(dest => dest.CreateById,o => o.Ignore())
                .ForMember(dest => dest.PaymentDate, o => o.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.TotalPrice, o => o.Ignore())
                .ForMember(dest => dest.DurationTime, o => o.Ignore());
            CreateMap<UpdateInvoiceRequest, InvoiceEntity>();
            CreateMap<InvoiceEntity, InvoiceDto>()
                .ForMember(dest => dest.CustomerName, o => o.MapFrom(src => src.Customer != null ? src.Customer.Name : ""))
                .ForMember(dest => dest.ServiceName, o => o.MapFrom(src => src.Service != null ? src.Service.ServiceName: ""))
                .ForMember(dest => dest.CreateById, o => o.MapFrom(src => src.CreateById))
                .ForMember(dest => dest.ActualTime, o => o.MapFrom(src => src.Schedules.Count));
            //Schedule
            CreateMap<CreateScheduleRequest, ScheduleEntity>();
            CreateMap<CreateManualScheduleRequest, ScheduleEntity>();
            CreateMap<UpdateScheduleRequest, ScheduleEntity>();
            CreateMap<ScheduleEntity, ScheduleDto>()
                .ForMember(dest => dest.CustomerId,
                    o => o.MapFrom(src => src.Invoice.Customer.Id))
                .ForMember(dest => dest.CustomerName, o => o.MapFrom(src => src.Invoice.Customer.Name ?? ""))
                .ForMember(dest => dest.TrainerId, o => o.MapFrom(src => src.Invoice.Trainer != null ? src.Invoice.Trainer.Id : Guid.Empty))
                .ForMember(dest => dest.TrainerName, o => o.MapFrom(src => src.Invoice.Trainer != null ? src.Invoice.Trainer.Name : ""))
                .ForMember(dest => dest.ServiceId, o => o.MapFrom(src => src.Invoice.Service.Id))
                .ForMember(dest => dest.ServiceName, o => o.MapFrom(src => src.Invoice.Service.ServiceName ?? ""));
            //Service
            CreateMap<CreateServiceRequest, ServiceEntity>();
            CreateMap<UpdateServiceRequest, ServiceEntity>();
            CreateMap<ServiceEntity, ServiceDto>();
            //Trainer
            CreateMap<CreateTrainerRequest, TrainerEntity>();
            CreateMap<UpdateTrainerRequest, TrainerEntity>();
            CreateMap<TrainerEntity, TrainerDto>()
                .ForMember(dest => dest.BranchName, o => o.MapFrom(src => src.Branch != null ? src.Branch.BranchName : ""));
            CreateMap<GetScheduleDto, ScheduleDto>();
        }
    }
}
