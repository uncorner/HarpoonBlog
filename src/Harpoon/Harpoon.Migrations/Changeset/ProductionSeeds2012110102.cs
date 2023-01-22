using FluentMigrator;

namespace Harpoon.Migrations.Changeset
{
    [Migration(2012110102)]
    public class ProductionSeeds2012110102 : ProfileMigration
    {
        public ProductionSeeds2012110102() : base("Production")
        {
        }

        protected override void ToUp()
        {
            Insert.IntoTable("PersonalSettings")
                .Row(new
                         {
                             Id = 10,
                             Title = "{BlogName}",
                             Subject = "{BlogSubject}",
                             FooterText = "{Copyright}",
                             Name = "{AuthorName}",
                             Email = "authorname@authorname.org",
                             PasswordHash = "356A192B7913B04C54574D18C28D46E6395428AB"
                         });
        }

        protected override void ToDown()
        {
        }

    }
}