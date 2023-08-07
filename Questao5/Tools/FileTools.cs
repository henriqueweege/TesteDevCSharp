namespace Tools
{
    public static class FileTools
    {
        public static void ExcludeIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}