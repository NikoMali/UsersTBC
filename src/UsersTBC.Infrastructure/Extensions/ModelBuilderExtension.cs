using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.Help;

namespace UsersTBC.Infrastructure.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(EnumHelpers.GetModelFromEnum<Gender, GenderEnum>());
            modelBuilder.Entity<RelatedType>().HasData(EnumHelpers.GetModelFromEnum<RelatedType, RelatedTypeEnum>());
            modelBuilder.Entity<MobileNumberType>().HasData(EnumHelpers.GetModelFromEnum<MobileNumberType, MobileNumberTypeEnum>());

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "English", Code = "en-US", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Language { Id = 2, Name = "Georgia", Code = "ka-GE", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            modelBuilder.Entity<City>().HasData(

                new City { Id = 1, Name = "Tbilisi", IsActive = "true", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new City { Id = 3, Name = "Khashuri", IsActive = "true", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            modelBuilder.Entity<City_Translation>().HasData(
                new City_Translation { Id = 1, CityId = 1, LanguageId = 2, NameTranslate = "თბილისი", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new City_Translation { Id = 2, CityId = 3, LanguageId = 2, NameTranslate = "ხაშური", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
        }
    }
}
