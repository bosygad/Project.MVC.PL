using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {

        Task<string> UploadAsync(IFormFile file, string FolderName);

        bool Delete(string FilePath);

    }
}
