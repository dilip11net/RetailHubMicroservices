using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Configure the Order entity here, e.g.:
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);
                

            builder.ComplexProperty(o => o.OrderName, orderName =>
            {
                orderName.Property(on => on.Value).HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, shippingAddress =>
            {
               shippingAddress.Property(a=>a.FirstName).HasMaxLength(50).IsRequired();
               shippingAddress.Property(a=>a.LastName).HasMaxLength(50).IsRequired();
                shippingAddress.Property(a => a.Email).HasMaxLength(50).IsRequired();
                shippingAddress.Property(a=>a.Street).HasMaxLength(200).IsRequired();
               shippingAddress.Property(a=>a.City).HasMaxLength(100).IsRequired();
               shippingAddress.Property(a=>a.State).HasMaxLength(100).IsRequired();
               shippingAddress.Property(a=>a.ZipCode).HasMaxLength(20).IsRequired();
               shippingAddress.Property(a=>a.Country).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.BillingAddress, billingAddress =>
            {
                billingAddress.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                billingAddress.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                billingAddress.Property(a => a.Email).HasMaxLength(50).IsRequired();
                billingAddress.Property(a => a.Street).HasMaxLength(200).IsRequired();
                billingAddress.Property(a => a.City).HasMaxLength(100).IsRequired();
                billingAddress.Property(a => a.State).HasMaxLength(100).IsRequired();
                billingAddress.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
                billingAddress.Property(a => a.Country).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.Payment, payment =>
            {
                payment.Property(p => p.CardName).HasMaxLength(50).IsRequired();
                payment.Property(p => p.CardNumber).HasMaxLength(50).IsRequired();
                payment.Property(p => p.Expiration).HasMaxLength(100);
                payment.Property(p => p.CVV).HasMaxLength(3).IsRequired();
                payment.Property(p => p.PaymentMethod).HasMaxLength(50).IsRequired();
            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalAmount);
        }
    }
}
