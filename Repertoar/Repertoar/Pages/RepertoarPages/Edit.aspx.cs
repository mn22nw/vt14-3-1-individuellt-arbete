using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class Edit : System.Web.UI.Page
    {
        private Service _service;
        string Instrument;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuccessUpdate"] as bool? == true)
            {
                MessagePlaceHolder.Visible = true;
                Session.Remove("Success");
            }

            if (!IsPostBack)
            {
               BindList();
            }
            // http://www.dotnetfox.com/articles/how-to-bind-data-to-radiobuttonlist-control-in-Asp-Net-using-C-Sharp-1048.aspx#sthash.yt1EPaNZ.dpuf

        }

        public Material ContactFormView_GetSong([RouteData]int id)
        {
            try
            {
                return Service.GetSong(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då låten hämtades vid redigering.");
                return null;
            }
        }

        public void ContactFormView_UpdateSong(int MID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var material = Service.GetSong(MID);
                    if (material == null)
                    {
                        // Hittade inte kunden.
                        ModelState.AddModelError(String.Empty,
                            String.Format("Låten med id {0} hittades inte.", MID));
                        return;
                    }

                    if (TryUpdateModel(material))
                    {
                        Service.SaveSong(material);

                        Page.SetTempData("SuccessMessage", "Låten uppdaterades.");
                        Response.RedirectToRoute("SongListing");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Fel inträffade då låten skulle uppdateras.");
                }
            }
        }

        private void BindList()
        {   

          List<string> myinstruments = new List<string>();

          myinstruments.Add("Piano");
          myinstruments.Add("Gitarr");
          myinstruments.Sort();

          
          rb.DataSource = myinstruments;
          rb.DataBind();
          rb.Items.FindByValue("Piano").Selected = true; //http://stackoverflow.com/questions/5662113/set-radiobuttonlist-selected-from-codebehind

           /* DataSet ds = new DataSet();
           
            return Service.GetInstrument(id);
            SqlDataAdapter adp = new SqlDataAdapter(cmdstr, CreateConnection());
            adp.Fill(ds);
            rblInstrument.DataSource = ds;
            rblInstrument.DataTextField = "Intrument";
            rblInstrument.DataValueField = "id";
            rblInstrument.DataBind();*/
        }

        private SqlConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           // lblValue.Text = rblCountry.SelectedItem.ToString();
        }
    }
}