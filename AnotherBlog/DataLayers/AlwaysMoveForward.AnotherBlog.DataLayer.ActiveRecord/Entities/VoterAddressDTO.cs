/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Castle.ActiveRecord;

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("VoterAddresses")]
    public class VoterAddressDTO
    {
        public VoterAddressDTO()
        {
            this.Id = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "VoterAddressId", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("Address", ColumnType = "String")]
        public String AddressString
        {
            get { return this.Address.ToString(); }
            set { this.Address = IPAddress.Parse(value); }
        }

        public IPAddress Address { get; private set; }

        [BelongsTo("PollOptionId", Type = typeof(PollOptionDTO))]
        public PollOptionDTO Option { get; set; }

    }
}
