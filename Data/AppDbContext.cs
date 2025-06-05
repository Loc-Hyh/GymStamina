using GymManagementProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        // DbSet cho tất cả các entity
        public DbSet<BranchEntity> Branches { get; set; }
        public DbSet<StaffEntity> Staffs { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<TrainerEntity> Trainers { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ScheduleEntity> Schedules { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<EquipmentEntity> Equipment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServiceEntity>()
                 .Property(s => s.Price)
                 .HasPrecision(18, 4);
            // Quan hệ 1-1: Branch - Staff (quản lý chi nhánh)
            modelBuilder.Entity<BranchEntity>()
                .HasOne(b => b.Staff)
                .WithMany()
                .HasForeignKey(b => b.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ 1-n: Branch - Staff
            modelBuilder.Entity<StaffEntity>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Staffs)
                .HasForeignKey(s => s.BranchId);
            modelBuilder.Entity<StaffEntity>()
                .Property(e => e.Role)
                .HasConversion<int>(); // ✅ chuyển enum sang int

            // Quan hệ 1-n: Branch - Customers
            /*modelBuilder.Entity<CustomerEntity>()
                .HasOne(c => c.Branch)
                .WithMany(b => b.Customers)
                .HasForeignKey(c => c.BranchId);*/

            // Branch - Trainers
            modelBuilder.Entity<TrainerEntity>()
                .HasOne(t => t.Branch)
                .WithMany(b => b.Trainers)
                .HasForeignKey(t => t.BranchId);

            // Branch - Equipment
            modelBuilder.Entity<EquipmentEntity>()
                .HasOne(e => e.Branch)
                .WithMany(b => b.Equipment)
                .HasForeignKey(e => e.BranchId);

            // Schedule liên kết: Customer - Trainer - Service
            // Schedule liên kết: Customer - Trainer - Service
            /*modelBuilder.Entity<ScheduleEntity>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // << THÊM DÒNG NÀY*/

            /*modelBuilder.Entity<ScheduleEntity>()
                .HasOne(s => s.Trainer)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Restrict); // ĐÃ ĐÚNG */
            modelBuilder.Entity<ScheduleEntity>()
                .HasOne(s => s.Invoice)
                .WithMany(ss => ss.Schedules )
                .HasForeignKey(i => i.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
            /*modelBuilder.Entity<ScheduleEntity>()
                .HasOne(s => s.Service)
                .WithMany(sv => sv.Schedules)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Restrict); // << THÊM DÒNG NÀY*/



            // Invoice: Customer - Service
            modelBuilder.Entity<InvoiceEntity>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InvoiceEntity>()
                .HasOne(i => i.Service)
                .WithMany(s => s.Invoices)
                .HasForeignKey(i => i.ServicesId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<InvoiceEntity>()
                .HasOne(s => s.CreateBy)
                .WithMany(s => s.Invoices)
                .HasForeignKey(s => s.CreateById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
