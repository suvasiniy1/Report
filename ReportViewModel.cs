using Microsoft.OData.Edm;
using System.ComponentModel.DataAnnotations;

namespace TMSS.Web.Models
{

    public class SurgeryDetailsReport
    {

        public string PatientNo { get; set; }
        public string FirstName { get; set; }
        public string SurgeonFirstName { get; set; }
        public string ProcedureName { get; set; }
        public string ClinicName { get; set; }
        public TimeSpan ArrivedInClinic { get; set; }
        public TimeSpan ArrivedInTheatre { get; set; }
        public TimeSpan AnesthesiaStartTime { get; set; }
        public TimeSpan KnifeSkinTime { get; set; }
        public TimeSpan ProcedureFinishTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string IncidentDetails { get; set; }

        public string? ComplicationName { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "RegistrationDate")]
        [Required(ErrorMessage = "Date is required!")]
        public Date RegistrationDate { get; set; }
        public double Duration { get; set; }

        public int ClinicId { get; set; }
        // public string FormattedArrivedInClinic => ArrivedInClinic.ToString(@"hh\:mm");
        public string FormattedArrivedInClinic
        {
            get
            {
                if (ArrivedInClinicDate.HasValue)
                {
                    // Combine the date and time into a single string
                    // Assuming ArrivedInClinicDate has the date and ArrivedInClinic has the time
                    return ArrivedInClinic.ToString(@"hh\:mm") + " (" + ArrivedInClinicDate.Value.ToString("yyyy-MM-dd") + ")";
                }
                else
                {
                    // If ArrivedInClinicDate is null, you might want to handle it appropriately
                    return ArrivedInClinic.ToString(@"hh\:mm") + " (Date Not Available)";
                }
            }
        }

        public string FormattedArrivedInTheater => ArrivedInTheatre.ToString(@"hh\:mm");
        public string FormattedAnesthesiaStartTime => AnesthesiaStartTime.ToString(@"hh\:mm");
        public string FormattedKnifeSkinTime => KnifeSkinTime.ToString(@"hh\:mm");
        public string FormattedProcedureFinishTime => ProcedureFinishTime.ToString(@"hh\:mm");
        //public string FormattedDepartureTime => DepartureTime.ToString(@"hh\:mm");

        public string FormattedDepartureTime
        {
            get
            {
                if (DepartureDate.HasValue)
                {
                    // Combine the date and time into a single string
                    // Assuming DepartureDate has the date and DepartureTime has the time
                    return DepartureTime.ToString(@"hh\:mm") + " (" + DepartureDate.Value.ToString("yyyy-MM-dd") + ")";
                }
                else
                {
                    // If DepartureDate is null, you might want to handle it appropriately
                    return DepartureTime.ToString(@"hh\:mm") + " (Date Not Available)";
                }
            }
        }
        public DateTime? ArrivedInClinicDate { get; set; }
        public DateTime? ArrivedInTheatreDate { get; set; }
        public DateTime? AnesthesiaStartDate { get; set; }
        public DateTime? KnifeSkinDate { get; set; }
        public DateTime? ProcedureFinishDate { get; set; }
        public DateTime? DepartureDate { get; set; }



    }

    public class ReportViewModel
    {
        public List<SurgeryDetailsReport> LstSurgeryDetailsReport { get; set; }
        public string IncidentDetails { get; set; }

        public string? ComplicationName { get; set; }

        //public List<SurgeryDetailsReport> LstSurgeryDetailsReport { get; set; }
        public int PatientId { get; set; }

        public int SurgeonId { get; set; }
        public int ProcedureId { get; set; }
        public int ClinicId { get; set; }
        public DateTime SurgeryDate { get; set; }
        public int ComplicationId { get; set; }
        public IEnumerable<SurgeryDetailsReport> Reports { get; set; }


    }
}
