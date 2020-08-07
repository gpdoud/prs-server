//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PrsServer.Models {
    public class RequestLine {

        public int Id { get; set; }
        public int Quantity { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual Request Request { get; set; }
        public virtual Product Product { get; set; }

        public RequestLine() { }
    }
}
