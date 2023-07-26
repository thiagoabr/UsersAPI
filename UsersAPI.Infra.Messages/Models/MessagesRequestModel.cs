﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAPI.Infra.Messages.Models
{
    public class MessagesRequestModel
    {
        public string? MailTo { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
