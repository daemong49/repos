﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringAopStudy.Core
{
    public interface IBusinessCore<T>
    {
         T Send(DateTime execDate);
    }



}
