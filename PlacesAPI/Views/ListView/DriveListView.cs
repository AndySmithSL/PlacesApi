﻿using PlacesAPI.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesAPI.Views.ListView
{
    public class DriveListView : DriveView
    {
        public int Legs => ViewObject.DriveLegs.Count();
    }
}
