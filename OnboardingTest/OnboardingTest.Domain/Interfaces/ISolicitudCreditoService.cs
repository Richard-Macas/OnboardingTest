using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface ISolicitudCreditoService
    {
        Task<IEnumerable<SolicitudCredito>> GetSolicitudCreditos();
        Task<SolicitudCredito?> GetSolicitudCredito(int Id);
        Task<SolicitudCredito?> GetSolicitudCredito(Expression<Func<SolicitudCredito, bool>> expr);
        Task<SolicitudCredito> InsertSolicitudCredito(SolicitudCredito solicitudCredito);
        Task<SolicitudCredito> UpdateSolicitudCredito(SolicitudCredito solicitudCredito);
        Task<bool> DeleteSolicitudCredito(int Id);
    }
}
