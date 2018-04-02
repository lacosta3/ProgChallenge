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
using BLL;
using Entity;

namespace GroceryChallenge
{
    public partial class GCProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGridProducts("ALL", "ALL", "ALL");
                GetSearchParams();

                System.Web.UI.HtmlControls.HtmlGenericControl li_guest = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("navguest_Products");
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
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>GetDivPosition(); </script>", false);
        }
        #region METHODS
        public void GetSearchParams()
        {
            DataSet ds = new GCItemsBLL().GetSearchParams();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    //Name Tables Returned
                    ds.Tables[0].TableName = "Description";
                    ds.Tables[1].TableName = "Department";
                    ds.Tables[2].TableName = "LastSold";
 
                    //Description
                    ddlSearchDesc.DataSource = ds.Tables["Description"];
                    ddlSearchDesc.DataBind();
                    ddlSearchDesc.Items.Insert(0, new ListItem("ALL", "ALL"));

                    //Department
                    ddlSearchDept.DataSource = ds.Tables["Department"];
                    ddlSearchDept.DataBind();
                    ddlSearchDept.Items.Insert(0, new ListItem("ALL", "ALL"));

                    //Last Sold Date
                    ddlSearchSold.DataSource = ds.Tables["LastSold"];
                    ddlSearchSold.DataBind();
                    ddlSearchSold.Items.Insert(0, new ListItem("ALL", "ALL"));

                }

            }
        }
        public void GetGridProducts(string _desc, string _dept, string _solddate)
        {
            DataTable tblGrid = new GCItemsBLL().GetProducts(_desc, _dept, _solddate);
            if (tblGrid != null)
            {
                if (tblGrid.Rows.Count > 0)
                {
                    pnlGCProduct.Visible = true;
                    grdGCProducts.DataSource = tblGrid;
                    grdGCProducts.DataBind();

                    int cnt = tblGrid.Rows.Count;
                    Label lblError = (Label)Page.Master.FindControl("lblError");
                    lblError.Visible = true;
                    lblError.Text = cnt + " Products.";

                    pnlNoData.Visible = false;
                }
                else
                {
                    grdGCProducts.DataSource = null;
                    grdGCProducts.DataBind();

                    pnlNoData.Visible = true;

                    Label lblError = (Label)Page.Master.FindControl("lblError");
                    lblError.Visible = true;
                    lblError.Text = "Status: No Tasks.";
                }
            }
        }
        #endregion
        #region EVENTS
        protected void grdGCProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            lblError.Text = string.Empty;
            HiddenField hfPartnerName = (HiddenField)Page.Master.FindControl("hfPartnerName");
            HiddenField hfPartnerID = (HiddenField)Page.Master.FindControl("hfPartnerID");

            Control ctrl = e.CommandSource as Control;

            if (e.CommandName == "UpdateRow")
            {

                if (ctrl != null)
                {
                    Product objProduct = new Product();
                    bool errorflg = true;

                    //Get Controls from Grid
                    GridViewRow gvRow = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblGCProductID = (Label)gvRow.FindControl("lblGCProductID");
                    DropDownList ddlGCEditDesc = (DropDownList)gvRow.FindControl("ddlGCEditDesc");
                    DropDownList ddlGCEditDept = (DropDownList)gvRow.FindControl("ddlGCEditDept");
                    TextBox txbGCEditShelfLife = (TextBox)gvRow.FindControl("txbGCEditShelfLife");
                    TextBox txbGCEditLastSold = (TextBox)gvRow.FindControl("txbGCEditLastSold");
                    TextBox txbGCEditPrice = (TextBox)gvRow.FindControl("txbGCEditPrice");
                    TextBox txbGCEditUnit = (TextBox)gvRow.FindControl("txbGCEditUnit");
                    TextBox txbGCEditxFor = (TextBox)gvRow.FindControl("txbGCEditxFor");
                    TextBox txbGCEditCost = (TextBox)gvRow.FindControl("txbGCEditCost");          

                    //VALIDATE START
                    if (txbGCEditCost.Text == string.Empty)
                    {
                        errorflg = false;
                        lblError.Text = "Error: Cost Cannot Be Blank.";
                    }
                    if (txbGCEditxFor.Text == string.Empty)
                    {
                        errorflg = false;
                        lblError.Text = "Error: XFOR Cannot Be Blank.";
                    }
                    if (txbGCEditUnit.Text == string.Empty)
                    {
                        errorflg = false;
                        lblError.Text = "Error: Unit Cannot Be Blank.";
                    }
                    if (txbGCEditPrice.Text == string.Empty)
                    {
                        errorflg = false;
                        lblError.Text = "Error: Price Cannot Be Blank.";
                    }
                    DateTime value;
                    if (!DateTime.TryParse(txbGCEditLastSold.Text, out value))
                    {
                        errorflg = false;
                        lblError.Text = "Error: Last Sold Date Is Not Valid (MM/DD/YYYY).";
                    }
                    if (txbGCEditShelfLife.Text == string.Empty)
                    {
                        errorflg = false;
                        lblError.Text = "Error: Shelf Life Cannot Be Blank.";
                    }
                    if (ddlGCEditDept.SelectedValue == "SELECT")
                    {
                        errorflg = false;
                        lblError.Text = "Error: Department Is Not Valid.";
                    }
                    if (ddlGCEditDesc.SelectedValue == "SELECT")
                    {
                        errorflg = false;
                        lblError.Text = "Error: Description Is Not Valid.";
                    }

                     //VALIDATE COMPLETE
                    if (errorflg)
                    {
                        //ADD VALUES TO OBJECT
                        objProduct.ID = Convert.ToInt32(lblGCProductID.Text);
                        objProduct.UserName = hfPartnerName.Value.ToUpper();
                        objProduct.Department = ddlGCEditDept.SelectedValue;
                        objProduct.Description = ddlGCEditDesc.SelectedValue;
                        objProduct.LastSold = Convert.ToDateTime(txbGCEditLastSold.Text);                        
                        objProduct.ShelfLife = txbGCEditShelfLife.Text;
                        objProduct.Price = txbGCEditPrice.Text;                        
                        objProduct.Unit = txbGCEditUnit.Text;
                        objProduct.xFor = txbGCEditxFor.Text;
                        objProduct.Cost = txbGCEditCost.Text;  


                        //SEND FOR UPDATE
                        bool returnval = new GCItemsBLL().UpdateProduct(objProduct);
                        if (returnval)
                        {
                            grdGCProducts.EditIndex = -1;
                            grdGCProducts.SelectedIndex = -1;

                            //Get Grid Parameters and REBIND
                            GetGridProducts(ddlSearchDesc.SelectedValue, ddlSearchDept.SelectedValue, ddlSearchSold.SelectedValue);

                            lblError.Visible = true;
                            lblError.Text = "Success: Update Was Successfull.";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Error: Update Was NOT Successfull.";
                        }

                    }
                    else
                    {
                        lblError.Visible = true;
                    }

                }
            }
            if (e.CommandName == "ViewImage")
            {
                 if (ctrl != null)
                {
                    //Get Controls from Grid
                    GridViewRow gvRow = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblGCDesc = (Label)gvRow.FindControl("lblGCDesc");

                    DataTable dt = new GCItemsBLL().GetCategoryImages(hfPartnerID.Value,lblGCDesc.Text);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {                                                        
                            pnlGifyView.Visible = true;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                switch (i)
                                {
                                    case 0:
                                        img1.Src = dt.Rows[i][0].ToString();
                                        break;
                                    case 1:
                                        img2.Src = dt.Rows[i][0].ToString();
                                        break;
                                    case 2:
                                        img3.Src = dt.Rows[i][0].ToString();
                                        break;
                                    case 3:
                                        img4.Src = dt.Rows[i][0].ToString();
                                        break;
                                    case 4:
                                        img5.Src = dt.Rows[i][0].ToString();
                                        break;
                                    default:
                                        lblError.Visible = true;
                                        lblError.Text = "No Images Found for that Category.";
                                        break;
                                }
                            }


                        lblError.Visible = true;
                        lblError.Text = "Success: Media was Successfull.";

                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Error: No Images found for this Category. Save an Image to View. ";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Error: No Images were Found for this Category. Save an Image to View.";
                }
            }
            if (e.CommandName == "DeleteRow")
            {
                    if (ctrl != null)
                    {
                        //Get Controls from Grid
                        GridViewRow gvRow = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblGCProductID = (Label)gvRow.FindControl("lblGCProductID");
                        int _id = Convert.ToInt32(lblGCProductID.Text.Trim());

                        HiddenField _hfPartnerName = (HiddenField)Page.Master.FindControl("hfPartnerName");

                        //Send control values 
                        bool returnOk = new GCItemsBLL().DelProduct(_id, _hfPartnerName.Value.Trim());
                        if (returnOk)
                        {
                            //Get Grid Parameters and REBIND
                            GetGridProducts("ALL", "ALL", "ALL");
                            //pnlATSTaskList.Visible = false;


                            lblError.Visible = true;
                            lblError.Text = "Success: Delete was Successful.";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Error: Delete From Data Access Layer Failed. Connection issue, please try again later. ";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Error: No Records were selected for Deleting.";
                    }
            }
        }
        protected void grdGCProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void grdGCProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            pnlGCProduct.Visible = false;

            //Get ItemTemplate Controls for Passing data to EditItemTemplate controls
            Label lblGCDesc = (Label)grdGCProducts.Rows[e.NewEditIndex].FindControl("lblGCDesc");
            Label lblGCDept = (Label)grdGCProducts.Rows[e.NewEditIndex].FindControl("lblGCDept");

            //Set to Edit Mode
            grdGCProducts.EditIndex = e.NewEditIndex;
            grdGCProducts.SelectedIndex = grdGCProducts.EditIndex;

            //Get Grid Parameters and REBIND
            GetGridProducts(ddlSearchDesc.SelectedValue, ddlSearchDept.SelectedValue, ddlSearchSold.SelectedValue);

            //************Set EditItemTemplate Controls for Display**************
            DropDownList ddlGCEditDesc = (DropDownList)grdGCProducts.Rows[e.NewEditIndex].FindControl("ddlGCEditDesc");
            DropDownList ddlGCEditDept = (DropDownList)grdGCProducts.Rows[e.NewEditIndex].FindControl("ddlGCEditDept");


            //Get MM,BDM,DEPT FOR DROPDOWN EDIT
            DataSet ds = new GCItemsBLL().GetSearchParams();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    //Name Tables Returned
                    ds.Tables[0].TableName = "Desc";
                    ds.Tables[1].TableName = "Dept";
                    //ds.Tables[2].TableName = "SoldDate";

                    //Description
                    ddlGCEditDesc.DataSource = ds.Tables["Desc"];
                    ddlGCEditDesc.DataBind();
                    ddlGCEditDesc.SelectedValue = lblGCDesc.Text.ToString();

                    //Department
                    ddlGCEditDept.DataSource = ds.Tables["Dept"];
                    ddlGCEditDept.DataBind();
                    ddlGCEditDept.SelectedValue = lblGCDept.Text.ToString();
 
                }

            }

        }
        protected void grdGCProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdGCProducts.EditIndex = -1;
            grdGCProducts.SelectedIndex = -1;

            //Get Grid Parameters and REBIND
            GetGridProducts(ddlSearchDesc.SelectedValue, ddlSearchDept.SelectedValue, ddlSearchSold.SelectedValue);
        }
#endregion
        #region BUTTON CLICK EVENTS
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            HiddenField hfPartnerName = (HiddenField)Page.Master.FindControl("hfPartnerName");
            HiddenField hfPartnerID = (HiddenField)Page.Master.FindControl("hfPartnerID");

            try
            {
                FileUpload fu = fuBlkUpload;
                if (fu.HasFile)
                {
                    // Get file Extension
                    string filePath = fuBlkUpload.PostedFile.FileName;
                    string filename = Path.GetFileName(filePath);

                    string ext = Path.GetExtension(filename);
                    if (ext == ".csv")
                    {
                        bool errorflg = true;
                        string errorlog = "";
    
                        //create columns
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new System.Data.DataColumn("UPC", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("DESCRIPTION", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("DEPARTMENT", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("SHELFLIFE", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("LAST_SOLD", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("PRICE", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("UNIT", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("XFOR", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("COST", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("CREATED_BY", typeof(String)));
                        dt.Columns.Add(new System.Data.DataColumn("PARTNER_ID", typeof(String)));

                        //Get File
                        string scope = string.Empty;
                        string line = null;

                        StreamReader reader = new StreamReader(fu.PostedFile.InputStream);
                        reader.ReadLine();

                        while ((line = reader.ReadLine()) != null)
                        {
                            //COLUMN COUNT VALIDATION
                            string[] data = line.Split(',');
                            if (data.Length > 1 && data.Length < 10)
                            {
                                DataRow row = dt.NewRow();
                                //UPC
                                if (data[0] != string.Empty)
                                {
                                    row[0] = data[0].Trim();
                                    //DESCRIPTION
                                    if (data[1] != string.Empty)
                                    {
                                        row[1] = data[1].Trim();
                                        //DEPARTMENT
                                        if (data[2] != string.Empty)
                                        {
                                            row[2] = data[2].Trim();
                                            //SHELFLIFE
                                            if (data[3] != string.Empty)
                                            {
                                                row[3] = data[3].Trim();
                                               //LAST SOLD
                                                DateTime resetout;
                                                if (DateTime.TryParse(data[4], out resetout))
                                                {
                                                    row[4] = data[4];
                                                    //PRICE
                                                    if (data[5] != string.Empty)
                                                    {   
                                                        row[5] = data[5].Trim();
                                                        //UNIT
                                                        if (data[6] != string.Empty)
                                                        {
                                                            row[6] = data[6].Trim();
                                                            //XFOR
                                                            //NO COMMA
                                                            int c = Regex.Matches(data[7], @"[,]").Count;
                                                            if (c == 0)
                                                            {
                                                                //XFOR INT 
                                                                if(int.TryParse(data[7], out c))
                                                                {
                                                                    //XFOR AMOUNT
                                                                    if(Convert.ToInt32(data[7]) >= 0 && Convert.ToInt32(data[7]) < 101)
                                                                    {
                                                                        row[7] = data[7];
                                                                        //COST
                                                                        if (data[8] != string.Empty)
                                                                        {
                                                                            row[8] = data[8].Trim();
                                                                            row[9] = hfPartnerName.Value;
                                                                            row[10] = hfPartnerID.Value;
                                                                            dt.Rows.Add(row);
                                                                        }
                                                                        else
                                                                        {
                                                                            errorflg = false;
                                                                            errorlog = "Error: COST is Blank.";
                                                                        }


                                                                    }
                                                                    else
                                                                    {
                                                                        errorflg = false;
                                                                        errorlog = "Error: XFOR Entered Must Be Between 0 and 100.";
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    errorflg = false;
                                                                    errorlog = "Error: XFOR Value Is Invalid. Enter A Valid Interger.";
                                                                }

                                                            }
                                                            else
                                                            {
                                                                errorflg = false;
                                                                errorlog = "Error: XFOR Value Is Invalid. No Commas Are Allowed.";

                                                            }

                                                        }
                                                       else
                                                        {
                                                            errorflg = false;
                                                            errorlog = "Error: UNIT is Blank.";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        errorflg = false;
                                                        errorlog = "Error: PRICE is Blank.";
                                                    }

                                                }
                                                else
                                                {
                                                    errorflg = false;
                                                    errorlog = "Error: LASTSOLD Date is Invalid.";
                                                }
                                            }
                                            else
                                            {
                                                errorflg = false;
                                                errorlog = "Error: SHELFLIFE is Blank.";
                                            }
                                        }
                                        else
                                        {
                                            errorflg = false;
                                            errorlog = "Error: DEPARTMENT is Blank.";
                                        }
                                    }
                                    else
                                    {
                                        errorflg = false;
                                        errorlog = "Error: DESCRIPTION is Blank.";
                                    }

                                }
                                else
                                {
                                    errorflg = false;
                                    errorlog = "Error:UPC is Blank or Invalid.";
                                }
                            }
                            else
                            {
                                errorflg = false;
                                errorlog = "Error: Incorrect File Column Count.";
                            }
                        }
                        if (errorflg)
                        {
                            int cnt = dt.Rows.Count;
                            if (cnt < 26)
                            {

                                //Send datatable obj to Server
                                int returnval = new GCItemsBLL().AddProductFile(dt);
                                if (returnval > 0)
                                {
                                    lblError.Visible = true;
                                    lblError.Text = "Success: File " + filename + " was uploaded successfully with a record count of: " + cnt.ToString();
                                }
                                else
                                {
                                    lblError.Visible = true;
                                    lblError.Text = "Error: Product File Failed. Connection issue, please try again later." + returnval;
                                }
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text = "Error: Record Count is:" + cnt.ToString() +"! Only 25 records are allowed. ";
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = errorlog;
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Error: Only files with .CSV extension are allowed.";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Error: No File Selected.";
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error:File Upload Exception Failure. Connection Issue, Please Try Again Later." + ex.ToString();
            }

        }
        protected void btnGetTemplate_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=GCProduct_Template.csv");
            Response.Charset = "";
            Response.ContentType = "application/vnd.csv";
            Response.TransmitFile(Server.MapPath("~/Files/GCProduct_Template.csv"));
            Response.End();
        }
        protected void btnGCSearch_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            try
            {
                lblError.Text = "";
                pnlNewProduct.Visible = false;
  
                //RESET GRIDS FROM EDIT MODE
                grdGCProducts.EditIndex = -1;
                grdGCProducts.SelectedIndex = -1;

                GetGridProducts(ddlSearchDesc.SelectedValue, ddlSearchDept.SelectedValue, ddlSearchSold.SelectedValue);
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error: btnGCSearch_Click(). " + ex;
            }
        }
        protected void btnGCNewView_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            try
            {
                lblError.Text = "";
                pnlGCProduct.Visible = false;
                pnlGifyView.Visible = false;
                pnlNewProduct.Visible = true;

            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error: btnActNew_Click(). " + ex;
            }
        }
        protected void btnGCNewSubmit_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");
            lblError.Text = string.Empty;
            HiddenField hfPartnerName = (HiddenField)Page.Master.FindControl("hfPartnerName");
            HiddenField hfPartnerID = (HiddenField)Page.Master.FindControl("hfPartnerID");
            try
            {
                lblError.Text = "";

                Product objProduct = new Product();
                
                bool errorflg = true;

                //VALIDATE START
                if (txbNewUPC.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: UPC Cannot Be Blank.";
                }
                if (txbNewDesc.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: Description Cannot Be Blank.";
                }
                if (txbNewDept.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: Department Cannot Be Blank.";
                }
                if (txbNewShelfLife.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: ShelfLife Cannot Be Blank.";
                }
                DateTime dt1;
                if (!DateTime.TryParse(txbNewLastSold.Text, out dt1))
                {
                    errorflg = false;
                    lblError.Text = "Error: Last Sold Date Is Not Valid (MM/DD/YYYY).";
                }
                if (txbNewPrice.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: Price Cannot Be Blank.";
                }
                if (txbNewUnit.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: Unit Cannot Be Blank.";
                }
                if (txbNewxFor.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: XFOR Cannot Be Blank.";
                }
                if (txbNewCost.Text == string.Empty)
                {
                    errorflg = false;
                    lblError.Text = "Error: Cost Cannot Be Blank.";
                }

                //VALIDATE COMPLETE
                if (errorflg)
                {
                    //ADD VALUES TO OBJECT                    
                    objProduct.UserName = hfPartnerName.Value.ToUpper();
                    objProduct.UserID = hfPartnerID.Value;
                    objProduct.UPC = txbNewUPC.Text;
                    objProduct.Description = txbNewDesc.Text.ToUpper();
                    objProduct.Department = txbNewDept.Text.ToUpper();
                    objProduct.ShelfLife = txbNewShelfLife.Text;
                    objProduct.LastSold = Convert.ToDateTime(txbNewLastSold.Text);
                    objProduct.Price = txbNewPrice.Text;
                    objProduct.Unit = txbNewUnit.Text;
                    objProduct.xFor = txbNewxFor.Text;
                    objProduct.Cost = txbNewCost.Text;

                    //SEND FOR UPDATE
                    bool returnval = new GCItemsBLL().AddNewProduct(objProduct);
                    if (returnval)
                    {
                        grdGCProducts.EditIndex = -1;
                        grdGCProducts.SelectedIndex = -1;

                        pnlGCProduct.Visible = true;
                        pnlNewProduct.Visible = false;

                        //Get Grid Parameters and REBIND
                        GetGridProducts(ddlSearchDesc.SelectedValue, ddlSearchDept.SelectedValue, ddlSearchSold.SelectedValue);

                        lblError.Visible = true;
                        lblError.Text = "Success: Product Was Added Successfully.";
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Error: Product Was NOT Added Successfully.";
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
                lblError.Text = "Error: Process Failed. " + ex;
            }
        }
        protected void btnGiphyClose_Click(object sender, EventArgs e)
        {
            Label lblError = (Label)Page.Master.FindControl("lblError");


            try
            {
                lblError.Text = "";
                pnlGifyView.Visible = false;



            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error: Close failed. ";
            }

        }

        #endregion
    }
}