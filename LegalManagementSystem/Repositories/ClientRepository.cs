using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class ClientRepository : IClient
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddClient(Client client)
        {
            try
            {
                db.Clients.Add(client);
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
        public void DeleteClient(int id)
        {
            try
            {
                var client = GetClient(id);
                db.Clients.Remove(client);
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
        public IEnumerable<ClientNameForDropDown> GetAllClientNameForDropDown()
        {
            try
            {
                var clientNames = new List<ClientNameForDropDown>();
                var clients= db.GetAllClientForDropDown().ToList();

                foreach (var client in clients)
                {
                    clientNames.Add(new ClientNameForDropDown
                    {
                        ClientId=client.ClientId,
                        ClientName=client.ClientName
                    });
                }
                return clientNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Client GetClient(int id)
        {
            try
            {
                return db.Clients.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Client GetClient(Func<Client, bool> expression)
        {
            try
            {
                return db.Clients.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Client> GetClientAsync(int id)
        {
            try
            {
                return await db.Clients.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Client> GetClients()
        {
            try
            {
                return db.Clients.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Client> GetClients(Expression<Func<Client,bool>> expression)
        {
            try
            {
                return db.Clients.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            try
            {
                return await db.Clients.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client,bool>> expression)
        {
            try
            {
                return await db.Clients.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int MaxClient()
        {
            try
            {
                return (db.Clients.Max(x => x.ClientId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateClient(int id)
        {
            try
            {
                var client = GetClient(id);
                db.Entry(client).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}