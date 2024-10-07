<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/userMasterpage.Master" AutoEventWireup="true" CodeBehind="eventLists.aspx.cs" Inherits="CreatedQR.events.eventLists" %>

<%@ Register Src="~/ctrl/ctrlModelEvent.ascx" TagPrefix="uc1" TagName="ctrlModelEvent" %>



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
                      <h2>Danh sách Sự kiện</h2>
                      <ul class="nav navbar-right">
                      </ul>
                      <div class="clearfix">
                      </div>
                  </div>
                  <div class="x_content" runat="server">
                      <div class="form-horizontal form-label-left">
                          <div class="form-group" runat="server">
                              <label class="control-label col-md-1 col-sm-2 col-xs-12">
                                  Từ khóa
                              </label>
                              <div class="col-md-7 col-sm-6 col-xs-12">
                                  <input id="txtKey" runat="server" class="form-control" placeholder="Tên sự kiện" />
                              </div>
                              <div class="col-md-2 col-sm-2 col-xs-6 btn-search">
                                  <asp:LinkButton ID="lbtSearch" runat="server" CssClass="btn btn-orange" OnClick="lbtSearch_Click" ToolTip="Tìm kiếm">Tìm kiếm</asp:LinkButton>
                              </div>
                              <div class="col-md-2 col-sm-2 col-xs-6 btn-add">
                                  <asp:HyperLink ID="hplInsert" runat="server" CssClass="btn btn-green" data-toggle="modal" ToolTip="Thêm mới" data-target="#ContentPlaceHolder1_ctrlModelEvent_myModelEvent">Thêm mới</asp:HyperLink>
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
                                              <th class="sorting_desc" tabindex="0" aria-controls="datatableComment" rowspan="1" colspan="1" style="width: 100px;"
                                                  aria-sort="descending">Số thứ tự
                                              </th>
                                              <th style="width: 160px;">Tên sự kiện</th>
                                              <th style="width: 50px;">Mã sự kiện</th>
                                              <th style="width: 100px;">Người tạo</th>
                                              <th style="width: 50px;">Ngày tạo</th>
                                              <th class="sorting" tabindex="0" aria-controls="datatable" rowspan="1" colspan="1"
                                                  style="width: 100px;" aria-label="Start date: activate to sort column ascending">Action
                                              </th>
                                          </tr>
                                      </thead>
                                      <tbody>
                                          <asp:Repeater ID="rptEvent" runat="server" OnItemDataBound="rptEvent_ItemDataBound" OnItemCommand="rptEvent_ItemCommand" OnDataBinding="rptEvent_ItemBinding">
                                              <ItemTemplate>
                                                  <tr role="row" class="odd" runat="server">
                                                      <td>
                                                          <asp:HiddenField ID="hdEventID" runat="server" Value="0"></asp:HiddenField>
                                                          <asp:Literal ID="ltrEventID" runat="server"></asp:Literal>
                                                      </td>
                                                      <td>
                                                          <asp:Literal ID="ltrEventName" runat="server"></asp:Literal>
                                                      </td>
                                                      <td>
                                                          <asp:Literal ID="ltrEventCode" runat="server"></asp:Literal>
                                                      </td>

                                                      <td>
                                                          <asp:Literal ID="ltrUserCreated" runat="server"></asp:Literal>
                                                      </td>
                                                      <td>
                                                          <asp:Literal ID="ltrDateCreated" runat="server"></asp:Literal>
                                                      </td>
                                                      <td>
                                                          <asp:LinkButton ID="lbtEdit" runat="server" ToolTip="Chỉnh sửa Sự kiện" CommandName="updateEvent">
                                                              <img src="../images/Edit.gif" alt="Chỉnh sửa Sự kiện" />
                                                          </asp:LinkButton>

                                                          <asp:LinkButton ID="lbtDelete" runat="server" ToolTip="Xóa Sự kiện" CommandName="deleteEvent" OnClientClick="return confirm('Bạn có thật sự muốn xóa Sự kiện này không?');">
                                                               <img src="../images/Delete.gif" alt="Xóa Sự kiện" />
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
    <uc1:ctrlModelEvent runat="server" ID="ctrlModelEvent" />
</asp:Content>
