﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAPrototype
{
    class Termination
    {

        public Termination()
        {
        }

        public Boolean doesTerminate(DateTime begin)
        {
            DateTime Now = DateTime.Now;

            double length = Now.Subtract(begin).TotalMinutes;

            if (length > 2)
            {
                return true;
            }
            return false;
        }
    }
}
