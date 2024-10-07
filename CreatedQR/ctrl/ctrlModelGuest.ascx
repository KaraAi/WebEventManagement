<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlModelGuest.ascx.cs" Inherits="CreatedQR.ctrl.ctrlModelGuest" %>

<div class="modal fade" id="myModelGuest" role="dialog" runat="server">
    <div class="modal-dialog modalSection">
        <div class="modal-content">
            <div class="modal-header">
                <asp:LinkButton ID="lbtCloseTop" runat="server" OnClick="lbtCloseTop_Click" CssClass="close" data-dismiss="modal">&times;</asp:LinkButton>
                <h4 class="modal-title">Chi tiết Khách hàng</h4>
            </div>
            <div class="modal-body bodyUserEdit" style="overflow: scroll;">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="x_content">
                            <div class="" role="tabpanel" data-example-id="togglable-tabs">
                                <div id="myTabContent" class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade active in" id="tab_contentInfor" aria-labelledby="home-tab" runat="server">
                                        <div class="x_content">
                                            <div class="row">
                                                <div class="form-horizontal form-label-left">
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Sự kiện</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:DropDownList ID="ddlEventID" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Mã khách mời</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12 paddingleft0">
                                                            Tên khách mời</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:HiddenField ID="hdUserID" runat="server" Value="0" />
                                                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12 paddingleft0">
                                                            Tham gia</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <div class="checkbox">
                                                                <label class="">
                                                                    <asp:CheckBox ID="chkIsCheck" runat="server" CssClass="flat" />
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Đơn vị</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtFacility" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Chức vụ</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtOffice" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            CMND/ CCCD</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtCMND" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Số điện thoại</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Email</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-2 col-sm-3 col-xs-12">
                                                            Ghi chú</label>
                                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="lbtInsertUser" runat="server" CssClass="btn btn-primary" OnClick="lbtInsertUser_Click">Lưu lại</asp:LinkButton>
                <asp:LinkButton ID="lbtClose" runat="server" CssClass="btn btn-default" data-dismiss="modal" OnClick="lbtClose_Click">Close</asp:LinkButton>
            </div>
        </div>

    </div>
</div>

