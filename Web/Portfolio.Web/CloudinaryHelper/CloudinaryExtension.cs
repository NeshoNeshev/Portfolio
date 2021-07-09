﻿using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Portfolio.Web.CloudinaryHelper
{
    public class CloudinaryExtension
    {
        public static async Task<IEnumerable<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile>files)
        {
            List<string> list = new List<string>();
            foreach (var file in files)
            {
                byte[] destinationImage;
                using (var memoryStream = new MemoryStream())
                {
                  await  file.CopyToAsync(memoryStream);
                  destinationImage = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, destinationStream)
                    };
                  var result=  await cloudinary.UploadAsync(uploadParams);

                  list.Add(result.Uri.AbsoluteUri);
                }
            }

            return list;
        }
    }
}