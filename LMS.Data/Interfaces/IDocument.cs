using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LMS.Data.Interfaces
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
