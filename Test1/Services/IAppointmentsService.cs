using Test1.Model;

namespace Test1.Services;

public interface IAppointmentsService
{
    public Task<AppointmentDisplayDataDto> GetAppointmentInfo(int appointmentId);
}