using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using BLL;

namespace GroceryChallenge
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //***********PRODUCTION
                System.Security.Principal.IPrincipal _user;
                _user = System.Web.HttpContext.Current.User;
                if (_user != null )
                {
                     if (_user.Identity.Name != "")
                    {
                        string[] _username = _user.Identity.Name.Split('\\');
                        //validate first character is a letter 
                        string _chkId = _username[1].Trim();
                        int n = Regex.Matches(_chkId[0].ToString(), @"[a-zA-Z]").Count;
                        int o = _chkId.Count();
                        if (n == 1 && o > 6)
                        {
                            int _val;
                            bool _aOK = int.TryParse(_chkId.Substring(1), out _val);
                            if (_aOK)
                            {
                                hfPartnerID.Value = _chkId;

                                string partnerRole = new GCItemsBLL().GetUserRole(_chkId);
                                switch (partnerRole)
                                {
                                    case "0"://First time User Registration
                                        pnlRegister.Visible = true;
                                        break;
                                    case "1"://GUEST
                                        Server.Transfer("~/GCProducts.aspx");
                                        break;
                                    case "2"://ADMIN
                                        Server.Transfer("~/GCProducts.aspx");
                                        break;
                                    default:
                                        Server.Transfer("~/NoAccess.aspx");
                                        break;
                                }
                            }
                            else
                            {
                                Server.Transfer("~/NoAccess.aspx");
                            }
                        }
                        else
                        {
                            Server.Transfer("~/NoAccess.aspx");
                        }
                    }
                    else
                    {
                        Server.Transfer("~/NoAccess.aspx");
                    }
                }
                else
                {
                    Server.Transfer("~/NoAccess.aspx");
                }

                //******END PRODUCTION
                // //START DEVELOPMENT
                //pnlRegister.Visible = true;
                //hfPartnerID.Value = "a636277";
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            lblError.Text = "Required: ";
            try
            {
                if (txtFirstName.Text != string.Empty)
                {
                    if(txtLastName.Text != string.Empty)
                    {
                        if(txtEmail.Text != string.Empty)
                        {
                            var _address = new EmailAddressAttribute();
                            bool aOK;
                            aOK = _address.IsValid(txtEmail.Text);
                            if(aOK)
                            {

                                DataTable dt = new DataTable();
                                dt.Columns.Add("PARTNER_ID", typeof(System.String));
                                dt.Columns.Add("FIRST_NAME", typeof(System.String));
                                dt.Columns.Add("LAST_NAME", typeof(System.String));
                                dt.Columns.Add("EMAIL", typeof(System.String));
                                dt.Columns.Add("CREATED_BY", typeof(System.String));

                                DataRow rows = dt.NewRow();
                                rows[0] = hfPartnerID.Value.Trim().ToLower();
                                rows[1] = txtFirstName.Text.Trim().ToUpper();
                                rows[2] = txtLastName.Text.Trim().ToUpper();
                                rows[3] = txtEmail.Text.Trim().ToLower();
                                rows[4] = hfPartnerID.Value.Trim().ToLower();

                                dt.Rows.Add(rows);
                                bool returnval = new GCItemsBLL().AddUserProfile(dt);
                                if (returnval)
                                {
                                    Server.Transfer("~/GCProducts.aspx");
                                }
 
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text += "Email Address Is Not Valid.";
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text += "Enter Your HEB Email Address.";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text += "Enter Your Last Name.";
                    }
                }
                else
                {
               lblError.Visible = true;
               lblError.Text += "Enter Your First Name.";
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error: Registration Failed. Connection issue, please try again later. ";
            }
        }
    }

}