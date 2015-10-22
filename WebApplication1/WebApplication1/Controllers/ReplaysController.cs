using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ReplaysController : ApiController
    {
        // GET: api/Replays
        public string Get(string fileid)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("replaycontainer");

            // Retrieve reference to a blob by using fileid
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference(fileid);

            // Extract the contents of the blob
            string text;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob2.DownloadToStream(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return text;
        }

        // POST: api/Replays
        public void Post(FormDataCollection form)
        {
            string fileid = form.Get("fileid");
            string content = form.Get("content");

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("replaycontainer");

            // Retrieve reference to a blob using file id.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileid);

            // Create blob with contents from a the received form.
            blockBlob.UploadText(content);
        }

        // POST /createcontainer (create new tables)
        [Route("createcontainer")]
        public void PostContainer()
        {
            //retrieve storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("replaycontainer");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            // Set container to be public
            container.SetPermissions(
                new BlobContainerPermissions { PublicAccess =
                BlobContainerPublicAccessType.Blob});

        }
    }
}
