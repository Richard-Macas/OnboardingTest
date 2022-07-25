using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface IMarcaService
    {
        Task<bool> InsertMultipleMarca();
    }
}
