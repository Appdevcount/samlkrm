using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.Script.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Net;
using System.Text;

namespace IdentityProvider
{
    public partial class AuthenticationCheck : System.Web.UI.Page
    {
        DataBaseEntities db = new DataBaseEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region logging Request
                string[] keys = Request.Form.AllKeys;
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        _testData.WriteLine(keys[i] + ": " + Request.Form[keys[i]] + "<br>");
                    }
                }
                #endregion logging Request

                #region Check Request

                if (Request.Form["returnURL"] != null && Request.Form["RequestType"] != null)
                {
                    Session["returnURL"] = Request.Form["returnURL"].ToString();
                    Session["RequestType"] = Request.Form["RequestType"].ToString();

                    if (Session["RequestType"].ToString() == "Logout" && Request.Form["ResponseID"] !=null )
                    {
                        String ResponseID = Request.Form["ResponseID"].ToString();

                        var userToLogout = (from users in db.Saml
                                            where users.ResponseID == ResponseID
                                               && users.IsActive == true
                                               && users.logoutDate == null
                                            select users).FirstOrDefault();

                        userToLogout.logoutDate = DateTime.Now;
                        userToLogout.IsActive = false;

                        db.SaveChanges();

                        String returnURL = Session["returnURL"].ToString();
                        Response.Redirect(returnURL);

                    }

                }
                else
                {
                    Response.Redirect("errorPage.aspx");
                }
                #endregion Check Request

            }
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            if (Request.Form["username"] != null && Request.Form["psw"] != null)
            {
                #region read User Inputs
                String userName = Request.Form["username"].ToString(); // "al.mahmoud"; // Environment.UserName;

                String Password = Request.Form["psw"].ToString();//"Sharkas83@gmail";

                String Selectedomain = selectedDomain.Value;//Request.Form["selectedDomain"].ToString();

                Boolean validUser = IsValidUser(userName, Password);

                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
                {
                    _testData.WriteLine(userName + "  " + Password + " " + validUser.ToString());
                }
                #endregion read User Inputs

                if (validUser)
                {
                    //Response.Write("Valid User !!!!");

                    var user = (from users in db.Saml
                                where users.username == userName
                                   && users.IsActive == true
                                   && users.logoutDate == null
                                select users).FirstOrDefault();

                    if (user == null)  // User Doesn't have Valid Authentication Session 
                    {



                        #region Generate SAML Response
                        //String certFile = @"C:\Users\afawzi\documents\visual studio 2017\Projects\saml\IdentityProvider\X509Certificate.cer";
                        String certFile = Server.MapPath("~/X509Certificate.cer");

                        String Issuer = "Idp_Mohammed_Tahtamouni";
                        int AssertionExpirationMinutes = 5;
                        String Audience = Session["returnURL"].ToString(); //"http://localhost:52120/login.aspx";//Session["returnURL"].ToString();  //"test";
                        String Subject = "Authentication";
                        String Recipient = Session["returnURL"].ToString();// "http://localhost:52120/login.aspx";//Session["returnURL"].ToString();//"http";
                        Dictionary<String, String> Attributes = new Dictionary<String, String>();
                        X509Certificate2 SigningCert = new X509Certificate2(certFile, "password");


                        String SAMLAssertion = SAML20Assertion.CreateSAML20Response(Issuer, AssertionExpirationMinutes,
                                Audience, Subject, Recipient, Attributes, SigningCert);

                        #endregion Generate SAML Response

                        #region Extract SAML Values
                        String SAML = String.Empty;
                        String ResponseID = String.Empty;
                        String IDP = String.Empty;
                        String Status = String.Empty;
                        String AssertionID = String.Empty;
                        String SamlStartDateTime = String.Empty;
                        String SamlEndDateTime = String.Empty;

                        XmlDocument xmlDoc;

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

                        }
                        #endregion Extract SAML Values


                        #region Database logging
                        Saml saml = new Saml();
                        saml.username = userName;
                        saml.domain = selectedDomain.Value.ToString();
                        saml.loginDate = DateTime.Now;
                        saml.IsActive = true;

                        saml.Issuer = IDP;
                        saml.Audience = Audience;
                        saml.Subject = Subject;
                        saml.Recipient = Recipient;
                        saml.ResponseID = ResponseID;
                        saml.Status = Status;
                        saml.AssertionID = AssertionID;
                        saml.SamlStartDate = SamlStartDateTime;
                        saml.SamlEndDate = SamlEndDateTime;

                        db.Saml.Add(saml);
                        db.SaveChanges();
                        #endregion Database logging

                        #region POST to Service Provider
                        try
                        {
                            Response.Clear();

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<html>");
                            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                            sb.AppendFormat("<form name='form' action='{0}' method='post'>", Recipient);
                            sb.AppendFormat("<input type='hidden' name='SAMLAssertion' value='{0}'>", SAMLAssertion);


                            sb.Append("</form>");
                            sb.Append("</body>");
                            sb.Append("</html>");

                            Response.Write(sb.ToString());

                            Response.End();

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }
                        #endregion POST to Service Provider

                    }

                }
                else
                {
                    // Show error Message
                }

            }
        }



        private Boolean IsValidUser(String userName, String Password)
        {
            PrincipalContext principalContext =
           new PrincipalContext(ContextType.Domain, "10.10.65.11"); // KGAC

            Boolean userValid = principalContext.ValidateCredentials(userName, Password);

            return userValid;

        }




    }
}