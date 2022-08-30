﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domasna.Domain.DomainModels
{
    public class EmailMessage : BaseEntity
    {
        public string MailTo { get; set; }
        
        public string Subject { get; set; }

        public string Content { get; set; }

    }
}
