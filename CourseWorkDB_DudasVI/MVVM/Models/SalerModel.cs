using System;
using System.Collections.Generic;
using LiveCharts;

namespace CourseWorkDB_DudasVI.MVVM.Models
{
    public class SalerModel
    {
        public SalerModel()
        {
            #region First

            #endregion

            #region Second

            #endregion

            #region Third

            #endregion
        }

        public static string ConvertAddress(WAREHOUSE warehouse, string number = "")
        {
            var address = warehouse.ADDRESS1;
            var response = "";
            if (address != null)
            {
                response = number + " " + address.COUNTRY + ", " + address.REGION + ", " + address.CITY + ", " +
                           address.STREET + ", " +
                           address.BUILDING_NUMBER;
            }
            return response;
        }

        public class RegionInfo
        {
            public RegionInfo(int index, DateTime from, DateTime to)
            {
                this.index = index;
                this.from = from;
                this.to = to;
            }

            public int index { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
        }

        //TabPages

        #region First

        #endregion

        #region Second

        #endregion

        #region Third

        #endregion

        #region So on

        #endregion

        #region OrderFilter

        public DateTime FromTime;
        public DateTime ToTime;
        public Dictionary<string, RegionInfo> options = new Dictionary<string, RegionInfo>();
        public List<string> OptionsList;
        public string selectedOption;
        public bool filterByPrice = false;
        public List<string> Labels;
        public string xTitle;
        public string yTitle;

        #endregion
    }
}