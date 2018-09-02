<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="serviceProvider1.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<style>
body {font-family: Arial, Helvetica, sans-serif;}
form {border: 3px solid #f1f1f1;}



.button {
    background-color: #4CAF50;
    color: white;
    padding: 14px 20px;
    /*margin: 8px 0;*/
    border: none;
    cursor: pointer;
    width: 18%;

     
}

.button:hover {
    opacity: 0.8;
}



.imgcontainer {
    text-align: center;
    margin: 24px 0 12px 0;
}

img.avatar {
    width: 10%;
    border-radius: 50%;
}

.container {
    padding: 16px;
    text-align:center;
}

span.psw {
    float: right;
    padding-top: 16px;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
    span.psw {
       display: block;
       float: none;
    }
    
}
</style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="imgcontainer">
    <img src="loginImg.png" alt="loginImg" class="avatar"/>
  </div>

  <div class="container">
    
      
       
   <%-- <button type="submit">Login</button>--%>
      <asp:Button Text="Login" id="loginBtn" OnClick="loginBtn_Click" runat="server" CssClass="button" />
    
  </div>

  <div class="container" style="background-color:#f1f1f1">
    
  </div>
    </form>
</body>
</html>
