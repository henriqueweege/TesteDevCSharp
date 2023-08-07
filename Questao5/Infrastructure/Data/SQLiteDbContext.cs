using Infrastructure.Data.Contracts;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using Tools;

namespace Infrastructure.Data;

public class SQLiteDbContext : IDbContext, IDisposable
{

    public SQLiteConnection DbConnection;

    private string SqliteFilePath { get; set; }
    public SQLiteDbContext()
    {
        SqliteFilePath = $"{System.IO.Directory.GetCurrentDirectory()}\\{AppSettings.FileNameOfSqliteDb}";
        CreateDbConnection();
    }

    private void CreateDbConnection()
    {
        DbConnection = new SQLiteConnection($"URI=file:{SqliteFilePath}");
        DbConnection.Open();
    }
    public void CreateDatabase()
    {
        try
        {
            DbConnection.Close();

            FileTools.ExcludeIfExists(SqliteFilePath);
            SQLiteConnection.CreateFile($"{SqliteFilePath}");

            DbConnection.Open();

            CreateTables();
            PopulateTables();
        }
        catch (Exception ex)
        {
            //Log;
            throw ex;
        }
    }
    private void CreateTables()
    {
        try
        {
            using (var cmd = DbConnection.CreateCommand())
            {

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS contacorrente (" +
                    "                   idcontacorrente VARCHAR(37) PRIMARY KEY," +
                    "                   numero INTEGER NOT NULL UNIQUE," +
                    "                   nome VARCHAR(100) NOT NULL," +
                    "                   ativo BOOLEAN NOT NULL DEFAULT false);" +
                    "                                                                     " +
                    "               CREATE TABLE IF NOT EXISTS movimento (" +
                    "                   idmovimento VARCHAR(37) PRIMARY KEY," +
                    "                   idcontacorrente VARCHAR(37) NOT NULL," +
                    "                   datamovimento DATE NOT NULL," +
                    "                   tipomovimento VARCHAR(1) NOT NULL," +
                    "                   valor REAL NOT NULL,CHECK (tipomovimento IN ('C', 'D'))," +
                    "               FOREIGN KEY (idcontacorrente) REFERENCES contacorrente (idcontacorrente));" +
                    "                                                                      " +
                    "               CREATE TABLE IF NOT EXISTS idempotencia (" +
                    "                   chave_idempotencia VARCHAR(37) PRIMARY KEY," +
                    "                   requisicao VARCHAR(1000)," +
                    "                   resultado VARCHAR(1000));";


                var result = cmd.ExecuteNonQuery();
            }


        }
        catch (Exception ex)
        {
            //Log;
            throw ex;
        }
    }

    public ArrayList GetTables()
    {
        try
        {

            ArrayList tablesName = new ArrayList();

            String query = "SELECT name FROM sqlite_master " +
                    "WHERE type = 'table'" +
                    "ORDER BY 1";


            DataTable tables = GetDataTable(query);

            foreach (DataRow row in tables.Rows)
            {
                tablesName.Add(row.ItemArray[0].ToString());
            }
            tables.Dispose();

            return tablesName;
        }
        catch (Exception ex)
        {
            //Log;
            throw ex;
        }
    }

    private DataTable GetDataTable(string sql)
    {
        try
        {

            DataTable dataTable = new DataTable();
            using (var connection = new SQLiteConnection(DbConnection))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);

                        reader.DisposeAsync();
                    }
                    cmd.Dispose();
                }
                connection.Dispose();
            }

            return dataTable;
        }
        catch (Exception ex)
        {
            //Log;
            throw ex;
        }

    }
    private void PopulateTables()
    {
        try
        {

            using (var cmd = DbConnection.CreateCommand())
            {
                cmd.CommandText =

                    "INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('b6bafc09-6967-ed11-a567-055dfa4a16c9', 123, 'Katherine Sanchez', 1);" +
                    "INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('fa99d033-7067-ed11-96c6-7c5dfa4a16c9', 456, 'Eva Woodward', 1);" +
                    "INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('382d323d-7067-ed11-8866-7d5dfa4a16c9', 789, 'Tevin Mcconnell', 1);" +
                    "INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('f475f943-7067-ed11-a06b-7e5dfa4a16c9', 741, 'Ameena Lynn', 0);" +
                    "INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('bcdaca4a-7067-ed11-af81-825dfa4a16c9', 852, 'Jarrad Mckee', 0);" +
                    "INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo) VALUES('d2e02051-7067-ed11-94c0-835dfa4a16c9', 963, 'Elisha Simons', 0);";


                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            //Log;
            throw ex;
        }
    }

    public void Dispose()
    {
        if (DbConnection is not null)
        {
            DbConnection.Close();
            DbConnection.DisposeAsync();
            DbConnection = null;
        }
    }
}
