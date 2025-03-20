using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                NameSurname = "Admin",
                PhoneNumber = "05559998833",
                Password = "123456",
                Role = "ADMIN",
                IsActive=true
            });
            builder.HasData(new User
            {
                Id = 2,
                NameSurname="Ogretmen",
                PhoneNumber="05534445566",
                Email = "ogretmen@gmail.com",
                Password = "123456",
                Role = "TEACHER",
            });
            builder.HasData(new User
            {
                Id = 3,
                NameSurname = "Ogrenci",
                PhoneNumber = "05534445526",
                Email = "ogrenci@gmail.com",
                Password = "123456",
                Role = "STUDENT",
            });
        }
    }
}
