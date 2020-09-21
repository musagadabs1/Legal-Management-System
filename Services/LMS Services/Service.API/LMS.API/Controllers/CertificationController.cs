using LMS.API.Models;
using LMS.Data.Interfaces;
using LMS.Data.Repositories;
using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.API.Controllers
{
    public class CertificationController : ApiController
    {
        private LMSDbContext _context;
        private CertificationRepository certRepo;
        public CertificationController(LMSDbContext context)
        {
            _context = context;
            certRepo = new CertificationRepository(_context);
        }
        [HttpPost]
        public IHttpActionResult AddCertification(Certification cert)
        {
            try
            {
                //Certification cert = new Certification();
                certRepo.Add(cert);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //return Ok();
        }
    }
}
