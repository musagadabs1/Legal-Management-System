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
        public IEnumerable<Document> GetDocuments()
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

        public CaseDocument GetDocumentWithCase(int? id)
        {
            var document = (from doc in db.Documents
                            from matter in db.Matters
                            where doc.MatterNumber == matter.MatterNumber && doc.DocumentId==id
                            select new CaseDocument
                            {
                                Id=doc.DocumentId,
                                Matter = matter.Subject,
                                MatterNumber = doc.MatterNumber,
                                Tags = doc.Tags,
                                Description = doc.Description,
                                DocName = doc.DocName,
                                DocPath = doc.DocPath,
                                AssignedDate = doc.AssignedDate
                            }).FirstOrDefault();
            return document;
        }

        public async Task<CaseDocument> GetDocumentWithCaseAsync(int? id)
        {
            try
            {
                return await (from doc in db.Documents
                                from matter in db.Matters
                                where doc.MatterNumber == matter.MatterNumber && doc.DocumentId==id
                                select new CaseDocument
                                {
                                    Id=doc.DocumentId,
                                    Matter = matter.Subject,
                                    MatterNumber = doc.MatterNumber,
                                    Tags = doc.Tags,
                                    Description = doc.Description,
                                    DocName = doc.DocName,
                                    DocPath = doc.DocPath,
                                    AssignedDate = doc.AssignedDate
                                }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Document>> GetDocumentsByMatterNumberAsync(string matterNumber)
        {
            try
            {
                return await db.Documents.Where(x => x.MatterNumber==matterNumber).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Document> GetDocumentsByMatterNumber(string matterNumber)
        {
            try
            {
                return db.Documents.Where(x => x.MatterNumber==matterNumber).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}