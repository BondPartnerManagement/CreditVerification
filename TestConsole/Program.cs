using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WealthGrowthCreditVerification.Controllers;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string assemblyPath =  Path.GetDirectoryName(path) + "\\Templates\\Compuscan_Xml_Input_Template.xml";

            //string idNumber = "8111105012089";
            //GetControlDigit(idNumber);

            //string year = idNumber.Substring(0, 2);
            //string month = idNumber.Substring(2, 2);
            //string day = idNumber.Substring(4, 2);

            int twoDigitYear = 16;
            int fourDigitYear = 0;
            int currentYear = int.Parse(DateTime.Today.Year.ToString().Substring(2, 2));
            int currentCentury = int.Parse(DateTime.Today.Year.ToString().Substring(0, 2));
            if (twoDigitYear > currentYear)
                fourDigitYear = int.Parse($"{currentCentury - 1}{twoDigitYear}");
            else
                fourDigitYear = int.Parse($"{currentCentury}{twoDigitYear}");

            //CompuscanController cc = new CompuscanController();

            string str = @"{\""id\"":\""zAD9UqPZJEpVAPvE5nPRpxrS4Lv4xdGw\"",\""webhook_id\"":\""dagJ9NcUNY\"",\""url\"":\""http:\/\/ 970ce9fa.ngrok.io\/ WealthGrowthCreditVerification\/ \"",\""is_test\"":true,\""status\"":200,\""sent_at\"":\""2017 - 05 - 09T10: 04:33.370206Z\"",\""request_headers\"":\""Content - Type: application\/ json\n\"",\""request_body\"":\""{\u0022event_id\u0022:\u0022hQJi65uTRz\u0022,\u0022event_type\u0022:\u0022form_response\u0022,\u0022form_response\u0022:{\u0022form_id\u0022:\u0022IoLHVD\u0022,\u0022token\u0022:\u00224969bac7b56e83a82ad060f0ae57faed\u0022,\u0022submitted_at\u0022:\u00222017 - 05 - 09T10: 04:33Z\u0022,\u0022hidden\u0022:{\u0022referralagent\u0022:\u0022hidden_value\u0022},\u0022definition\u0022:{\u0022id\u0022:\u0022IoLHVD\u0022,\u0022title\u0022:\u0022Prequalification\u0022,\u0022fields\u0022:[{\u0022id\u0022:\u0022hIx5\u0022,\u0022title\u0022:\u0022\\u003cstrong\\u003eFirst Name:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u0022iwEq\u0022,\u0022title\u0022:\u0022\\u003cstrong\\u003eSurname:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u0022WPrA\u0022,\u0022title\u0022:\u0022\\u003cstrong\\u003eMobile Number:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u0022eowk\u0022,\u0022title\u0022:\u0022\\u003cstrong\\u003eEmail:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022email\u0022},{\u0022id\u0022:\u002250430415\u0022,\u0022title\u0022:\u0022Address Line 1:\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002250430522\u0022,\u0022title\u0022:\u0022Address Line 2\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002250430597\u0022,\u0022title\u0022:\u0022Postal Code\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002250430221\u0022,\u0022title\u0022:\u0022Gender:\u0022,\u0022type\u0022:\u0022multiple_choice\u0022},{\u0022id\u0022:\u002250430276\u0022,\u0022title\u0022:\u0022Date Of Birth:\u0022,\u0022type\u0022:\u0022date\u0022},{\u0022id\u0022:\u0022WTCK\u0022,\u0022title\u0022:\u0022\\u003cstrong\\u003eID Number:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u002249625657\u0022,\u0022title\u0022:\u0022\\u003cstrong\\u003eApplication Type:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022multiple_choice\u0022},{\u0022id\u0022:\u002249835996\u0022,\u0022title\u0022:\u0022Secondary Applicant\\u003cbr \/\\u003e\\u003cstrong\\u003eName:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002249836172\u0022,\u0022title\u0022:\u0022Secondary Applicant\\u003cbr \/\\u003e\\u003cstrong\\u003eSurname:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002249626353\u0022,\u0022title\u0022:\u0022Secondary Applicant\\u003cbr \/\\u003e\\u003cstrong\\u003eMobile Number:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u002249626396\u0022,\u0022title\u0022:\u0022Secondary Applicant\\u003cbr \/\\u003e\\u003cstrong\\u003eEmail:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022email\u0022},{\u0022id\u0022:\u002250432145\u0022,\u0022title\u0022:\u0022Secondary Applicant Gender:\u0022,\u0022type\u0022:\u0022multiple_choice\u0022},{\u0022id\u0022:\u002250432222\u0022,\u0022title\u0022:\u0022Secondary Applicant Date Of Birth\u0022,\u0022type\u0022:\u0022date\u0022},{\u0022id\u0022:\u002249626408\u0022,\u0022title\u0022:\u0022Secondary Applicant\\u003cbr \/\\u003e\\u003cstrong\\u003eID Number:\\u003c\/strong\\u003e\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u002250433572\u0022,\u0022title\u0022:\u0022Same address as above:\u0022,\u0022type\u0022:\u0022yes_no\u0022},{\u0022id\u0022:\u002250433600\u0022,\u0022title\u0022:\u0022Secondary Applicant Address Line 1:\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002250433609\u0022,\u0022title\u0022:\u0022Secondary Applicant\u00a0Address Line 2\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002250433612\u0022,\u0022title\u0022:\u0022Secondary Applicant\u00a0Postal Code\u0022,\u0022type\u0022:\u0022short_text\u0022},{\u0022id\u0022:\u002249628373\u0022,\u0022title\u0022:\u0022Monthly Net Income:\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u002249628408\u0022,\u0022title\u0022:\u0022Monthly expenses and loan commitments:\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u002249826707\u0022,\u0022title\u0022:\u0022MONTHLY HOUSEHOLD DISPOSABLE INCOME\u0022,\u0022type\u0022:\u0022number\u0022},{\u0022id\u0022:\u002249626483\u0022,\u0022title\u0022:\u0022\u201cI give Bond Partner consent to perform a credit bureau inquiry on my behalf. The resultant credit score and report will be emailed to me and shared with my estate agent.\u201d\u0022,\u0022type\u0022:\u0022yes_no\u0022},{\u0022id\u0022:\u002249626512\u0022,\u0022title\u0022:\u0022\\\u0022I confirm that the information provided is correct.\\\u0022\u0022,\u0022type\u0022:\u0022yes_no\u0022}]},\u0022answers\u0022:[{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u0022hIx5\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u0022iwEq\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u0022WPrA\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022email\u0022,\u0022email\u0022:\u0022an_account@example.com\u0022,\u0022field\u0022:{\u0022id\u0022:\u0022eowk\u0022,\u0022type\u0022:\u0022email\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250430415\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250430522\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250430597\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022choice\u0022,\u0022choice\u0022:{\u0022label\u0022:\u0022Barcelona\u0022},\u0022field\u0022:{\u0022id\u0022:\u002250430221\u0022,\u0022type\u0022:\u0022multiple_choice\u0022}},{\u0022type\u0022:\u0022date\u0022,\u0022date\u0022:\u00222017-02-06\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250430276\u0022,\u0022type\u0022:\u0022date\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u0022WTCK\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022choice\u0022,\u0022choice\u0022:{\u0022label\u0022:\u0022Barcelona\u0022},\u0022field\u0022:{\u0022id\u0022:\u002249625657\u0022,\u0022type\u0022:\u0022multiple_choice\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002249835996\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002249836172\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u002249626353\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022email\u0022,\u0022email\u0022:\u0022an_account@example.com\u0022,\u0022field\u0022:{\u0022id\u0022:\u002249626396\u0022,\u0022type\u0022:\u0022email\u0022}},{\u0022type\u0022:\u0022choice\u0022,\u0022choice\u0022:{\u0022label\u0022:\u0022Barcelona\u0022},\u0022field\u0022:{\u0022id\u0022:\u002250432145\u0022,\u0022type\u0022:\u0022multiple_choice\u0022}},{\u0022type\u0022:\u0022date\u0022,\u0022date\u0022:\u00222017-02-06\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250432222\u0022,\u0022type\u0022:\u0022date\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u002249626408\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022boolean\u0022,\u0022boolean\u0022:true,\u0022field\u0022:{\u0022id\u0022:\u002250433572\u0022,\u0022type\u0022:\u0022yes_no\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250433600\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250433609\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022text\u0022,\u0022text\u0022:\u0022Lorem ipsum dolor\u0022,\u0022field\u0022:{\u0022id\u0022:\u002250433612\u0022,\u0022type\u0022:\u0022short_text\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u002249628373\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u002249628408\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022number\u0022,\u0022number\u0022:42,\u0022field\u0022:{\u0022id\u0022:\u002249826707\u0022,\u0022type\u0022:\u0022number\u0022}},{\u0022type\u0022:\u0022boolean\u0022,\u0022boolean\u0022:true,\u0022field\u0022:{\u0022id\u0022:\u002249626483\u0022,\u0022type\u0022:\u0022yes_no\u0022}},{\u0022type\u0022:\u0022boolean\u0022,\u0022boolean\u0022:true,\u0022field\u0022:{\u0022id\u0022:\u002249626512\u0022,\u0022type\u0022:\u0022yes_no\u0022}}]}}\n\"",\""response_headers\"":\""Content-Length: 3576\nCache-Control: private\nContent-Type: text\/html; charset=utf-8\nServer: Microsoft-IIS\/10.0\nX-Aspnetmvc-Version: 5.2\nX-Aspnet-Version: 4.0.30319\nX-Powered-By: ASP.NET\nDate: Tue, 09 May 2017 10:04:33 GMT\n\"",\""response_body\"":\""\ufeff\u003C!DOCTYPE html\u003E\r\n\u003Chtml\u003E\r\n\u003Chead\u003E\r\n    \u003Cmeta charset=\u0022utf-8\u0022 \/\u003E\r\n    \u003Cmeta name=\u0022viewport\u0022 content=\u0022width=device-width\u0022 \/\u003E\r\n    \u003Ctitle\u003EHome Page\u003C\/title\u003E\r\n    \u003Clink href=\u0022\/WealthGrowthCreditVerification\/Content\/bootstrap.css\u0022 rel=\u0022stylesheet\u0022\/\u003E\r\n\u003Clink href=\u0022\/WealthGrowthCreditVerification\/Content\/site.css\u0022 rel=\u0022stylesheet\u0022\/\u003E\r\n\r\n    \u003Cscript src=\u0022\/WealthGrowthCreditVerification\/Scripts\/modernizr-2.6.2.js\u0022\u003E\u003C\/script\u003E\r\n\r\n\u003C\/head\u003E\r\n\u003Cbody\u003E\r\n    \u003Cdiv class=\u0022navbar navbar-inverse navbar-fixed-top\u0022\u003E\r\n        \u003Cdiv class=\u0022container\u0022\u003E\r\n            \u003Cdiv class=\u0022navbar-header\u0022\u003E\r\n                \u003Cbutton type=\u0022button\u0022 class=\u0022navbar-toggle\u0022 data-toggle=\u0022collapse\u0022 data-target=\u0022.navbar-collapse\u0022\u003E\r\n                    \u003Cspan class=\u0022icon-bar\u0022\u003E\u003C\/span\u003E\r\n                    \u003Cspan class=\u0022icon-bar\u0022\u003E\u003C\/span\u003E\r\n                    \u003Cspan class=\u0022icon-bar\u0022\u003E\u003C\/span\u003E\r\n                \u003C\/button\u003E\r\n                \u003Ca class=\u0022navbar-brand\u0022 href=\u0022\/WealthGrowthCreditVerification\/\u0022\u003EApplication name\u003C\/a\u003E\r\n            \u003C\/div\u003E\r\n            \u003Cdiv class=\u0022navbar-collapse collapse\u0022\u003E\r\n                \u003Cul class=\u0022nav navbar-nav\u0022\u003E\r\n                    \u003Cli\u003E\u003Ca href=\u0022\/WealthGrowthCreditVerification\/\u0022\u003EHome\u003C\/a\u003E\u003C\/li\u003E\r\n                    \u003Cli\u003E\u003Ca href=\u0022\/WealthGrowthCreditVerification\/Help\u0022\u003EAPI\u003C\/a\u003E\u003C\/li\u003E\r\n                \u003C\/ul\u003E\r\n            \u003C\/div\u003E\r\n        \u003C\/div\u003E\r\n    \u003C\/div\u003E\r\n    \u003Cdiv class=\u0022container body-content\u0022\u003E\r\n        \r\n\u003Cdiv class=\u0022jumbotron\u0022\u003E\r\n    \u003Ch1\u003EASP.NET\u003C\/h1\u003E\r\n    \u003Cp class=\u0022lead\u0022\u003EASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.\u003C\/p\u003E\r\n    \u003Cp\u003E\u003Ca href=\u0022http:\/\/asp.net\u0022 class=\u0022btn btn-primary btn-lg\u0022\u003ELearn more \u0026raquo;\u003C\/a\u003E\u003C\/p\u003E\r\n\u003C\/div\u003E\r\n\u003Cdiv class=\u0022row\u0022\u003E\r\n    \u003Cdiv class=\u0022col-md-4\u0022\u003E\r\n        \u003Ch2\u003EGetting started\u003C\/h2\u003E\r\n        \u003Cp\u003EASP.NET Web API is a framework that makes it easy to build HTTP services that reach\r\n        a broad range of clients, including browsers and mobile devices. ASP.NET Web API\r\n        is an ideal platform for building RESTful applications on the .NET Framework.\u003C\/p\u003E\r\n        \u003Cp\u003E\u003Ca class=\u0022btn btn-default\u0022 href=\u0022http:\/\/go.microsoft.com\/fwlink\/?LinkId=301870\u0022\u003ELearn more \u0026raquo;\u003C\/a\u003E\u003C\/p\u003E\r\n    \u003C\/div\u003E\r\n    \u003Cdiv class=\u0022col-md-4\u0022\u003E\r\n        \u003Ch2\u003EGet more libraries\u003C\/h2\u003E\r\n        \u003Cp\u003ENuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.\u003C\/p\u003E\r\n        \u003Cp\u003E\u003Ca class=\u0022btn btn-default\u0022 href=\u0022http:\/\/go.microsoft.com\/fwlink\/?LinkId=301871\u0022\u003ELearn more \u0026raquo;\u003C\/a\u003E\u003C\/p\u003E\r\n    \u003C\/div\u003E\r\n    \u003Cdiv class=\u0022col-md-4\u0022\u003E\r\n        \u003Ch2\u003EWeb Hosting\u003C\/h2\u003E\r\n        \u003Cp\u003EYou can easily find a web hosting company that offers the right mix of features and price for your applications.\u003C\/p\u003E\r\n        \u003Cp\u003E\u003Ca class=\u0022btn btn-default\u0022 href=\u0022http:\/\/go.microsoft.com\/fwlink\/?LinkId=301872\u0022\u003ELearn more \u0026raquo;\u003C\/a\u003E\u003C\/p\u003E\r\n    \u003C\/div\u003E\r\n\u003C\/div\u003E\r\n\r\n        \u003Chr \/\u003E\r\n        \u003Cfooter\u003E\r\n            \u003Cp\u003E\u0026copy; 2017 - My ASP.NET Application\u003C\/p\u003E\r\n        \u003C\/footer\u003E\r\n    \u003C\/div\u003E\r\n\r\n    \u003Cscript src=\u0022\/WealthGrowthCreditVerification\/Scripts\/jquery-1.10.2.js\u0022\u003E\u003C\/script\u003E\r\n\r\n    \u003Cscript src=\u0022\/WealthGrowthCreditVerification\/Scripts\/bootstrap.js\u0022\u003E\u003C\/script\u003E\r\n\u003Cscript src=\u0022\/WealthGrowthCreditVerification\/Scripts\/respond.js\u0022\u003E\u003C\/script\u003E\r\n\r\n    \r\n\r\n\u003C!-- Visual Studio Browser Link --\u003E\r\n\u003Cscript type=\u0022application\/json\u0022 id=\u0022__browserLink_initializationData\u0022\u003E\r\n    {\u0022appName\u0022:\u0022Unknown\u0022,\u0022requestId\u0022:\u0022d486908a869d40a3a5437d4cb42ba6e9\u0022}\r\n\u003C\/script\u003E\r\n\u003Cscript type=\u0022text\/javascript\u0022 src=\u0022http:\/\/localhost:64260\/0119895ebe5f4fa9bac9428e339be9a7\/browserLink\u0022 async=\u0022async\u0022\u003E\u003C\/script\u003E\r\n\u003C!-- End Browser Link --\u003E\r\n\r\n\u003C\/body\u003E\r\n\u003C\/html\u003E\r\n\"",\""received_at\"":\""2017-05-09T10:04:33.801097Z\""}";

            string url = "http://localhost:56214/api/Compuscan";//?jsonMessage={" + str + "}";
            // Create a request using a URL that can receive a post. 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = str;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "text/xml; charset=utf-8";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            Console.ReadLine();
        }





        // This method assumes that the 13-digit id number has 
        // valid digits in position 0 through 12.  
        // Stored in a property 'ParseIdString'.  
        // Returns: the valid digit between 0 and 9, or  
        // -1 if the method fails.
        private static int GetControlDigit(string idNumberString)
        {
            int d = -1;
            try
            {
                int a = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(idNumberString[2 * i].ToString());
                }
                int b = 0;
                for (int i = 0; i < 6; i++)
                {
                    b = b * 10 + int.Parse(idNumberString[2 * i + 1].ToString());
                }
                b *= 2;
                int c = 0;
                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);
                c += a;
                d = 10 - (c % 10);
                if (d == 10) d = 0;
            }
            catch {/*ignore*/}
            return d;
        }
    }
}
