using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace Azure_Storage_Manager.Controllers
{
    [ApiController]
    [Route("api")]
    public class StorageManager : ControllerBase
    {
        [HttpGet, Route("createcontainer")]
        public async Task<IActionResult> CreateContainer()
        {
            try
            {
                BlobContainerClient blobContainer = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=gasparistorage;AccountKey=LEJPG03t9GfKpQgL0GaO5LTiAgo9bmqYP/8gNUC0xDL19KRdXUZuSNooOMuGhcbnVFc38z4W2z5V+AStLLZCfw==;EndpointSuffix=core.windows.net", "createifnotexistscontainer");

                string result = string.Empty;

                //use code below to check if the container exists, then determine to create it or not
                bool exists = blobContainer.Exists();
                if (!exists)
                {
                    result += "Does not exists";
                    //blobContainer.Create();
                }

                //or use this code to create the container directly, if it does not exist.
                var createResult = await blobContainer.CreateIfNotExistsAsync();

                if (createResult.HasValue)
                {
                    result += " - " + createResult.Value.LastModified;
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BadRequestObjectResult(ex.Message));
            }

            return Ok(@"Creato!");
        }


    }
}
