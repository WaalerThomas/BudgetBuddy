using Dapper;
using Microsoft.Data.Sqlite;

namespace BudgetBuddySqlite;

public class DatabaseContext : IDisposable
{
    private readonly SqliteConnection _connection;
    private string _connectionString = "";

    public DatabaseContext(string filePath, bool rebuildDatabase = false)
    {
        // TODO: Test that a filename is also given. Can also be given an abstract filepath.
        string[] fileExtentions = { ".db", ".sqlite", ".sqlite3", ".db3", ".s3db", ".sl3"};
        if (! fileExtentions.Any(s => filePath.Contains(s)) )
            throw new ArgumentException($"filePath needs to contain a filename with one of these file extentions: {string.Join(',', fileExtentions)}");

        bool databaseExists = File.Exists(filePath);

        _connectionString = $"Data Source={filePath}";
        _connection = new SqliteConnection(_connectionString);

        if (rebuildDatabase)
        {
            DropAllTables();
            databaseExists = false;
        }

        CreateTables(insertInitialData: !databaseExists);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public SqliteConnection GetConnection()
    {
        return _connection;
    }

    private void DropAllTables()
    {
        _connection.Execute(
            @"
                DROP TABLE IF EXISTS budget_group;
                DROP TABLE IF EXISTS category;
                DROP TABLE IF EXISTS account;
                DROP TABLE IF EXISTS transaction_status;
                DROP TABLE IF EXISTS flow_control;
                DROP TABLE IF EXISTS budget_transaction;
                DROP TABLE IF EXISTS category_transfer;
            "
        );
    }

    private void CreateTables(bool insertInitialData = false)
    {
        _connection.Execute(
            @"
            CREATE TABLE IF NOT EXISTS budget_group (
                id   INTEGER PRIMARY KEY NOT NULL UNIQUE,
                name TEXT NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS category (
                id INTEGER PRIMARY KEY NOT NULL UNIQUE,
                name TEXT NOT NULL,
                monthly_amount TEXT,
                goal_amount TEXT,
                group_id INTEGER NOT NULL,
                FOREIGN KEY (group_id) REFERENCES budget_group(id)
            );

            CREATE TABLE IF NOT EXISTS account (
                id INTEGER PRIMARY KEY NOT NULL UNIQUE,
                name TEXT NOT NULL UNIQUE,
                balance TEXT
            );

            CREATE TABLE IF NOT EXISTS transaction_status (
                id INTEGER PRIMARY KEY NOT NULL UNIQUE,
                status TEXT
            );

            CREATE TABLE IF NOT EXISTS flow_control (
                id INTEGER PRIMARY KEY NOT NULL UNIQUE,
                name TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS budget_transaction (
                id INTEGER PRIMARY KEY NOT NULL UNIQUE,
                date TEXT NOT NULL,
                amount INTEGER,
                category_id INTEGER DEFAULT NULL,
                account_id INTEGER DEFAULT NULL,
                memo TEXT,
                flow_control_id INTEGER DEFAULT NULL,
                status_id INTEGER NOT NULL,
                FOREIGN KEY(category_id) REFERENCES category(id),
                FOREIGN KEY(account_id) REFERENCES account(id),
                FOREIGN KEY(flow_control_id) REFERENCES flow_control(id),
                FOREIGN KEY(status_id) REFERENCES transaction_status(id)
            );

            CREATE TABLE IF NOT EXISTS category_transfer (
                id INTEGER PRIMARY KEY NOT NULL UNIQUE,
                date TEXT NOT NULL,
                amount TEXT NOT NULL,
                from_category_id INTEGER NOT NULL,
                to_category_id INTEGER NOT NULL,
                memo TEXT NOT NULL,
                FOREIGN KEY(from_category_id) REFERENCES category(id)
                FOREIGN KEY(to_category_id) REFERENCES category(id)
            );
            "
        );

        if (insertInitialData)
        {
            _connection.Execute(
                @"
                INSERT INTO transaction_status (status) VALUES ('Completed');
                INSERT INTO transaction_status (status) VALUES ('Pending');
                INSERT INTO transaction_status (status) VALUES ('Reconciled');

                INSERT INTO flow_control (name) VALUES ('Account Transfer');
                INSERT INTO flow_control (name) VALUES ('Balance Adjustment');
                INSERT INTO flow_control (name) VALUES ('Starting Balance');
                "
            );
        }
    }
}