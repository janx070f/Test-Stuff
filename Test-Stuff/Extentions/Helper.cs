using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Test_Stuff.Extentions
{
    public class Helper
    {
        public class TextFormat
        {
            #region CropText
            public static string CropText(string text, int maxLength, bool doDots)
            {
                return (text.Length <= maxLength ? text : text.Substring(0, maxLength) + (doDots ? "..." : ""));
            }
            #endregion
        }
        public class Mail
        {
            #region MailSender
            public static bool MailSender(string smtp, string from, string to, string subject, string body)
            {
                var status = false;
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient(smtp);

                    if (!IsValidEmail(to)) return false;
                    mail.From = new MailAddress(@from);
                    mail.To.Add(new MailAddress(to));
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    var htmlbody = body.ToString();
                    mail.Body = htmlbody;
                    smtpServer.Send(mail);
                    return (status = true);
                }
                catch (Exception)
                {
                    return status;
                }
            }
            #endregion

            #region Validate Email
            public static bool IsValidEmail(string mailAddress)
            {
                Regex reg = new Regex("^((([a-z]|\\d|[!#\\$%&amp;'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&amp;'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
                if (!string.IsNullOrEmpty(mailAddress) && reg.IsMatch(mailAddress))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            internal static bool MailSender(string email, string title, string mail)
            {
                throw new NotImplementedException();
            }
            #endregion
        }

        public class Log
        {
            #region Log
            /// <summary>
            /// LogStatus represents a status of a log
            /// </summary>
            public enum LogStatus
            {
                Ok,
                Error,
                Send,
                NotSend,
                Exception
            }
            /// <summary>
            /// Log sets a new entry in log.txt file
            /// </summary>
            /// <param name="logMessage">the message to parse to the log</param>
            /// <param name="status">the status from enum LogStatus</param>
            public static void LogEntry(string logMessage, LogStatus status)
            {
                var path = HttpContext.Current.Server.MapPath("/App_Data/Logs/Log.txt");
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    TextWriter writer = w;
                    w.Write("\r\nLog Entry : {0} {1} : Status {2} ---- {3}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), status, logMessage);
                }
            }
            #endregion

            #region DumpLog
            /// <summary>
            /// DumpLog helps output a list of log messages
            /// </summary>
            /// <returns></returns>
            public static List<string> DumpLog()
            {
                string path = HttpContext.Current.Server.MapPath("/App_Data/Logs/myLog.txt");
                List<string> logs = new List<string>();
                string line;
                using (StreamReader r = File.OpenText(path))
                {
                    while ((line = r.ReadLine()) != null)
                    {
                        logs.Add(line);
                    }
                }
                return logs;
            }
            #endregion
        }

        public class Numbers
        {
            #region Shuffle int
            public static void Shuffle(int[] arr)
            {
                Random r = new Random();
                for (int n = arr.Length - 1; n > 0; --n)
                {
                    int k = r.Next(n + 1);
                    int temp = arr[n];
                    arr[n] = arr[k];
                    arr[k] = temp;
                }
            }
            #endregion

            #region Shuffle String
            public static void Shuffle(string[] arr)
            {
                Random r = new Random();
                for (int n = arr.Length - 1; n > 0; --n)
                {
                    int k = r.Next(n + 1);
                    string temp = arr[n];
                    arr[n] = arr[k];
                    arr[k] = temp;
                }
            }
            #endregion
        }

        public class PagingExtensions
        {
            #region Paging
            public class Paging
            {
                public int ItemsPerPage { get; set; }
                public int CurrentPage { get; set; }
                public int PreviousPage { get; set; }
                public int NextPage { get; set; }
                public double TotalPages { get; set; }
                public int Skip { get; set; }
                public int Take { get; set; }
            }
            #endregion

            #region Paging GetPages
            public static Paging GetPages(int itemCount, int itemsPerPage)
            {
                int page;
                int.TryParse(HttpContext.Current.Request.QueryString["page"], out page);
                if (page == 0) { page = 1; }
                var pages = new Paging
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = page,
                    PreviousPage = page - 1,
                    NextPage = page + 1,
                    TotalPages = Math.Ceiling(itemCount / (double)itemsPerPage),
                    Skip = (page * itemsPerPage) - itemsPerPage,
                    Take = itemsPerPage
                };
                return pages;
            }
            #endregion
        }
        public class Encryption
        {
            #region MD5 Hashing
            public static string MD5Hash(string data)
            {
                // namespace System.Security.Cryptography
                MD5 md5 = MD5.Create();

                byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

                StringBuilder returnValue = new StringBuilder();

                for (int i = 0; i < hashData.Length; i++)
                {
                    returnValue.Append(hashData[i].ToString());
                }
                // returner Hexadecimal streng
                return returnValue.ToString();

            }
            #endregion

            #region Validate MD5
            public static bool ValidateMD5Hash(string inputData, string hashData)
            {
                //MD5 hash ny string
                string getHashInputData = MD5Hash(inputData);
                // sammenlign de to strenge
                if (string.Compare(getHashInputData, hashData) == 0)
                    return true;
                else
                    return false;
            }
            #endregion

            #region SHA1 Hashing
            public static string SHA1Hash(string data)
            {
                // namespace System.Security.Cryptography
                SHA1 sha1 = SHA1.Create();

                byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

                StringBuilder returnValue = new StringBuilder();

                for (int i = 0; i < hashData.Length; i++)
                {
                    returnValue.Append(hashData[i].ToString());
                }
                // returnere hexadecimal streng
                return returnValue.ToString();
            }
            #endregion

            #region Validate SHA1
            public static bool ValidateSHA1Hash(string inputData, string hashData)
            {
                //SHA1 hash ny string
                string getHashInputData = SHA1Hash(inputData);
                // sammenlign de to strenge
                if (string.Compare(getHashInputData, hashData) == 0)
                    return true;
                else
                    return false;
            }
            #endregion
        }
    }

//public static class ApplicationHelpers
//{
//    public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
//    {
//        // optional condition: I didn't wanted it to show on home and account controller
//        //if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
//        //    helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
//        //{
//        //    return string.Empty;
//        //}

//        StringBuilder breadcrumb = new StringBuilder("<div class=\"breadcrumb\"><li>").Append(helper.ActionLink("Shop", "Index", "Shop").ToHtmlString()).Append("</li>");

//        //breadcrumb.Append("<li>");
//        //breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString().Titleize(),
//        //                                   "Index",
//        //                                   helper.ViewContext.RouteData.Values["controller"].ToString()));
//        //breadcrumb.Append("</li>");

//        if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
//        {
//            breadcrumb.Append("<li>");
//            //breadcrumb.Append(helper.ActionLink(name.Titleize(),
//            breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["action"].ToString().Titleize(),
//            helper.ViewContext.RouteData.Values["action"].ToString(),
//            helper.ViewContext.RouteData.Values["controller"].ToString()));
//            breadcrumb.Append("</li>");
//        }

//        return breadcrumb.Append("</div>").ToString();
//    }
//}
    public static class StringExtensions
    {
        // Breadcrumbs TitleHelper
        public static string Titleize(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text).ToSentenceCase();
        }
        // Breadcrumbs ToSentanceCaseHelper
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        public static string ToUrlName(this string extension)
        {
            string NewUrlName = extension.ToLower().Trim();

            NewUrlName = NewUrlName.Replace(",-", "");
            NewUrlName = NewUrlName.Replace(" ", "-");
            NewUrlName = NewUrlName.Replace("c#", "c-sharp");
            NewUrlName = NewUrlName.Replace("æ", "ae");
            NewUrlName = NewUrlName.Replace("ø", "oe");
            NewUrlName = NewUrlName.Replace("å", "aa");
            NewUrlName = NewUrlName.Replace("'", "");
            NewUrlName = NewUrlName.Replace("/", "");
            NewUrlName = NewUrlName.Replace("&", "");
            NewUrlName = NewUrlName.Replace(";", "");
            NewUrlName = NewUrlName.Replace(":", "");
            NewUrlName = NewUrlName.Replace(",", "");
            NewUrlName = NewUrlName.Replace(".", "");
            NewUrlName = NewUrlName.Replace("+", "");
            NewUrlName = NewUrlName.Replace("=", "");
            NewUrlName = NewUrlName.Replace("(", "");
            NewUrlName = NewUrlName.Replace(")", "");
            NewUrlName = NewUrlName.Replace("%", "");
            NewUrlName = NewUrlName.Replace("#", "");
            NewUrlName = NewUrlName.Replace("!", "");
            NewUrlName = NewUrlName.Replace("---", "-");
            NewUrlName = NewUrlName.Replace("--", "-");
            NewUrlName = NewUrlName.Replace("_", "-");

            return NewUrlName;

        }

        public static string Cut(this object text, int cut = 50)
        {
            if (text.ToString().Length <= cut)
                return text.ToString();
            else
                return text.ToString().Remove(cut - 3) + "...";
        }

        public static string CapFirst(this string text)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        public static decimal ToTax(this decimal price, int addFee = 0)
        {
            decimal NewPrice = price + addFee;
            return Math.Round(Decimal.Multiply(new decimal(0.2), NewPrice), 2);
        }

        public static string ToPrice(this object price)
        {
            return string.Format("{0},-", price.ToString().Replace(",00", ""));
        }

        public static int ToInt(this object data)
        {
            int value = 0;
            int.TryParse(data.ToString(), out value);

            return value;
        }
    }
}