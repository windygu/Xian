using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Enterprise.Common.Admin.AuthorityGroupAdmin;
using ClearCanvas.ImageServer.Common;
using ClearCanvas.ImageServer.Common.Services.Admin;
using ClearCanvas.ImageServer.Web.Common.Security;

namespace ClearCanvas.ImageServer.Web.Application.Test
{
    public partial class AuthorityGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        public override void DataBind()
        {
            LoginCredentials credential = SessionManager.Current.Credentials;
            Login.Text = String.Format("{0} expire at: {1}",
                    credential.DisplayName,
                    credential.SessionToken.ExpiryTime
                );


            Platform.GetService<IAuthorityAdminService>(
                delegate(IAuthorityAdminService services)
                {
                    IList<AuthorityGroupSummary> list = services.ListAllAuthorityGroups();
                    List<AuthorityRowData> rows = CollectionUtils.Map<AuthorityGroupSummary, AuthorityRowData>(
                        list, delegate(AuthorityGroupSummary group)
                                   {
                                       AuthorityRowData row = new AuthorityRowData(group);
                                       return row;
                                   });

                    List.DataSource = rows;

                    IList<AuthorityTokenSummary> tokens = services.ListAuthorityTokens();
                    List<AuthorityTokenRowData> tokenRows = CollectionUtils.Map<AuthorityTokenSummary, AuthorityTokenRowData>(
                        tokens, delegate(AuthorityTokenSummary token)
                                   {
                                       AuthorityTokenRowData row = new AuthorityTokenRowData(token);
                                       return row;
                                   });

                    TokenList.DataSource = tokenRows;

                    IList<ListItem> items = CollectionUtils.Map<AuthorityTokenSummary, ListItem>(
                        tokens,
                        delegate(AuthorityTokenSummary token)
                            {
                                return new ListItem(token.Description, token.Name);
                            }
                        );
                    NewGroupTokenListBox.Items.AddRange(CollectionUtils.ToArray(items));
                });


            base.DataBind();
        }

        protected void LogoutClicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void CreateNewGroupClicked(object sender, EventArgs e)
        {
            Platform.GetService<IAuthorityAdminService>(
                delegate(IAuthorityAdminService service)
                    {
                        List<AuthorityTokenSummary> tokens = new List<AuthorityTokenSummary>();
                        foreach(ListItem item in NewGroupTokenListBox.Items)
                        {
                            if (item.Selected)
                            {
                                tokens.Add(new AuthorityTokenSummary(item.Value, item.Text));
                            }
                        }

                        service.AddAuthorityGroup(NewGroupName.Text, tokens);
                    });
        }


        protected void NewTokenClicked(object sender, EventArgs e)
        {
            Platform.GetService<IAuthorityAdminService>(
                delegate(IAuthorityAdminService service)
                {
                    List<AuthorityTokenSummary> tokenList = new List<AuthorityTokenSummary>();
                    tokenList.Add(new AuthorityTokenSummary(NewTokenDescriptionTextBox.Text, NewTokenDescriptionTextBox.Text));
                    service.ImportAuthorityTokens(tokenList);
                });
        }
    }

    class AuthorityRowData
    {
        private string _name;

        public AuthorityRowData(AuthorityGroupSummary group)
        {
            this.Name = group.Name;

            //_tokens = StringUtilities.Combine(group.AuthorityTokens, "\n\r",
            //                                 delegate(AuthorityTokenSummary token)
            //                                     {
            //                                         return String.Format("{0} [{1}]", token.Name, token.Description);
            //                                     });
            
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }

    class AuthorityTokenRowData
    {
        private string _name;
        private string _description;

        public AuthorityTokenRowData(AuthorityTokenSummary token)
        {
            Name = token.Name;
            Description = token.Description;
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }

        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

    }
}
