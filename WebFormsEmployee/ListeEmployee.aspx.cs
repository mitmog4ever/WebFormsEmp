using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsEmployee.DB;

namespace WebFormsEmployee
{
    public partial class ListeEmployee : System.Web.UI.Page
    {
        static bool sqlNative = false;
        CompanyDBEntities contxt = new CompanyDBEntities();
        string connectionString = @"data source=.\SQLEXPRESS;initial catalog=CompanyDB;integrated security=True;MultipleActiveResultSets=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillGridViewWithNativeSql();
                FillGridViewWithEntityFramework();
            }
        }
        void FillGridViewWithNativeSql()
        {
            List<Employee> lst_emp = new List<Employee>();
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                SqlCommand sqlcmd = new SqlCommand("Select * from Employees", sqlCon);
                using (SqlDataReader reader = sqlcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lst_emp.Add(new Employee
                        {
                            id_emp = Convert.ToInt32(reader["id_emp"].ToString()),
                            nom_emp = reader["nom_emp"].ToString(),
                            prenom_emp = reader["prenom_emp"].ToString(),
                            date_recrute_emp = Convert.ToDateTime(reader["date_recrute_emp"].ToString()),
                            id_dep = Convert.ToInt32(reader["id_dep"].ToString()),
                            Salaire_emp = Convert.ToDouble(reader["Salaire_emp "].ToString())
                        });
                    }
                }
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Employees", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                DropDownList dd = (DropDownList)GVEmployee.FooterRow.FindControl("dddeprt_footer");
                BindDeprt(dd, PopulateDeprt());
                GVEmployee.DataSource = dtbl;
                GVEmployee.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                GVEmployee.DataSource = dtbl;
                GVEmployee.DataBind();
                GVEmployee.Rows[0].Cells.Clear();
                GVEmployee.Rows[0].Cells.Add(new TableCell());
                GVEmployee.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                GVEmployee.Rows[0].Cells[0].Text = "No data Found...!";
                GVEmployee.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }



        }

        void FillGridViewWithEntityFramework()
        {
            var listDtoEmp = new List<DtoEmployee>();
            var list_emp = contxt.Employees.OrderBy(x => x.id_emp).ToList();
            foreach(var emp in list_emp)
            {
                listDtoEmp.Add(new DtoEmployee
                {
                    id_emp = emp.id_emp,
                    nom_emp = emp.nom_emp,
                    prenom_emp = emp.prenom_emp,
                    Salaire_emp = emp.Salaire_emp,
                    id_dep = emp.id_dep,
                    nom_dep = emp.Departement.nom_dep,
                    date_recrute_emp = emp.date_recrute_emp,
                    tele_emp = emp.tele_emp
                    
                });
            }
            DataTable dtbl = ToDataTable(listDtoEmp);
            if (dtbl.Rows.Count > 0)
            {
                GVEmployee.DataSource = dtbl;
                GVEmployee.DataBind();
                DropDownList dd = GVEmployee.FooterRow.FindControl("dddeprt_footer") as DropDownList;
                BindDeprt(dd, PopulateDeprt());
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                GVEmployee.DataSource = dtbl;
                GVEmployee.DataBind();
                GVEmployee.Rows[0].Cells.Clear();
                GVEmployee.Rows[0].Cells.Add(new TableCell());
                GVEmployee.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                GVEmployee.Rows[0].Cells[0].Text = "No data Found...!";
                GVEmployee.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }



        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected void GVEmployee_RowInsert(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    if (sqlNative)
                    {
                        using (SqlConnection sqlcon = new SqlConnection())
                        {
                            string insrtquery = "Insert into Employees (nom_emp,prenom_emp,Salaire_emp,date_recrute_emp,tele_emp,id_dep) Values(@nom_emp,@prenom_emp,@Salaire_emp,@date_recrute_emp,@tele_emp,@id_dep)";
                            SqlCommand slqcmd = new SqlCommand(insrtquery, sqlcon);
                            //Text.trim() : removes all leading and trailing whitespace from text value
                            slqcmd.Parameters.AddWithValue("@nom_emp", (GVEmployee.FooterRow.FindControl("txt_nom_emp_footer") as TextBox).Text.Trim());
                            slqcmd.Parameters.AddWithValue("@prenom_emp", (GVEmployee.FooterRow.FindControl("txt_prenom_emp_footer") as TextBox).Text.Trim());
                            slqcmd.Parameters.AddWithValue("@Salaire_emp", (GVEmployee.FooterRow.FindControl("txt_Salaire_emp_footer") as TextBox).Text.Trim());
                            slqcmd.Parameters.AddWithValue("@date_recrute_emp", (GVEmployee.FooterRow.FindControl("txt_date_recrute_emp_footer") as TextBox).Text.Trim());
                            slqcmd.Parameters.AddWithValue("@tele_emp", (GVEmployee.FooterRow.FindControl("txt_tele_emp_footer") as TextBox).Text.Trim());
                            DropDownList dddeprt = (DropDownList)GVEmployee.FooterRow.FindControl("dddeprt_footer");
                            slqcmd.Parameters.AddWithValue("@id_dep", dddeprt.SelectedValue);
                            slqcmd.ExecuteNonQuery();
                            FillGridViewWithNativeSql();
                            lblSuccessMessage.Text = "New Record Added";
                            lblEroorMessage.Text = "";
                        }
                    }
                    else
                    {
                        DropDownList dddeprt = (DropDownList)GVEmployee.FooterRow.FindControl("dddeprt_footer");
                        contxt.Employees.Add(new Employee
                        {
                            nom_emp = (GVEmployee.FooterRow.FindControl("txt_nom_emp_footer") as TextBox).Text.Trim(),
                            prenom_emp = (GVEmployee.FooterRow.FindControl("txt_prenom_emp_footer") as TextBox).Text.Trim(),
                            Salaire_emp = Convert.ToDouble((GVEmployee.FooterRow.FindControl("txt_Salaire_emp_footer") as TextBox).Text.Trim()),
                            date_recrute_emp = Convert.ToDateTime((GVEmployee.FooterRow.FindControl("txt_date_recrute_emp_footer") as TextBox).Text.Trim()),
                            tele_emp = Convert.ToDouble((GVEmployee.FooterRow.FindControl("txt_tele_emp_footer") as TextBox).Text.Trim()),
                            id_dep = Convert.ToInt32(dddeprt.SelectedValue.Trim())

                        });
                        contxt.SaveChanges();
                        FillGridViewWithEntityFramework();
                        lblSuccessMessage.Text = "New Record Added";
                        lblEroorMessage.Text = "";
                    }

                }

            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblEroorMessage.Text = ex.Message;
            }
        }

        protected void GVEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GVEmployee.EditIndex = e.NewEditIndex;
            FillGridViewWithEntityFramework();
            DropDownList dd = (DropDownList)GVEmployee.Rows[e.NewEditIndex].FindControl("dddeprt");
            BindDeprt(dd, PopulateDeprt());
        }

        protected void GVEmployee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVEmployee.EditIndex = -1;
            FillGridViewWithEntityFramework();
        }

        protected void GVEmployee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            try
            {
                if (sqlNative)
                {
                    using (SqlConnection sqlcon = new SqlConnection())
                    {
                        string Updatequery = "Update Employees nom_emp=@nom_emp,prenom_emp=@prenom_emp,Salaire_emp=@Salaire_emp,date_recrute_emp=@date_recrute_emp,tele_emp=@tele_emp,id_dep=@id_dep Where id_emp = @id_emp";
                        SqlCommand slqcmd = new SqlCommand(Updatequery, sqlcon);
                        //Text.trim() : removes all leading and trailing whitespace from text value
                        slqcmd.Parameters.AddWithValue("@id_emp", (GVEmployee.DataKeys[e.RowIndex].Value.ToString()));
                        slqcmd.Parameters.AddWithValue("@nom_emp", (GVEmployee.Rows[e.RowIndex].FindControl("txt_nom_emp") as TextBox).Text.Trim());
                        slqcmd.Parameters.AddWithValue("@prenom_emp", (GVEmployee.Rows[e.RowIndex].FindControl("txt_prenom_emp") as TextBox).Text.Trim());
                        slqcmd.Parameters.AddWithValue("@Salaire_emp", (GVEmployee.Rows[e.RowIndex].FindControl("txt_Salaire_emp") as TextBox).Text.Trim());
                        slqcmd.Parameters.AddWithValue("@date_recrute_emp", (GVEmployee.Rows[e.RowIndex].FindControl("txt_date_recrute_emp") as TextBox).Text.Trim());
                        slqcmd.Parameters.AddWithValue("@tele_emp", (GVEmployee.Rows[e.RowIndex].FindControl("txt_tele_emp") as TextBox).Text.Trim());
                        DropDownList dddeprt = (DropDownList)GVEmployee.Rows[e.RowIndex].FindControl("dddeprt");
                        slqcmd.Parameters.AddWithValue("@id_dep", dddeprt.SelectedValue.Trim());
                        slqcmd.ExecuteNonQuery();
                        GVEmployee.EditIndex = -1;
                        FillGridViewWithNativeSql();
                        lblSuccessMessage.Text = "Selected Record Updated";
                        lblEroorMessage.Text = "";
                    }
                }
                else
                {
                    int Id = Convert.ToInt32((GVEmployee.DataKeys[e.RowIndex].Value.ToString()));
                    var Upemp = contxt.Employees.Find(Id);
                    Upemp.nom_emp = (GVEmployee.Rows[e.RowIndex].FindControl("txt_nom_emp") as TextBox).Text.Trim();
                    Upemp.prenom_emp = (GVEmployee.Rows[e.RowIndex].FindControl("txt_prenom_emp") as TextBox).Text.Trim();
                    Upemp.Salaire_emp = Convert.ToDouble((GVEmployee.Rows[e.RowIndex].FindControl("txt_Salaire_emp") as TextBox).Text.Trim());
                    Upemp.date_recrute_emp = Convert.ToDateTime((GVEmployee.Rows[e.RowIndex].FindControl("txt_date_recrute_emp") as TextBox).Text.Trim());
                    Upemp.tele_emp = Convert.ToDouble((GVEmployee.Rows[e.RowIndex].FindControl("txt_tele_emp") as TextBox).Text.Trim());
                    DropDownList dddeprt = (DropDownList)GVEmployee.Rows[e.RowIndex].FindControl("dddeprt");
                    Upemp.id_dep = Convert.ToInt32(dddeprt.SelectedValue.Trim());
                    contxt.SaveChanges();
                    GVEmployee.EditIndex = -1;
                    FillGridViewWithEntityFramework();
                    lblSuccessMessage.Text = "Record UpDated";
                    lblEroorMessage.Text = "";
                }



            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblEroorMessage.Text = ex.Message;
            }

        }

        protected void GVEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (sqlNative)
                {
                    using (SqlConnection sqlcon = new SqlConnection())
                    {
                        string deletequery = "delete from Employees Where id_emp = @id_emp";
                        SqlCommand slqcmd = new SqlCommand(deletequery, sqlcon);
                        //Text.trim() : removes all leading and trailing whitespace from text value
                        slqcmd.Parameters.AddWithValue("@id_dep", GVEmployee.DataKeys[e.RowIndex].Value.ToString());
                        slqcmd.ExecuteNonQuery();
                        GVEmployee.EditIndex = -1;
                        FillGridViewWithNativeSql();
                        lblSuccessMessage.Text = "Record Deleted";
                        lblEroorMessage.Text = "";
                    }
                }
                else
                {
                    int Id = Convert.ToInt32((GVEmployee.DataKeys[e.RowIndex].Value.ToString()));
                    var Upemp = contxt.Employees.Find(Id);
                    contxt.Employees.Remove(Upemp);
                    contxt.SaveChanges();
                    FillGridViewWithEntityFramework();
                    lblSuccessMessage.Text = "Record Deleted";
                    lblEroorMessage.Text = "";
                }



            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblEroorMessage.Text = ex.Message;
            }
        }
        private List<Departement> PopulateDeprt()
        {
             return contxt.Departements.OrderBy(a => a.id_dep).ToList();
            
        }
        private void BindDeprt(DropDownList dddeprt, List<Departement> listdeprt)
        {

            dddeprt.Items.Clear();
            dddeprt.Items.Add(new ListItem { Text = "Select Departement", Value = "0" });
            dddeprt.AppendDataBoundItems = true;
             
            dddeprt.DataTextField = "nom_dep";
            dddeprt.DataValueField = "id_dep";
            dddeprt.DataSource = listdeprt;
            dddeprt.DataBind();
        }
    }
}