using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace CreatedQR.bussiness
{
    public class ITServicesUtil
    {
        public static bool SendMailGmail(string fromName, string fromEmail, string toName, string toAddress, string ccList, string subject, string body,
      string host, string emailServer, string passWord)
        {
            bool sendSuccess = true;
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                MailAddress fromAddress = new MailAddress(fromEmail, fromName);
                message.From = fromAddress;
                message.To.Add(new MailAddress(toAddress, toName));
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                // We use gmail as our smtp client
                smtpClient.Host = host;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtpClient.Credentials = new System.Net.NetworkCredential(emailServer, passWord);

                smtpClient.Send(message);
                Thread.Sleep(3000);
            }
            catch
            {
                sendSuccess = false;
            }
            return sendSuccess;
        }

        public static DataSet ImportByFileExcel(string pathFile)
        {
            DataSet dsData = new DataSet();
            OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'", pathFile));
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);

            da.Fill(dsData, "UserList");

            con.Close();

            //xóa bỏ dòng dữ liệu rỗng
            dsData = RemoveRowEmty(dsData);

            //xóa bỏ cột
            //dsData = RemoveColumnEmty(dsData);

            if (File.Exists(pathFile))
                File.Delete(pathFile);

            return dsData;
        }
        public static DataSet RemoveRowEmty(DataSet dsData)
        {
            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dsData.Tables[0].Rows[i][0].ToString()))
                {
                    dsData.Tables[0].Rows[i].Delete();
                }
            }
            dsData.Tables[0].AcceptChanges();
            return dsData;
        }
        public static DataSet RemoveColumnEmty(DataSet dsData)
        {
            int desiredSize = 11;
            if (dsData.Tables[0].Columns.Count > desiredSize)
            {
                while (dsData.Tables[0].Columns.Count > desiredSize)
                {
                    dsData.Tables[0].Columns.RemoveAt(desiredSize);
                }
            }
            return dsData;
        }

    }

    public static class StringExtension
    {
        public static string Base64Encode(this string plainText)
        {
            Base64UrlEncoder.Encode(plainText);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64UrlEncode(this string plainText)
        {
            return Base64UrlEncoder.Encode(plainText);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64UrlDecode(this string base64EncodedData)
        {
            return Base64UrlEncoder.Decode(base64EncodedData);
        }

        // HÀM CHECK ARRAY1 CONTAINS ALL ITEMS IN ARR2
        public static bool ArrayContains(this List<int> arr1, List<int> arr2)
        {
            if (arr2.Any(i => !arr1.Contains(i)))
            {
                return false;
            }
            return true;
        }
        public static string MD5Hash(this string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString().ToLower();
        }
        public static string RemoveAccents(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            var normalizedString = text.ToString().Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string Standardizing(this string text)
        {
            return text.RemoveAccents().ToLower();
        }

        public static string RemoveWhitespace(this string input) => Regex.Replace(input, @"\s+", "");

        public static bool Match(this string text, string compareText)
        {
            string text1 = text.Standardizing().Trim();
            string text2 = compareText.Standardizing().Trim();
            return text1.Contains(text2) || text2.Contains(text1);
        }

        public static string RemoveExtraSpaces(this string s)
        {
            if (s == null)
                return null;
            return string.Join(" ", s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static int ConvertToInt(this string text)
        {
            int result;
            int.TryParse(text, out result);
            return result;
        }

        public static DateTime ConvertToDateTime(this string text)
        {
            DateTime result;
            if (!DateTime.TryParse(text, out result))
            {
                result = DateTime.FromOADate(double.Parse(text));
            }
            return result;
        }

        public static string ToBase64(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return "";
            }
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}