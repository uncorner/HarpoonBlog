using System;
using FluentMigrator;

namespace Harpoon.Migrations
{
    public abstract class ProfileMigration : Migration
    {
        private readonly string profileName;

        protected ProfileMigration(string profileName)
        {
            if (string.IsNullOrEmpty(profileName))
            {
                throw new ArgumentNullException(profileName);
            }

            this.profileName = profileName;
        }

        protected abstract void ToUp();
        protected abstract void ToDown();

        #region Overrides of MigrationBase

        public override void Up()
        {
            if (IsNeedProcess())
            {
                ToUp();
            }
        }

        public override void Down()
        {
            if (IsNeedProcess())
            {
                ToDown();
            }
        }

        private bool IsNeedProcess()
        {
            var profileVariable = Environment.GetEnvironmentVariable("PROFILE");
            if (string.IsNullOrEmpty(profileVariable))
            {
                return false;
            }

            return profileVariable.Equals(profileName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}