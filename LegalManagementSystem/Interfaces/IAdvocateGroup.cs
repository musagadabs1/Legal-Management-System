using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IAdvocateGroup
    {
        void Dispose();
        IEnumerable<AdvocateGroup> GetAdvocateGroups();
    }
}
