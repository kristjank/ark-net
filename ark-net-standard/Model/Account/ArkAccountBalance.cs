﻿using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Account
{
    public class ArkAccountBalance : ArkResponseBase
    {
        public long Balance { get; set; }
        public long UnconfirmedBalance { get; set; }
    }
}
