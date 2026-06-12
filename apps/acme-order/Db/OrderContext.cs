using AcmeOrder.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeOrder.Db;

public abstract class OrderContext : DbContext
{
    public virtual DbSet<Order> Orders { get; set; }
}