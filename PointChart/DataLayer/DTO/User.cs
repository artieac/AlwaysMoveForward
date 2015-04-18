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
using System.Web;
using System.Security.Principal;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="Users")]
    public class User 
    {
        public User()
            : base()
        {
            this.Id = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual bool IsSiteAdministrator { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long OAuthServiceUserId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string FirstName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string LastName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string AccessToken { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string AccessTokenSecret { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "PointEarners", Cascade = "Save-Update")]
        [NHibernate.Mapping.Attributes.Key(1, Column = "UserId")]
        [NHibernate.Mapping.Attributes.ManyToMany(2, Column = "PointEarnerId", ClassType = typeof(DTO.User))]
        public virtual IList<DTO.User> PointEarners { get; set; }
    }
}
