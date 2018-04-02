using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;



namespace GroceryChallenge
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////****************************DEVELOPMENT**********************
                //System.Security.Principal.IPrincipal _user;
                //_user = System.Web.HttpContext.Current.User;

                ////Set ID
                //hfPartnerID.Value = "a636277";//_user.Identity.Name;

                ////if (_user.Identity.Name != "")
                //if (lblUserName.Text != "")
                //{
                //    //string[] _username = _user.Identity.Name.Split('\\');
                //   // hfPartnerName.Value = "LEO ACOSTA";
                //    lblUserName.Text = "a636277";

                //    //DataTable tblProfile = new ATSItemsBLL().GetUserAccessRole(_username[1]);
                //    string userRole = new GCItemsBLL().GetUserRole(lblUserName.Text);
                //    if (userRole != string.Empty)
                //    {
                        
                //        switch (userRole)
                //        {
                //            //Menu Logic
                //            case "1":// GUEST USER
                //                pnlGuestUser.Visible = true;
                //                pnlPowerAdmin.Visible = false;
                //                break;
                //            case "2"://POWER USER
                //                pnlPowerAdmin.Visible = true;
                //                pnlGuestUser.Visible = false;
                //                break;
                //            default:
                //                Response.Redirect("~/NoAccess.aspx");
                //                break;
                //        }
                //    }
                //    else
                //    {
                //        Response.Redirect("~/NoAccess.aspx");
                //    }
                //}
                //else
                //{
                //    Response.Redirect("~/NoAccess.aspx");
                //}

                //****************************PRODUCTION**********************
                System.Security.Principal.IPrincipal _user;
                _user = System.Web.HttpContext.Current.User;

                //Set Footer Title
                lblUserName.Text = _user.Identity.Name;

                if (_user.Identity.Name != "")
                {
                    string[] _username = _user.Identity.Name.Split('\\');
                    hfPartnerID.Value = _username[1].Trim();

                    string _role = "REGISTER";
                    DataTable tblProfile = new GCItemsBLL().GetUserAccessRole(_username[1]);
                    if (tblProfile.Rows.Count > 0)
                    {
                        _role = tblProfile.Rows[0]["ROLE"].ToString();

                        lblUserName.Text = tblProfile.Rows[0]["USERNAME"].ToString();
                        hfPartnerName.Value = tblProfile.Rows[0]["USERNAME"].ToString();

                        switch (_role)
                        {
                            //Menu Logic
                            case "GUEST USER":// GUEST USER
                                pnlGuestUser.Visible = true;
                                pnlPowerAdmin.Visible = false;
                                break;
                            case "POWER USER"://POWER USER
                                pnlPowerAdmin.Visible = true;
                                pnlGuestUser.Visible = false;
                                break;
                            default:
                                Response.Redirect("~/NoAccess.aspx");
                                break;
                        }
                    }
                    else
                    {
                        Response.Redirect("~/NoAccess.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/NoAccess.aspx");
                }



                //********************************END************************
            }
        }
    }
}