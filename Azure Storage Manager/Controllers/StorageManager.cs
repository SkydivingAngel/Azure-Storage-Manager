using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Azure_Storage_Manager.Controllers
{
    [ApiController]
    [Route("api")]
    public class StorageManager : ControllerBase
    {
        private IWebHostEnvironment env;
        private readonly IConfiguration configuration;

        public StorageManager(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.configuration = configuration;
        }

        [HttpGet, Route("createcontainer")]
        public async Task<IActionResult> CreateContainer()
        {
            try
            {
                BlobContainerClient blobContainer = new BlobContainerClient(configuration.GetConnectionString("ContainerConnectionString"), "createifnotexistscontainer");

                string result = string.Empty;

                //use code below to check if the container exists, then determine to create it or not
                bool exists = await blobContainer.ExistsAsync();
                if (!exists)
                {
                    result += "Does not exists";
                    //blobContainer.Create();
                }

                //or use this code to create the container directly, if it does not exist.
                //var createResult = await blobContainer.CreateIfNotExistsAsync();
                await blobContainer.CreateIfNotExistsAsync();
                //if (createResult.HasValue)
                //{
                //    result += " - " + createResult.Value.LastModified;
                //}

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BadRequestObjectResult(ex.Message));
            }

            return Ok(@"Creato!");
        }

        [HttpGet, Route("createfile")]
        public async Task<IActionResult> CreateFile()
        {
            try
            {
                BlobContainerClient blobContainer = new BlobContainerClient(configuration.GetConnectionString("ContainerConnectionString"), "createifnotexistscontainer");

                BlobClient blobClient = blobContainer.GetBlobClient("IDataProtector.txt");

                string result = string.Empty;

                Response<BlobContentInfo>? uploadFile = await blobClient.UploadAsync(Path.Combine(env.ContentRootPath, "appsettings.json"), true);  // <--- true SOVRASCRIVE

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BadRequestObjectResult(ex.Message));
            }

            return Ok(@"Creato!");
        }
    }
}
