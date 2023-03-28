using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Net;
using System.Xml;

namespace WePayOffer.Portal.Customs
{
    public class SoupUI
    {

        public static void CallWebService()
        {
            var _url = "https://10.20.129.109:17800/services/SubmitProductOrder";
            var _action = "https://10.20.129.109:17800/services/SubmitProductOrder?op=sub";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

            }
            using (var stream = new StreamWriter(@"E:\MyFolder\webservice.txt"))
            {
                stream.WriteLine(soapResult);
            }
    }

    private static HttpWebRequest CreateWebRequest(string url, string action)
    {
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Headers.Add("SOAPAction", action);
        webRequest.ContentType = "text/xml;charset=\"utf-8\"";
        webRequest.Accept = "text/xml";
        webRequest.Method = "POST";
        return webRequest;
    }

    private static XmlDocument CreateSoapEnvelope()
    {
            //hash
    //        soapEnvelopeDocument.LoadXml(
    //         @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
    //           xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" 
    //           xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
    //    <SOAP-ENV:Body>
    //        <HelloWorld xmlns=""http://tempuri.org/"" 
    //            SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
    //            <int1 xsi:type=""xsd:integer"">12</int1>
    //            <int2 xsi:type=""xsd:integer"">32</int2>
    //        </HelloWorld>
    //    </SOAP-ENV:Body>
    //</SOAP-ENV:Envelope>");
    //        return soapEnvelopeDocument;
    //    } ")

        XmlDocument soapEnvelopeDocument = new XmlDocument();
        soapEnvelopeDocument.LoadXml(
        @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:sub=""http://www.te.eg/esb/messages/submitproductorder"">
       <soapenv:Header/>
       <soapenv:Body>
          <sub:SubmitProductOrderRq>
             <sub:RequestHeader>
               <FunctionId>10002004</FunctionId>
                <RequestId>BI_109224153</RequestId>
                <Timestamp>2022-09-11T22:00:27.704</Timestamp>
                <Security>
                   <UsernameToken>
                      <Username>bsi_engine01</Username>
                      <Password>Admin123!</Password>
                   </UsernameToken>
                </Security>
                <Caller>
                   <CallerId>777</CallerId>
                </Caller>
             </sub:RequestHeader>
             <sub:RequestBody>
                <sub:OfferOrder>
                   <ProductOffer>
                      <ProductOfferId>-1</ProductOfferId>
                      <isPrimaryOffer>Y</isPrimaryOffer>
                   </ProductOffer>
                   <ProductOffer>
                      <ProductOfferId>460476114</ProductOfferId>
                      <isPrimaryOffer>N</isPrimaryOffer>
                   </ProductOffer>
                   <Subscriber>
                      <SubscriberNo>1555901291</SubscriberNo>
                      <LineType></LineType>

                   </Subscriber>
                </sub:OfferOrder>
                <sub:OldOfferOrder>
                   <ProductOffer>
                      <ProductOfferId>-1</ProductOfferId>
                      <isPrimaryOffer>Y</isPrimaryOffer>
                   </ProductOffer>
                   <ProductOffer>
                      <ProductOfferId>-1</ProductOfferId>
                      <isPrimaryOffer>N</isPrimaryOffer>
                   </ProductOffer>
                </sub:OldOfferOrder>
             </sub:RequestBody>
          </sub:SubmitProductOrderRq>
       </soapenv:Body>
    </soapenv:Envelope>");
        return soapEnvelopeDocument;
    }


    private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
    {
        using (Stream stream = webRequest.GetRequestStream())
        {
            soapEnvelopeXml.Save(stream);
        }
    }




}

}
