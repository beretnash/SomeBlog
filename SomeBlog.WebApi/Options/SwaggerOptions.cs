using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeBlog.WebApi.Options
{
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UIEndpoint { get; set; }
    }
}
