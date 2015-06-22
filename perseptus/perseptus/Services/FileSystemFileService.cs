using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using perseptus.Services;

namespace perseptus.Services
{
    public class FileSystemFileService : IFileService
    {
        const String BaseDirectory = @"~\data\";

        public List<String> GetFileNames()
        {
            try
            {
                var files = Directory.GetFiles(BaseDirectory, "*.jpg")
                    .ToList()
                    .Select(path => path = Path.GetFileNameWithoutExtension(path))
                    .ToList();
                
                return files;
            }
            catch
            {
                return new List<String>();
            }
        }

        public Stream GetFile(String name)
        {
            try
            {
                if (Directory.Exists(BaseDirectory))
                {
                    var files = Directory.GetFiles(BaseDirectory, name + ".*");
                    if (files.Length > 0)
                    {
                        name = files[0];
                        var file = File.Open(name, FileMode.Open);
                        var res = new MemoryStream();
                        file.CopyTo(res);
                        file.Close();
                        return res;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public bool AddFile(Stream stream, String name)
        {
            name = BaseDirectory + name;
            try
            {
                if (!Directory.Exists(BaseDirectory))
                    Directory.CreateDirectory(BaseDirectory);

                var fileStream = File.Open(name, FileMode.CreateNew);
                stream.CopyTo(fileStream);
                fileStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<KeyValuePair<String, double>> GetSimilarImages(String filePath)
        {
            var allFiles = GetFileNames();
            var result = new List<KeyValuePair<String, double>>();
            foreach (var filename in allFiles)
            {
                result.Add(new KeyValuePair<String, double>(filename, 71.3));
            }
            return result;
        }
    }
}