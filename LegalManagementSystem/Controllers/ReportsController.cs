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
        public ActionResult CasesReport()
        {
            //ViewBag.ClientName = new SelectList(db.GetAllClientNames(), "ClientName", "Client");
            ViewBag.ExportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="All",Value="1"},
                new SelectListItem{Text="Cases by Dates",Value="2"},
                new SelectListItem{Text="Cases by Client Name",Value="3"},
                new SelectListItem{Text="Cases by Practice Area",Value="4"},
                new SelectListItem{Text="Cases by Number",Value="5"}
            };
            ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
            return View();
        }
        [HttpPost]
        public ActionResult CasesReport(FormCollection form)
        {
            int reportType = int.Parse(Request.Form["ReportType"]);
            switch (reportType)
            {
                case 1: //for all cases
                    int exportType = int.Parse(Request.Form["DocType"]);
                    if (exportType == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/AllCases.rpt");


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
                        string Path = Server.MapPath("~/Reports/AllCases.rpt");


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
                        break;
                case 2: //Cases by Date
                    DateTime startDate2 = Convert.ToDateTime( Request.Form["From"]);
                    DateTime endDate2 = Convert.ToDateTime(Request.Form["To"]);
                    int exportType1 = int.Parse(Request.Form["DocType"]);
                    if (exportType1 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesBetweenDates.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@fromDate", startDate2);
                        reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType1 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesBetweenDates.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@fromDate", startDate2);
                        reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType1 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesBetweenDates.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@fromDate", startDate2);
                        reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 3: // Cases by Client Name
                    int exportType2 = int.Parse(Request.Form["DocType"]);
                    string clientName = Request.Form["PdfType"];
                    if (exportType2 == 1)//1 for Excel
                    {
                        //int exportType3 = int.Parse(Request.Form["DocType"]);
                        //string clientName = Request.Form["PdfType"];
                        
                            System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                            string Path = Server.MapPath("~/Reports/CasesByClientName.rpt");


                            reportDocument.Load(Path);
                        //
                        reportDocument.SetParameterValue("@clientfirstName", clientName);
                        reportDocument.SetParameterValue("@clientMiddleName", clientName);
                        reportDocument.SetParameterValue("@clientLastName", clientName);
                        System.IO.Stream oStream = null;
                            byte[] byteArray = null;
                            oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                            byteArray = new byte[oStream.Length];
                            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                            oStream.Seek(0, SeekOrigin.Begin);
                            return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                        
                    }
                    else if (exportType2 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesByClientName.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@clientfirstName", clientName);
                        reportDocument.SetParameterValue("@clientMiddleName", clientName);
                        reportDocument.SetParameterValue("@clientLastName", clientName);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType2 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesByClientName.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@clientfirstName", clientName);
                        reportDocument.SetParameterValue("@clientMiddleName", clientName);
                        reportDocument.SetParameterValue("@clientLastName", clientName);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 4:// Practice Area
                    int exportType3 = int.Parse(Request.Form["DocType"]);
                    string practiceArea = Request.Form["Town"];
                    if (exportType3 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesByPracticeArea.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@pArea", practiceArea);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType3 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesByPracticeArea.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@pArea", practiceArea);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType3 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CasesByPracticeArea.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@pArea", practiceArea);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 5://Case by Number
                    int exportType4 = int.Parse(Request.Form["DocType"]);
                    string caseNumber = Request.Form["Number"];
                    if (exportType4 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/GetCasesByNumber.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@caseNumber", caseNumber);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("Client {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType4 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/GetCasesByNumber.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@caseNumber", caseNumber);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("Client {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType4 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/GetCasesByNumber.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@caseNumber", caseNumber);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("Client {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
            }
            ViewBag.ExportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="All",Value="1"},
                new SelectListItem{Text="Cases by Dates",Value="2"},
                new SelectListItem{Text="Cases by Client Name",Value="3"},
                new SelectListItem{Text="Cases by Practice Area",Value="4"},
                new SelectListItem{Text="Cases by Number",Value="5"}
            };
            ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
            return View();
        }
        public ActionResult CourtActivities()
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
                new SelectListItem{Text="Court Activities by Case Number",Value="2"},
                new SelectListItem{Text="Court Actitivies by Hearing Date",Value="3"},
                new SelectListItem{Text="Court Activities by Status",Value="4"},
                new SelectListItem{Text="Court Activities by Court Name",Value="5"}
            };
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"}


            };
            return View();
        }

        [HttpPost]
        public ActionResult CourtActivities(FormCollection form)
        {
            int reportType = int.Parse(Request.Form["ReportType"]);
            switch (reportType)
            {
                case 1: //for all court activities
                    int exportType = int.Parse(Request.Form["DocType"]);
                    if (exportType == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/AllCourtActivities.rpt");


                        reportDocument.Load(Path);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("CourtActivities {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/AllCourtActivities.rpt");


                        reportDocument.Load(Path);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("CourtActivities {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/AllCourtActivities.rpt");


                        reportDocument.Load(Path);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("CourtActivities {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 2: //Court Activities by Case Number
                    string caseNumber = Request.Form["From"];
                    //DateTime endDate2 = Convert.ToDateTime(Request.Form["To"]);
                    int exportType1 = int.Parse(Request.Form["DocType"]);
                    if (exportType1 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByCaseNumber.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@caseNumber", caseNumber);
                        //reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("CourtActivities {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType1 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByCaseNumber.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@caseNumber", caseNumber);
                        //reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("CourtActivities {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType1 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByCaseNumber.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@caseNumber", caseNumber);
                        //reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("CourtActivities {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 3: // Court Activities by Date
                    DateTime startDate2 = Convert.ToDateTime(Request.Form["From"]);
                    //DateTime endDate2 = Convert.ToDateTime(Request.Form["To"]);
                    int exportType2 = int.Parse(Request.Form["DocType"]);
                    if (exportType2 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByHearingDate.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@hearingDate", startDate2);
                        //reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("CourtActivities {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType2 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByHearingDate.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@hearingDate", startDate2);
                        //reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("CourtActivities {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType2 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByHearingDate.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@hearingDate", startDate2);
                        //reportDocument.SetParameterValue("@toDate", endDate2);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("CourtActivities {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 4:// Court Activities by Case Status
                    int exportType3 = int.Parse(Request.Form["DocType"]);
                    string status = Request.Form["Town"];
                    if (exportType3 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByStatus.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@status", status);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("CourtActivities {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType3 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByStatus.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@status", status);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("CourtActivities {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType3 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByStatus.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@status", status);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("CourtActivities {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
                case 5://Court Activities by Court Name
                    int exportType4 = int.Parse(Request.Form["DocType"]);
                    string courtName = Request.Form["PdfType"];
                    if (exportType4 == 1)//1 for Excel
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByCourtName.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@courtName", courtName);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/excel", string.Format("Courtactivities {0}.xls", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType4 == 2)//for Pdf
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByCourtName.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@courtName", courtName);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "application/pdf", string.Format("CourtActivities {0}.pdf", DateTime.Now.ToLongTimeString()));
                    }
                    else if (exportType4 == 3)//For Word
                    {
                        System.IO.MemoryStream stream1 = new System.IO.MemoryStream();
                        string Path = Server.MapPath("~/Reports/CourtActivitiesByCourtName.rpt");


                        reportDocument.Load(Path);
                        reportDocument.SetParameterValue("@courtName", courtName);
                        System.IO.Stream oStream = null;
                        byte[] byteArray = null;
                        oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Seek(0, SeekOrigin.Begin);
                        return File(oStream, "Application/msword", string.Format("CourtActivities {0}.doc", DateTime.Now.ToLongTimeString()));
                    }
                    break;
            }
            ViewBag.ExportType = new List<SelectListItem>
            {
                new SelectListItem{Text="Excel",Value="1"},
                new SelectListItem{Text="Pdf",Value="2"},
                new SelectListItem{Text="Word",Value="3"}
            };
            ViewBag.ReportType = new List<SelectListItem>
            {
                new SelectListItem{Text="All",Value="1"},
                new SelectListItem{Text="Cases by Dates",Value="2"},
                new SelectListItem{Text="Cases by Client Name",Value="3"},
                new SelectListItem{Text="Cases by Practice Area",Value="4"},
                new SelectListItem{Text="Cases by Number",Value="5"}
            };
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"}


            };
            return View();
        }
    }
}
