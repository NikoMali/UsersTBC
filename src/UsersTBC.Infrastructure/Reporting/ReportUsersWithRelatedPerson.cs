using Aspose.Words;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Interfaces;

namespace UsersTBC.Infrastructure.Reporting
{
    public class ReportUsersWithRelatedPerson
    {
        //private readonly IWebHostEnvironment _hostingEnvironment;
        //private readonly IUserRepository _userRepository;
        /*public ReportUsersWithRelatedPerson(IWebHostEnvironment hostEnvironment, IUserRepository userRepository)
        {
            _hostingEnvironment = hostEnvironment;
            _userRepository = userRepository;
        }*/
        public static void ReportUsers(IWebHostEnvironment _hostingEnvironment,IUserService userService)
        {
            var dataDir = _hostingEnvironment.WebRootPath;
            string fileName = "MailMerge.ExecuteWithRegions.docx";
            Document doc = new Document(dataDir +"\\"+ fileName);

            var users = System.Text.Json.JsonSerializer.Serialize(userService.UsersWithRelatedPersons(Domain.Enum.RelatedType.Colleague).Result);
            var node = JsonConvert.DeserializeXNode("{\"Row\":" + users + "}", "root");
            StringReader sr = new StringReader(node.ToString());

            // Create the Dataset and read the XML.
            DataSet customersDs = new DataSet();
            customersDs.ReadXml(sr);

            
            

            // Execute mail merge to fill the template with data from XML using DataTable.
            doc.MailMerge.ExecuteWithRegions(customersDs.Tables["Row"]);

            //dataDir = dataDir;
            dataDir = dataDir + "\\MailMerge.AlternatingRows_out.pdf";
            // Save the output document.
            doc.Save(dataDir);
        }
    }
}
