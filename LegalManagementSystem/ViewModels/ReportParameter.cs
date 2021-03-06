﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.ViewModels
{
    public class ReportParameter
    {
        public int Id { get; set; }
        public string ExcelType { get; set; }
        public string PdfType { get; set; }
        public string DocType { get; set; }
        public string ReportType { get; set; }
        public string Town { get; set; }
        public string Number { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}