<%--  License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudyDetailsTabs.ascx.cs"
    Inherits="ClearCanvas.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.StudyDetailsTabs" %>
<%@ Register Src="StudyDetailsView.ascx" TagName="StudyDetailsView" TagPrefix="localAsp" %>
<%@ Register Src="SeriesGridView.ascx" TagName="SeriesGridView" TagPrefix="localAsp" %>
<%@ Register Src="WorkQueueGridView.ascx" TagName="WorkQueueGridView" TagPrefix="localAsp" %>
<%@ Register Src="FileSystemQueueGridView.ascx" TagName="FileSystemQueueGridView" TagPrefix="localAsp" %>
<%@ Register Src="StudyStorageView.ascx" TagName="StudyStorageView" TagPrefix="localAsp" %>
<%@ Register Src="ArchivePanel.ascx" TagName="ArchivePanel" TagPrefix="localAsp" %>
<%@ Register Src="HistoryPanel.ascx" TagName="HistoryPanel" TagPrefix="localAsp" %>
<%@ Register Src="StudyIntegrityQueueGridView.ascx" TagName="StudyIntegrityQueueGridView" TagPrefix="localAsp" %>
<%@ Register Src="DataAccessPanel.ascx" TagName="DataAccessPanel" TagPrefix="localAsp" %>

<aspAjax:TabContainer ID="StudyDetailsTabContainer" runat="server" ActiveTabIndex="0"
    CssClass="TabControl" Width="100%" Style="border-bottom: #618FAD 1px solid;">
    <aspAjax:TabPanel ID="StudyDetailsTab" HeaderText="<%$Resources: Titles, StudyDetails %>"
        runat="server">
        <ContentTemplate>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                            <tr>
                                <td>
                                    <localAsp:StudyDetailsView ID="StudyDetailsView" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="SeriesDetailsTab" HeaderText="<%$Resources: Titles, SeriesDetails %>"
        runat="server">
        <ContentTemplate>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <table width="100%" cellpadding="4" cellspacing="0" style="background-color: #B8D8EE;">
                            <tr>
                                <td>
                                    <div style="padding-top: 5px; padding-left: 1px;" />
                                    <ccUI:ToolbarButton runat="server" ID="ViewSeriesButton" SkinID="<%$Image:ViewSeriesButton%>" />&nbsp;
                                    <ccUI:ToolbarButton runat="server" ID="MoveSeriesButton" SkinID="<%$Image:MoveSeriesButton%>" />&nbsp;
                                    <ccUI:ToolbarButton runat="server" ID="DeleteSeriesButton" SkinID="<%$Image:DeleteSeriesButton%>"
                                        OnClick="DeleteSeriesButton_Click" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="border: solid 1px #3d98d1;">
                                        <localAsp:SeriesGridView ID="SeriesGridView" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="WorkQueueTab" HeaderText="<%$Resources: Titles, WorkQueue %>"
        runat="server">
        <HeaderTemplate>
            <asp:Label ID="WorkQueueTabTitle" runat="server" Text="<%$Resources: Titles, WorkQueue %>"></asp:Label>
            <asp:Image runat="server" Visible='<%# Study.RequiresWorkQueueAttention %>' ImageAlign="AbsBottom"
                ID="StuckIcon" SkinID="WarningSmall" />
        </HeaderTemplate>
        <ContentTemplate>
            <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <div style="border: solid 1px #3d98d1;">
                            <localAsp:WorkQueueGridView ID="WorkQueueGridView" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="TabPanel4" HeaderText="<%$Resources: Titles, StudyIntegrityQueue %>"
        runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <div style="border: solid 1px #3d98d1;">
                            <localAsp:StudyIntegrityQueueGridView ID="StudyIntegrityQueueGridView" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="FileSystemQueueTab" HeaderText="<%$Resources: Titles, FileSystemQueue %>"
        runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <div style="border: solid 1px #3d98d1;">
                            <localAsp:FileSystemQueueGridView ID="FSQueueGridView" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="TabPanel1" HeaderText="<%$Resources: Titles, StudyStorage %>"
        runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <localAsp:StudyStorageView ID="StudyStorageView" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="TabPanel2" HeaderText="<%$Resources: Titles, Archive %>" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <localAsp:ArchivePanel ID="ArchivePanel" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="TabPanel3" HeaderText="<%$Resources: Titles, StudyHistory %>"
        runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="8" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <div style="border: solid 1px #3d98d1;">
                            <localAsp:HistoryPanel ID="HistoryPanel" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
    <aspAjax:TabPanel ID="TabPanel5" HeaderText="<%$Resources: Titles, DataAccess %>" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="4" cellspacing="0" style="background-color: #B8D8EE;">
                <tr>
                    <td>
                        <div style="padding-top: 5px; padding-left: 1px;" />
                        <ccUI:ToolbarButton runat="server" ID="AddAuthorityGroupButton" SkinID="<%$Image:AddButton%>" OnClick="AddAuthorityGroupButton_Click" />&nbsp;
                        <ccUI:ToolbarButton runat="server" ID="DeleteAuthorityGroupButton" SkinID="<%$Image:DeleteButton%>" OnClick="DeleteAuthorityGroupButton_Click" />&nbsp;
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="border: solid 1px #3d98d1;">
                            <localAsp:DataAccessPanel runat="server" ID="DataAccessPanel" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </aspAjax:TabPanel>
</aspAjax:TabContainer>
<ccAsp:MessageBox ID="DeleteConfirmation" runat="server" Title="Delete Series Confirmation" />
