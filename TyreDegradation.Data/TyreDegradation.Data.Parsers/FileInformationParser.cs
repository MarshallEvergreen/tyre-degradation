using System.IO.Abstractions;

namespace TyreDegradation.Data.Parsers
{
    public abstract class FileInformationParser
    {
        protected FileInformationParser(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        protected IFileSystem FileSystem { get; }
    }
}