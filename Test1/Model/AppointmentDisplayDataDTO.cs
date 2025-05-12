namespace Test1.Model;

public class AppointmentDisplayDataDto
{
    public DateTime Date { get; set; }
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<AppointmentDto> AppointmentServices { get; set; }
}

public class PatientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class DoctorDto
{
    public int DoctorId { get; set; }
    public string PWZ { get; set; }
}

public class AppointmentDto
{
    public string Name { get; set; }
    public decimal ServiceFee { get; set; }
}

public class PostAppointmentDto
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string PWZ { get; set; }
    public List<AppointmentDto> services { get; set; }
}