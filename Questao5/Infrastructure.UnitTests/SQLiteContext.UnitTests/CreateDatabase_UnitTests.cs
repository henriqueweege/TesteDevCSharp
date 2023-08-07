using Infrastructure.Data;
using System.Collections;
using Tools;

namespace Infrastructure.UnitTests.SQLiteContext.UnitTests;

public class CreateDatabase_UnitTests : IDisposable
{
    private SQLiteDbContext Context { get; set; }
    private string SqliteFilePath { get; set; }

    public CreateDatabase_UnitTests()
    {
        Context = new SQLiteDbContext();
        AppSettings.Set();
        SqliteFilePath = $"{System.IO.Directory.GetCurrentDirectory()}\\{AppSettings.FileNameOfSqliteDb}";
    }


    [Fact]
    public void GivenCallToCreateDataBase_ShouldCreateArchive()
    {


        //arrange
        Context.DbConnection.Close();

        FileTools.ExcludeIfExists(SqliteFilePath);

        //act
        Context.CreateDatabase();
        Context.Dispose();

        //assert
        Assert.True(File.Exists($"{SqliteFilePath}"));

    }


    [Fact]
    public void GivenCallToCreateDataBase_ShouldCreateTables()
    {
        //arrange
        Context.CreateDatabase();

        //act
        ArrayList tables = Context.GetTables();

        //assert
        Assert.True(tables.Contains("contacorrente"));
        Assert.True(tables.Contains("movimento"));
        Assert.True(tables.Contains("idempotencia"));

        Context.Dispose();

    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
