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
        void DeleteDocument(Document document);
        Document GetDocument(int id);
        IEnumerable<Document> GetDocument();
        Document GetDocument(Expression<Func<bool>> expression);
        void UpdateDocument(Document document);
    }
}
