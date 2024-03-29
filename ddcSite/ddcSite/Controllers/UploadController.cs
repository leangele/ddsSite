﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ddcSite.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace ddcSite.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UploadController : Controller
    {
        //[System.Web.Http.HttpPost]
        //public KeyValuePair<bool, string> UploadFile()
        //{
        //    try
        //    {
        //        if (HttpContext.Current.Request.Files.AllKeys.Any())
        //        {
        //            // Get the uploaded image from the Files collection
        //            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

        //            if (httpPostedFile != null)
        //            {
        //                // Validate the uploaded image(optional)

        //                // Get the complete file path
        //                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

        //                // Save the uploaded file to "UploadedFiles" folder
        //                httpPostedFile.SaveAs(fileSavePath);

        //                return new KeyValuePair<bool, string>(true, "File uploaded successfully.");
        //            }

        //            return new KeyValuePair<bool, string>(true, "Could not get the uploaded file.");
        //        }

        //        return new KeyValuePair<bool, string>(true, "No file found to upload.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //public List<string> Upload()
        //{
        //    List<string> rsptList=new List<string>();
        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        //var httpPostedFile = HttpContext.Current.Request.Files["attach"];
        //        //foreach (var item in HttpContext.Current.Request.Files)
        //        //{
        //        //    CheckConection();
        //        //    SaveFilesAzure(item);
        //        //    rsptList.Add(item.FileName);
        //        //}
        //        //for (int i = 0; i < Request.Files.Count; i++)
        //        //{
        //        //    var file = Request.Files[i];
        //        //    if (file != null && file.ContentLength > 0)
        //        //    {

        //        //    }
        //        //}
        //    }
        //    //var js = "<script>alert('guardadocorrectamente')</script>";
        //    //var js = "notifyAlertify('File(s) saved correctly.', 'success');";
        //    return rsptList;
        //}

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

        public ActionResult Attach()
        {
            return View();
        }

        public ActionResult simple_upload(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

                uploader.InsertText = "Select a file to upload";

                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();

                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    //for single file , the value is guid string
                    Guid fileguid = new Guid(myuploader);
                    CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                    if (file != null)
                    {
                        //you should validate it here:
                        //now the file is in temporary directory, you need move it to target location
                        //file.MoveTo("~/myfolder/" + file.FileName);
                        //set the output message
                        ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult selecting_multiple_files(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
                //allow select multiple files
                uploader.MultipleFilesUpload = true;
                //tell uploader attach a button
                uploader.InsertButtonID = "uploadbutton";
                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();
                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    List<string> processedfiles = new List<string>();
                    //for multiple files , the value is string : guid/guid/guid 
                    foreach (string strguid in myuploader.Split('/'))
                    {
                        Guid fileguid = new Guid(strguid);
                        CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                        if (file != null)
                        {
                            //you should validate it here:
                            //now the file is in temporary directory, you need move it to target location
                            //file.MoveTo("~/myfolder/" + file.FileName);
                            processedfiles.Add(file.FileName);
                        }
                    }
                    if (processedfiles.Count > 0)
                    {
                        ViewData["UploadedMessage"] = string.Join(",", processedfiles.ToArray()) + " have been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult simple_upload_UI(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

                uploader.InsertText = "Select a file to upload";
                uploader.InsertButtonID = "Uploader1Insert";
                uploader.ProgressCtrlID = "Uploader1Progress";
                uploader.ProgressTextID = "Uploader1ProgressText";
                uploader.CancelButtonID = "Uploader1Cancel";

                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();

                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    //for single file , the value is guid string
                    Guid fileguid = new Guid(myuploader);
                    CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                    if (file != null)
                    {
                        //you should validate it here:
                        //now the file is in temporary directory, you need move it to target location
                        //file.MoveTo("~/myfolder/" + file.FileName);
                        //set the output message
                        ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult simple_upload_Validation(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "jpeg,jpg,gif,png";
                uploader.MaxSizeKB = 100;
                uploader.InsertText = "Select a file to upload";
                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();

                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    //for single file , the value is guid string
                    Guid fileguid = new Guid(myuploader);
                    CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                    if (file != null)
                    {
                        //you should validate it here:
                        //now the file is in temporary directory, you need move it to target location
                        //file.MoveTo("~/myfolder/" + file.FileName);
                        //set the output message
                        ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult multiple_files_upload(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
                //allow select multiple files
                uploader.MultipleFilesUpload = true;
                //tell uploader attach a button
                uploader.InsertButtonID = "uploadbutton";
                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();
                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    List<string> processedfiles = new List<string>();
                    //for multiple files , the value is string : guid/guid/guid 
                    foreach (string strguid in myuploader.Split('/'))
                    {
                        Guid fileguid = new Guid(strguid);
                        CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                        if (file != null)
                        {
                            //you should validate it here:
                            //now the file is in temporary directory, you need move it to target location
                            //file.MoveTo("~/myfolder/" + file.FileName);
                            processedfiles.Add(file.FileName);
                        }
                    }
                    if (processedfiles.Count > 0)
                    {
                        ViewData["UploadedMessage"] = string.Join(",", processedfiles.ToArray()) + " have been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult Large_File_Upload(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar,*.txt,*.exe,*.doc,*.docx,*.pdf";
                uploader.InsertText = "Select a file to upload";
                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();
                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    //for single file , the value is guid string
                    Guid fileguid = new Guid(myuploader);
                    CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                    if (file != null)
                    {
                        //you should validate it here:
                        //now the file is in temporary directory, you need move it to target location
                        //file.MoveTo("~/myfolder/" + file.FileName);
                        //set the output message
                        ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult Persist_upload_file(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
                uploader.MaxSizeKB = 10240;
                //tell uploader attach a button
                uploader.InsertText = "Upload File";
                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();
                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    List<string> processedfiles = new List<string>();
                    //for multiple files , the value is string : guid/guid/guid 
                    foreach (string strguid in myuploader.Split('/'))
                    {
                        Guid fileguid = new Guid(strguid);
                        CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                        if (file != null)
                        {
                            processedfiles.Add(file.FileName);
                        }
                    }
                    if (processedfiles.Count > 0)
                    {
                        ViewData["UploadedMessage"] = string.Join(",", processedfiles.ToArray()) + " have been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult multiple_files_upload_control_file_number(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
                uploader.MaxSizeKB = 10240;
                uploader.MultipleFilesUpload = true;
                //tell uploader attach a button
                uploader.InsertButtonID = "uploadbutton";
                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();
            }
            return View();
        }

        public ActionResult Start_uploading_manually(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
                uploader.MaxSizeKB = 1024;
                uploader.ManualStartUpload = true;
                uploader.InsertText = "Browse Files (Max 1M)";

                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();

                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    //for single file , the value is guid string
                    Guid fileguid = new Guid(myuploader);
                    CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                    if (file != null)
                    {
                        //you should validate it here:
                        //now the file is in temporary directory, you need move it to target location
                        //file.MoveTo("~/myfolder/" + file.FileName);
                        //set the output message
                        ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult Drag_drop_file(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
                uploader.MaxSizeKB = 1024;
                uploader.ManualStartUpload = true;
                uploader.InsertText = "Browse Files (Max 1M)";

                uploader.MultipleFilesUpload = true;
                uploader.InsertButtonID = "InsertButton";
                uploader.QueuePanelID = "QueuePanel";
                //uploader.FileDropPanelID = "DropPanel";

                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();

                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    string msgs = "";
                    foreach (string guidstr in myuploader.Split('/'))
                    {
                        //for single file , the value is guid string
                        Guid fileguid = new Guid(guidstr);
                        CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                        if (file != null)
                        {
                            //you should validate it here:
                            //now the file is in temporary directory, you need move it to target location
                            //file.MoveTo("~/myfolder/" + file.FileName);
                            //set the output message
                            msgs += "The file " + file.FileName + " has been processed.\r\n";
                        }
                    }
                    ViewData["UploadedMessage"] = msgs;
                }
            }
            return View();
        }

        public ActionResult Customize_Progress_Bar(string myuploader)
        {
            using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            {
                uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
                //the data of the uploader will render as <input type='hidden' name='myuploader'> 
                uploader.Name = "myuploader";
                uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";

                uploader.InsertText = "Select a file to upload";

                //prepair html code for the view
                ViewData["uploaderhtml"] = uploader.Render();

                //if it's HTTP POST:
                if (!string.IsNullOrEmpty(myuploader))
                {
                    //for single file , the value is guid string
                    Guid fileguid = new Guid(myuploader);
                    CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(fileguid);
                    if (file != null)
                    {
                        //you should validate it here:
                        //now the file is in temporary directory, you need move it to target location
                        //file.MoveTo("~/myfolder/" + file.FileName);
                        //set the output message
                        ViewData["UploadedMessage"] = "The file " + file.FileName + " has been processed.";
                    }
                }
            }
            return View();
        }

        public ActionResult FileUploadAjaxWithoutSave(string guidlist, string limitcount, string hascount)
        {
            List<FileManagerJsonItem> items = new List<FileManagerJsonItem>();
            int maxcount = 0;
            int existcount = 0;
            int.TryParse(hascount, out existcount);
            int.TryParse(limitcount, out maxcount);
            if (!string.IsNullOrEmpty(guidlist))
            {
                using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
                {
                    int ix = 0;
                    foreach (string strguid in guidlist.Split('/'))
                    {
                        if (maxcount > 0 && ix + existcount >= maxcount)
                        {
                            break;
                        }
                        CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(new Guid(strguid));
                        if (file == null)
                            continue;
                        ix++;
                        FileManagerJsonItem item = new FileManagerJsonItem();
                        item.FileID = file.FileGuid.ToString();
                        item.FileName = file.FileName;
                        item.FileSize = file.FileSize;
                        items.Add(item);
                    }
                }
            }
            JsonResult json = new JsonResult();
            json.Data = items;
            return json;
        }

        public ActionResult FilesUploadAjax(string guidlist, string deleteid)
        {
            FileManagerProvider manager = new FileManagerProvider();
            string username = GetCurrentUserName();

            if (!string.IsNullOrEmpty(guidlist))
            {
                using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
                {
                    foreach (string strguid in guidlist.Split('/'))
                    {
                        CuteWebUI.MvcUploadFile file = uploader.GetUploadedFile(new Guid(strguid));
                        if (file == null)
                            continue;
                        //savefile here
                        manager.MoveFile(username, file.GetTempFilePath(), file.FileName, null);
                    }
                }
            }

            if (!string.IsNullOrEmpty(deleteid))
            {
                FileItem file = manager.GetFileByID(username, deleteid);
                if (file != null)
                {
                    file.Delete();
                }
            }

            FileItem[] files = manager.GetFiles(username);
            Array.Reverse(files);
            FileManagerJsonItem[] items = new FileManagerJsonItem[files.Length];
            string baseurl = Response.ApplyAppPathModifier("~/FileManagerDownload.ashx?user=" + username + "&file=");
            for (int i = 0; i < files.Length; i++)
            {
                FileItem file = files[i];
                FileManagerJsonItem item = new FileManagerJsonItem();
                item.FileID = file.FileID;
                item.FileName = file.FileName;
                item.Description = file.Description;
                item.UploadTime = file.UploadTime.ToString("yyyy-MM-dd HH:mm:ss");
                item.FileSize = file.FileSize;
                item.FileUrl = baseurl + file.FileID;
                items[i] = item;
            }
            JsonResult json = new JsonResult();
            json.Data = items;
            return json;
        }

        protected string GetCurrentUserName()
        {
            return "Guest";
        }
    }


    public class SampleTempJsonItem
    {
        public string FileGuid;
        public string FileName;
        public int FileSize;
        public bool Exists;
        public string Error;
    }

    public class FileManagerJsonItem
    {
        public string FileID;
        public string FileName;
        public int FileSize;
        public string Description;
        public string UploadTime;
        public string FileUrl;
    }
}
