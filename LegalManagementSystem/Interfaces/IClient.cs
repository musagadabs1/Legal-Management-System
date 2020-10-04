using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IClient
    {
        void ProxySetting();
        void AddClient(Client client);
        void DeleteClient(Client client);
        Client GetClient(int? id);
        Task<Client> GetClientAsync(int? id);
        int MaxClient();
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client,bool>> expression);
        IEnumerable<Client> GetClients(Expression<Func<Client,bool>> expression);
        IEnumerable<Client> GetClients();
        Client GetClient(Func<Client, bool> expression);
        //Task<Client> GetClientAsync(Func<Client, bool> expression);
        void UpdateClient(Client client);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
        IEnumerable<ClientNameForDropDown> GetAllClientNameForDropDown();
    }
}
