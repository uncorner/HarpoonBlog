using System;
using FluentMigrator;

namespace Harpoon.Migrations.Changeset
{
    [Migration(2012110600)]
    public class CreateGuestComments2012110600 : Migration
    {
        #region Overrides of MigrationBase

        public override void Up()
        {
            Delete.FromTable("Comments").AllRows();

            Delete.Column("IsMaster").FromTable("Comments");
            Delete.Column("Name").FromTable("Comments");
            Delete.Column("Email").FromTable("Comments");

            Create.Table("GuestComments")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Name").AsStringWithCollate(30).NotNullable()
                .WithColumn("Email").AsStringWithCollate(50).Nullable();

            Create.ForeignKey()
                .FromTable("GuestComments").ForeignColumn("Id")
                .ToTable("Comments").PrimaryColumn("Id");
        }

        public override void Down()
        {
        }

        #endregion
    }
}