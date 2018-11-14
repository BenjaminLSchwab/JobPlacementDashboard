using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public enum JPCompanyState
    {
        // USA States
        [Display(Name = "AL")]
        Alabama,
        [Display(Name = "AK")]
        Alaska,
        [Display(Name = "AZ")]
        Arizona,
        [Display(Name = "AR")]
        Arkansas,
        [Display(Name = "CA")]
        California,
        [Display(Name = "CO")]
        Colorado,
        [Display(Name = "CT")]
        Connecticut,
        [Display(Name = "DE")]
        Delaware,
        [Display(Name = "FL")]
        Florida,
        [Display(Name = "GA")]
        Georgia,
        [Display(Name = "HI")]
        Hawaii,
        [Display(Name = "ID")]
        Idaho,
        [Display(Name = "IL")]
        Illinois,
        [Display(Name = "IN")]
        Indiana,
        [Display(Name = "IA")]
        Iowa,
        [Display(Name = "KS")]
        Kansas,
        [Display(Name = "KY")]
        Kentucky,
        [Display(Name = "LA")]
        Louisiana,
        [Display(Name = "ME")]
        Maine,
        [Display(Name = "MD")]
        Maryland,
        [Display(Name = "MA")]
        Massachusetts,
        [Display(Name = "MI")]
        Michigan,
        [Display(Name = "MN")]
        Minnesota,
        [Display(Name = "MS")]
        Mississippi,
        [Display(Name = "MO")]
        Missouri,
        [Display(Name = "MT")]
        Montana,
        [Display(Name = "NE")]
        Nebraska,
        [Display(Name = "NV")]
        Nevada,
        [Display(Name = "NH")]
        New_Hampshire,
        [Display(Name = "NJ")]
        New_Jersey,
        [Display(Name = "NM")]
        New_Mexico,
        [Display(Name = "NY")]
        New_York,
        [Display(Name = "NC")]
        North_Carolina,
        [Display(Name = "ND")]
        North_Dakota,
        [Display(Name = "OH")]
        Ohio,
        [Display(Name = "OK")]
        Oklahoma,
        [Display(Name = "OR")]
        Oregon,
        [Display(Name = "PA")]
        Pennsylvania,
        [Display(Name = "RI")]
        Rhode_Island,
        [Display(Name = "SC")]
        South_Carolina,
        [Display(Name = "SD")]
        South_Dakota,
        [Display(Name = "TN")]
        Tennessee,
        [Display(Name = "TX")]
        Texas,
        [Display(Name = "UT")]
        Utah,
        [Display(Name = "VT")]
        Vermont,
        [Display(Name = "VA")]
        Virginia,
        [Display(Name = "WA")]
        Washington,
        [Display(Name = "WV")]
        West_Virginia,
        [Display(Name = "WI")]
        Wisconsin,
        [Display(Name = "WY")]
        Wyoming,

        // Canadian Provinces and Territories
        [Display(Name = "AB")]
        Alberta,
        [Display(Name = "BC")]
        British_Columbia,
        [Display(Name = "MB")]
        Manitoba,
        [Display(Name = "NB")]
        New_Brunswick,
        [Display(Name = "NL")]
        Newfoundland_and_Labrador,
        [Display(Name = "NT")]
        Northwest_Territories,
        [Display(Name = "NS")]
        Nova_Scotia,
        [Display(Name = "NU")]
        Nunavut,
        [Display(Name = "ON")]
        Ontario,
        [Display(Name = "PE")]
        Prince_Edward_Island,
        [Display(Name = "QC")]
        Quebec,
        [Display(Name = "SK")]
        Saskatchewan,
        [Display(Name = "YT")]
        Yukon,

        // generic remote employee
        [Display(Name = "Remote Employee")]
        RemoteEmployee
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}