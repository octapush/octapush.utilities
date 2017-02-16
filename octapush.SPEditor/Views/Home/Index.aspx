<%@ Page Title="octapush - SP EDITOR" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" MasterPageFile="~/Views/Shared/MasterPage.Master" %>

<asp:Content runat="server" ID="Head" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <!-- BEGIN CONTAINER -->
    <div class="page-container">
        <!-- BEGIN SIDEBAR -->
        <div class="page-sidebar-wrapper">
            <div class="page-sidebar navbar-collapse collapse">
                <div class="row">
                    <div style="padding: 20px 30px 10px; font-size: 14pt;" class="col-md-12 font-green-sharp bold">
                        APPLICATIONS

                        <a data-app-add="" class="btn purple pull-right btn-circle">
                            <i class="glyphicon glyphicon-plus"></i>
                        </a>
                    </div>
                </div>
                <ul class="page-sidebar-menu" data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200">
                    <%--<li class="start active"><a href="#"><span class="title">TEST 1</span></a></li>--%>
                </ul>
            </div>
        </div>
        <!-- END SIDEBAR -->
        
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="page-head">
                    <div class="page-title">
                        <h1>Application Editor <small>Edit your application &amp; queries</small></h1>
                    </div>
                </div>
                
                <div class="row ">
                    <div class="col-md-12">
                        <div class="portlet light bordered">
							<div class="portlet-title">
								<div class="caption pull-right">
								    <span class="caption-helper">Queries of</span>
                                    <span class="caption-subject font-green-sharp bold uppercase" id="applicationNameTitle">
                                        ApplicationName
                                    </span>
								</div>

								<div class="action pull-left">
								    <a title="" data-original-title="" href="javascript:;" class="btn btn-circle blue btn-icon-only fullscreen">
									    <i class="fa fa-arrows-alt"></i>
									</a>

									<a data-app-delete="" class="btn btn-circle btn-warning">
									    <i class="fa fa-times"></i> DELETE APPLICATION
									</a>

									<a data-app-save="" href="javascript:;" class="btn btn-circle green-haze">
									    <i class="fa fa-save"></i> SAVE CHANGES
									</a>
								</div>
							</div>
							<div class="portlet-body">
								<div class="row" style="padding: 0 20px;">
								    <div class="col-md-6">
								        <div class="row">
								            <div class="form-body">
								                <form class="form-horizontal" id="formApplication">
								                    <input name="Id" id="Id" type="hidden"/>

								                    <div class="row margin-bottom-15">
								                        <div class="col-md-12">
								                            <input name="Name" id="Name" type="text" class="form-control input-lg" placeholder="Application Name"/>
								                        </div>
								                    </div>

								                    <div class="row margin-bottom-15">
								                        <div class="col-md-12">
								                            <textarea name="ConnectionString" id="ConnectionString" class="form-control" rows="3" placeholder="OLEDB Connection String"></textarea>
								                        </div>
								                    </div>

								                    <div class="row margin-bottom-15">
								                        <div class="col-md-12">
								                            <input name="IsActive" id="IsActive" type="checkbox" class="make-switch" checked data-on-text="&nbsp;Enable&nbsp;" data-off-text="&nbsp;Disable&nbsp;" data-on-color="primary" data-off-color="danger" />
								                        </div>
								                    </div>
								                </form>
								            </div>
								        </div>
								    </div>

                                    <div class="col-md-6">
                                        <div class="row margin-bottom-15">
                                            <div class="col-md-10">
                                                <div class="input-icon input-icon-lg">
											        <i class="fa fa-search"></i>
											        <input class="form-control input-lg" placeholder="FILTER BY NAME" type="text">
										        </div>
                                            </div>
                                            <div class="col-md-2">
                                                <a id="btnAddQuery" class="btn red-thunderbird btn-circle btn-lg pull-right">
                                                    <i class="glyphicon glyphicon-plus"></i>
                                                </a>
                                            </div>
                                        </div>
                                        
                                        <div id="query-list-container">
                                            <div class="row margin-bottom-15">
                                                <div class="col-md-12">
                                                    <div class="note note-success">
                                                        <h4>
                                                            <strong>Query Name</strong>
                                                        </h4>
                                                        <p>Query Content</p>
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
    </div>
    <!-- END CONTAINER -->
    
    <!-- BEGIN QUERY FORM -->
    <div id="QueryModal" class="modal fade" tabindex="-1" role="dialog" aria-labeledby="QueryModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title"><strong>Query Editor</strong></h4>
                </div>
                
                <div class="modal-body">
                    <div class="tabbable-custom">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab_query_editor" data-toggle="tab">Editor</a></li>
                            <li><a href="#tab_query_result" data-toggle="tab">Result</a></li>
                        </ul>
                        
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_query_editor">
                                <form class="form-horizontal" id="formQuery">
                                    <input type="hidden" name="Id" />
                                    <input type="hidden" name="ApplicationId" />
                                    
								    <div class="row margin-bottom-15">
								        <div class="col-md-4 pull-right">
								            <input name="IsActive" type="checkbox" class="make-switch" checked data-on-text="&nbsp;Enable&nbsp;" data-off-text="&nbsp;Disable&nbsp;" data-on-color="primary" data-off-color="danger" />
								        </div>
								    </div>

                                    <div class="row margin-bottom-15">
								        <div class="col-md-12">
								            <input name="Name" type="text" class="form-control input-lg" placeholder="Query Name"/>
								        </div>
								    </div>

                                    <div class="row margin-bottom-15">
								        <div class="col-md-12">
								            <textarea name="Query" class="form-control" rows="10" placeholder="Write your query here"></textarea>
								        </div>
								    </div>
                                </form>
                            </div>

                            <div class="tab-pane" id="tab_query_result">
                                page 2
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="modal-footer">
                    <button class="btn default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <button class="btn green" name="test_query">Test Query</button>
                    <button class="btn blue" name="save">Save</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END QUERY FORM -->
</asp:Content>

<asp:Content runat="server" ID="Footer" ContentPlaceHolderID="FooterContent">
    <script src="<%: ResolveClientUrl("~/Scripts/speditor.js") %>" type="text/javascript"></script>
</asp:Content>