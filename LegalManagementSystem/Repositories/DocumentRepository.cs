using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class DocumentRepository : IDocument
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddDocument(Document document)
        {
            try
            {
                db.Documents.Add(document);
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
        public void DeleteDocument(Document document)
        {
            try
            {
                //var client = GetClient(id);
                db.Documents.Remove(document);
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
        public Document GetDocument(int? id)
        {
            try
            {
                return db.Documents.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Document> GetDocumentAsync(int? id)
        {
            try
            {
                return await db.Documents.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Document> GetDocument()
        {
            try
            {
                return db.Documents.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            try
            {
                return await db.Documents.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Document GetDocument(Expression<Func<Document,bool>> expression)
        {
            try
            {
                return db.Documents.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Document>> GetDocumentsAsync(Expression<Func<Document, bool>> expression)
        {
            try
            {
                return await db.Documents.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateDocument(Document document)
        {
            try
            {
                db.Entry(document).State = EntityState.Modified;
                ///db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}