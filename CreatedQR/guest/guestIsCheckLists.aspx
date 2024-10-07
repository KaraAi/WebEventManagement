<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/userMasterpage.Master" AutoEventWireup="true" CodeBehind="guestIsCheckLists.aspx.cs" Inherits="CreatedQR.guest.guestIsCheckLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="clearfix">
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Danh Sách Tham Dự Sự Kiện</h2>
                        <ul class="nav navbar-right">
                        </ul>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="x_content" runat="server">
                        <div class="form-horizontal form-label-left">
                            <div class="form-group" runat="server">
                                <label class="control-label col-md-1 col-sm-3 col-xs-12">
                                    Từ khoá
                                </label>
                                <div class="col-md-2 col-sm-9 col-xs-12">
                                    <input id="txtKeySearch" runat="server" class="form-control" placeholder="Tên khách mời" />
                                </div>
                                <label class="control-label col-md-1 col-sm-3 col-xs-12">
                                    Sự kiện</label>
                                <div class="col-md-2 col-sm-9 col-xs-12">
                                    <asp:DropDownList ID="ddlEvent" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <label class="control-label col-md-1 col-sm-3 col-xs-12">
                                    Trạng thái</label>
                                <div class="col-md-2 col-sm-9 col-xs-12">
                                    <asp:DropDownList ID="ddlIsCheck" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="----- Chọn Trạng thái -----"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Chưa tham gia"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đã tham gia"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 col-sm-9 col-xs-6 btn-search">
                                    <asp:LinkButton ID="lbtSearch" runat="server" CssClass="btn btn-orange" OnClick="lbtSearch_Click" ToolTip="Tìm kiếm">Tìm kiếm</asp:LinkButton>
                                </div>
                                <div class="col-md-1 col-sm-9 col-xs-6 btn-search">
                                    <asp:LinkButton ID="lbtSend" CssClass="btn btn-success" runat="server" ToolTip="Send Mail" OnClick="lbtSendMail_Click">Gửi Mail</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="datatable_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="datatableUserList" class="table table-striped table-bordered dataTable no-footer"
                                        role="grid" aria-describedby="datatable_info">
                                        <thead>
                                            <tr role="row">
                                                <th style="width: 50px;" tabindex="0">
                                                      <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" />
                                                    Chọn
                                                </th>
                                                <th class="sorting_desc" tabindex="0" aria-controls="datatableComment" rowspan="1" colspan="1" style="width: 50px;"
                                                    aria-sort="descending">STT
                                                </th>
                                                <th style="width: 160px;">Họ và tên</th>
                                                <th style="width: 50px;">Số CMND</th>
                                                <th style="width: 50px;">Số điện thoại</th>
                                                <th style="width: 160px;">Đơn vị\Cơ Quan</th>
                                                <th style="width: 100px;">Email</th>
                                               
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptGuest" runat="server" OnItemDataBound="rptGuest_ItemDataBound" OnDataBinding="rptGuest_DataBinding">
                                                <ItemTemplate>
                                                    <tr role="row" class="odd" runat="server">
                                                        <td>
                                                            <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="true" /><%--OnCheckedChanged="chkCheck_CheckedChanged"--%>
                                                            <asp:HiddenField ID="hdUserID" runat="server" Value="0"></asp:HiddenField>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrSTT" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrCCCD" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrPhone" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrFacility" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
