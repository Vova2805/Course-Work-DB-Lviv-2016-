using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWorkDB_DudasVI.General
{
    public class API
    {
        public static PRODUCT_PRICE getlastPrice(ICollection<PRODUCT_PRICE> prices)
        {
            var result = prices.OrderBy(pr => pr.CHANGED_DATE).Last();
            return Session.FactoryEntities.PRODUCT_PRICE
                .AsNoTracking()
                .FirstOrDefault(p => p.PRICE_ID == result.PRICE_ID);
        }

        public static string ConvertAddress(ADDRESS address, string number = "",string additionalInfo="")
        {
            var response = "";
            if (address != null)
            {
                response = number + " " + address.COUNTRY + ", " + address.REGION + ", " + address.CITY + ", " +
                           address.STREET + ", " +
                           address.BUILDING_NUMBER+". "+additionalInfo;
            }
            return response;
        }

        public static List<PRODUCT_PRICE> getPrice(ICollection<PRODUCT_PRICE> prices, DateTime startPeriod,
            DateTime endPeriod)
        {
//TODO Поставити межі на дати
            return
                prices.ToList().FindAll(pr => pr.CHANGED_DATE <= endPeriod && pr.CHANGED_DATE >= startPeriod).ToList();
        }

        public static SALARY getlastSalary(ICollection<SALARY> salaries)
        {
            return salaries.OrderBy(pr => pr.CHANGED_DATE).Last();
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
            var res = DateTime.Now;
            return res;
        }

        public static List<ORDER_PRODUCT> getPackagesInRange(DateTime from, DateTime to, List<ORDER_PRODUCT> packages)
        {
            var res = packages.FindAll(p => p.SALE_ORDER.ORDER_DATE >= @from && p.SALE_ORDER.ORDER_DATE <= to).ToList();
            return res;
        }

        public static decimal getlastDeliveryCostPerKM()
        {
            var result = Session.FactoryEntities.DELIVERY.ToList().OrderBy(del => del.DELIVERY_DATE).Last();
            return result.COST_PER_KM;
        }

        public static decimal getVolume(PACKAGE_DESCRIPTION description)
        {
            decimal width = description.WIDTH;
            decimal height = description.HEIGTH;
            decimal lenght = description.LENGTH;
            //в мм
            return width*height*lenght / (decimal)Math.Pow(10,9);// до метрів^3
        }

        public static decimal getLastCostPerKM()
        {
            decimal res = 2;
            var temp = Session.FactoryEntities.DELIVERY.ToList();
            temp.Sort(
                (del1, del2) =>
                {
                    return del1.DELIVERY_DATE > del2.DELIVERY_DATE
                        ? 1
                        : del1.DELIVERY_DATE == del2.DELIVERY_DATE ? 0 : -1;
                }
                );
            res = temp.Last().COST_PER_KM;
            return res;
        }

        public static void CopyAddress(ref ADDRESS one, ADDRESS two)
        {
            one.ADDRESS_ID = two.ADDRESS_ID;
            one.COUNTRY = two.COUNTRY;
            one.CITY = two.CITY;
            one.REGION = two.REGION;
            one.STREET = two.STREET;
            one.BUILDING_NUMBER = two.BUILDING_NUMBER;
        }
        public static void CopyStaff(ref STAFF one, STAFF two)
        {
            one.STAFF_ID = two.STAFF_ID;
            one.STAFF_NAME = two.STAFF_NAME;
            one.STAFF_SURNAME = two.STAFF_SURNAME;
            one.BIRTH_DATE = two.BIRTH_DATE;
            one.EMAIL = two.EMAIL;
            one.LOGIN = two.LOGIN;
            one.PASSWORD = two.PASSWORD;
            one.POST_ID = two.POST_ID;
            one.ADDRESS = two.ADDRESS;
            one.POST_ID = two.POST_ID;
            one.FULL_SALARY_PERSENTAGE = two.FULL_SALARY_PERSENTAGE;
            one.THEME = two.THEME;
        }
        public static void CopyClient(ref CLIENT one, CLIENT two)
        {
            one.CLIENT_ID = two.CLIENT_ID;
            one.CLIENT_NAME = two.CLIENT_NAME;
            one.CLIENT_SURNAME = two.CLIENT_SURNAME;
            one.CLIENT_MIDDLE_NAME = two.CLIENT_MIDDLE_NAME;
            one.EMAIL = two.EMAIL;
            one.COMPANY_TITLE = two.COMPANY_TITLE;
            one.MOBILE_PHONE = two.MOBILE_PHONE;
            one.COMPANY_PHONE = two.COMPANY_PHONE;
            one.ADDRESS = two.ADDRESS;
            one.ADDRESS = two.ADDRESS;
        }
    }
}