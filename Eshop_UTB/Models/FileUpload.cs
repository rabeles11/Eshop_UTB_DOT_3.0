using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models
{
    public class FileUpload
    {
        string RootPath;
        string FolderName;
        string ContentType;
        public FileUpload(string rootPath,string folderName,string contentType)
        {
            this.RootPath = rootPath;
            this.ContentType = contentType;
            this.FolderName = folderName;
        }
        public bool CheckFileContent(IFormFile formFile)
        {
            return formFile != null && formFile.ContentType.ToLower().Contains(ContentType);
        }
        public bool CheckFileLengh(IFormFile formFile)
        {
            return formFile.Length > 0 && formFile.Length < 2_000_000;
        }
        public async Task<string> FileUploadAsync(IFormFile formFile)
        {
            string imgSrc = null;
            var img = formFile;
            if (CheckFileContent(img)&& CheckFileLengh(img))
            {
                var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                var fileExtension = Path.GetExtension(img.FileName);
                // ošetři cehck pokud už je v DB fotka tak jí změn jméno.
                var fileNameGenerated = Path.GetRandomFileName();
                var FileRelativePath = Path.Combine(ContentType + "s", FolderName, fileName + fileExtension);
                var filePath = Path.Combine(RootPath, FileRelativePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                imgSrc = $"/{FileRelativePath}";

            }

            return imgSrc;
        }
    }
}
