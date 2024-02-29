using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class FileService
    {
        public async Task<string> SaveFile(IFormFile file,String folder)
        {
            MediaService _mediaService = new MediaService();
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, folder , file.ContentType);
            return fileName;
        }
    }
}
