using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded6.Data;
using TechJobsMVCAutograded6.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsMVCAutograded6.Controllers;

public class ListController : Controller
{
    internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"employer", "Employer"},
            {"location", "Location"},
            {"positionType", "Position Type"},
            {"coreCompetency", "Skill"}
        };
    internal static Dictionary<string, List<JobField>> TableChoices = new Dictionary<string, List<JobField>>()
        {
            //{"all", "View All"},
            {"employer", JobData.GetAllEmployers()},
            {"location", JobData.GetAllLocations()},
            {"positionType", JobData.GetAllPositionTypes()},
            {"coreCompetency", JobData.GetAllCoreCompetencies()}
        };

    public IActionResult Index()
    {
        ViewBag.columns = ColumnChoices;
        ViewBag.tableChoices = TableChoices;
        ViewBag.employers = JobData.GetAllEmployers();
        ViewBag.locations = JobData.GetAllLocations();
        ViewBag.positionTypes = JobData.GetAllPositionTypes();
        ViewBag.skills = JobData.GetAllCoreCompetencies();

        return View();
    }

    // TODO #2 - Complete the Jobs action method
    public IActionResult Jobs(string column, string value)
    {
        List<Job> jobs; // Declare a list to store the job results
        string title; // Declare a string to store the title for the view

        // Check if 'column' is null, empty, or "all"
        if (string.IsNullOrEmpty(column) || column.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            jobs = JobData.FindAll(); // Retrieve all jobs
            title = "All Jobs"; // Set the title for the view
        }
        else
        {
            jobs = JobData.FindByColumnAndValue(column, value); // Retrieve jobs based on the selected column and value
            title = $"Jobs with {ColumnChoices[column]}: {value}"; // Set the title to include the selected column and value
        }

        ViewBag.jobs = jobs; // Pass the jobs list to the view
        ViewBag.title = title; // Pass the title to the view
        return View(); // Return the view to be rendered
    }
}
