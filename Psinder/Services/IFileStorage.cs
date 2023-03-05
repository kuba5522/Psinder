namespace Psinder.Services
{
    public interface IFileStorage
    {
        bool SaveFile(byte[] file, string fileName);
    }
}
