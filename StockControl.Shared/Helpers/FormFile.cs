namespace StockControl.Shared.Helpers
{
    public class FormFile
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }

        public FormFile(Stream fileStream, string fileName)
        {
            FileStream = fileStream;
            FileName = fileName;
        }
    }
}
