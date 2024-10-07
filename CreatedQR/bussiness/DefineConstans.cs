using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace CreatedQR.bussiness
{
    public class DefineConstans
    {
    }
    public enum EnumLenghtCode
    {
        CodeLenghtContent = 2,
        CodeLenghtContentContract = 1,
    }
    public class LibraryCommon
    {
        public String DropText(string strBegin, int intNumber)
        {
            if (strBegin.Length > intNumber)
            {
                int intSpace = strBegin.IndexOf(' ', intNumber);
                if (intSpace > 0)
                    return string.Format("{0}...", strBegin.Substring(0, intSpace));
                else
                    return strBegin;
            }
            else if (strBegin.Length == intNumber)
                return strBegin;
            else
                return string.Format("{0}", strBegin);

        }
        public String DropTextNumber(string strBegin, int intNumberStare)
        {
            if (strBegin.Length > intNumberStare)
            {
                int intSpace = strBegin.IndexOf(' ', intNumberStare);
                if (intSpace > 0)
                    return string.Format("{0}", strBegin.Substring(intSpace));
                else
                    return strBegin;
            }
            else if (strBegin.Length == intNumberStare)
                return strBegin;
            else
                return string.Format("{0}", strBegin);

        }

        public string RemoveUnicode(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public string FormatUrlString(string urlString)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    urlString = urlString.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            urlString = urlString.Trim();
            urlString = urlString.Replace("  ", " ");
            urlString = urlString.Replace(" ", "-");
            urlString = urlString.Replace("--", "-");
            urlString = urlString.Replace("--", "-");
            urlString = urlString.Replace("--", "-");
            urlString = urlString.Replace(":", string.Empty);
            urlString = urlString.Replace(";", string.Empty);
            urlString = urlString.Replace("(", string.Empty);
            urlString = urlString.Replace(")", string.Empty);
            urlString = urlString.Replace("/", string.Empty);
            urlString = urlString.Replace("+", string.Empty);
            urlString = urlString.Replace("?", string.Empty);
            urlString = urlString.Replace("%", string.Empty);
            urlString = urlString.Replace("#", string.Empty);
            urlString = urlString.Replace("&", string.Empty);
            urlString = urlString.Replace("<", string.Empty);
            urlString = urlString.Replace(">", string.Empty);
            urlString = urlString.Replace(@"\", string.Empty);
            urlString = urlString.Replace("\"", string.Empty);
            urlString = urlString.Replace("'", string.Empty);
            urlString = urlString.Replace("`", string.Empty);
            urlString = urlString.Replace("$", string.Empty);
            urlString = urlString.Replace(".", string.Empty);
            urlString = urlString.Replace(",", string.Empty);
            urlString = urlString.Replace("=", string.Empty);
            urlString = urlString.Replace("@", string.Empty);
            urlString = urlString.Replace("{", string.Empty);
            urlString = urlString.Replace("}", string.Empty);
            urlString = urlString.Replace("|", string.Empty);
            urlString = urlString.Replace("^", string.Empty);
            urlString = urlString.Replace("~", string.Empty);
            urlString = urlString.Replace("[", string.Empty);
            urlString = urlString.Replace("]", string.Empty);
            urlString = urlString.Replace("’", string.Empty);
            urlString = urlString.Replace("û", "u");
            urlString = urlString.Replace("*", "");
            urlString = urlString.Replace("--", "-");

            return HttpUtility.HtmlEncode(urlString.ToLower());
        }

        public string NumberToText(double inputNumber)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            if (string.IsNullOrEmpty(sNumber))
            {
                return "";
            }
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            //return result + " đồng";// + (suffix ? " đồng chẵn" : "");

            return ToUpperFirstLetter(result.Trim()) + " đồng";
        }

        public string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }


        public string formatDateUpperFirst(int day, int month, int year)
        {
            string dayF = day.ToString();
            if (day < 10)
                dayF = "0" + day;

            string monthF = month.ToString();
            if (month < 3)
                monthF = "0" + month;

            string result = string.Format("Ngày {0} tháng {1} năm {2}", dayF, monthF, year);

            return result;
        }
        public string formatDate(int day, int month, int year)
        {
            string dayF = day.ToString();
            if (day < 10)
                dayF = "0" + day;

            string monthF = month.ToString();
            if (month < 3)
                monthF = "0" + month;

            string result = string.Format("ngày {0} tháng {1} năm {2}", dayF, monthF, year);

            return result;
        }
        public string formatTimDate(int hour, int minute, int day, int month, int year)
        {
            string dayF = day.ToString();
            if (day < 10)
                dayF = "0" + day;

            string monthF = month.ToString();
            if (month < 3)
                monthF = "0" + month;

            //gio 
            string hourF = hour.ToString();
            if (hour < 10)
                hourF = "0" + hour;

            // phut
            string minuteF = minute.ToString();
            if (minute < 10)
                minuteF = "0" + minute;

            string result = string.Format("Lúc {0} giờ {1} phút, ngày {2} tháng {3} năm {4}", hourF, minuteF, dayF, monthF, year);

            return result;
        }
        public string formatTimeDateNotLUC(int hour, int minute, int day, int month, int year)
        {
            string dayF = day.ToString();
            if (day < 10)
                dayF = "0" + day;

            string monthF = month.ToString();
            if (month < 3)
                monthF = "0" + month;

            //gio 
            string hourF = hour.ToString();
            if (hour < 10)
                hourF = "0" + hour;

            // phut
            string minuteF = minute.ToString();
            if (minute < 10)
                minuteF = "0" + minute;

            string result = string.Format("{0} giờ {1} phút, ngày {2} tháng {3} năm {4}", hourF, minuteF, dayF, monthF, year);

            return result;
        }
        public string formatTimeDateFromTo(int day, int month, int year, int dayTo, int monthTo, int yearTo)
        {
            string dayF = day.ToString();
            if (day < 10)
                dayF = "0" + day;

            string monthF = month.ToString();
            if (month < 3)
                monthF = "0" + month;

            string dayToF = dayTo.ToString();
            if (dayTo < 10)
                dayToF = "0" + dayTo;

            string monthToF = monthTo.ToString();
            if (monthTo < 3)
                monthToF = "0" + monthTo;

            string result = string.Format("từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}", dayF, monthF, year, dayToF, monthToF, yearTo);

            return result;
        }
        public string formatTimeFromDateToDate(int hour, int minute, int day, int month, int year, int hourTo, int minuteTo, int dayTo, int monthTo, int yearTo)
        {
            string dayF = day.ToString();
            if (day < 10)
                dayF = "0" + day;

            string monthF = month.ToString();
            if (month < 3)
                monthF = "0" + month;

            //gio 
            string hourF = hour.ToString();
            if (hour < 10)
                hourF = "0" + hour;

            // phut
            string minuteF = minute.ToString();
            if (minute < 10)
                minuteF = "0" + minute;

            //-----------date to
            string dayToF = dayTo.ToString();
            if (dayTo < 10)
                dayToF = "0" + dayTo;

            string monthToF = monthTo.ToString();
            if (monthTo < 3)
                monthToF = "0" + monthTo;

            //gio 
            string hourToF = hourTo.ToString();
            if (hourTo < 10)
                hourToF = "0" + hourTo;

            // phut
            string minuteToF = minuteTo.ToString();
            if (minuteTo < 10)
                minuteToF = "0" + minuteTo;

            string result = string.Format("phát hành từ {0} giờ {1} phút ngày {2} tháng {3} năm {4} đến {5} giờ {6} phút ngày {7} tháng {8} năm {9}",
                            hourF, minuteF, dayF, monthF, year,
                            hourToF, minuteToF, dayToF, monthToF, yearTo);

            return result;
        }

        public string formatTimeBBTT(int hour, int minute)
        {
            //gio 
            string hourF = hour.ToString();
            if (hour < 10)
                hourF = "0" + hour;

            // phut
            string minuteF = minute.ToString();
            if (minute < 10)
                minuteF = "0" + minute;

            string result = string.Format("{0} giờ {1} phút cùng ngày", hourF, minuteF);

            return result;
        }
        private string GetLenghtIDRemain(int lenghtUserID, int numUser)
        {
            string result = string.Empty;
            result = numUser.ToString().PadLeft(lenghtUserID, '0');
            return result;
        }
    }
}