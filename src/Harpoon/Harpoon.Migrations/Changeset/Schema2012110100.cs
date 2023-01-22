using FluentMigrator;

namespace Harpoon.Migrations.Changeset
{
    [Migration(2012110100)]
    public class Schema2012110100 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("ArticleTags")
                .WithColumn("ArticleId").AsInt32().NotNullable()
                .WithColumn("TagId").AsStringWithCollate(50).NotNullable();

            Create.Table("Articles")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Title").AsStringWithCollate(200).NotNullable()
                .WithColumn("ContentId").AsInt32().NotNullable()
                .WithColumn("IsPublished").AsBoolean().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().Nullable()
                .WithColumn("PublishedAt").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable()
                .WithColumn("PreviewContentId").AsInt32().NotNullable();

            Create.Table("Chapters")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Title").AsStringWithCollate(50).NotNullable()
                .WithColumn("ContentId").AsInt32().NotNullable()
                .WithColumn("IsPublished").AsBoolean().NotNullable()
                .WithColumn("TagName").AsStringWithCollate(50).NotNullable()
                .WithColumn("OrderValue").AsInt32().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().Nullable()
                .WithColumn("PublishedAt").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable();

            Create.Table("Comments")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("ArticleId").AsInt32().NotNullable()
                .WithColumn("Name").AsStringWithCollate(30).NotNullable()
                .WithColumn("Email").AsStringWithCollate(50).Nullable()
                .WithColumn("Content").AsStringWithCollate(2000).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("IsMaster").AsBoolean().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable();

            Create.Table("Images")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Data").AsBinary().NotNullable()
                .WithColumn("ContentType").AsStringWithCollate(50).NotNullable();

            Create.Table("PersonalSettings")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Title").AsStringWithCollate(100).NotNullable()
                .WithColumn("Subject").AsStringWithCollate(200).Nullable()
                .WithColumn("FooterText").AsStringWithCollate(50).NotNullable()
                .WithColumn("Name").AsStringWithCollate(50).NotNullable()
                .WithColumn("Email").AsStringWithCollate(50).NotNullable()
                .WithColumn("PasswordHash").AsStringWithCollate(50).NotNullable();

            Create.Table("PostContents")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Data").AsStringWithCollate().NotNullable();

            Create.Table("Tags")
                .WithColumn("Id").AsStringWithCollate(50).NotNullable().PrimaryKey()
                .WithColumn("Name").AsStringWithCollate(50).NotNullable();

            Create.ForeignKey().FromTable("ArticleTags").ForeignColumn("ArticleId")
                .ToTable("Articles").PrimaryColumn("Id");

            Create.ForeignKey().FromTable("ArticleTags").ForeignColumn("TagId")
                .ToTable("Tags").PrimaryColumn("Id");

            Create.ForeignKey().FromTable("Articles").ForeignColumn("ContentId")
                .ToTable("PostContents").PrimaryColumn("Id");

            Create.ForeignKey().FromTable("Articles").ForeignColumn("PreviewContentId")
                .ToTable("PostContents").PrimaryColumn("Id");

            Create.ForeignKey().FromTable("Chapters").ForeignColumn("ContentId")
                .ToTable("PostContents").PrimaryColumn("Id");

            Create.ForeignKey().FromTable("Comments").ForeignColumn("ArticleId")
                .ToTable("Articles").PrimaryColumn("Id");
        }

    }
}