﻿using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement
{
    public class ClientScope
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Scope { get; set; }
    }
}