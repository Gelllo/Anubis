using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Domain
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string AphroditeURL { get; set; }
        public string GoogleCloudId { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }

    }
}
