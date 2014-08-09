using Repertoar.MODEL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL
{
    public class Service  // kommunicerar med dataatkomstlagret som man kan instansiera i presentationslagret
    {
        #region Fält

        private MaterialDAL _materialDAL;
        private KategoriDAL _kategoriDAL;
        private ComposerDAL _composerDAL;

        #endregion

        #region Egenskaper

        private MaterialDAL MaterialDAL
        {
            // Ett materialDAL-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _materialDAL ?? (_materialDAL = new MaterialDAL()); }
        }

        private KategoriDAL KategoriDAL
        {
            get { return _kategoriDAL ?? (_kategoriDAL = new KategoriDAL()); }
        }

        private ComposerDAL ComposerDAL
        {
            get { return _composerDAL ?? (_composerDAL = new ComposerDAL()); }
        }

        #endregion
       
        public void DeleteContact(Material material)
        {
            //kod
        }

        public void DeleteSong(int MID)
        {
            MaterialDAL.DeleteSong(MID);
        }

        public Material GetSongByID(int MID)
        {
            return MaterialDAL.GetSongById(MID);
        }

        public IEnumerable<Material> GetSongs()
        {
            return MaterialDAL.GetSongs();

        }

        #region Kategory (C)R(UD)-metoder

        /// <summary>
        /// Hämtar alla kontakttyper.
        /// </summary>
        /// <returns>Ett List-objekt innehållande referenser till ContactType-objekt.</returns>
        public IEnumerable<Kategori> GetKategories(bool refresh = false)
        {
            // Försöker hämta lista med kontakttyper från cachen.
            var kategories = HttpContext.Current.Cache["Kategory"] as IEnumerable<Kategori>;

            // Om det inte finns det en lista med kontakttyper...
            if (kategories == null || refresh)
            {
                // ...hämtar då lista med kontakttyper...
                kategories = KategoriDAL.GetKategories();

                // ...och cachar dessa. List-objektet, inklusive alla ContactType-objekt, kommer att cachas 
                // under 10 minuter, varefter de automatiskt avallokeras från webbserverns primärminne.
                HttpContext.Current.Cache.Insert("ContactTypes", kategories, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            // Returnerar listan med kontakttyper.
            return kategories;
        }

        #endregion

        public void SaveSong(Material material)
        {
            ICollection<ValidationResult> validationResults;


            if (!material.Validate(out validationResults))
            {
                Debug.WriteLine("iiig");
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                Debug.WriteLine(ex.Message);
                throw ex;
            }

            try
            {
                if (material.MID == 0) // Ny post om CustomerId är 0!
                {
                    MaterialDAL.InsertSong(material);
                }
                else
                {
                    MaterialDAL.UpdateSong(material);
                }

                //  else
                //  {
                // throw new ArgumentOutOfRangeException("Värdet måste vara större eller lika med 0");
                //   throw new ApplicationException("Det uppstod ett fel vid uppdatering/tillägg av kontakt");
                //  }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Det uppstod ett fel vid uppdatering/tillägg av låt");
            }
        }
    }
}