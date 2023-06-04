namespace Psinder.Services
{
    public class LocalStorageProvider : IFileStorage
    {
        private readonly string _filePath = Environment.CurrentDirectory+@"\PostsImages";

        public bool SaveFile(byte[] file, string fileName)
        {
            try
            {
                if (!Directory.Exists(_filePath))
                {
                    Directory.CreateDirectory(_filePath);
                }
                var imagePath = Path.Combine(_filePath, fileName);
                var fileStream = System.IO.File.Create(imagePath);
                fileStream.Write(file, 0, file.Length);
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public byte[]? GetImageFile(string fileName)
        {
            var imagePath = Path.Combine(_filePath, fileName);
            byte[]? file = null;
            try
            {
                file = File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                return null;
            }

            return file;
        }
    }
}