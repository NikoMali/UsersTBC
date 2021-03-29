using Aspose.Words;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using UsersTBC.Application.Models;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.Interfaces;

namespace UsersTBC.Infrastructure.Reporting
{
    public class ReportUsersWithRelatedPerson
    {
        private static string fileName = "MailMerge.ExecuteWithRegions.docx";
        private static string folderName = "reports";
        private static string nameWithGuidId = Guid.NewGuid().ToString() + "_MailMerge.AlternatingRows_out.pdf";
        

        public static string ReportUsers(IWebHostEnvironment hostingEnvironment,string baseUrl, List<UserResponseModel> userResponseModel)
        {
            var dataDir = hostingEnvironment.WebRootPath;
            
            Document doc = new Document(dataDir +"\\"+ fileName);

            var users = System.Text.Json.JsonSerializer.Serialize(userResponseModel);

            var node = JsonConvert.DeserializeXNode("{\"Row\":" + users + "}", "root");

            StringReader sr = new StringReader(node.ToString());
            
            DataSet customersDs = new DataSet();

            customersDs.ReadXml(sr);

           
            doc.MailMerge.ExecuteWithRegions(customersDs.Tables["Row"]);

            dataDir = dataDir + "\\"+ folderName+ "\\"+ nameWithGuidId;

            // Save the output document.
            doc.Save(dataDir);
            return $"{baseUrl}/{folderName}/{nameWithGuidId}";
        }
    }
}
