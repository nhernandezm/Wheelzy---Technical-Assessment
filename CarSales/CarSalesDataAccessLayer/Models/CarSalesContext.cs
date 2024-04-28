using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarSalesDataAccessLayer.Models;

public partial class CarSalesContext : DbContext
{
    public CarSalesContext()
    {
    }

    public CarSalesContext(DbContextOptions<CarSalesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Costumer> Costumers { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<PotentialBuyer> PotentialBuyers { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<SubModel> SubModels { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }    
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarSales;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.BuyerId).HasName("pk_Buyers_BuyerID");

            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Location).WithMany(p => p.Buyers)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Buyers__Location__38996AB5");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("pk_Cars_CarID");

            entity.HasIndex(e => e.RegistrationNumber, "UQ__Cars__E886460231C73B9B").IsUnique();

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.CostumerId).HasColumnName("CostumerID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubModelId).HasColumnName("SubModelID");

            entity.HasOne(d => d.Costumer).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CostumerId)
                .HasConstraintName("FK__Cars__CostumerID__33D4B598");

            entity.HasOne(d => d.Location).WithMany(p => p.Cars)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Cars__LocationID__32E0915F");

            entity.HasOne(d => d.Make).WithMany(p => p.Cars)
                .HasForeignKey(d => d.MakeId)
                .HasConstraintName("FK__Cars__MakeID__300424B4");

            entity.HasOne(d => d.Model).WithMany(p => p.Cars)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK__Cars__ModelID__30F848ED");

            entity.HasOne(d => d.SubModel).WithMany(p => p.Cars)
                .HasForeignKey(d => d.SubModelId)
                .HasConstraintName("FK__Cars__SubModelID__31EC6D26");
        });

        modelBuilder.Entity<Costumer>(entity =>
        {
            entity.HasKey(e => e.CostumerId).HasName("pk_Costumers_CostumerID");

            entity.Property(e => e.CostumerId).HasColumnName("CostumerID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Location).WithMany(p => p.Costumers)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Costumers__Locat__2C3393D0");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("pk_Locations_LocationID");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.LocationName).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("pk_logs_LogID");

            entity.ToTable("logs");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogMessage).HasMaxLength(50);
            entity.Property(e => e.PotentialBuyerId).HasColumnName("PotentialBuyerID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.PotentialBuyer).WithMany(p => p.Logs)
                .HasForeignKey(d => d.PotentialBuyerId)
                .HasConstraintName("FK__logs__PotentialB__412EB0B6");

            entity.HasOne(d => d.Status).WithMany(p => p.Logs)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__logs__StatusID__4222D4EF");
        });

        modelBuilder.Entity<Make>(entity =>
        {
            entity.HasKey(e => e.MakeId).HasName("pk_Makes_MakeID");

            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.MakeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("pk_Models_ModelID");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.ModelName).HasMaxLength(50);
        });

        modelBuilder.Entity<PotentialBuyer>(entity =>
        {
            entity.HasKey(e => e.PotentialBuyerId).HasName("pk_PotentialBuyers_PotentialBuyerID");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_PotentialBuyers_Insert");
                    tb.HasTrigger("tr_PotentialBuyers_Update");
                });

            entity.Property(e => e.PotentialBuyerId).HasColumnName("PotentialBuyerID");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.DatePickup).HasColumnType("datetime");
            entity.Property(e => e.IsCurrentOne).HasDefaultValue(false);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Buyer).WithMany(p => p.PotentialBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__Potential__Buyer__3B75D760");

            entity.HasOne(d => d.Car).WithMany(p => p.PotentialBuyers)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK__Potential__CarID__3D5E1FD2");

            entity.HasOne(d => d.Status).WithMany(p => p.PotentialBuyers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Potential__Statu__3E52440B");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("pk_status_StatusID");

            entity.ToTable("status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubModel>(entity =>
        {
            entity.HasKey(e => e.SubModelId).HasName("pk_SubModels_SubModelID");

            entity.Property(e => e.SubModelId).HasColumnName("SubModelID");
            entity.Property(e => e.SubModelName).HasMaxLength(50);
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrderID).HasName("pk_Orders_OrderID");

            entity.Property(e => e.OrderID).HasColumnName("OrderID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
