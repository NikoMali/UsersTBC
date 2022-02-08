using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UsersTBC.Application.ApplicationDbContext;
using UsersTBC.Domain;

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Infrastructure.Extensions;

namespace UsersTBC.Infrastructure.Data
{
    public class DataContext : DbContext, IApplicationDbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
        //Main Migration and Invoke PM Code: Add-Migration <Name > -Context DataContext -Project ProductTermsControl.Insfrastructure
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<City_Translation> City_Translations { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<UserMobileNumber> UserMobileNumbers { get; set; }
        public DbSet<RelatedType> RelatedTypes { get; set; }
        public DbSet<UseRelated> UseRelateds { get; set; }
        public DbSet<MobileNumberType> MobileNumberTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ModifyDetectChange();

            return await base.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            ModifyDetectChange();

            return await base.SaveChangesAsync();
        }
        private void ModifyDetectChange()
        {
            var entries = ChangeTracker.Entries();
            var Now = DateTime.Now;

            foreach (var entry in entries)
            {

                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:

                            trackable.UpdateDate = Now;

                            entry.Property("CreateDate").IsModified = false;
                            trackable.CreateDate = DateTime.Parse(entry.GetDatabaseValues().GetValue<object>("CreateDate").ToString());

                            /*foreach (var property in entry.Properties)
                            {
                                if (property.IsModified)
                                {
                                    var original = entry.GetDatabaseValues().GetValue<object>(property.Metadata.Name);
                                    var current = property.CurrentValue;
                                }
                                
                            }*/
                            break;

                        case EntityState.Added:

                            trackable.CreateDate = Now;
                            trackable.UpdateDate = Now;
                            break;
                    }
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Gender>().Property(e => e.Name).HasConversion<string>();
            builder.Entity<RelatedType>().Property(e => e.Name).HasConversion<string>();
            builder.Entity<MobileNumberType>().Property(e => e.Name).HasConversion<string>();

            builder.Entity<UseRelated>().HasOne<User>().WithMany().HasForeignKey(e => e.RelatedUserId);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Seed(builder);
            


            //builder.Entity<UseRelated>().HasOne<User>().WithMany().HasForeignKey(e => e.UserId);


            builder.Seed();
            base.OnModelCreating(builder);
        }
        /*public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "English", Code = "en-US", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Language { Id = 2, Name = "Georgia", Code = "ka-GE", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            builder.Entity<City>().HasData(

                new City { Id = 1, Name = "Tbilisi", IsActive = "true", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new City { Id = 3, Name = "Khashuri", IsActive = "true", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            builder.Entity<City_Translation>().HasData(
                new City_Translation { Id = 1, CityId = 1, LanguageId = 2, NameTranslate = "თბილისი", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new City_Translation { Id = 2, CityId = 3, LanguageId = 2, NameTranslate = "ხაშური", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
        }*/


    }
}