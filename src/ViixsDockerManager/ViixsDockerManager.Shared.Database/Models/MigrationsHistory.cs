using System.Text;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Shared.Database.Models;

public record MigrationsHistory(string tableName)
{
    private const string MigrationsHistoryTableSuffix = "MigrationsHistory";
    private const string DoubleUnderscore = "__";
    private const char Underscore = '_';
    public string GetMigrationTableName()
    {
        var migrationsTable = Guard.ValueIsNotNullOrEmpty(tableName, nameof(tableName));

        migrationsTable = migrationsTable.TrimStart(Underscore);

        if (migrationsTable.EndsWith(MigrationsHistoryTableSuffix, StringComparison.OrdinalIgnoreCase))
        {
            migrationsTable = migrationsTable[..^MigrationsHistoryTableSuffix.Length];
        }

        return $"{DoubleUnderscore}{migrationsTable}{MigrationsHistoryTableSuffix}";        
    }
}
