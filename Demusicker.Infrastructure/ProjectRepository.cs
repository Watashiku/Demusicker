using Demusicker.Core;
using Demusicker.Core.Models;
using System.Data.SQLite;

namespace Demusicker.Infrastructure
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _dbPath;

        public ProjectRepository(ProjectConfig conf)
        {
            _dbPath = conf.DatabasePath;
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
                InitSchema();
            }
        }

        private void InitSchema()
        {
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS ProcessingStep (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StepName TEXT NOT NULL,
                    Status TEXT NOT NULL, -- e.g. Pending, Done, Error
                    OutputPath TEXT,
                    LastUpdated DATETIME NOT NULL
                );
            ";
            cmd.ExecuteNonQuery();
        }

        public void UpdateStep(string stepName, string status, string outputPath)
        {
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO ProcessingStep (StepName, Status, OutputPath, LastUpdated)
                VALUES (@step, @status, @path, @updated)
                ON CONFLICT(StepName) DO UPDATE SET 
                    Status = excluded.Status,
                    OutputPath = excluded.OutputPath,
                    LastUpdated = excluded.LastUpdated;
            ";

            cmd.Parameters.AddWithValue("@step", stepName);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@path", outputPath);
            cmd.Parameters.AddWithValue("@updated", DateTime.UtcNow);

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<(string StepName, string Status, string? OutputPath)> GetAllSteps()
        {
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT StepName, Status, OutputPath FROM ProcessingStep";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return (
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.IsDBNull(2) ? null : reader.GetString(2)
                );
            }
        }
    }
}
