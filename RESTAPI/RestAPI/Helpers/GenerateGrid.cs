using RestAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Helpers
{
    public class GenerateGrid
    {
        private readonly string _stringConnection;
        public GenerateGrid(string connection)
        {
            _stringConnection = connection;
        }

        #region Generate Table for Deployment
        public static StringBuilder GenerateDeployment(IEnumerable<EnvDeployment> result)
        {
            // draw table with border and columns..
            StringBuilder sb = new StringBuilder();
            sb.Append("<table><tbody>");
            sb.Append("<table border=1 cellpadding=20 width = '100%'>");
            sb.Append("<th style='background-color:#474747;'> <font size = 2 color = white face = Calibri><span style = 'font-size:11.0pt;color:white;font-weight:bold'> Ticket </th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Tasks</th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Errors</th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Deployed Location</th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Date </th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Issued By</th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Description</th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>PackageType</th>");
            sb.Append("<th style='background-color:#474747;'> <font size=2 color=white face=Calibri><span style='font-size:11.0pt;color:white;font-weight:bold'>Issued</th>");

            // iterate through results..
            foreach (EnvDeployment item in result)
            {
                sb.Append("<tr>");

                sb.Append("<TD ALIGN='center'>" + "<a href='/test.html' </a>" + item.Ticket + "</TD>");
                sb.Append("<TD ALIGN='center'>" + "<a href ='javascript:void(0)'><img src='../tasks.png' border='0' WIDTH=45 HEIGHT=45 ALIGN=center /> </a></TD>");
                sb.Append("<TD ALIGN='center'>" + "<a href ='test.html'><img src='../error.png' border='0' WIDTH=45 HEIGHT=45 ALIGN=center /> </a></TD>");
                sb.Append("<TD ALIGN='center'>" + item.Env + "</TD>");
                sb.Append("<TD ALIGN='center'>" + item.Start_time + "</TD>");
                sb.Append("<TD ALIGN='center'>" + item.User_id + "</TD>");
                sb.Append("<TD ALIGN='center'>" + item.Shortdes + "</TD>");
                sb.Append("<TD ALIGN='center'> Package </TD>");
                sb.Append("<TD ALIGN='center'> Issued </TD>");

                sb.Append("</tr>\n");
            }

            sb.Append("</tbody></table>");
            return sb;
        }
        #endregion

    }
}
