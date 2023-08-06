﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SystemModels
{
    public class SMTPSettings
    {
        public string FromEmail { get; set; }
        public string FromEmailPassword { get; set; }
        public int HostPort { get; set; }
        public string HostServer { get; set; }
        public bool SSLStatus { get; set; }
    }
}
