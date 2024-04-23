using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Prometheus.DotNetRuntime.EventListening.Parsers.Util;
using System.Globalization;
using TMSS.DataAccess.DataContext;
using TMSS.Services.Interfaces;
using TMSS.Web.Models;

namespace TMSS.Web.Controllers
{
    public class DatatableReportController : Controller
    {
        public ISurgeryService _isurgeryInterface { get; set; }
        public ISurgeonService _surgeonService { get; set; }
        public IProcedureService _procedureService { get; set; }
        public IClinicService _clinicService { get; set; }
        public IComplicationService _complicationService { get; set; }
        public IIncidentService _incidentService { get; set; }
        public TMSSDbContext? _dbcontext;

        private readonly IMapper _mapper;
        public DatatableReportController(TMSSDbContext context, IIncidentService iincidentService, ISurgeryService isurgeryInterface, ISurgeonService isurgeonService, IProcedureService iprocedureService, IClinicService iclinicService, IComplicationService icomplicationService, IMapper mapper)
        {
            _mapper = mapper;
            _isurgeryInterface = isurgeryInterface;
            _surgeonService = isurgeonService;
            _procedureService = iprocedureService;
            _clinicService = iclinicService;
            _complicationService = icomplicationService;
            _incidentService = iincidentService;
            _dbcontext = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Get(DateTime searchFromDate, DateTime searchToDate, string searchSurgeon, string searchProcedure, string searchClinic, string searchComplication, int? page)
        //{
        //    ViewBag.searchFromDate = searchFromDate;
        //    ViewBag.searchToDate = searchToDate;
        //    ViewBag.searchSurgeon = searchSurgeon;
        //    ViewBag.searchProcedure = searchProcedure;
        //    ViewBag.searchClinic = searchClinic;
        //    ViewBag.searchComplication = searchComplication;

        //    string formatFromDate = searchFromDate.ToString("yyyy-MM-dd"); // Customize the format as needed
        //                                                                   // String DateString = Convert.ToString("07/02/1993");
        //    IFormatProvider culture = new CultureInfo("en-US");
        //    Date dateFromDate = Convert.ToDateTime(formatFromDate);



        //    string formatToDate = searchToDate.ToString("yyyy-MM-dd"); // Customize the format as needed
        //    Date parsedToDate = Convert.ToDateTime(formatToDate);


        //    var _surgeryresult = _isurgeryInterface.ExecuteStoredProcedure(dateFromDate, parsedToDate, searchSurgeon, searchProcedure, searchClinic, searchComplication);
        //    ReportViewModel reportViewModel = new();
        //    reportViewModel.LstSurgeryDetailsReport = new();

        //    // Define common columns
        //    var columns = new List<string>
        //   {
        //            "RegistrationDate", "PatientNo", "FirstName", "ProcedureName",
        //        "SurgeonFirstName", "ClinicName", "ArrivedInClinic", "ArrivedInTheatre",
        //        "AnesthesiaStartTime", "KnifeSkinTime", "ProcedureFinishTime", "DepartureTime",
        //        "Duration", "ComplicationName", "IncidentDetails"

        //    };


        //    //    "AnesthesiaStartTime", "ArrivedInClinic", "ArrivedInTheatre", "ClinicName",
        //    //"ComplicationName", "DepartureTime", "Duration", "FirstName",
        //    //"IncidentDetails", "KnifeSkinTime", "PatientNo", "ProcedureFinishTime",
        //    //"ProcedureName", "RegistrationDate", "SurgeonFirstName"
        //    // Conditionally select columns based on searchComplication
        //    var selectedColumns = columns;

        //    if (!string.IsNullOrEmpty(searchComplication))
        //    {
        //        // If searching with complication, only include relevant columns
        //        selectedColumns = new List<string> { "RegistrationDate", "PatientNo", "FirstName", "ProcedureName", "ComplicationName", "IncidentDetails" };
        //    }

        //    // Create an anonymous type with the selected columns
        //    var result = _surgeryresult.Select(item => new
        //    {

        //        RegistrationDate = item.RegistrationDate,
        //        PatientNo = item.PatientNo,
        //        FirstName = item.FirstName,
        //        ProcedureName = item.ProcedureName,
        //        SurgeonFirstName = item.SurgeonFirstName,
        //        ClinicName = item.ClinicName,
        //        ArrivedInClinic = item.ArrivedInClinic,
        //        ArrivedInTheatre = item.ArrivedInTheatre,
        //        AnesthesiaStartTime = item.AnesthesiaStartTime,
        //        KnifeSkinTime = item.KnifeSkinTime,
        //        ProcedureFinishTime = item.ProcedureFinishTime,
        //        DepartureTime = item.DepartureTime,
        //        Duration = 0,
        //        ComplicationName = item.ComplicationName,
        //        IncidentDetails = item.IncidentDetails,
        //    }).ToList();

        //    // ... existing code ...

        //    return Json(new { lstSurgeryDetailsReport = result, selectedColumns });
        //}
        [HttpGet]
        //public async Task<IActionResult> Get(DateTime searchFromDate, DateTime searchToDate, string searchSurgeon, string searchProcedure, string searchClinic, string searchComplication, int? page)
        //{

        //    ViewBag.searchFromDate = searchFromDate;
        //    ViewBag.searchToDate = searchToDate;
        //    ViewBag.searchSurgeon = searchSurgeon;
        //    ViewBag.searchProcedure = searchProcedure;
        //    ViewBag.searchClinic = searchClinic;
        //    ViewBag.searchComplication = searchComplication;

        //    string formatFromDate = searchFromDate.ToString("yyyy-MM-dd"); // Customize the format as needed
        //                                                                   // String DateString = Convert.ToString("07/02/1993");
        //    IFormatProvider culture = new CultureInfo("en-US");
        //    Date dateFromDate = Convert.ToDateTime(formatFromDate);



        //    string formatToDate = searchToDate.ToString("yyyy-MM-dd"); // Customize the format as needed
        //    Date parsedToDate = Convert.ToDateTime(formatToDate);


        //    var _surgeryresult = _isurgeryInterface.ExecuteStoredProcedure(dateFromDate, parsedToDate, searchSurgeon, searchProcedure, searchClinic, searchComplication);
        //    ReportViewModel reportViewModel = new();
        //    reportViewModel.LstSurgeryDetailsReport = new();

        //    foreach (var item in _surgeryresult)
        //    {
        //        // Initialize default values for optional fields
        //        string surgeonFirstName = item.SurgeonFirstName ?? "";
        //        string clinicName = item.ClinicName ?? "";
        //        string complicationName = item.ComplicationName ?? "";
        //        string incidentDetails = item.IncidentDetails ?? "";


        //        TimeSpan ArrivedInClinic = item.ArrivedInClinic;
        //        TimeSpan ArrivedInTheatre = item.ArrivedInTheatre;
        //        TimeSpan AnesthesiaStartTime = item.AnesthesiaStartTime;
        //        TimeSpan ProcedureFinishTime = item.ProcedureFinishTime;
        //        TimeSpan KnifeSkinTime = item.KnifeSkinTime;
        //        TimeSpan DepartureTime = item.DepartureTime;


        //        TimeSpan DurationInClinic = new TimeSpan();
        //        TimeSpan DurationInAnesthesia = new TimeSpan();
        //        TimeSpan DurationFromSkinKnifeToDeparture = new TimeSpan();


        //        if (ArrivedInTheatre < ArrivedInClinic)
        //        {
        //            // Adjust for the next day
        //            ArrivedInTheatre = ArrivedInTheatre.Add(new TimeSpan(24, 0, 0));
        //            DurationInClinic = ArrivedInTheatre - ArrivedInClinic;

        //        }
        //        else
        //        {
        //            DurationInClinic = ArrivedInTheatre - ArrivedInClinic;
        //        }
        //        if (ProcedureFinishTime < AnesthesiaStartTime)
        //        {
        //            ProcedureFinishTime = AnesthesiaStartTime.Add(new TimeSpan(24, 0, 0));
        //            DurationInAnesthesia = ProcedureFinishTime - AnesthesiaStartTime;
        //        }
        //        else
        //        {
        //            DurationInAnesthesia = ProcedureFinishTime - AnesthesiaStartTime;
        //        }
        //        if (DepartureTime < KnifeSkinTime)
        //        {
        //            DepartureTime = KnifeSkinTime.Add(new TimeSpan(24, 0, 0));
        //            DurationFromSkinKnifeToDeparture = DepartureTime - KnifeSkinTime;
        //        }
        //        else
        //        {
        //            DurationFromSkinKnifeToDeparture = DepartureTime - KnifeSkinTime;
        //        }

        //        var totalDuration = DurationInClinic.Add(DurationInAnesthesia).Add(DurationFromSkinKnifeToDeparture);

        //        TimeSpan dummy = totalDuration.Duration();
        //        double hoursdummy;



        //        if (!DepartureTime.Equals(TimeSpan.Zero))
        //        {
        //            hoursdummy = dummy.TotalHours;
        //            hoursdummy = Math.Round(hoursdummy, 2); // Rounds to two decimal places
        //        }
        //        else
        //        {
        //            hoursdummy = 0.0;
        //        }
        //        // Assuming you have some way to determine if the search is by complicationName or date range
        //        bool isSearchByComplication = /* condition to check if search is by complicationName 
        //        bool isSearchByDateRange = /* condition to check if search is by fromDate and toDate */;
        //        foreach (var itemres in _surgeryresult)
        //        {
        //            // Create a new report object
        //            var reportItem = new SurgeryDetailsReport
        //            {
        //                RegistrationDate = itemres.RegistrationDate,
        //                PatientNo = itemres.PatientNo,
        //                FirstName = itemres.FirstName,
        //                SurgeonFirstName = surgeonFirstName ?? "Default Surgeon", // Replace with actual default value
        //                ProcedureName = itemres.ProcedureName,
        //                ClinicName = clinicName ?? "Default Clinic", // Replace with actual default value
        //                ArrivedInClinic = itemres.ArrivedInClinic,
        //                ArrivedInTheatre = itemres.ArrivedInTheatre,
        //                AnesthesiaStartTime = itemres.AnesthesiaStartTime,
        //                KnifeSkinTime = itemres.KnifeSkinTime,
        //                ProcedureFinishTime = itemres.ProcedureFinishTime,
        //                DepartureTime = itemres.DepartureTime,
        //                Duration = hoursdummy
        //            };

        //            // Conditionally add complicationName and incidentDetails
        //            if (isSearchByComplication)
        //            {
        //                reportItem.ComplicationName = complicationName ?? "Default Complication"; // Replace with actual default value
        //                reportItem.IncidentDetails = incidentDetails ?? "Default Incident"; // Replace with actual default value
        //            }

        //            // Add the report item to the list
        //            reportViewModel.LstSurgeryDetailsReport.Add(reportItem);
        //        }


        //        //reportViewModel.LstSurgeryDetailsReport.Add(new SurgeryDetailsReport
        //        //{
        //        //    RegistrationDate = item.RegistrationDate,
        //        //    PatientNo = item.PatientNo,
        //        //    FirstName = item.FirstName,
        //        //    SurgeonFirstName = surgeonFirstName, // use default value if null
        //        //    ProcedureName = item.ProcedureName,
        //        //    ClinicName = clinicName, // use default value if null
        //        //    ComplicationName = complicationName, // use default value if null
        //        //    IncidentDetails = incidentDetails, // use default value if null
        //        //    ArrivedInClinic = item.ArrivedInClinic,
        //        //    ArrivedInTheatre = item.ArrivedInTheatre,
        //        //    AnesthesiaStartTime = item.AnesthesiaStartTime,
        //        //    KnifeSkinTime = item.KnifeSkinTime,
        //        //    ProcedureFinishTime = item.ProcedureFinishTime,
        //        //    DepartureTime = item.DepartureTime,
        //        //    Duration = hoursdummy,

        //        //});
        //    }



        //    return Json(reportViewModel);
        //    // return PartialView("~/Views/DatatableReport/GetDataTable.cshtml", reportViewModel);
        //}
        public string CombineDateTimeAndTimeSpan(DateTime? date, TimeSpan time)
        {
            if (date.HasValue)
            {
                // Combine the date and time into a single string
                return date.Value.ToString("yyyy-MM-dd") + " " + time.ToString(@"hh\:mm\:ss");
            }
            else
            {
                // Handle the scenario when the date is null
                // You could return just the time or a default string
                return time.ToString(@"hh\:mm\:ss");
            }
        }

        public async Task<IActionResult> Get(DateTime searchFromDate, DateTime searchToDate, string searchSurgeon, string searchProcedure, string searchClinic, string searchComplication, int? page)
        {
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            ViewBag.searchSurgeon = searchSurgeon;
            ViewBag.searchProcedure = searchProcedure;
            ViewBag.searchClinic = searchClinic;
            ViewBag.searchComplication = searchComplication;

            // Determine if the search is by complicationName or by date range
            bool isSearchByComplication = !string.IsNullOrEmpty(searchComplication);
            bool isSearchByDateRange = searchFromDate != default(DateTime) && searchToDate != default(DateTime);

            var _surgeryresult = _isurgeryInterface.ExecuteStoredProcedure(searchFromDate, searchToDate, searchSurgeon, searchProcedure, searchClinic, searchComplication);

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.LstSurgeryDetailsReport = new List<SurgeryDetailsReport>();

            foreach (var item in _surgeryresult)
            {
                double totalDurationInHours = 0;
                string formattedDuration = "";

                // Check if DepartureTime is not the minimum value
                if (item.DepartureTime > TimeSpan.Zero)
                {
                    if (item.DepartureDate.HasValue && item.ArrivedInClinicDate.HasValue)
                    {
                        // Calculate duration
                        DateTime actualDepartureTime = item.DepartureDate.Value.Date + item.DepartureTime;
                        DateTime actualArrivalTime = item.ArrivedInClinicDate.Value.Date + item.ArrivedInClinic;
                        TimeSpan duration = actualDepartureTime - actualArrivalTime;

                        // Store duration in hours
                        totalDurationInHours = duration.TotalHours;
                        // Check if duration is less than 1 hour
                        if (duration.TotalHours < 1)
                        {
                            // If less than an hour, display in minutes
                            int totalMinutes = (int)Math.Round(duration.TotalMinutes);
                            formattedDuration = totalMinutes.ToString() + " minutes";
                        }
                        else
                        {
                            // If 1 hour or more, display in hours
                            double roundedHours = Math.Round(duration.TotalHours, 2);
                            formattedDuration = roundedHours.ToString() + " hours";
                        }
                    }
                                        
                }

                double durationAsDouble = 0.0;
                if (formattedDuration.EndsWith("hours") && double.TryParse(formattedDuration.Replace(" hours", ""), out durationAsDouble))
                {
                    // Use durationAsDouble for calculations
                }
                else if (formattedDuration.EndsWith("minutes") && double.TryParse(formattedDuration.Replace(" minutes", ""), out double minutesValue))
                {
                    // Handle the case where the formattedDuration cannot be parsed to a double
                    durationAsDouble = minutesValue; // Divide by 60 to convert minutes to hours
                }
                else
                {
                    durationAsDouble = 0.0;
                }

                string arrivedInClinicForReport = CombineDateTimeAndTimeSpan(item.ArrivedInClinicDate, item.ArrivedInClinic);
                List<SurgeryDetailsReport> reports = new List<SurgeryDetailsReport>();

                // Use formattedDuration for display                // Create a new report item
                var reportItem = new SurgeryDetailsReport
                {
                    RegistrationDate = item.RegistrationDate,
                    PatientNo = item.PatientNo,
                    FirstName = item.FirstName,
                    SurgeonFirstName = item.SurgeonFirstName ?? "Default Surgeon",
                    ProcedureName = item.ProcedureName,
                    ClinicName = item.ClinicName ?? "Default Clinic",
                    ArrivedInClinic = item.ArrivedInClinic,
                //ArrivedInClinic = item.ArrivedInClinic +" "+ item.ArrivedInClinicDate,
                    ArrivedInTheatre = item.ArrivedInTheatre,
                    AnesthesiaStartTime = item.AnesthesiaStartTime,
                    KnifeSkinTime = item.KnifeSkinTime,
                    ProcedureFinishTime = item.ProcedureFinishTime,
                    DepartureTime = item.DepartureTime,
                    Duration = durationAsDouble, // Assuming TotalHours is a property of the returned durations
                    ArrivedInClinicDate = item.ArrivedInClinicDate,
                    DepartureDate = item.DepartureDate
                };

                // Add complication details only if searching by complication
                if (isSearchByComplication)
                {
                    reportItem.ComplicationName = item.ComplicationName ?? "Default Complication";
                    reportItem.IncidentDetails = item.IncidentDetails ?? "Default Incident";
                }

                reportViewModel.LstSurgeryDetailsReport.Add(reportItem);
               


            }

            return Json(reportViewModel);
            // return PartialView("~/Views/DatatableReport/GetDataTable.cshtml", reportViewModel);
        }

    }
}
