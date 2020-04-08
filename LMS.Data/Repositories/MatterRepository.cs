using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class MatterRepository //: IMatter
    {
        //private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        //public void AddMatter(Matter matter)
        //{
        //    try
        //    {
        //        db.Matters.Add(matter);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public int Complete()
        //{
        //    try
        //    {
        //        return db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public async Task<int> CompleteAsync()
        //{
        //    try
        //    {
        //      return await db.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public void Dispose()
        //{
        //    try
        //    {
        //        db.Dispose();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public void DeleteMatter(Matter matter)
        //{
        //    try
        //    {
        //        //var client = GetClient(id);
        //        db.Matters.Remove(matter);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public Matter GetMatter(string id)
        //{
        //    try
        //    {
        //        return db.Matters.Find(id);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public IEnumerable<Matter> GetMatters()
        //{
        //    try
        //    {
        //        return db.Matters.ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public Matter GetMatter(Expression<Func<Matter,bool>> expression)
        //{
        //    try
        //    {
        //        return db.Matters.FirstOrDefault(expression);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public Matter GetMatterByMatterNumber(string matterNumber)
        //{
        //    try
        //    {
        //        return db.Matters.FirstOrDefault(x => x.MatterNumber == matterNumber);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public IEnumerable<MatterForDropDown> GetMatterForDropDowns()
        //{
        //    try
        //    {
        //        var matterForDrops = new List<MatterForDropDown>();
        //        var mattersFromDb= db.GetAllMattersForDropDown().ToList();
        //        foreach (var item in mattersFromDb)
        //        {
        //            matterForDrops.Add(new MatterForDropDown
        //            {
        //                MatterNumber = item.MatterNumber,
        //                Subject = item.Subject
        //            });
        //        }
        //        return matterForDrops;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public void UpdateMatter(Matter matter)
        //{
        //    try
        //    {
        //        db.Entry(matter).State = EntityState.Modified;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public IEnumerable<Matter> GetMatters(Expression<Func<Matter, bool>> expression)
        //{
        //    try
        //    {
        //        return db.Matters.Where(expression).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public async Task<IEnumerable<Matter>> GetMattersAsync(Expression<Func<Matter, bool>> expression)
        //{
        //    try
        //    {
        //        return await db.Matters.Where(expression).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public async Task<IEnumerable<Matter>> GetMattersAsync()
        //{
        //    try
        //    {
        //        return await db.Matters.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public async Task<IEnumerable<Matter>> GetMattersIncludeClientAsync()
        //{
        //    try
        //    {
        //        return await db.Matters.Include(c=> c.Client).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public async Task<IEnumerable<Matter>> GetMattersIncludeClientAsync(Expression<Func<Matter, bool>> expression)
        //{
        //    try
        //    {
        //        return await db.Matters.Include(c => c.Client).Where(expression).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public int GetCurrentId()
        //{
        //    try
        //    {
        //        return (db.Matters.ToList().Count);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("some reason to rethrow", ex);
        //    }
        //}
        //public void ConFigProxy()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //}
        //public async Task<Matter> GetMatterAsync(string id)
        //{
        //    try
        //    {
        //        return await db.Matters.FindAsync(id);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //public IEnumerable<Advocate> AdvocateGroup()
        //{
        //    try
        //    {
        //        var group = new List<Advocate>();
        //        var getAdvocates= db.GetAllAdvocateGroups().ToList();
        //        foreach (var item in getAdvocates)
        //        {
        //            group.Add(new Advocate
        //            {
        //                Id=item.Id,
        //                GroupName=item.GroupName
        //            });
        //        }

        //        return group;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}