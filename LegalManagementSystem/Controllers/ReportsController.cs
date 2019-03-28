using CrystalDecisions.CrystalReports.Engine;
using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        private readonly ReportDocument reportDocument = new ReportDocument();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AllStaffReport()
        {
            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            return View();
        }
        [HttpPost]
        public ActionResult AllStaffReport(FormCollection form)
        {
            int reportType = int.Parse(Request.Form["DocType"]);
            if (reportType == 1)//1 for Excel
            {
                System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                string Path = Server.MapPath("~/Reports/AllStaff.rpt");


                reportDocument.Load(Path);
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                oStream.Seek(0, SeekOrigin.Begin);
                return File(oStream, "application/excel", string.Format("Staff {0}.xls", DateTime.Now.ToLongTimeString()));
            }
            else if (reportType == 2)//for Pdf
            {
                System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                string Path = Server.MapPath("~/Reports/AllStaff.rpt");


                reportDocument.Load(Path);
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                oStream.Seek(0, SeekOrigin.Begin);
                return File(oStream, "application/pdf", string.Format("Staff {0}.pdf", DateTime.Now.ToLongTimeString()));
            }
            else if (reportType == 3)//For Word
            {
                System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                string Path = Server.MapPath("~/Reports/AllStaff.rpt");


                reportDocument.Load(Path);
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                oStream.Seek(0, SeekOrigin.Begin);
                return File(oStream, "application/msword", string.Format("Staff {0}.doc", DateTime.Now.ToLongTimeString()));
            }
            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            return View();
        }
        [HttpGet]
        public ActionResult AllClients()
        {
            ViewBag.ExportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="All",Value="1"},
                new SelectListItem{Text="Client By Town",Value="2"},
                new SelectListItem{Text="Client By Number",Value="3"}
            };

            return View();
        }
        [HttpPost]
        public ActionResult AllClients(FormCollection form)
        {
            int reportType = int.Parse(Request.Form["ReportType"]);

            if (reportType==1) //For All Clients
            {
                int exportType = int.Parse(Request.Form["DocType"]);

                if (exportType == 1)//1 for Excel
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/AllClients.rpt");


                    reportDocument.Load(Path);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                }
                else if (exportType == 2)//for Pdf
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/AllClients.rpt");


                    reportDocument.Load(Path);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                }
                else if (exportType == 3)//For Word
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/AllClients.rpt");


                    reportDocument.Load(Path);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                }
            }
            else if (reportType==2) // for Report by Client Town
            {
                int exportType = int.Parse(Request.Form["DocType"]);
                string town = Request.Form["Town"];

                if (exportType == 1)//1 for Excel
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/ClientByTown.rpt");


                    reportDocument.Load(Path);
                    reportDocument.SetParameterValue("@clientTown", town);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                }
                else if (exportType == 2)//for Pdf
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/ClientByTown.rpt");


                    reportDocument.Load(Path);
                    reportDocument.SetParameterValue("@clientTown", town);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                }
                else if (exportType == 3)//For Word
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/ClientByTown.rpt");


                    reportDocument.Load(Path);
                    reportDocument.SetParameterValue("@clientTown", town);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                }
            }
            else //For Client by Client Number
            {
                int exportType = int.Parse(Request.Form["DocType"]);
                string clientNumber = Request.Form["Number"];
                if (exportType == 1)//1 for Excel
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/ClientByNumber.rpt");


                    reportDocument.Load(Path);
                    reportDocument.SetParameterValue("@clientNumber", clientNumber);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                }
                else if (exportType == 2)//for Pdf
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/ClientByNumber.rpt");


                    reportDocument.Load(Path);
                    reportDocument.SetParameterValue("@clientNumber", clientNumber);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                }
                else if (exportType == 3)//For Word
                {
                    System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                    string Path = Server.MapPath("~/Reports/ClientByNumber.rpt");


                    reportDocument.Load(Path);
                    reportDocument.SetParameterValue("@clientNumber", clientNumber);
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    oStream.Seek(0, SeekOrigin.Begin);
                    return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                }
            }

            

            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            return View();

        }
    }
}
