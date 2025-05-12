using System.Data.Common;
using Microsoft.Data.SqlClient;
using Test1.Model;

namespace Test1.Services;

public class AppointmentsService : IAppointmentsService
{
    private readonly IConfiguration _configuration;

    public AppointmentsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task AddAppointment(PostAppointmentDto postAppointmentDto)
    {
        string command = @"INSERT INTO Appointment
                        VALUES(@Idappointment);"
        
        await using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Default")))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            DbTransaction transaction = await connection.BeginTransactionAsync();
            command.Transaction = transaction as SqlTransaction;   
        }
        
    }

    public async Task<AppointmentDisplayDataDto> GetAppointmentInfo(int appointmentId)
    {
        string command = @"select * from Appointment
                            join Doctor D on D.doctor_id = Appointment.doctor_id
                            join dbo.Patient P on P.patient_id = Appointment.patient_id
                            join dbo.Appointment_Service ApS on Appointment.appointment_id = ApS.appointment_id
                            join dbo.Service S on S.service_id = ApS.service_id
                            where Appointment.appointment_id = @AppointmentId";
        
        AppointmentDisplayDataDto? displayDataDto = null;
        
        await using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Default")))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("AppointmentId", appointmentId);

            await conn.OpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    if (displayDataDto is null)
                    {
                        displayDataDto = new AppointmentDisplayDataDto()
                        {
                            Date = reader.GetDateTime(reader.GetOrdinal("date")),
                            Patient = new PatientDto()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth"))
                            },
                            Doctor = new DoctorDto()
                            {
                                DoctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                PWZ = reader.GetString(reader.GetOrdinal("PWZ"))
                            },
                            AppointmentServices = new List<AppointmentDto>()
                        };
                    }
                    
                    displayDataDto.AppointmentServices.Add(new AppointmentDto()
                    {
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        ServiceFee = reader.GetDecimal(reader.GetOrdinal("service_fee"))
                    });
                    
                }
            }

            if (displayDataDto is null)
            {
                throw new Exception("No appointments for this id");
            }
        }

        return displayDataDto;
    }
}