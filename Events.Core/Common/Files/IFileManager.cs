using Events.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Events.core.Common.Files
{
   public interface IFileManager
    {
        public string SaveFile(IFormFile file);
        public string CreateFileLocationServer(string[] paths);
        public string CreateDirectoryName(string path);

        public string CreateFileOnServer(IFormFile file, string pathServer, string pathToSave, int origenDoc);
        //public List<Model.Files> SetFilesStatus(bool newStatus, List<Model.Files> fileList);
        public Task<MemoryStream> GetPhysicalFile(FileData document);

    }
}
