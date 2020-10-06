using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    interface IDocument
    {
        void AddDocument(Document document);
        int Complete();
        Task<int> CompleteAsync();
        void DeleteDocument(Document document);
        void Dispose();
        Document GetDocument(int? id);
        CaseDocument GetDocumentWithCase(int? id);
        IEnumerable<Document> GetDocuments();
        Task<IEnumerable<Document>> GetDocumentsByMatterNumberAsync(string matterNumber);
        IEnumerable<Document> GetDocumentsByMatterNumber(string matterNumber);
        Document GetDocument(Expression<Func<Document, bool>> expression);
        Task<Document> GetDocumentAsync(int? id);
        Task<CaseDocument> GetDocumentWithCaseAsync(int? id);
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<IEnumerable<Document>> GetDocumentsAsync(Expression<Func<Document, bool>> expression);
        void UpdateDocument(Document document);
    }
}
