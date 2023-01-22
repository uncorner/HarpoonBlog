using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class PersonalSettingMapping : EntityTypeConfiguration<PersonalSetting>
    {
        public PersonalSettingMapping()
        {
            Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            Property(e => e.Subject)
                .HasMaxLength(200);

            Property(e => e.FooterText)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(50);
        }
        
    }
}