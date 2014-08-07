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
        private MaterialDAL _materialDAL;

        private MaterialDAL MaterialDAL
        {
            get { return _materialDAL ?? (_materialDAL = new MaterialDAL()); }
        }

        public void DeleteContact(Material material)
        {
            //kod
        }

        public void DeleteSong(int MID)
        {
            MaterialDAL.DeleteSong(MID);
        }

        public Material GetSong(int MID)
        {
            return MaterialDAL.GetSongById(MID);
        }

        public IEnumerable<Material> GetSongs()
        {
            return MaterialDAL.GetSongs();

        }

        public IEnumerable<Material> GetSongsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return MaterialDAL.GetSongsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }


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