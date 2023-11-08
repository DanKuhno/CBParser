using CBParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CBParser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file != null)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    try
                    {
                        var xmlString = reader.ReadToEnd();
                        var xmlDocument = XDocument.Parse(xmlString);

                        
                        var bicDirectoryEntries = xmlDocument.Root.Elements("ED807")
                    .Elements("BICDirectoryEntry")
                    .Select(entryElement => new BICDirectoryEntry
                    {
                        BIC = (string)entryElement.Attribute("BIC"),
                        ParticipantInfo = new ParticipantInfo
                        {
                            NameP = (string)entryElement.Element("ParticipantInfo").Attribute("NameP"),
                            Rgn = (string)entryElement.Element("ParticipantInfo").Attribute("Rgn"),
                            // Заполните остальные свойства ParticipantInfo
                        },
                        Accounts = entryElement
                            .Elements("Accounts")
                            .Select(accountElement => new Account
                            {
                                AccountNumber = (string)accountElement.Attribute("Account"),
                                RegulationAccountType = (string)accountElement.Attribute("RegulationAccountType"),
                                // Заполните остальные свойства Account
                            })
                            .ToList()
                    })
                    .ToList();


                        return View("Table", bicDirectoryEntries);
                    }
                    catch (Exception ex)
                    {
                        return View();
                    }
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}