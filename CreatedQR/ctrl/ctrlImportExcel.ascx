<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlImportExcel.ascx.cs" Inherits="CreatedQR.ctrl.ctrlImportExcel" %>

<div class="modal fade" id="myModelImportExcel" role="dialog" runat="server">
    <div class="modal-dialog modalSection">
        <div class="modal-content">
            <div class="modal-header">
                <asp:LinkButton ID="lbtCloseTop" runat="server" OnClick="lbtCloseTop_Click" CssClass="close" data-dismiss="modal">&times;</asp:LinkButton>
                <h4 class="modal-title">Chi tiết FileImport</h4>
            </div>
            <div class="modal-body" style="overflow: hidden;">
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
                                                        <label class="col-md-12 col-sm-3 col-xs-12">
                                                            File</label>
                                                        <div class="col-md-12 col-sm-9 col-xs-12">
                                                              <asp:FileUpload ID="FileUploadUser" runat="server" />
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
                 <asp:LinkButton ID="lbtImportUser" CssClass="btn btn-success" runat="server" ToolTip="Import Excel"  OnClick="lbtImportUser_Click">Import Excel</asp:LinkButton>
                <asp:LinkButton ID="lbtClose" runat="server" CssClass="btn btn-default" data-dismiss="modal" OnClick="lbtClose_Click">Close</asp:LinkButton>
            </div>
        </div>

    </div>
</div>
