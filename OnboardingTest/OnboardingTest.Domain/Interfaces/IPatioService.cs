using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface IPatioService
    {
        Task<IEnumerable<Patio>> GetPatios();
        Task<Patio> GetPatio(int Id);
        Task<Patio> GetPatio(Expression<Func<Patio, bool>> expr);
        Task<Patio> InsertPatio(Patio patio);
        Task<Patio> UpdatePatio(Patio patio);
        Task<bool> DeletePatio(int Id);
    }
}
