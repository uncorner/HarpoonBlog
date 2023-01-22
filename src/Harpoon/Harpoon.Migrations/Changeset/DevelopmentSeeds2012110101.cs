using System;
using FluentMigrator;

namespace Harpoon.Migrations.Changeset
{
    [Migration(2012110101)]
    public class DevelopmentSeeds2012110101 : ProfileMigration
    {
        public DevelopmentSeeds2012110101() : base("Development")
        {
        }

        #region Overrides of ProfileMigration

        protected override void ToUp()
        {
            Execute.EmbeddedScript("dev-seeds.sql");
        }

        protected override void ToDown()
        {
        }

        #endregion
    }
}