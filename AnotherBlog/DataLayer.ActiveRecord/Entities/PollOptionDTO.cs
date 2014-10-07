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
using Castle.ActiveRecord;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("PollOptions")]
    public class PollOptionDTO
    {
        public PollOptionDTO()
        {
            this.Id = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "PollOptionId", UnsavedValue = "-1")]
        public int Id { get; set; }
        
        [Property("OptionText")]
        public string OptionText { get; set; }

        [BelongsTo("PollQuestionId", Type = typeof(PollQuestionDTO))]
        public PollQuestionDTO Question { get; set; }

        [HasMany(typeof(VoterAddressDTO), Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Inverse = true)]
        public IList<VoterAddressDTO> VoterAddresses { get; set; }
    }
}
