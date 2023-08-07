namespace Tools.UnitTests
{
    public class FileTools_UnitTests
    {
        public string Path { get; set; }
        public FileTools_UnitTests()
        {
            Path = $"{System.IO.Directory.GetCurrentDirectory()}\\test.txt";
        }


        [Fact]
        public void GivenExistingFile_ExcludeIfExists_ShouldExcludeFile()
        {
            //arrange
            var file = File.Create(Path);
            file.Dispose();
            //act
            FileTools.ExcludeIfExists(Path);

            //assert
            Assert.False(File.Exists(Path));
        }
    }
}