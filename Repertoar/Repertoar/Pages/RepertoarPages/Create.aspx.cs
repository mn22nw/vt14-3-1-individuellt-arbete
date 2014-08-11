using Repertoar.App_GlobalResourses;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            
           // if (!IsPostBack)
            //{
                BindList();
            //}
        
        }
        #region Bindlist för Radiobuttonlists och DropDownLists
        private void BindList()
        { //TODO try catch
            rblKategori.DataSource = Service.GetKategories();
            rblKategori.DataTextField = "Namn";
            rblKategori.DataValueField = "KaID";
           // rblKategori.AppendDataBoundItems = true;
            rblKategori.DataBind();
            
            rblKategori.SelectedIndex = 0;
            if (rblKategori.SelectedIndex == -1) // -1 är om inget är valt
            {
                rblKategori.SelectedIndex = 0; 
            }

            if (rblStatus.SelectedIndex == -1) 
            {
                rblStatus.SelectedIndex = 0; 
            }

            ddlComposers.DataSource = Service.GetComposers();
            ddlComposers.DataTextField = "Namn";
            ddlComposers.DataValueField = "KompID";
            ddlComposers.DataBind();

            ddlInstruments.SelectedValue = "Piano";

            ddlGenre.SelectedValue = "Klassisk";
        }
        #endregion

        public IEnumerable<Material> MaterialListView_GetData()
        {
            return Service.GetSongs();
        }

        public IEnumerable<Kategori> KategoriDropDownList_GetData()
        {
            return Service.GetKategories();
        }
        //TODO kolla om denna behövs
        #region KAN BEHÖVAS?  
        /*  protected void MaterialListView_ItemDataBound(object sender, ListViewItemEventArgs e)
         {
             var label = e.Item.FindControl("KategoryNameLabel") as Label;
             if (label != null)
             {
                 // Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas och...
                 var material = (Material)e.Item.DataItem;

                 // ...som sedan kan användas för att hämta ett ("cachat") kategoriobjekt...
                 var Kategori = Service.GetKategories()
                     .Single(ka => ka.KaID == material.KaID);

                 // ...så att en beskrivning av kategori kan presenteras; ex: Kategori:Not
                 label.Text = String.Format(label.Text, Kategori.Namn);
             }

             var label2 = e.Item.FindControl("ComposerNameLabel") as Label;
             if (label2 != null)
             {
                 var material2 = (Material)e.Item.DataItem;

                 var Composer = Service.GetComposers()
                     .Single(co => co.KompID == material2.KompID);
 
                 label2.Text = String.Format(label2.Text, Composer.Namn);
             }
         }*/
        #endregion

        public void MaterialListView_InsertItem(object sender, EventArgs e)
        { 
             if (Page.ModelState.IsValid)
             {  
                 Material material = new Material();
                
                 
               //  try
               //  {  // Anger värden från listorna
                     string Namn = Name.Text;
                     string kaID = rblKategori.SelectedValue;
                     Debug.WriteLine(kaID +"KaID");
                     string Status= rblStatus.SelectedItem.Value;
                     Debug.WriteLine(Status);
                     string Genre = ddlGenre.SelectedItem.Value;
                     Debug.WriteLine(Genre);
                     string KompNamn = ddlComposers.SelectedItem.Value;
                     Debug.WriteLine(KompNamn);
                     int Level = Convert.ToInt32(ddlLevel.SelectedItem.Value);
                     Debug.WriteLine(Level);
                     string Instrument = ddlInstruments.SelectedItem.Value; ;
                    
                     material.MID = 0;
                     material.KaID = Convert.ToInt32(kaID);
                     material.Status = Status;
                     material.Namn = Namn;
                     material.Genre = Genre;
                     material.Level = Level;
                     material.Instrument = Instrument;
                     
                     Service.SaveSong(material, KompNamn);
                     Debug.WriteLine("intehiti!");
                     Page.SetTempData("SuccessMessage", Strings.Action_Song_Saved);
                     Response.RedirectToRoute("Default");
                     Context.ApplicationInstance.CompleteRequest();
                /* }
                 catch (Exception)
                 {
                     Page.ModelState.AddModelError(String.Empty, Strings.Song_Inserting_Error);
                 }*/
             }
         }


    }
}