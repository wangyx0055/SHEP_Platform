﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SHEP_Platform.Enum;

namespace SHEP_Platform.Controllers
{
    public class AjaxController : AjaxControllerBase
    {
        public JsonResult Access()
        {
            return ParseRequest();
        }

        // GET: Ajax
        protected override JsonResult ExecuteFun(string functionName)
        {
            switch (functionName)
            {
                case "getStatAvgReport":
                    return GetStatAvgReport();
                case "getStatsActualData":
                    return GetStatsActualData();
            }

            return null;
        }

        private JsonResult GetStatAvgReport()
        {
            var pollutantType = Request["pollutantType"];
            var queryDateRange = Request["queryDateRange"];
            var datePickerValue = Request["datePickerValue"]?.Split(',');

            var startDate = DateTime.MinValue;
            var endDate = DateTime.Now;
            switch (queryDateRange)
            {
                case QueryDateRange.LastWeek:
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case QueryDateRange.LastMonth:
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
                case QueryDateRange.LastYear:
                    startDate = DateTime.Now.AddYears(-1);
                    break;
                case QueryDateRange.Customer:
                    if (datePickerValue == null || datePickerValue.Length < 2)
                    {
                        throw new Exception("参数错误");
                    }
                    startDate = DateTime.Parse(datePickerValue[0]);
                    endDate = DateTime.Parse(datePickerValue[1]);
                    break;
            }

            var stringBuilder = new StringBuilder();

            if (pollutantType == PollutantType.ParticulateMatter)
            {
                stringBuilder.AppendFormat("UpdateTime >='{0}' and UpdateTime <='{1}'",
                    startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    endDate.ToString("yyyy-MM-dd HH:mm:ss"));
                var ret =
                    from item in DbContext.T_ESDay_GetAvgCMPStatList(WdContext.User.Remark, stringBuilder.ToString())
                    group item by item.StatId
                    into newresult
                    select newresult;

                var dict = ret.ToDictionary(p => p.Key).Select(item => new
                {
                    Name = WdContext.StatList.First(o => o.Id == item.Key).StatName,
                    MaxVal = double.Parse((item.Value.OrderBy(i => i.AvgTP).First().AvgTP / 1000).ToString()).ToString("f2"),
                    AvgVal = double.Parse((item.Value.Average(j => j.AvgTP) / 1000).ToString()).ToString("f2"),
                    MinVal = double.Parse((item.Value.OrderByDescending(k => k.AvgTP).First().AvgTP / 1000).ToString()).ToString("f2"),
                    ValidNum = item.Value.Count()
                }).ToList();

                return Json(dict);
            }

            if (pollutantType == PollutantType.Noise)
            {
                stringBuilder.AppendFormat("UpdateTime >='{0}' and UpdateTime <='{1}'",
                    startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    endDate.ToString("yyyy-MM-dd HH:mm:ss"));
                var ret =
                    from item in DbContext.T_ESDay_GetAvgNoiseStatList(WdContext.User.Remark, stringBuilder.ToString())
                    group item by item.StatId
                    into newresult
                    select newresult;

                var dict = ret.ToDictionary(p => p.Key).Select(item => new
                {
                    Name = WdContext.StatList.First(o => o.Id == item.Key).StatName,
                    MaxVal = double.Parse(item.Value.OrderBy(i => i.AvgDB).First().AvgDB.ToString()).ToString("f2"),
                    AvgVal = double.Parse(item.Value.Average(j => j.AvgDB).ToString()).ToString("f2"),
                    MinVal = double.Parse(item.Value.OrderByDescending(k => k.AvgDB).First().AvgDB.ToString()).ToString("f2"),
                    ValidNum = item.Value.Count()
                }).ToList();

                return Json(dict);
            }

            return null;
        }

        private JsonResult GetStatsActualData()
        {
            var statId = int.Parse(Request["statId"]);
            var startDate = DateTime.Now.AddHours(-1);
            var ret = DbContext.T_ESMin.Where(item => item.StatId == statId && item.UpdateTime > startDate)
                .OrderBy(obj => obj.UpdateTime).ToList()
                .Select(i => new { TP = (i.TP / 1000).ToString("f2"), DB = i.DB.ToString("f2"), UpdateTime = ((DateTime)i.UpdateTime).ToString("HH:mm:ss") });

            return Json(ret);
        }
    }
}