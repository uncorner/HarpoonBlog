using FluentMigrator.Builders.Create.Table;

namespace Harpoon.Migrations
{
    internal static class MigratorExtensions
    {
        public const string DEFAULT_COLLATE = "Cyrillic_General_CI_AS";

        public static ICreateTableColumnOptionOrWithColumnSyntax AsStringWithCollate(
            this ICreateTableColumnAsTypeSyntax syntax, int size, string collate = DEFAULT_COLLATE)
        {
            return syntax.AsCustom(GetColumnType(size.ToString(), collate));
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsStringWithCollate(
            this ICreateTableColumnAsTypeSyntax syntax, string collate = DEFAULT_COLLATE)
        {
            return syntax.AsCustom(GetColumnType("max", collate));
        }

        private static string GetColumnType(string formattedSize, string collate)
        {
            return string.Format("nvarchar({0}) COLLATE {1}", formattedSize, collate);
        }

    }
}