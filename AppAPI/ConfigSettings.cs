﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppAPI
{
    public class ConfigSettings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["AppCS"].ConnectionString;
    }
}