<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/userMasterpage.Master" AutoEventWireup="true" CodeBehind="guestLists.aspx.cs" Inherits="CreatedQR.guest.guestLists" %>

<%@ Register Src="~/ctrl/ctrlModelGuest.ascx" TagPrefix="uc1" TagName="ctrlModelGuest" %>
<%@ Register Src="~/ctrl/ctrlImportExcel.ascx" TagPrefix="uc1" TagName="ctrlImportExcel" %>



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
                        <h2>Danh sách Khách mời</h2>
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
                                <div class="col-md-1 col-sm-9 col-xs-6 btn-search">
                                    <asp:LinkButton ID="lbtSearch" runat="server" CssClass="btn btn-orange" OnClick="lbtSearch_Click" ToolTip="Tìm kiếm">Tìm kiếm</asp:LinkButton>
                                </div>
                                <div class="col-md-1 col-sm-9 col-xs-6 btn-add">
                                    <asp:HyperLink ID="hplInsert" runat="server" CssClass="btn btn-green" data-toggle="modal" ToolTip="Thêm mới" data-target="#ContentPlaceHolder1_ctrlModelGuest_myModelGuest">Thêm mới</asp:HyperLink>
                                </div>
                                <div class="col-md-1 col-sm-9 col-xs-6 btn-search">
                                    <asp:HyperLink ID="hplImport" CssClass="btn btn-success" runat="server" data-toggle="modal" ToolTip="Import Excel" data-target="#ContentPlaceHolder1_ctrlImportExcel_myModelImportExcel">Import Excel</asp:HyperLink>
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
                                                <th class="sorting_desc" tabindex="0" aria-controls="datatableComment" rowspan="1" colspan="1" style="width: 50px;"
                                                    aria-sort="descending">STT
                                                </th>
                                                <th style="width: 160px;">Họ và tên</th>
                                                <th style="width: 50px;">Số CMND</th>
                                                <th style="width: 50px;">Số điện thoại</th>
                                                <th style="width: 160px;">Đơn vị\Cơ Quan</th>
                                                <th style="width: 100px;">Email</th>
                                                <th class="sorting" tabindex="0" aria-controls="datatable" rowspan="1" colspan="1"
                                                    style="width: 100px;" aria-label="Start date: activate to sort column ascending">Action
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptGuest" runat="server" OnItemDataBound="rptGuest_ItemDataBound" OnItemCommand="rptGuest_ItemCommand" OnDataBinding="rptGuest_DataBinding">
                                                <ItemTemplate>
                                                    <tr role="row" class="odd" runat="server">
                                                        <td>
                                                            <asp:HiddenField ID="hdUserID" runat="server" Value="0"></asp:HiddenField>
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
                                                        <td>
                                                            <asp:LinkButton ID="lbtEdit" runat="server" ToolTip="Chỉnh sử khách mời" CommandName="updateUser">
                                                                  <img src="../images/Edit.gif" alt="Chỉnh sửa khách mời" />
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="lbtDelete" runat="server" ToolTip="Xóa khách mời" CommandName="deleteUser" OnClientClick="return confirm('Bạn có thật sự muốn xoá khách mời này không?');">
                                                                  <img src="../images/Delete.gif" alt="Xóa khách mời" />
                                                            </asp:LinkButton>
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
    <uc1:ctrlModelGuest runat="server" ID="ctrlModelGuest" />
    <uc1:ctrlImportExcel runat="server" ID="ctrlImportExcel" />
</asp:Content>
