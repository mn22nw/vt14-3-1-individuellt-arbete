﻿using Repertoar.App_GlobalResourses;
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

        #region Material CRUD-metoder
        public void SaveSong(Material material, string KompNamn)
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
                if (material.MID == 0) // Ny post om MID är 0!
                {
                    MaterialDAL.InsertSong(material, KompNamn);
                }
                else
                {
                    MaterialDAL.UpdateSong(material);
                }

               }
            catch 
            {
                throw new ApplicationException(Strings.Song_Inserting_Error_IU);
            }
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
        #endregion
        #region Kategory (C)R(UD)-metoder

        /// <summary>
        /// Hämtar alla kategorier.
        /// </summary>
        /// <returns>Ett List-objekt innehållande referenser till Kategori-objekt.</returns>
        public IEnumerable<Kategori> GetKategories(bool refresh = false)
        {
            // Försöker hämta lista med kategorier från cachen.
            var kategories = HttpContext.Current.Cache["Kategory"] as IEnumerable<Kategori>;

            // Om det inte finns det en lista med kategorier...
            if (kategories == null || refresh)
            {
                // ...hämtar då lista med kontakttyper...
                kategories = KategoriDAL.GetKategories();

                // ...och cachar dessa. List-objektet, inklusive alla kategori-objekt, kommer att cachas 
                // under 10 minuter, varefter de automatiskt avallokeras från webbserverns primärminne.
                HttpContext.Current.Cache.Insert("Kategory", kategories, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            // Returnerar listan med kategorier.
            return kategories;
        }

        #endregion
        #region Kompositör (C)R(UD)-metoder

        public IEnumerable<Kompositör> GetComposers(bool refresh = false)
        {
            var composers = HttpContext.Current.Cache["Composer"] as IEnumerable<Kompositör>;

            // Om det inte finns det en lista med kategorier...
            if (composers == null || refresh)
            {
                // ...hämtar då lista med kontakttyper...
                composers = ComposerDAL.GetComposers();

                // ...och cachar dessa. List-objektet, inklusive alla kategori-objekt, kommer att cachas 
                // under 10 minuter, varefter de automatiskt avallokeras från webbserverns primärminne.
                HttpContext.Current.Cache.Insert("Composer", composers, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            // Returnerar listan med kategorier.
            return composers;
        }

        #endregion
        
    }
}