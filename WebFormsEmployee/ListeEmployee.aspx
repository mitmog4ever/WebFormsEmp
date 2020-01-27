<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListeEmployee.aspx.cs" Inherits="WebFormsEmployee.ListeEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="Row">
        <div class="col-md-6">
            <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GVEmployee" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="id_emp"
                ShowHeaderWhenEmpty="true"
                OnRowCommand ="GVEmployee_RowInsert"
                OnRowEditing ="GVEmployee_RowEditing"
                OnRowCancelingEdit ="GVEmployee_RowCancelingEdit"
                OnRowUpdating="GVEmployee_RowUpdating"
                OnRowDeleting="GVEmployee_RowDeleting"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="364px" Width="876px"  style="margin-right: 20px" >
                <%-- Theme properties--%>                
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <Columns>
                    <%-- *******************Employee ID Column*************************** --%>
<%--                    <asp:TemplateField HeaderText ="Employee ID">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("id_emp") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_id_emp" Text='<%# Eval("id_emp") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_id_emp_footer"  runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>         --%> 
                    <%-- ********************Employee Name Column************************** --%>
                    <asp:TemplateField HeaderText ="Employee Nom">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("nom_emp") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_nom_emp" Text='<%# Eval("nom_emp") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_nom_emp_footer"  runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField> 
                    <%-- ********************Employee Prenom Column************************** --%>
                    <asp:TemplateField HeaderText ="Employee Prenom">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("prenom_emp") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_prenom_emp" Text='<%# Eval("prenom_emp") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_prenom_emp_footer"  runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>  
                    <%-- ********************Employee Salary Column************************** --%>
                    <asp:TemplateField HeaderText ="Employee Salary">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Salaire_emp") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Salaire_emp" Text='<%# Eval("Salaire_emp") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_Salaire_emp_footer"  runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField> 
                    <%-- ********************Employee Recrute date Column************************** --%>
                    <asp:TemplateField HeaderText ="Employee Recrute date">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("date_recrute_emp") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_date_recrute_emp" Text='<%# Eval("date_recrute_emp") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_date_recrute_emp_footer"  runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>  
                    <%-- ********************Employee Tele Column************************** --%>
                    <asp:TemplateField HeaderText ="Employee Tele">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("tele_emp") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_tele_emp" Text='<%# Eval("tele_emp") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_tele_emp_footer"  runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%-- ********************Employee Id dep Column************************** --%>
                    <asp:TemplateField HeaderText ="Employee departement">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("nom_dep") %>' runat="server"/>
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:DropDownList ID="dddeprt" runat="server">
                                 <asp:ListItem Text="Select Departement" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="dddeprt_footer" runat="server">
                                 <asp:ListItem Text="Select Departement" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%-- ********************Employee Action************************** --%>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />                           
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                       </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px"/>
                        </FooterTemplate>
                    </asp:TemplateField>                    
                </Columns>
            </asp:GridView>
            <br/>
            <asp:label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br/>
            <asp:label ID="lblEroorMessage" Text="" runat="server" ForeColor="Red" />
        </div>
    </form>
        </div>
    </div>
    
</body>
</html>
