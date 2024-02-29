
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{

    public class MediaService
    {
        //public string GetMediaUrl(CoreMedium media)
        //{
        //    if (media == null)
        //    {
        //        return GetMediaUrl("no-image.png");
        //    }

        //    return GetMediaUrl(media.FileName);
        //}




        //public string GetThumbnailUrl(CoreMedium media)
        //{
        //    return GetMediaUrl(media);
        //}






        public string GetMediaUrl(string fileName, string rootFolder = null)
        {
            return $"/{rootFolder}/{fileName}";
        }

        public async Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string rootFolder, string mimeType = null)
        {
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), MediaRootFoler, fileName);
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Upload\\", rootFolder);
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Upload\\", rootFolder,
               fileName);


            using (var output = new FileStream(path, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            }
        }

        public async Task DeleteMediaAsync(string fileName, string rootFolder = null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Upload\\", rootFolder, fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }



}
