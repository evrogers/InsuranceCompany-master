using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ClientGroups> ClientGroups { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Policys> Policys { get; set; }
        public virtual DbSet<PolicyTypes> PolicyTypes { get; set; }
        public virtual DbSet<Risks> Risks { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public DbSet<User> User { get; set; }
    }
}
