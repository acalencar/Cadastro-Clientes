using Microsoft.EntityFrameworkCore;


public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }

}