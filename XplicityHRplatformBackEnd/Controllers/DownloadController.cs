using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/download/offer")]
    [ApiController]
    public class DownloadController : ControllerBase
    {

        [HttpGet]
        [Route("/offer/{name}/{lastName}")]
        public async Task<IActionResult> DownloadOffer(string name, string lastName)
        {
            DateTime date = DateTime.Today;
            string datestr = date.ToString("dd MMMM yyyy", new CultureInfo("en-US"));
            Document document = new Document();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "templates.docx");
            document.LoadFromFile(path);
            document.Replace("{firstName}", name.ToUpper(), false, true);
            document.Replace("{firstNamee}", name, false, true); 
            document.Replace("{lastName}", lastName.ToUpper(), false, true);
            document.Replace("{lastNamee}", lastName, false, true);
            document.Replace("{date}", datestr, false, true);
            document.SaveToFile($"Job_offer_Xplicity_{name}_{lastName}.docx", FileFormat.Docx);
            path = Path.Combine(Directory.GetCurrentDirectory(), "", $"Job_offer_Xplicity_{name}_{lastName}.docx");

            return File(
                fileContents: System.IO.File.ReadAllBytes(path),
                contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                fileDownloadName: $"Job_offer_Xplicity_{name}_{lastName}.docx");


        }
    }
}
