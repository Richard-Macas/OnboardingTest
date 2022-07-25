using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface ITrackingSolicitudService
    {
        Task<IEnumerable<TrackingSolicitud>> GetTrackingSolicituds();
        Task<TrackingSolicitud> GetTrackingSolicitud(int Id);
        Task<TrackingSolicitud> GetTrackingSolicitud(Expression<Func<TrackingSolicitud, bool>> expr);
        Task<TrackingSolicitud> InsertTrackingSolicitud(TrackingSolicitud trackingSolicitud);
        Task<TrackingSolicitud> UpdateTrackingSolicitud(TrackingSolicitud trackingSolicitud);
        Task<bool> DeleteTrackingSolicitud(int Id);
        Task<IEnumerable<TrackingSolicitud>> DeleteTrackingSolicitudMultiple(Expression<Func<TrackingSolicitud, bool>> expr);
    }
}
