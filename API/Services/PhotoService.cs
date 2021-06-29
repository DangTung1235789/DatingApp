
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
/*
- what we can do now is add a service that we can use to upload and delete our photos and the reason for creating services like this is
that our photo upload service, we want it to have one responsibility
- that's to receive a file as a parameter and then upload the file to cloud memory and receive the result back from Cloudinary and return
the upload results to whatever needs it 
*/
namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        //we need to give this the details of our API keys
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                 using var stream = file.OpenReadStream();
                 var uploadParams = new ImageUploadParams
                 {
                     File = new FileDescription(file.FileName, stream),
                     Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                 };
                 uploadResult = await _cloudinary.UploadAsync(uploadParams);                 
            }

            return uploadResult;
        }

        public  async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}