using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using CustomerMsgApp.Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CustomerMsgApp.Service
{
    public class CustomerContext : DbContext
    {
        public CustomerContext()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<NotificationData> NotificationData { get; set; }
        public DbSet<NotificationDataSeeds> NotificationDataSeeds { get; set; }
        public DbSet<NotificationLog> NotificationLog { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public System.Data.Linq.DataLoadOptions LoadOptions { get; set; }
    }

    public class DatabaseContextSeedInitializer : CreateDatabaseIfNotExists<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            try
            {
                using (CustomerContext db = new CustomerContext())
                {
                    NotificationLog emaillogreport = new NotificationLog();
                    emaillogreport.SentTo = "Admin";
                    db.NotificationLog.Add(emaillogreport);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ErrorLog.InsertLog(ex);
            }
        }
    }
}
