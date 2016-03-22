﻿using System.Collections.Generic;

namespace SHEP_Platform.Models.Monitor
{
    public class ScheduleCompareViewModel
    {
        public string BasicAvgTp { get; set; }

        public string BasicAvgDb { get; set; }

        public string StructureTp { get; set; }

        public string StructureDb { get; set; }
    }

    public class StatHourInfo
    {
        public T_ESHour Hour { get; set; }

        public T_ESMin Current { get; set; }
    }
}