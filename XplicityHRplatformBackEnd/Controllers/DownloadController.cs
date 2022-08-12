using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XplicityHRplatformBackEnd.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XplicityHRplatformBackEnd.Controllers
{
    [ApiController]
    public class DownloadController : ControllerBase
    {

        [HttpPost]
        [Route("/offer")]
        public async Task<IActionResult> DownloadOffer([FromBody] Person p)
        {
            DateTime date = DateTime.Today;
            string datestr = date.ToString("dd MMMM yyyy", new CultureInfo("en-US"));
            Document document = new Document();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "templates.docx");
            document.LoadFromFile(path);
            document.Replace("{firstName}", p.FirstName.ToUpper(), false, true);
            document.Replace("{firstNamee}", p.FirstName, false, true); 
            document.Replace("{lastName}", p.LastName.ToUpper(), false, true);
            document.Replace("{lastNamee}", p.LastName, false, true);
            document.Replace("{date}", datestr, false, true);
            document.SaveToFile($"Job_offer_Xplicity_{p.FirstName}_{p.LastName}.docx", FileFormat.Docx);
            path = Path.Combine(Directory.GetCurrentDirectory(), "", $"Job_offer_Xplicity_{p.FirstName}_{p.LastName}.docx");

            return File(
                fileContents: System.IO.File.ReadAllBytes(path),
                contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                fileDownloadName: $"Job_offer_Xplicity_{p.FirstName}_{p.LastName}.docx");


        }
    }
}
