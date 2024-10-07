<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMenuLeftTop.ascx.cs" Inherits="CreatedQR.ctrl.ctrlMenuLeftTop" %>
<div class="col-md-3 left_col">
    <div class="left_col scroll-view">
        <div class="navbar nav_title" style="border: 0;">
            <a href="../default.aspx" class="site_title" title="CreateQR">
                <img src="../images/logo.png" alt="CreateQR" width="35px" />
                <span>&nbsp;</span>
            </a>
        </div>
        <div class="clearfix">
        </div>

        <br />
        <!-- sidebar menu -->
        <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
            <div class="menu_section">
                <%--<h3>General</h3>--%>
                <ul class="nav side-menu">
                    <li runat="server" id="htmlRollCall"><a href="../guest/guestIsCheckLists.aspx"><i class="fa fa-solid fa-calendar-check"></i>Khách mời tham dự</a></li>
                    <li runat="server" id="liHistoryRollCall"><a href="../guest/guestLists.aspx"><i class="fa fa-solid fa-clock-rotate-left"></i>Danh sách khách mời</a></li>
                    <li runat="server" id="htmlReportStudent"><a href="../events/eventLists.aspx"><i class="fa fa-solid fa-calculator"></i>Danh sách sự kiện</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
<!-- top navigation -->
<div class="top_nav">
    <div class="nav_menu">
        <nav>
            <div class="nav toggle">
                <a id="menu_toggle"><i class="fa fa-bars"></i></a>
            </div>
            <ul class="nav navbar-nav navbar-right">
                <li class="">
                    <a href="javascript:;" class="user-profile dropdown-toggle" data-toggle="dropdown" aria-expanded="false" style="color: white">
                        <%--<img src="images/img.jpg" alt="">--%>
                        <asp:Image ID="imgUser1" runat="server" />
                        <%--Administators--%>
                        <asp:Literal ID="ltrUserCode1" runat="server"></asp:Literal>
                        <span class="fa fa-angle-down"></span>
                    </a>
                    <ul class="dropdown-menu dropdown-usermenu pull-right customertop100">
                        <li><a href="../logins.aspx"><i class="fa fa-sign-out pull-right"></i>Log Out</a></li>
                    </ul>
                </li>
            </ul>
        </nav>
    </div>
</div>

