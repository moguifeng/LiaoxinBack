using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Liaoxin.ViewModel
{

    public class BackReportRequest
    {
        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

   
        public string RealName { get; set; }
        public string LiaoxinNumber { get; set; }

    }
    public class BackReportResponse
    {

        public BackReportResponse()
        {
            BackReports = new List<BackReports>();
        }
        public int ClientCount { get; set; }

        public int GroupCount { get; set; }
        public decimal Recharges { get; set; }

        public decimal Withdraws { get; set; }

        public decimal Wins { get; set; }


        public List<BackReports> BackReports { get; set; }




    }

    public class BackReports
    {
        public string LiaoxinNumber { get; set; }
        public string RealName { get; set; }

        public decimal Wins { get; set; }
    }
}