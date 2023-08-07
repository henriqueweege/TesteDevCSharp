namespace Infrastructure.Data;

public static class DataBaseSetter
{
    private static bool AlreadyDone { get; set; } = false;

    public static void Set()
    {
        if (!AlreadyDone)
        {
            AlreadyDone = true;
            var context = new SQLiteDbContext();
            context.CreateDatabase();
            context.Dispose();
        }
    }
}
