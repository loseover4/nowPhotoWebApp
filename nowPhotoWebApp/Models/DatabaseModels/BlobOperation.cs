using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class BlobOperation
    {
        private static CloudBlobClient blobClient;

        /// <summary>
        /// Initialize BLOB and Queue Here
        /// </summary>
        public BlobOperation()
        {
            // Try to get the Azure storage account token from app settings.
            var storageAccountName = ConfigurationManager.AppSettings["STORAGE_ACCOUNT_NAME"];
            var storagePrimaryAccessKey = ConfigurationManager.AppSettings["STORAGE_ACCOUNT_PRIMARY_ACCESS_KEY"];

            if (storageAccountName == null || storagePrimaryAccessKey == null)
            {
                //TODO: Log
                return;
            }

            // Set the URI for the Blob Storage service.
            Uri blobEndpoint = new Uri(string.Format("https://{0}.blob.core.windows.net", storageAccountName));

            // Create the BLOB service client.
            blobClient = new CloudBlobClient(blobEndpoint, new StorageCredentials(storageAccountName, storagePrimaryAccessKey));
        }


        /// <summary>
        /// Method to Upload the BLOB
        /// </summary>
        /// <param name=""profileFile"">
        /// <returns></returns>
        public async Task<CloudBlockBlob> UploadBlob(string userName, HttpPostedFileBase file, bool isPublic = false)
        {
            // Get the blob container reference.
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("test");
            blobContainer.CreateIfNotExists();

            if (isPublic)
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // GET a blob reference. 
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobName);

            // Uploading a local file and Create the blob.
            using (var fs = file.InputStream)
            {
                await blob.UploadFromStreamAsync(fs);
            }

            return blob;
        }

    }
}