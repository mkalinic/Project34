using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace IGG.TenderPortal.WebService.Compression
{
    public class Zip
    {
      

        public static bool ZipFolder(string fromPath, string toPath) {
            //ZipFile.CreateFromDirectory(fromPath, toPath, CompressionLevel.Optimal, false);
            return true;
        }

        
    }
}