using AutoMapper;
using Events.core.Common.Files;
using Events.Core.Common.Messages;
using Events.Core.Common.Validators;
using Events.Core.DTOs;
using Events.Core.Model;
using EventsManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class FilesController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;
        private readonly IFileManager fileManager;

        public FilesController(EventsContext context,
           IMapper mapper,
           IDataValidator validator,
           IMessages messages,
           IFileManager fileManager
           )
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
            this.fileManager = fileManager;
        }


        /// <summary>
        /// Allows to upload a File to put it in a specific folder according to the value of the OrigenDocument
        /// </summary>
        /// <returns>the pyshical URL of the file </returns>
        /// 
        /// <response code="200">Returns the Url of the file uploaded</response>
        /// <response code="500">If there was a problem with the document</response>
        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadFile")]
        public async Task<ActionResult> UploadFile()
        {
            try
            {
                //getting the file data
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                int folder;
                string dbPath;

                if (int.TryParse(file.Name, out folder))//image_data
                {
                    dbPath = fileManager.SaveFile(file);
                }
                else
                {
                    throw new ApplicationException("We were unable to get the File Name");
                }
                //    return Ok(ret);
                return Ok(new { dbPath });
            }
            catch (Exception ex)
            {
                //         logger.LogError(ex.Message);

                return StatusCode(500, $"Internal server error: {ex}");
            }


        }

        /// <summary>
        /// Allow to upload the data of the uploaded file in the database
        /// </summary>
        /// <returns>OK</returns>
        ///
        /// <response code="200">If the data was upload in the database</response>
        /// <response code="400">If there is a problem with the database query</response>
        [HttpPost]
        [Route("UploadDataFile")]
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> UploadDataFile([FromBody] FileDataDTO newFile)
        {
            try
            {
                newFile.DateUploaded = DateTime.Now;

                FileData fileToUpload = mapper.Map<FileData>(newFile);

                
                //check if exist
                if (newFile.Update)
                {
                    var file = await context.FileData.Where(x => x.Id == newFile.Id).FirstOrDefaultAsync();
                    if (file != null)
                    {
                        //change others values
                        file.WebUrl = newFile.WebUrl;
                    }
                    context.Update(file);

                }
                else
                {
                    var fileUpload = await context.FileData.AddAsync(fileToUpload);

                }
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                //         logger.LogError(ex.Message);


                return BadRequest(ex.Message);
            }
        }


      

        /// <summary>
        /// Get the list of the document
        /// </summary>
        /// <returns>OK</returns>
        ///
        /// <response code="200">If the data was upload in the database</response>
        /// <response code="400">If there is a problem with the database query</response>

        //GetDocumentsList
        [HttpGet]
        [Route("GetDocumentsList")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> GetDocumentsList()
        //        public async Task<ActionResult> GetDocumentsList([FromBody] FIleDTO fileOptions)
        {
            try
            {
                List<FileData> fileList = await context.FileData.ToListAsync();

                List<FileDataDTO> ret = mapper.Map<List<FileDataDTO>>(fileList);

                List<FileDataDTO> fileRet = new List<FileDataDTO>();

                foreach (FileData file in fileList)
                {
                   // int contador = await context.Download.CountAsync(x => x.FileId.Id == file.Id);
                    FileDataDTO find = ret.Find(x => x.Id == file.Id);
                    
                    fileRet.Add(find);
                }

                return Ok(fileRet);
            }
            catch (Exception ex)
            {
                //        logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// set a new status of a document
        /// </summary>
        /// <returns>OK</returns>
        ///
        /// <response code="200">If the document was updated in the new status</response>
        /// <response code="400">If there is a problem with the update</response>
        //SetStatusDocument
        [HttpPost]
        [Route("SetStatusDocument")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> SetStatusDocument([FromBody] FileDataDTO docDto)
        {
            try
            {
                FileData ret = mapper.Map<FileData>(docDto);

                FileData original = await context.FileData.Where(x => x.Id == ret.Id).FirstOrDefaultAsync();

                if (original == null)
                {
                    return BadRequest("We couldn't find The original document");
                }
              

                context.FileData.Update(original);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                //        logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get the blog items 
        /// </summary>
        /// <returns>A list with all the data of the files uploaded as a blog</returns>
        ///
        /// <response code="200">with the list of the blog items</response>
        /// <response code="400">If there is a problem getting the items</response>
        [HttpGet]
        [Route("GetBlogItems")]
        [ResponseCache(Duration = 1200)]
        public async Task<ActionResult> GetBlogItems(bool getAll = false)
        {
            try
            {
                //Where(x => x.DocumentType == ((int)OrigenDocument.blog).ToString())
                List<FileData> fileList = await context.FileData.OrderByDescending(x => x.DateUploaded).ToListAsync();

               
                List<FileDataDTO> ret = mapper.Map<List<FileDataDTO>>(fileList);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                //            logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

    }
}
