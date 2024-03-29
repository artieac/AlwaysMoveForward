﻿/**
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

namespace AlwaysMoveForward.Core.Common.Utilities
{
    public class DateCompare : IEqualityComparer<DateTime>
    {
        #region IEqualityComparer<DateTime> Members

        public bool Equals(DateTime x, DateTime y)
        {
            bool retVal = false;

            if (x.Month == y.Month && x.Day == y.Day && x.Year == y.Year)
                retVal = true;

            return retVal;
        }

        public int GetHashCode(DateTime obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }

        #endregion
    }
}
