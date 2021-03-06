﻿using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface IClient
    {
        void AddClient(Client client);
        void DeleteClient(int id);
        Client GetClient(int id);
        Task<Client> GetClientAsync(int id);
        int MaxClient();
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client,bool>> expression);
        IEnumerable<Client> GetClients(Expression<Func<Client,bool>> expression);
        IEnumerable<Client> GetClients();
        Client GetClient(Func<Client, bool> expression);
        //Task<Client> GetClientAsync(Func<Client, bool> expression);
        void UpdateClient(int id);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
        //IEnumerable<ClientNameForDropDown> GetAllClientNameForDropDown();
    }
}
