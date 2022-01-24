using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskLightlamp.models
{
    public class products
    {
        public int id { get; set; }
        public string NameProdect { get; set; }
        public string url { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        [JsonIgnore]
        public  ApplicationUser ApplicationUser{get; set;}
    }
}
