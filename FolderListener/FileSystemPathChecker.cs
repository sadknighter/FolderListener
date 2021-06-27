using System.IO;

namespace BCLTaskApp
{
    public static class FileSystemPathChecker
    {
        public static void CreateDirectoryIfNotExist(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public static string RenameFileIfThereAreFileWithSameName(string path)
        {
            var index = 0;
            while (new FileInfo(path).Exists)
            {
                var fileName = Path.GetFileNameWithoutExtension(path);
                var extension = Path.GetExtension(path);
                var directory = Path.GetDirectoryName(path) ?? throw new DirectoryNotFoundException(path);
                fileName += index + extension;
                path = Path.Combine(directory, fileName);
                index++;
            }

            return path;
        }
    }
}