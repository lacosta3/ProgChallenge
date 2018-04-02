<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/Site1.Master" CodeBehind="GCMedia.aspx.cs" Inherits="GroceryChallenge.GCMedia" %>
<%@ MasterType VirtualPath="~/Site1.Master" %>

<asp:Content runat="server" ID="MainContent_GCProducts" ContentPlaceHolderID="MainContent">

     <%-- Title --%>
        <table style="width:100%">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" runat="server" Font-Names="Arial, Courier New, Courier" ForeColor="Salmon" Font-Size="2.3em" Text="Giphy Media"></asp:Label>
                </td>
            </tr>
        </table>
    <%--Lookup Products--%>
        <table border="1" style="font-family:Arial,Courier New;width:65%; margin-top:10px; font-size:9pt;color:black; border:outset;height:100%;float:left;">
            <caption class="caption">Lookup Giphy Images And Save Them To Your Products</caption>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:center;font-weight:600;font-size:8pt">ENTER TEXT TO SEARCH</td>
                <td style ="text-align:center;font-weight:600;font-size:8pt">SELECT A CATEGORY WHEN SAVING</td>
            </tr>
            <tr style="border:solid;border-width:1px;">
                <td style ="text-align:center">
                   <input type="text" id="txbGiphyParam"  />
                </td>
               <td style ="text-align:center">
                   <asp:DropDownList ID="ddlSearchCat" runat="server" Font-Size="8pt" DataTextField="Description" DataValueField="Description"></asp:DropDownList>
                </td>
            </tr>
             <tr>
               <td style ="text-align:center;background-color:black; color:white; border:outset;border-width:1px; height:26px;">
                   <button type="submit" id="btnGiphySubmit" class="Giphy_Submit"> Search for GIFs </button>
                  <%-- <input type="reset" id="btnGiphyReset" value="Reset"/>--%>
               </td>
               <td style ="text-align:center;background-color:black; color:white; border:outset;border-width:1px; height:26px;">
                   
               </td>
            </tr>
        </table>
     <br />
     <br />
  
    <asp:Panel ID="pnlGiphyCont" runat="server">
    <div id="divGiphyContainer" style ="font-family:Arial,Courier New;height:300px;border:solid;border-width:1px; Width:65%; overflow:auto;font-size:9pt;text-align:center;margin-top:110px;">
     
     <!-- Slideshow container -->
    <div class="slideshow-container">

     <div class="mySlides fade">
        <div class="numbertext">1 / 5</div>
        <img id="img1" src="~/edit_plus.png" style="width:400px; height:200px"/>
      </div>

      <div class="mySlides fade">
        <div class="numbertext">2 / 5</div>
        <img id="img2" src="" style="width:400px; height:200px"/>
      </div>

      <div class="mySlides fade">
        <div class="numbertext">3 / 5</div>
        <img id="img3" src="" style="width:400px; height:200px"/>
     </div>
      <div class="mySlides fade">
        <div class="numbertext">4 / 5</div>
        <img id="img4" src="" style="width:400px; height:200px"/>
      </div>
      <div class="mySlides fade">
        <div class="numbertext">5 / 5</div>
        <img id="img5" src="" style="width:400px; height:200px"/>
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
                    <asp:Button ID="btnGiphySave" runat="server" Text="SAVE TO PRODUCTS"  OnClick="btnGiphySave_Click" OnClientClick="return " Font-Size="12px" Visible="true" ToolTip="Save Giphy Image." /> 
                </td>
             </tr>
         </table>

    </asp:Panel>
  
 
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
