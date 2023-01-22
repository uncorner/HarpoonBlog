using System;
using FluentMigrator;

namespace Harpoon.Migrations.Changeset
{
    [Migration(2012112600)]
    public class ImageDataFixing2012112600 : AutoReversingMigration
    {
        #region Overrides of MigrationBase

        public override void Up()
        {
            Alter.Column("Data").OnTable("Images")
                .AsBinary(Int32.MaxValue).NotNullable();
        }

        #endregion
    }
}