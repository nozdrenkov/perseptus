using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perseptus.ViewInterfaces
{
    public interface IImageModel
    {
        IEnumerable<String> GetAllImages();
    }
}
