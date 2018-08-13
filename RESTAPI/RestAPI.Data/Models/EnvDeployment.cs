using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestAPI.Data.Models
{
    public class EnvDeployment
    {
        [Key]
        public string Type { get; set; }
        public string Ticket { get; set; }
        public string Id { get; set; }
        public string Env { get; set; }
        public string Start_time { get; set; }
        public string User_id { get; set; }
        public string Shortdes { get; set; }
        public string Package { get; set; }
        public string Issued { get; set; }
    }
}
