using System;
using System.IO;

namespace VersionKeeper.Contracts.Services
{
    public interface IVersionReader
    {
        Version Read(string versionFilePath);
        Version Read(FileInfo versionFile);
    }
}