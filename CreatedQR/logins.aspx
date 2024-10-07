<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logins.aspx.cs" Inherits="CreatedQR.logins" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CreatedQR</title>
 <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <!-- NProgress -->
    <link href="vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- Animate.css -->
    <link href="vendors/animate.css/animate.min.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <!--<link href="build/css/custom.min.css" rel="stylesheet">-->
    <link href="build/css/custom.css" rel="stylesheet" />

  

    <script>
        function button_click(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }
    </script>

</head>
<body style="background-color: white;">
    <form id="form1" runat="server">
        <div class="login_wrapper">
            <div class="animate form login_form">
                <div style="width: 100%; text-align: center; min-width: 400px;">
                    <a href="logins.aspx" title="CreateQR">
                        <img src="../images/Login.png" alt="" />
                    </a>
                </div>
                <section class="login_content" style="padding-top: 0px;">
                    <h1>Login Form</h1>
                    <div style="margin-top: 5%;">
                        <asp:TextBox ID="txtCode" runat="server" placeholder="Username" required="" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="margin-top: 5%;">
                        <asp:TextBox ID="txtPassWord" runat="server" placeholder="Password" required="" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div style="margin-top: 5%;">
                        <asp:LinkButton ID="lbtLogin" runat="server" CssClass="btn btn-default submit" ToolTip="Đăng nhập" OnClick="lbtLogin_Click">Đăng nhập</asp:LinkButton>
                        <a class="reset_pass" href="#">Lost your password?</a>
                    </div>

                    <div class="clearfix"></div>

                    <div class="separator">
                        <p class="change_link" style="display: none;">
                            New to site?
                 
                                <a href="#signup" class="to_register">Create Account </a>
                        </p>
                        <div class="clearfix"></div>
                        <br />
                        <div>
                            <p>©2023 All Rights Reserved. Privacy and Terms</p>
                        </div>
                    </div>
                </section>
            </div>


        </div>
    </form>
    <!-- jQuery -->
    <script src="vendors/jquery/dist/jquery.min.js"></script>
</body>
</html>