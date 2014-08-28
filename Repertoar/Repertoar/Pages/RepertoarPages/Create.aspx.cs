﻿using Repertoar.App_GlobalResourses;
using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class Create : System.Web.UI.Page
    {
        #region Service-objekt
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        #endregion

        public int MID { get; set; }
        public int KaID { get; set; }
        public int KompID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // BindList();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            generateDynamicControls();
        }


        protected void MaterialListView_InsertItem(object sender, EventArgs e)
        {
           
            // Creates new material and sets the values for each field. //
            Material materialInsert = new Material();
            if (KompID == 0)
            {
                Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
            }

            materialInsert.KompID = KompID;
            materialInsert.KaID = KaID;
            materialInsert.Namn = Name.Text;
            materialInsert.Level = Convert.ToInt32(ddlLevel.SelectedItem.Value);
            materialInsert.Genre = ddlGenre.SelectedItem.Value;
            materialInsert.Status = rblStatus.SelectedItem.Value;
            materialInsert.Instrument = ddlInstruments.SelectedItem.Value;
            materialInsert.Anteckning = TextBox2.Text;

            string KompNamn = "Kompnamn";
            Debug.WriteLine(KompID);
            Debug.WriteLine("KaID = " + KaID);
            Debug.WriteLine("namn= " + materialInsert.Namn);
            Debug.WriteLine("level = " + materialInsert.Level);
            Debug.WriteLine("genre = " + materialInsert.Genre);
            Debug.WriteLine(" Status = "+ materialInsert.Status);
            Debug.WriteLine("anteckning " + materialInsert.Anteckning);
            materialInsert.MID = Service.SaveSong(materialInsert, KompNamn);
            
            Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
            Response.RedirectToRoute("Details", new { id = materialInsert.MID });
            Context.ApplicationInstance.CompleteRequest();
        }


        public IEnumerable<Material> MaterialListView_GetData()
        {
            return Service.GetSongs();
        }

        public IEnumerable<Kategori> KategoriDropDownList_GetData()
        {
            return Service.GetKategories();
        }




        protected void ddlComposers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //när index ändras ska det skickas somehow till insertitem...
            //  Debug.WriteLine(Composer + " indexch");
            //Composer = ddlComposers.SelectedValue;

        }

        protected void rblKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kod här
        }

        #region databind
        /*   protected void ListView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
           {
               DropDownList DropDownList1 = ListView1.FindControl("ddlComposers") as DropDownList;
               //save ddl index to viewstate.
               ViewState["ddlindex"] = DropDownList1.SelectedIndex;
               ListView1.DataBind();

           }

         /*  protected void DetailsView1_DataBound(object sender, EventArgs e)
           {
            
                   DropDownList DropDownList1 = ListView1.FindControl("DropDownList1") as DropDownList;
                   DropDownList1.DataSource = Service.GetComposers();
                   DropDownList1.DataTextField = "Name";
                   DropDownList1.DataValueField = "KompID";
                   DropDownList1.DataBind();
                   //when you first load detailsview,the viewstate["ddlindex"] is null,when you insert item to detailsview,
                   //rebind your it,it will fire the databound event again.
                   if (ViewState["ddlindex"] != null)
                   {
                       DropDownList1.SelectedIndex = (int)ViewState["ddlindex"];
                   }

               }*/
        #endregion

        #region Dynamic Methods

        public void generateDynamicControls()
        {

            // lblValue.Text = string.Empty;

            ViewState["control"] = "dropdownlist";
            createDynamicDropDownListComposer();
            createDynamicRadioButtonListCategory();


        }

        public void createDynamicDropDownListComposer()
        {
            string _ddlId = "ddlComposer";
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            HtmlGenericControl td1 = new HtmlGenericControl("td");

            DropDownList ddl = new DropDownList();
            ddl.ID = _ddlId.Replace(" ", "").ToLower();
            ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
            ddl.AutoPostBack = true;
            ddl.DataSource = Service.GetComposers();
            ddl.DataTextField = "Namn";
            ddl.DataValueField = "KompID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-- Välj Kompositör --", string.Empty));
            ddl.SelectedIndex = 0;
            td1.Controls.Add(ddl);
            tr.Controls.Add(td1);
            PlaceHolder1.Controls.Add(tr);
            KompID = 0;
        }

        public void createDynamicRadioButtonListCategory()
        {
            string _RadioButtonListID = "rblComposer";
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            HtmlGenericControl td2 = new HtmlGenericControl("td");
            RadioButtonList RadioButtonList = new RadioButtonList();
            RadioButtonList.ID = _RadioButtonListID.Replace(" ", "").ToLower();
            RadioButtonList.SelectedIndexChanged += rbl_SelectedIndexChanged;
            RadioButtonList.DataSource = Service.GetKategories();
            RadioButtonList.DataTextField = "Namn";
            RadioButtonList.DataValueField = "KaID";
            RadioButtonList.DataBind();
            RadioButtonList.SelectedIndex = 0;
            td2.Controls.Add(RadioButtonList);
            tr.Controls.Add(td2);
            PlaceHolder2.Controls.Add(tr);
            KaID = 1;
        }

        public void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            if (ddl.SelectedIndex > 0)
            {
                KompID = Convert.ToInt32(ddl.SelectedItem.Value);
            }
        }

        public void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rbl = (RadioButtonList)sender;
            KaID = Convert.ToInt32(rbl.SelectedItem.Value);
            
        } 
        #endregion
    }
}