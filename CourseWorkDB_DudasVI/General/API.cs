using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWorkDB_DudasVI.General
{
    public class API
    {
        public static decimal getlastPrice(ICollection<PRODUCT_PRICE> prices)
        {
            return prices.OrderBy(pr => pr.CHANGED_DATE).Last().PRICE_VALUE;
        }

        public static List<PRODUCT_PRICE> getPrice(ICollection<PRODUCT_PRICE> prices, DateTime startPeriod,
            DateTime endPeriod)
        {
//TODO Поставити межі на дати
            return
                prices.ToList().FindAll(pr => pr.CHANGED_DATE <= endPeriod && pr.CHANGED_DATE >= startPeriod).ToList();
        }

        public static decimal getlastSalary(ICollection<SALARY> salaries)
        {
            return salaries.OrderBy(pr => pr.CHANGED_DATE).Last().SALARY_VALUE;
        }

        public static List<SALARY> getPrice(ICollection<SALARY> salaries, DateTime startPeriod, DateTime endPeriod)
        {
            return
                salaries.ToList().FindAll(pr => pr.CHANGED_DATE <= endPeriod && pr.CHANGED_DATE >= startPeriod).ToList();
        }

        public static DateTime getLastPlanDate(WAREHOUSE warehouse)
        {
            var result = warehouse.PRODUCTION_SCHEDULE.ToList();
            if (result.Count > 0)
            {
                return result.OrderBy(p => p.CREATED_DATE).ToList().Last().CREATED_DATE;
            }
            return getTodayDate();
        }

        public static DateTime getLastPlanDate(STAFF staff)
        {
            var result = staff.PRODUCTION_SCHEDULE.ToList();
            if (result.Count > 0)
            {
                return result.OrderBy(p => p.CREATED_DATE).ToList().Last().CREATED_DATE;
            }
            return getTodayDate();
        }

        //return ac date
        public static DateTime getTodayDate()
        {
            var res = DateTime.Today;
            return res;
        }

        public static List<ORDER_PRODUCT> getPackagesInRange(DateTime from, DateTime to, List<ORDER_PRODUCT> packages)
        {
            var res = packages.FindAll(p => p.SALE_ORDER.ORDER_DATE >= from && p.SALE_ORDER.ORDER_DATE <= to).ToList();
            return res;
        }
    }
}