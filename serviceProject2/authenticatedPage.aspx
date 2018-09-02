<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authenticatedPage.aspx.cs" Inherits="serviceProject2.authenticatedPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        body {
            width: 100%;
            height: 100%;
            background: #48A9E6;
            font-family: sans-serif;
            font-weight: 300;
            margin: 0;
            padding: 0;
        }

        #title {
            text-align: center;
            font-size: 40px;
            margin-top: 40px;
            margin-bottom: -40px;
            position: relative;
            color: rgb(0, 0, 0);
        }



        .circles {
           

            text-align: center;
            position: relative;
            margin-top: 60px;
            
        }

            .circles p {
                font-size: 240px;
                color: rgb(0, 0, 0);
                padding-top: 60px;
                position: relative;
                z-index: 9;
                line-height: 100%;
            }


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
    </style>

</head>
<body>
    <section id="not-found">
        <div id="title">
            Welcome, You Are
            <h1 style="color: rgba(255, 0, 0, 0.52);">Authenticated !!!!</h1>

        </div>
        <form id="form1" runat="server">
            <div class="circles">
                <br />
                <asp:Button Text="Logout" ID="logoutBtn" OnClick="logoutBtn_Click" runat="server" CssClass="button" />
            </div>

        </form>


    </section>
</body>
</html>
