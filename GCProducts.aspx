<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/Site1.Master" CodeBehind="GCProducts.aspx.cs" Inherits="GroceryChallenge.GCProducts" %>
<%@ MasterType VirtualPath="~/Site1.Master" %>

<asp:Content runat="server" ID="MainContent_GCProducts" ContentPlaceHolderID="MainContent">
     <%-- Title --%>
        <table style="width:100%">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" runat="server" Font-Names="Arial, Courier New, Courier" ForeColor="Salmon" Font-Size="2.3em" Text="My Products"></asp:Label>
                </td>
            </tr>
        </table>
    <%--Lookup Products--%>
        <table border="1" style="font-family:Arial,Courier New;width:65%; margin-top:10px; font-size:9pt;color:black; border:outset;height:100%;float:left;">
            <caption class="caption">Lookup Products</caption>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:center;font-weight:600;font-size:8pt">DESCRIPTION</td>
                <td style ="text-align:center;font-weight:600;font-size:8pt">DEPARTMENT</td>
                <td style ="text-align:center;font-weight:600;font-size:8pt">LAST SOLD</td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:center">
                    <asp:DropDownList ID="ddlSearchDesc" runat="server" Font-Size="8pt" DataTextField="Description" DataValueField="Description"></asp:DropDownList>
                </td>
                <td style ="text-align:center">
                    <asp:DropDownList ID="ddlSearchDept" runat="server" Font-Size="8pt"  DataTextField="Department" DataValueField="Department" ></asp:DropDownList>
                </td>
                <td style ="text-align:center">
                    <asp:DropDownList ID="ddlSearchSold" runat="server" Font-Size="8pt"  DataTextField="Last_Sold" DataValueField="Last_Sold" ></asp:DropDownList>
                </td>
            </tr>
             <tr>
               <td style ="text-align:center;background-color:black; color:white; border:outset;border-width:1px; height:26px;" colspan="7">
                    <asp:Button ID="btnGCSearch" runat="server" Text="LOOKUP PRODUCTS" Font-Size="12px" OnClick="btnGCSearch_Click" ToolTip="Get Products"></asp:Button>
               </td>
            </tr>
        </table>
     <%-- FILE UPLOAD--%>
    <table  border="1"  style="font-family:Arial,Courier New; width:350px; margin-left:10px; margin-top:10px; font-size:9pt;color:black;border:outset;display:inline-block;">
        <caption style="background:#cccccc;	font-size:16px; font-family:Arial, Courier New, Courier, monospace;border:solid;border-bottom-width:4px;border-left-style:none;border-right-style:none;border-top-style:none;
           border-bottom-color:black; padding:5px;text-align:left; color:black;font-weight:600;">Upload A Product File (.csv)</caption>
         <tr>
            <td style ="text-align:center;font-weight:600;font-size:8pt;border-width:1px; border-color:black; "colspan="2" >FILE TO UPLOAD
                <%--<asp:Label ID="lblHdrTitle" runat="server"  Text="FILE TO UPLOAD" ></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td style ="text-align:center; border-width:1px; "  colspan="2"  >
                <asp:FileUpload ID="fuBlkUpload" runat="server" Width="325px" Font-Size="8pt"  BorderWidth="1px" ToolTip="Select a CSV file to Upload" EnableTheming="False" EnableViewState="False"></asp:FileUpload >
            </td>
        </tr>
        <tr>
            <td style ="text-align:center;background-color:black; color:white; border:outset;height:26px;width:50%;" >
                <asp:Button ID="btnFileUpload" runat="server" Text="Upload File (.csv)" Font-Size="12px" OnClick="btnFileUpload_Click" ToolTip="Upload A Schedule Project Rollout."></asp:Button>
            </td>
            <td style ="text-align:center;background-color:black; color:white; border:outset;height:26px;width:50%;" >
                <asp:Button ID="btnGetTemplate" runat="server" Text="Download Template" Font-Size="12px" OnClick="btnGetTemplate_Click" ToolTip="Get Template." ></asp:Button> 
            </td>
        </tr>
    </table>
     <br />
     <br />
    <asp:Panel ID="pnlGCProduct" Visible="false" runat="server">
    <%-- PRODUCTS GRIDVIEW --%>
        <div id="gridDivProducts" onscroll="SetDivPosition()" style ="font-family:Arial,Courier New;height:225px;border:solid;border-width:1px; Width:100%; overflow:auto;font-size:9pt;text-align:center;">
           <asp:GridView ID="grdGCProducts" runat="server" AutoGenerateColumns="false" Width="99%" Height="100%"
               OnRowCommand="grdGCProducts_RowCommand" 
               OnRowCancelingEdit="grdGCProducts_RowCancelingEdit" OnRowEditing="grdGCProducts_RowEditing"
               DataKeyNames="Product_ID" AllowPaging="False" AllowSorting="False" 
               ShowHeader="True" ShowFooter="False" BorderStyle="None" GridLines="None"  BackColor="#e2e2e2"   >
               <SelectedRowStyle BackColor="#BCD6E8" Font-Bold="True" ForeColor="Black" />
               <AlternatingRowStyle BackColor="White" ForeColor="Black"  />
               <HeaderStyle BackColor="Black" ForeColor="White" Font-Bold="True" Font-Names="Calibri" Height="25px"/>
               <RowStyle Font-Names="Calibri"/>
                <Columns>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" > 
                        <ItemTemplate> 
                            <asp:ImageButton ID="imgbtnGCDelete"  ImageUrl="~/Images/Del.png" Width="20px" OnClientClick="return confirm('Are you sure you want to DELETE this Product?');"  runat="server" CommandArgument='<%# Eval("Product_ID") %>' CommandName="DeleteRow" ToolTip="Delete Item"></asp:ImageButton> 
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnGCUpdate" ImageUrl="~/Images/update.png" Width="20px"  runat="server" CommandName="UpdateRow" ToolTip="Update Item"></asp:ImageButton> 
                         </EditItemTemplate>   
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate> 
                            <asp:ImageButton ID="imgbtnGCView" ImageUrl="~/Images/edit_plus.png" Width="20px" runat="server" CommandName="ViewImage" ToolTip="View Images by Category"></asp:ImageButton> 
                        </ItemTemplate> 
                         <EditItemTemplate>
                              <asp:ImageButton ID="imgbtnGCCancel" ImageUrl="~/Images/minus.png" OnClientClick="return confirm('Are you sure you want to Cancel without Saving?');"  Width="20px"  runat="server" CommandName="Cancel" ToolTip="Exit Without Saving"></asp:ImageButton> 
                         </EditItemTemplate>                      
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate> 
                            <asp:ImageButton ID="imgbtnGCEdit" ImageUrl="~/Images/Edit1.png" Width="20px" runat="server" CommandName="Edit" ToolTip="Edit Product"></asp:ImageButton> 
                        </ItemTemplate>
                         <EditItemTemplate>
                         </EditItemTemplate> 
                    </asp:TemplateField>
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Label ID="lblGCProductID" runat="server" Width="54px" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "Product_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="UPC"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCUPC" runat="server" Width="84px" Text='<%# Bind("UPC") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                             <asp:Label ID="lblEditGCUPC" runat="server" Width="84px" Text='<%# Bind("UPC") %>'></asp:Label>
                         </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="DESCRIPTION"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCDesc" runat="server" Width="224px" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlGCEditDesc" runat="server" DataTextField="Description" DataValueField="Description"  ></asp:DropDownList>
                        </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="DEPTARTMENT"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCDept" runat="server" Width="94px" Text='<%# Bind("Department") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlGCEditDept" runat="server" DataTextField="Department" DataValueField="Department"  ></asp:DropDownList>
                        </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="SHELF LIFE"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCShelfLife" runat="server" Width="54px" Text='<%# Bind("Shelf_Life") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txbGCEditShelfLife" runat="server" Width="54px" Text='<%# Bind("Shelf_Life") %>'></asp:TextBox>
                        </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="LAST SOLD"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCLastSold" runat="server" Width="104px" Text='<%# Bind("Last_Sold","{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txbGCEditLastSold" runat="server" Width="104px" Text='<%# Bind("Last_Sold","{0:MM/dd/yyyy}") %>'></asp:TextBox>
                        </EditItemTemplate>                         
                    </asp:TemplateField> 
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="PRICE"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCPrice" runat="server" Width="54px" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txbGCEditPrice" runat="server" Width="54px" Text='<%# Bind("Price") %>'></asp:TextBox>
                        </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="UNIT"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCUnit" runat="server" Width="54px" Text='<%# Bind("Unit") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txbGCEditUnit" runat="server" Width="54px" Text='<%# Bind("Unit") %>'></asp:TextBox>
                        </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="XFOR"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCxFor" runat="server" Width="54px" Text='<%# Bind("xFor") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txbGCEditxFor" runat="server" Width="54px" Text='<%# Bind("xFor") %>'></asp:TextBox>
                        </EditItemTemplate>                          
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderText="COST"> 
                        <ItemTemplate >
                            <asp:Label ID="lblGCCost" runat="server" Width="54px" Text='<%# Bind("Cost") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txbGCEditCost" runat="server" Width="54px" Text='<%# Bind("Cost") %>'></asp:TextBox>
                        </EditItemTemplate>                          
                    </asp:TemplateField>


                </Columns> 
            </asp:GridView>                 
        </div>
    <%-- PRODUCTS FOOTER --%>
       <table  border="1" style="font-family:Arial,Courier New;width:100%; border:outset;border-width:2px; font-size:9pt; background-color:darkgray; color:black; height:25px;">
            <tr>
                <td style ="text-align:center">
                    <asp:Button ID="btnGCNewView" runat="server" Text="NEW PRODUCT" Font-Size="12px" Visible="true" OnClick="btnGCNewView_Click" ToolTip="View New Form." /> 
                </td>
             </tr>
 
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlNewProduct" Visible="false" runat="server">
    <%-- NEW PRODUCT FORM --%>
        <table border="1" style="font-family:Arial,Courier New;width:45%; margin-top:10px; font-size:9pt;color:black; border:outset;height:100%;float:left;">
            <caption class="caption">Add A New Product</caption>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">UPC</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewUPC" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">DESCRIPTION</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewDesc" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
           <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">DEPARTMENT</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewDept" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">SHELF LIFE</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewShelfLife" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;padding-left:10px;font-weight:600;font-size:8pt">LAST SOLD</td>
                <td style ="text-align:left">
                    <asp:TextBox ID="txbNewLastSold"  Width="120px" runat="server"></asp:TextBox>
                    <a id="a5" href="#" onclick="cal.select(document.forms['form1'].MainContent_txbNewLastSold,'a5','MM/dd/yyyy'); return false;"> <img src="/Images/cal.gif" width="16" height="16" alt="Click Here to Select Date" style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; vertical-align:middle " /> </a>
                    <asp:Label ID="lblNewLastSold" runat="server" Text="MM/DD/YYYY"></asp:Label>
                </td>
            </tr>
           <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">PRICE</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewPrice" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">UNIT</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewUnit" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">xFOR</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewxFor" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">COST</td>
                <td style ="text-align:left;">
                    <asp:TextBox ID="txbNewCost" Width="64px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
               <td style ="text-align:center;background-color:black; color:white; border:outset;border-width:1px; height:26px;" colspan="10">
                    <asp:Button ID="btnGCNewSubmit" runat="server" Text="SUBMIT" Font-Size="12px" OnClick="btnGCNewSubmit_Click" ToolTip="Add A New Product"></asp:Button>
               </td>
            </tr>
        </table>
    </asp:Panel>

    <br />
    <asp:Panel ID="pnlGifyView" runat="server" Visible="false">
    <div id="divGiphyContainer" style ="font-family:Arial,Courier New;height:225px;border:solid;border-width:1px; Width:65%; overflow:auto;font-size:9pt;text-align:center;">
     
     <!-- Slideshow container -->
    <div class="slideshow-container">

     <div class="mySlides fade">
        <div id="divimg1" class="numbertext">1 / 5</div>
        <img id="img1" src="~/edit_plus.png" runat="server" style="width:400px; height:200px"/>
      </div>

      <div class="mySlides fade">
        <div id="divimg2" class="numbertext">2 / 5</div>
        <img id="img2" src="" runat="server" style="width:400px; height:200px"/>
      </div>

      <div class="mySlides fade">
        <div id="divimg3" class="numbertext">3 / 5</div>
        <img id="img3" src="" runat="server" style="width:400px; height:200px"/>
     </div>
      <div class="mySlides fade">
        <div id="divimg4" class="numbertext">4 / 5</div>
        <img id="img4" src="" runat="server" style="width:400px; height:200px"/>
      </div>
      <div class="mySlides fade">
        <div id="divimg5" class="numbertext">5 / 5</div>
        <img id="img5" src="" runat="server" style="width:400px; height:200px"/>
      </div>

      <!-- Next and previous buttons -->
        <a class="prev" onclick="plusSlides(-1)">&#10094;</a>
        <a class="next" onclick="plusSlides(1)">&#10095;</a>
    </div>
    <br/>

    <!-- The dots/circles -->
    <div style="text-align:center">
      <span class="dot" onclick="currentSlide(1)"></span> 
      <span class="dot" onclick="currentSlide(2)"></span> 
      <span class="dot" onclick="currentSlide(3)"></span>
      <span class="dot" onclick="currentSlide(4)"></span> 
      <span class="dot" onclick="currentSlide(5)"></span>  
    </div>         

 </div>
   <%-- GIPHY FOOTER --%>
       <table  border="1" style="font-family:Arial,Courier New;width:65%; border:outset;border-width:2px; font-size:9pt; background-color:darkgray; color:black; height:25px;">
            <tr>
                <td style ="text-align:center">
                    <asp:Button ID="btnGiphyClose" runat="server" Text="CLOSE"  OnClick="btnGiphyClose_Click" Font-Size="12px" Visible="true" ToolTip="Save Giphy Image." /> 
                </td>
             </tr>
         </table>

    </asp:Panel>
 <br />
    <asp:Panel ID="pnlNoData" Visible="false" runat="server" Height="350px">
        <table style="width:98%;text-align:center;  ">
            <tr>
                <td>
                    <asp:Label ID="lblNoDataFound" runat="server" ForeColor="Black" Font-Names="Arial,Courier New" Font-Size="2.3em" Text="No Data Found!"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

        <br/>

    <div class="loading"  >
         Loading... Please wait<br />
         <br />
        <img src="/Images/loader.gif" alt="" />
    </div>

</asp:Content>
