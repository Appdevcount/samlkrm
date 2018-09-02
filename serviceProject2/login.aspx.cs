using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Xml.Linq;

namespace serviceProject2
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Test SAML XML Reading

            //String test = "<Response xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:saml='urn:oasis:names:tc:SAML:2.0:assertion' ID='_0d3d6141-94cf-4954-9a10-ca0caddc53b2' Version='2.0' IssueInstant='2018-08-06T15:27:38.7931151+03:00' Destination='http://localhost:52120/login.aspx' xmlns='urn:oasis:names:tc:SAML:2.0:protocol'><saml:Issuer>Idp_Mohammed_Tahtamouni</saml:Issuer><Status><StatusCode Value='urn:oasis:names:tc:SAML:2.0:status:Success' /></Status><saml:Assertion Version='2.0' ID='_5bb27f4b-f190-4bbc-9ae4-c4545fec353b' IssueInstant='2018-08-06T15:27:38.7941152+03:00'><saml:Issuer>Idp_Mohammed_Tahtamouni</saml:Issuer><Signature xmlns='http://www.w3.org/2000/09/xmldsig#'><SignedInfo><CanonicalizationMethod Algorithm='http://www.w3.org/2001/10/xml-exc-c14n#' /><SignatureMethod Algorithm='http://www.w3.org/2001/04/xmldsig-more#rsa-sha256' /><Reference URI='#_5bb27f4b-f190-4bbc-9ae4-c4545fec353b'><Transforms><Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' /><Transform Algorithm='http://www.w3.org/2001/10/xml-exc-c14n#' /></Transforms><DigestMethod Algorithm='http://www.w3.org/2001/04/xmlenc#sha256' /><DigestValue>e5DxYO6UKAbt21WDryNmvYdFbxd7HmXUryOnsKlFUso=</DigestValue></Reference></SignedInfo><SignatureValue>SVUqe9QFeVwtpgYvm9OMoAevS2n+pgCNtKD1mjGhyFawWc3kGrvAVAorbuKJwnr/fS0zSxeUkE8F/DrxVTd8xWJOjFn8KriqdFlMqpLxnLbEAabuIBjUf+Rss6RKOazlpm5jCzhrhr4k7hGZExL+bAWYzbt7ff+GrHAcP4dt2z8=</SignatureValue><KeyInfo><KeyValue><RSAKeyValue><Modulus>qfLbknPGfeOeZos/SbqGhR9CIPFt+0VURDZPi6iODvyDlFI3mHULCs0oLj/lA21bnjF6+9onFJ7kfpYms1djE4iEWOG3FbghtyurfGDpbtkQiI0jWadVfwnK69lyz8SsrFiZlQ5thcmaIKJTPw+J/zQANSZ0Rn3Ktb4i2ah6u+8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue></KeyInfo></Signature><saml:Subject><saml:NameID Format='urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified'>Authentication</saml:NameID><saml:SubjectConfirmation Method='urn:oasis:names:tc:SAML:2.0:cm:bearer'><saml:SubjectConfirmationData Recipient='http://localhost:52120/login.aspx' NotOnOrAfter='2018-08-06T15:32:38.7941152+03:00' /></saml:SubjectConfirmation></saml:Subject><saml:Conditions NotBefore='2018-08-06T15:27:38.7941152+03:00' NotOnOrAfter='2018-08-06T15:32:38.7941152+03:00'><saml:AudienceRestriction><saml:Audience>http://localhost:52120/login.aspx</saml:Audience></saml:AudienceRestriction></saml:Conditions><saml:AuthnStatement AuthnInstant='2018-08-06T15:27:38.7941152+03:00' SessionIndex='_5bb27f4b-f190-4bbc-9ae4-c4545fec353b'><saml:AuthnContext><saml:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:unspecified</saml:AuthnContextClassRef></saml:AuthnContext></saml:AuthnStatement><saml:AttributeStatement /></saml:Assertion></Response>";

            //XmlDocument doc = new XmlDocument();
            //doc.PreserveWhitespace = true;
            //doc.XmlResolver = null;
            //doc.LoadXml(test);

            //XmlNodeList xmlIssuer = doc.GetElementsByTagName("saml:Issuer");
            //XmlNodeList xmlResponse = doc.GetElementsByTagName("Response");
            //XmlNodeList xmlStatus = doc.GetElementsByTagName("StatusCode");
            //XmlNodeList xmlAssertion = doc.GetElementsByTagName("saml:Assertion");
            //XmlNodeList xmlSamlConditions = doc.GetElementsByTagName("saml:Conditions");


            //for (int i = 0; i < xmlResponse.Count; i++)
            //{
            //    String ResponseID = xmlResponse[i].Attributes["ID"].Value; // EX. _0d3d6141-94cf-4954-9a10-ca0caddc53b2
            //}

            //for (int i = 0; i <= xmlIssuer.Count - 1; i++)
            //{
            //    String IDP = xmlIssuer[i].ChildNodes.Item(0).InnerText.Trim(); // Idp_Mohammed_Tahtamouni
            //}

            //for (int i = 0; i <= xmlStatus.Count - 1; i++)
            //{
            //    String Status = xmlStatus[i].Attributes[0].Value.ToString(); // urn:oasis:names:tc:SAML:2.0:status:Success
            //}

            //for (int i = 0; i < xmlAssertion.Count; i++)
            //{
            //    String AssertionID = xmlAssertion[i].Attributes["ID"].Value; // EX. _5bb27f4b-f190-4bbc-9ae4-c4545fec353b
            //}


            //for (int i = 0; i < xmlSamlConditions.Count; i++)
            //{
            //    String SamlStartDateTime = xmlSamlConditions[i].Attributes["NotBefore"].Value; // EX.  2018-08-06T15:27:38.7941152+03:00
            //    String SamlEndDateTime = xmlSamlConditions[i].Attributes["NotOnOrAfter"].Value; // EX. 2018-08-06T15:32:38.7941152+03:00

            //    DateTime SamlStartDate = Convert.ToDateTime(SamlStartDateTime);
            //    DateTime SamlEndDate = Convert.ToDateTime(SamlEndDateTime);

            //    DateTime currentDateTime = DateTime.Now;

            //    int sessionValid = DateTime.Compare(SamlEndDate, currentDateTime); // Not 1: means ==> NOT Valid

            //    }

            #endregion Test SAML XML Reading

            #region Test Domain Settings
            // String userNameWithDomain = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //LOGISTICS\AFawzi

            //String userName = "al.mahmoud"; // Environment.UserName; Afawzi     
            // String Password = "Sharkas83@gmail";

            //       String userName = Environment.UserName; //Afawzi     
            //       String Password = "Abd@98995673";

            //       PrincipalContext principalContext =
            //new PrincipalContext(ContextType.Domain, "10.10.65.11"); // KGACHQ 10.10.65.11          logistics.intra 10.200.65.55

            //       bool userValid = principalContext.ValidateCredentials(userName, Password);
            #endregion Test Domain Settings


            if (!IsPostBack)
            {


                #region Test Response Values
                //string[] keys = Request.Form.AllKeys;
                //for (int i = 0; i < keys.Length; i++)
                //{
                //    Response.Write(keys[i] + ": " + Request.Form[keys[i]] + "<br>");
                //}
                #endregion  Test Response Values

                #region Variables 

                String SAMLAssertion = String.Empty;
                String SAML = String.Empty;
                String ResponseID = String.Empty;
                String IDP = String.Empty;
                String Status = String.Empty;
                String AssertionID = String.Empty;
                String SamlStartDateTime = String.Empty;
                String SamlEndDateTime = String.Empty;
                int AssertionValid = -1;

                XmlDocument xmlDoc;

                #endregion Variables


                if (Session["sessionValid"] != null)
                {
                    String IsValid = Session["sessionValid"].ToString();

                    if (IsValid == "1")
                    {
                        Response.Redirect("authenticatedPage.aspx");
                    }

                }
                else
                {
                    #region Read SAML Response
                    if (Request.Form["SAMLAssertion"] != null)
                    {
                        SAMLAssertion = Request.Form["SAMLAssertion"].ToString();

                        ASCIIEncoding enc = new ASCIIEncoding();
                        SAML = enc.GetString(Convert.FromBase64String(SAMLAssertion));

                        xmlDoc = new XmlDocument();
                        xmlDoc.PreserveWhitespace = true;
                        xmlDoc.XmlResolver = null;
                        xmlDoc.LoadXml(SAML);


                        XmlNodeList xmlIssuer = xmlDoc.GetElementsByTagName("saml:Issuer");
                        XmlNodeList xmlResponse = xmlDoc.GetElementsByTagName("Response");
                        XmlNodeList xmlStatus = xmlDoc.GetElementsByTagName("StatusCode");
                        XmlNodeList xmlAssertion = xmlDoc.GetElementsByTagName("saml:Assertion");
                        XmlNodeList xmlSamlConditions = xmlDoc.GetElementsByTagName("saml:Conditions");


                        for (int i = 0; i < xmlResponse.Count; i++)
                        {
                            ResponseID = xmlResponse[i].Attributes["ID"].Value; // EX. _0d3d6141-94cf-4954-9a10-ca0caddc53b2
                        }

                        for (int i = 0; i <= xmlIssuer.Count - 1; i++)
                        {
                            IDP = xmlIssuer[i].ChildNodes.Item(0).InnerText.Trim(); // Idp_Mohammed_Tahtamouni
                        }

                        for (int i = 0; i <= xmlStatus.Count - 1; i++)
                        {
                            Status = xmlStatus[i].Attributes[0].Value.ToString(); // urn:oasis:names:tc:SAML:2.0:status:Success
                        }

                        for (int i = 0; i < xmlAssertion.Count; i++)
                        {
                            AssertionID = xmlAssertion[i].Attributes["ID"].Value; // EX. _5bb27f4b-f190-4bbc-9ae4-c4545fec353b
                        }


                        for (int i = 0; i < xmlSamlConditions.Count; i++)
                        {
                            SamlStartDateTime = xmlSamlConditions[i].Attributes["NotBefore"].Value; // EX.  2018-08-06T15:27:38.7941152+03:00
                            SamlEndDateTime = xmlSamlConditions[i].Attributes["NotOnOrAfter"].Value; // EX. 2018-08-06T15:32:38.7941152+03:00

                            DateTime SamlStartDate = Convert.ToDateTime(SamlStartDateTime);
                            DateTime SamlEndDate = Convert.ToDateTime(SamlEndDateTime);

                            DateTime currentDateTime = DateTime.Now;

                            AssertionValid = DateTime.Compare(SamlEndDate, currentDateTime); // Not 1: means ==> NOT Valid

                            Session["sessionValid"] = AssertionValid;
                            Session["AssertionID"] = AssertionID;
                            Session["ResponseID"] = ResponseID;

                            if (AssertionValid == 1)
                            {
                                Response.Redirect("authenticatedPage.aspx");
                            }
                        }

                    }
                    #endregion Read SAML Response
                }
            }
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            #region POST Request
            //String userNameWithDomain = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ////LOGISTICS\AFawzi

            //if (!String.IsNullOrEmpty(userNameWithDomain) && userNameWithDomain.Contains(@"\"))
            //{
            //    userNameWithDomain = userNameWithDomain.Replace(@"\", "_");

            //    String URI = WebConfigurationManager.AppSettings["IdentityProvider"].ToString();

            //    String[] array = userNameWithDomain.Split('_');

            //    String returnURL = HttpContext.Current.Request.Url.AbsoluteUri;


            //    String myParameters = "userName=" + array[1] + "&domain=" + array[0] + "&returnURL=" + returnURL;

            //    using (WebClient wc = new WebClient())
            //    {
            //        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //        String HtmlResult = wc.UploadString(URI, myParameters);
            //    }
            //}
            #endregion POST Request

            #region REDIRECT Request
            String userNameWithDomain = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //LOGISTICS\AFawzi

            if (!String.IsNullOrEmpty(userNameWithDomain) && userNameWithDomain.Contains(@"\"))
            {
                userNameWithDomain = userNameWithDomain.Replace(@"\", "_");

                String URI = WebConfigurationManager.AppSettings["IdentityProvider"].ToString();

                String[] array = userNameWithDomain.Split('_');

                String returnURL = HttpContext.Current.Request.Url.AbsoluteUri;
                String RequestType = "Login";

                //String myParameters = "userName=" + array[1] + "&domain=" + array[0] +
                //    "&returnURL=" + returnURL + "&RequestType=" + RequestType;
                //Response.Redirect(URI);



                Response.Clear();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", URI);
                sb.AppendFormat("<input type='hidden' name='userName' value='{0}'>", array[1]);
                sb.AppendFormat("<input type='hidden' name='domain' value='{0}'>", array[0]);
                sb.AppendFormat("<input type='hidden' name='returnURL' value='{0}'>", returnURL);
                sb.AppendFormat("<input type='hidden' name='RequestType' value='{0}'>", RequestType);

                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");

                Response.Write(sb.ToString());

                Response.End();

            }
            #endregion REDIRECT Request
        }
    }
}