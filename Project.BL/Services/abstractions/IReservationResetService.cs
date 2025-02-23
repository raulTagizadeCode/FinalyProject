using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Services.abstractions
{
    public interface IReservationResetService
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
        Task ResetReservationsAsync();
    }
}
