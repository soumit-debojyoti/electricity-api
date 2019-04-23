﻿using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.Common_Service rs = null;
        private IHostingEnvironment _hostingEnvironment;
        public CommonController(IOptions<ConnectionStrings> config, IHostingEnvironment hostingEnvironment)
        {
            this.config = config;
            rs = new Common_Service(this.config.Value);
            _hostingEnvironment = hostingEnvironment;
        }


        // GET api/values
        [Route("addressproofs")]
        [HttpGet]
        public async Task<ActionResult> GetAddressProof()
        {
            List<AddressProof> addressProof = await rs.GetAddressProof();
            return Ok(addressProof);
        }

        // GET api/values
        [Route("idproofs")]
        [HttpGet]
        public async Task<ActionResult> GetIdProof()
        {
            List<IdProof> addressProof = await rs.GetIdProof();
            return Ok(addressProof);
        }

        // GET api/values
        [Route("states")]
        [HttpGet]
        public async Task<ActionResult> GetStates()
        {
            List<State> states = await rs.GetStates();
            return Ok(states);
        }

        [Route("upload/{filetype}")]
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile(string filetype)
        {

            string fullPath = string.Empty;
            string fileName = string.Empty;
            try
            {
                var file = Request.Form.Files[0];
                string folderName = filetype;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok(filetype+"/"+fileName);
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: ");
            }
        }

        ////[Authorize]
        //[Route("download/{fileName}/type/{fileType}")]
        //[HttpGet]
        //public FileResult GetFile(string fileName, string fileType)
        //{

        //    string _folderName = fileType;
        //    string webRootPath = _hostingEnvironment.WebRootPath;
        //    string newPath = Path.Combine(webRootPath, _folderName);
        //    //Set the File Path.
        //    string filePath = Path.Combine(newPath, fileName);



        //    FileInfo file = new FileInfo(filePath);
        //    IFileProvider provider = new PhysicalFileProvider(newPath);
        //    IFileInfo fileInfo = provider.GetFileInfo(fileName);
        //    var readStream = fileInfo.CreateReadStream();
        //    var mimeType = "application/octet-stream";
        //    return File(readStream, mimeType, fileName);
        //}


        [Route("pagepermission/role/{roleId}")]
        [HttpGet]
        public async Task<ActionResult> PagePermission(string roleId)
        {
            PagePermissionResponse pages = await rs.PagePermission(roleId);
            return Ok(pages);
        }


    }
}