using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ddcSite.Controllers
{

    public class UploadFilesController : ApiController
    {
        [HttpPost]
        public List<string> Upload()
        {
            List<string> rsptList = new List<string>();

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                //var httpPostedFile = HttpContext.Current.Request.Files["attach"];
                //foreach (var item in HttpContext.Current.Request.Files)
                //{
                //    CheckConection();
                //    SaveFilesAzure(item);
                //    rsptList.Add(item.FileName);
                //}
                //for (int i = 0; i < Request.Files.Count; i++)
                //{
                //    var file = Request.Files[i];
                //    if (file != null && file.ContentLength > 0)
                //    {

                //    }
                //}
            }
            //var js = "<script>alert('guardadocorrectamente')</script>";
            //var js = "notifyAlertify('File(s) saved correctly.', 'success');";
            return rsptList;
        }

        public void CheckConection()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("filesddc");
            container.CreateIfNotExists();
        }

        public void SaveFilesAzure(HttpPostedFileBase file)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("filesddc");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
            byte[] data = new byte[file.ContentLength];
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes((int)file.InputStream.Length);
            file.InputStream.Read(data, 0, data.Length);
            blockBlob.UploadFromByteArray(data, 0, file.ContentLength);
        }
    }
}
