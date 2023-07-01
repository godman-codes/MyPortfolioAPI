using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITechnologiesRepository
    {
        void CreateTechnology(TechnologiesModel technology);
        Task<IEnumerable<TechnologiesModel>> GetAllTechnologies(string userId, bool trackChanges);
        Task<TechnologiesModel> GetTechnology(Guid id, bool trackChanges, string userId);
        void DeleteTechnology(TechnologiesModel technologies);
        Task<TechnologiesModel> GetTechnologyByName(string name, bool trackChanges, string userId);

    }
}
