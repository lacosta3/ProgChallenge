using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Configuration;
using System.Security;
using System.Text.RegularExpressions;
using System.IO;

using System.Net;
using System.Web.Script.Serialization;
using BLL;
using Entity;

namespace GroceryChallenge
{
    public partial class GCMedia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSearchParams();

                System.Web.UI.HtmlControls.HtmlGenericControl li_guest = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("navguest_Media");
                if (li_guest != null)
                {
                    li_guest.Style.Add("background-color", "#ffffff");
                }
                System.Web.UI.HtmlControls.HtmlGenericControl li_poweradmin = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("navpoweradmin_Challenge");
                if (li_poweradmin != null)
                {
                    li_poweradmin.Style.Add("background-color", "#ffffff");
                }
               
            }
            //Maintain Activity Grid Scroll Position
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>GetDivPosition(); </script>", false);
        }

        protected void btnGiphySave_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            HiddenField hfPartnerID = (HiddenField)Page.Master.FindControl("hfPartnerID");
            HiddenField hdnImgNumber = (HiddenField)Page.Master.FindControl("hfParam");
            HiddenField hfParam = (HiddenField)Page.Master.FindControl("hdnImgNumber");

            HiddenField hdnUrl1 = (HiddenField)Page.Master.FindControl("hdnUrl1");
            HiddenField hdnUrl2 = (HiddenField)Page.Master.FindControl("hdnUrl2");
            HiddenField hdnUrl3 = (HiddenField)Page.Master.FindControl("hdnUrl3");
            HiddenField hdnUrl4 = (HiddenField)Page.Master.FindControl("hdnUrl4");
            HiddenField hdnUrl5 = (HiddenField)Page.Master.FindControl("hdnUrl5");

            try
            {
                lblError.Text = "";
                
                Media objMedia = new Media();
                bool errorflg = true;

                //VALIDATE START
                if (ddlSearchCat.SelectedValue == "SELECT")
                {
                    errorflg = false;
                    lblError.Text = "Error: CATEGORY is required.";
                }
                //VALIDATE COMPLETE
                if (errorflg)
                {
                    //ADD VALUES TO OBJECT                    
                    objMedia.UserID = hfPartnerID.Value;
                    objMedia.Category = ddlSearchCat.SelectedValue;

                    //GET URL SELECTED
                    int n = Convert.ToInt32(hdnImgNumber.Value);
                    switch (n)
                    {
                        case 1:
                            objMedia.URL = hdnUrl1.Value;
                            break;
                        case 2:
                            objMedia.URL = hdnUrl2.Value;
                            break;
                        case 3:
                            objMedia.URL = hdnUrl3.Value;
                            break;
                        case 4:
                            objMedia.URL = hdnUrl4.Value;
                            break;
                        case 5:
                            objMedia.URL = hdnUrl5.Value;
                            break;
                        default:
                            errorflg = false;
                            lblError.Text = "Error: Image URL Not Found. Select and Image.";
                            break;
                    }

                    if (errorflg)
                    {
                        //SEND FOR INSERT
                        bool returnval = new GCItemsBLL().AddNewImage(objMedia);
                        if (returnval)
                        {

                            lblError.Visible = true;
                            lblError.Text = "Success: Image Was Added Successfully.";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Error: Image Was NOT Added Successfully.";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Visible = true;
                }



            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error: btnActNew_Click(). " + ex;
            }

        }
        public void GetSearchParams()
        {
            DataSet ds = new GCItemsBLL().GetSearchParams();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    //Name Tables Returned
                    ds.Tables[0].TableName = "Description";

                    //Description
                    ddlSearchCat.DataSource = ds.Tables["Description"];
                    ddlSearchCat.DataBind();
                    ddlSearchCat.Items.Insert(0, new ListItem("SELECT", "SELECT"));

                }

            }
        }


    }
}