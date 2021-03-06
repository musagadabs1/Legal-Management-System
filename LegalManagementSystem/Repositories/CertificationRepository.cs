﻿using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class CertificationRepository : ICertification
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddCertification(Certification certification)
        {
            try
            {
                db.Certifications.Add(certification);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Complete()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteCertification(Certification certification)
        {
            try
            {
                //var cert = GetCertification(id);
                db.Certifications.Remove(certification);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Certification GetCertification(int? id)
        {
            try
            {
                return db.Certifications.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Certification GetCertification(Expression<Func<Certification, bool>> expression)
        {
            try
            {
                return db.Certifications.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Certification> GetCertificationAsync(int? id)
        {
            try
            {
                return await db.Certifications.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Certification> GetCertifications()
        {
            try
            {
                return db.Certifications.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Certification> GetCertifications(Expression<Func<Certification, bool>> expression)
        {
            try
            {
                return db.Certifications.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Certification>> GetCertificationsAsync()
        {
            try
            {
                return await db.Certifications.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Certification>> GetCertificationsAsync(Expression<Func<Certification, bool>> expression)
        {
            try
            {
                return await db.Certifications.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Certification> GetCertificationsWithStaff()
        {
            try
            {
                return db.Certifications.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync()
        {
            try
            {
                return await db.Certifications.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(Expression<Func<Certification, bool>> expression)
        {
            try
            {
                return await db.Certifications.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCertification(Certification certification)
        {
            try
            {
                db.Entry(certification).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}