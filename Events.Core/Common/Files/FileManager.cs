using Events.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Events.core.Common.Files
{
    public class FileManager : IFileManager
    {

        public string SaveFile(IFormFile file)
        {
            int origenDoc = Int32.Parse(file.Name);
            string[] paths = { "Resources", "tmp" }; //TODO: change the tmp for the correct folder
            string locationServer = CreateFileLocationServer(paths);
            string directoryName = CreateDirectoryName(locationServer);

            string urlRet = CreateFileOnServer(file, locationServer, directoryName, origenDoc);

            return urlRet;

        }

        public string CreateDirectoryName(string fullPath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), fullPath);
        }

        public string CreateFileLocationServer(string[] paths)
        {
            return Path.Combine(paths);

        }

        public string CreateFileOnServer(IFormFile file, string pathServer, string pathToSave, int origenDoc)
        {
            var tmp = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string ext = Path.GetExtension(tmp);

            var fileName = Guid.NewGuid().ToString() + ext;//ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(pathServer, fileName);


            //TODO: validar existencia directorio
            //ver el tema de la imagen del blog
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return dbPath;
        }


        public async Task<MemoryStream> GetPhysicalFile(FileData document)
        {
            var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), document.Url);
           // var fileRet = Directory.EnumerateFiles(pathToRead);


            if (!System.IO.File.Exists(pathToRead))
                 throw new ApplicationException("File not found.");

            var memory = new MemoryStream();
            await using (var stream = new FileStream(pathToRead, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return memory;
        }


    }
}
