using CBParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
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

                        XNamespace ns = "urn:cbr-ru:ed:v2.0";

                        var bicDirectoryEntries = xmlDocument.Root.Elements(ns + "BICDirectoryEntry")
                            .Select(entryElement => new BICDirectoryEntry
                            {
                                BIC = (string)entryElement.Attribute("BIC"),
                                Accounts = entryElement
                                    .Elements(ns + "Accounts")
                                    .Select(accountElement => new Account
                                    {
                                        AccountNumber = (string)accountElement.Attribute("Account"),
                                        RegulationAccountType = (string)accountElement.Attribute("RegulationAccountType"),
                                        CK = (string)accountElement.Attribute("CK"),
                                        AccountCBRBIC = (string)accountElement.Attribute("AccountCBRBIC"),
                                        DateIn = (string)accountElement.Attribute("DateIn"),
                                        AccountStatus = (string)accountElement.Attribute("AccountStatus")
                                    })
                                    .ToList(),
                                ParticipantInfos = entryElement
                                    .Elements(ns + "ParticipantInfo")
                                    .Select(partInfoElement => new ParticipantInfo
                                    {
                                        NameP = (string)partInfoElement.Attribute("NameP"),
                                        CntrCd = (string)partInfoElement.Attribute("CntrCd"),
                                        Rgn = (string)partInfoElement.Attribute("Rgn"),
                                        Ind = (string)partInfoElement.Attribute("Ind"),
                                        Tnp = (string)partInfoElement.Attribute("Tnp"),
                                        Nnp = (string)partInfoElement.Attribute("Nnp"),
                                        Adr = (string)partInfoElement.Attribute("Adr"),
                                        DateIn = (string)partInfoElement.Attribute("DateIn"),
                                        PtType = (string)partInfoElement.Attribute("PtType"),
                                        Srvcs = (string)partInfoElement.Attribute("Srvcs"),
                                        XchType = (string)partInfoElement.Attribute("XchType"),
                                        UID = (string)partInfoElement.Attribute("UID"),
                                        ParticipantStatus = (string)partInfoElement.Attribute("ParticipantStatus")
                                    })
                                    .ToList()

                            })
                            .ToList();


                        return View("Table", bicDirectoryEntries );
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