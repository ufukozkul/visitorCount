using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Azure.Core;
using System.Collections;

namespace MyWebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public void OnGet()
        {
            
            
            System.Diagnostics.Trace.WriteLine("Abiskoo!");
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            

            System.IO.File.AppendAllText("./visitors.txt", $"{remoteIpAddress}\n");

            var ipList = GetFileContent("./visitors.txt");

            int visitorCount = CountDistinctVisitors(ipList);

            Console.WriteLine($"Visitor count is: {visitorCount}");

        }

        private List<string> GetFileContent(string filePath)
        {
            var fileContent = System.IO.File.ReadAllLines(filePath).ToList();

            return fileContent;
        }

        private int CountDistinctVisitors(List<string> ipAddressList)
        {
            if(ipAddressList == null)
            {
                Console.WriteLine("Ip address list cannot be null!");
                return 0;
            }

           return ipAddressList.Distinct().Count();

        }

        public int GetUniqeVisitorCount(string filePath)
        {

            var ipList = GetFileContent("./visitors.txt");

            int visitorCount = CountDistinctVisitors(ipList);

           return visitorCount;

        }

    }
}
