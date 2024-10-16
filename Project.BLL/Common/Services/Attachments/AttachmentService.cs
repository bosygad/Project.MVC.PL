using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Common.Services.Attachments
{
    internal class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private const int _allwedMaxsize = 2_097_152;
        public string Upload(IFormFile file, string FolderName)
        {
            var extension = Path.GetExtension(file.FileName);

            if (_allowedExtensions.Contains(extension))
                return null;

            if (file.Length > _allwedMaxsize)
            {
                return null;
            }

            //  var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{FolderName}";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);

            var FileName = $"{Guid.NewGuid()}{extension}";
            var FilePath = Path.Combine(FolderPath, FileName);


            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);
            return FileName;
        }
        public bool Delete(string FilePath)
        {
            if (File.Exists(FilePath))
            {

                File.Delete(FilePath);
                return true;
            }
            return false;
        }

        
    }
}
