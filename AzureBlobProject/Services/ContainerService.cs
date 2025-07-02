
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;
       

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            
        }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerNames = new();

            await foreach(BlobContainerItem item in _blobServiceClient.GetBlobContainersAsync())
            {
                containerNames.Add(item.Name);
            }

            return containerNames;
        }

        public async Task<List<string>> GetAllContainersAndBlobs()
        {
            List<string> containerAndBlobName = new();
            containerAndBlobName.Add("-----AccountName : " + _blobServiceClient.AccountName + "------");
            containerAndBlobName.Add("---------------------------------------------------------------");

            await foreach (BlobContainerItem item in _blobServiceClient.GetBlobContainersAsync())
            {
                containerAndBlobName.Add("-----"+item.Name);

                BlobContainerClient _blobContainer = _blobServiceClient.GetBlobContainerClient(item.Name);

                await foreach (BlobItem blobItem in _blobContainer.GetBlobsAsync())
                { 
                    containerAndBlobName.Add($">>{blobItem.Name}");
                }
            }

            return containerAndBlobName;
        }
    }
}
