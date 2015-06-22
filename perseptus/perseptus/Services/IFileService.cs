using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perseptus.Services
{
    public interface IFileService
    {
        Stream GetFile(String name);
        List<String> GetFileNames();
        List<KeyValuePair<String, double>> GetSimilarImages(String name);
        bool AddFile(Stream file, String name);
    }
}