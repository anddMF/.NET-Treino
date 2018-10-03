using BecaDotNet.Domain.Model;
using BecaDotNet.Repository.Configs;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BecaDotNet.Repository.Context
{
    public class BecaContext : DbContext
    {
        public BecaContext() : base("DefaultConnString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            this.ProcessConfiguration(modelBuilder);
            modelBuilder.Entity<Projeto>()
                .HasRequired(c => c.Cliente)
                .WithMany(m => m.Projetos)
                .HasForeignKey(f => f.ClienteId)
                .WillCascadeOnDelete(false);
        }

        private void ProcessConfiguration(DbModelBuilder builder)
        {
            builder.Configurations.Add(new UserEntityConfig());
            builder.Configurations.Add(new UserTypeEntityConfig());
            builder.Configurations.Add(new UserTypeUserEntityConfig());
            builder.Configurations.Add(new ClienteEntityConfig());
            builder.Configurations.Add(new ProjetoEntityConfig());
            builder.Configurations.Add(new ProjetoUserEntityConfig());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserTypeUser> UserTypeUsers { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<ProjetoUser> ProjetoUser { get; set; }
    }
}
