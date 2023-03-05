namespace Psinder.Services
{
    public interface IFileStorage
    {
        byte[]? GetImageFile(string fileName);
        bool SaveFile(byte[] file, string fileName);
    }
}