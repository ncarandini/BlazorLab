using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiLab.Data;
using ApiLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private ApiLabDataContext dbContext;

        public ImageController(ApiLabDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetImagesAsync()
        {
            var labImages = await dbContext.LabImages.ToListAsync();
            return Ok(labImages);
        }

        [HttpPost]
        public async Task<IActionResult> PostImageAsync([FromBody]LabImage labImage)
        {
            labImage.Id = Guid.NewGuid();
            dbContext.LabImages.Add(labImage);
            await dbContext.SaveChangesAsync();
            return Ok(labImage);
        }

        [HttpPost("multipart")]
        public async Task<IActionResult> PostImagesAsync(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        LabImage labImage = new LabImage()
                        {
                            Id = Guid.NewGuid(),
                            Title = Path.GetFileName(file.FileName),
                            ContentType = Path.GetExtension(file.FileName).Replace(".", ""),
                            ContentSize = file.Length,
                            Content = memoryStream.ToArray()
                        };

                        dbContext.LabImages.Add(labImage);

                        await dbContext.SaveChangesAsync();

                        return Ok(labImage);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The file is too large.");
                    }
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No file submitted.");
            }
        }
    }
}