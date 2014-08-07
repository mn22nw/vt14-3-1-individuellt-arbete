﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Configuration;
using Repertoar.MODEL;
using Repertoar.MODEL.DAL;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Repertoar.MODEL.DAL
{
    public class MaterialDAL : DALBase
    {
        public void DeleteSong(int MID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Usp_DeleteSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = MID;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }

                catch
                {
                    throw new ApplicationException("Det gick inte att ta bort låten från databasen");
                }
            }
        }

        public Material GetSongById(int MID)
        {
            using (var conn = CreateConnection())  // å
            {
                try
                {
                    var cmd = new SqlCommand("appSchema_GetSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = MID;

                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    using (var reader = cmd.ExecuteReader())
                    {
                        var MIDIndex = reader.GetOrdinal("MID"); // ger tillbaka ett heltal där ContactId finns
                        var KaIdIndex = reader.GetOrdinal("KaId");
                        var KompIndex = reader.GetOrdinal("KompID");
                        var NameIndex = reader.GetOrdinal("Namn");
                        var LevelIndex = reader.GetOrdinal("Svårighetsgrad");
                        var GenreIndex = reader.GetOrdinal("Genre");
                        var StatusIndex = reader.GetOrdinal("StatusSong");
                        var InstrumentIndex = reader.GetOrdinal("Instrument");
                        var DateIndex = reader.GetOrdinal("Datum");
                        var NoteIndex = reader.GetOrdinal("Anteckning");

                        if (reader.Read())
                        {
                            return new Material
                            {
                                MID = reader.GetInt32(MIDIndex),
                                KaId = reader.GetInt32(KaIdIndex),
                                KompID = reader.GetInt32(KompIndex),
                                Namn = reader.GetString(NameIndex),
                                Level = reader.GetInt32(LevelIndex),
                                Genre = reader.GetString(GenreIndex),
                                Status = reader.GetString(StatusIndex),
                                Instrument = reader.GetString(InstrumentIndex),
                                Datum = reader.GetDateTime(DateIndex),
                                Anteckning = reader.GetString(NoteIndex)
                            };
                        }
                    }

                    return null;
                }

                catch
                {
                    throw new ApplicationException("Det gick inte att hämta ut låten  från databasen");
                }
            }
        }

        public IEnumerable<Material> GetSongs()
        {
            using (var conn = CreateConnection())  // å
            {
                //  try
                //{
                var materials = new List<Material>(100);   // Object som håller ordning på de objekt som ska instansieras

                var cmd = new SqlCommand("Repertoar_GetSongs", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                using (var reader = cmd.ExecuteReader())
                {
                    var MIDIndex = reader.GetOrdinal("MID"); // ger tillbaka ett heltal där MID finns
                    var KaIdIndex = reader.GetOrdinal("KaId");
                    var KompIndex = reader.GetOrdinal("KompID");
                    var NameIndex = reader.GetOrdinal("Namn");
                    var LevelIndex = reader.GetOrdinal("Svårighetsgrad");
                    var GenreIndex = reader.GetOrdinal("Genre");
                    var StatusIndex = reader.GetOrdinal("StatusSong");
                    var InstrumentIndex = reader.GetOrdinal("Instrument");
                    var DateIndex = reader.GetOrdinal("Datum");
                    var NoteIndex = reader.GetOrdinal("Anteckning");

                    while (reader.Read()) //så länge metoden read returnerar true finns det data att hämta. 
                    {
                        materials.Add(new Material
                        {

                            MID = reader.GetInt32(MIDIndex),
                            KaId = reader.GetInt32(KaIdIndex),
                            KompID = reader.GetInt32(KompIndex),
                            Namn = reader.GetString(NameIndex),
                            Level = reader.GetByte(LevelIndex),
                            Genre = reader.GetString(GenreIndex),
                            Status = reader.GetString(StatusIndex),
                            Instrument = reader.GetString(InstrumentIndex),
                            Datum = reader.GetDateTime(DateIndex),
                            Anteckning = reader.GetString(NoteIndex)
                        });
                    }
                    materials.TrimExcess(); // krymper till det faktiskta antalet element som är utnyttjat 
                }
                return materials;

                /*  }
                  catch
                  {
                      throw new ApplicationException("Det gick inte att hämta ut kontakterna från databasen");
                  }*/
            }
        }

        public IEnumerable<Material> GetSongsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            totalRowCount = GetSongs().Count();
            return GetSongs().Skip(startRowIndex).Take(maximumRows);
        }

        public void InsertSong(Material material)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Usp.NewSong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@KaID", SqlDbType.Int, 4).Value = material.KaId;
                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = material.KompID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 30).Value = material.Namn;
                    cmd.Parameters.Add("@Svårighetsgrad", SqlDbType.TinyInt).Value = material.Level;
                    cmd.Parameters.Add("@Genre", SqlDbType.VarChar, 20).Value = material.Genre;
                    cmd.Parameters.Add("@StatusSong", SqlDbType.VarChar, 15).Value = material.Status;
                    cmd.Parameters.Add("@Intrument", SqlDbType.VarChar, 25).Value = material.Instrument;
                    cmd.Parameters.Add("@Datum", SqlDbType.SmallDateTime).Value = material.Datum;
                    cmd.Parameters.Add("@Anteckning", SqlDbType.VarChar, 4000).Value = material.Anteckning;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    //ExecuteNonQuery används för att exekvera den lp. 
                    cmd.ExecuteNonQuery();

                    material.MID = (int)cmd.Parameters["@MID"].Value;
                }

                catch
                {
                    throw new ApplicationException("Det gick inte att lägga till låten i databasen");
                }
            }
        }

        public void UpdateSong(Material material)
        {
            using (var conn = CreateConnection())  // å
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MID", SqlDbType.Int, 4).Value = material.MID;
                    cmd.Parameters.Add("@KaID", SqlDbType.Int, 4).Value = material.KaId;
                    cmd.Parameters.Add("@KompID", SqlDbType.Int, 4).Value = material.KompID;
                    cmd.Parameters.Add("@Namn", SqlDbType.VarChar, 30).Value = material.Namn;
                    cmd.Parameters.Add("@Svårighetsgrad", SqlDbType.TinyInt).Value = material.Level;
                    cmd.Parameters.Add("@Genre", SqlDbType.VarChar, 20).Value = material.Genre;
                    cmd.Parameters.Add("@StatusSong", SqlDbType.VarChar, 15).Value = material.Status;
                    cmd.Parameters.Add("@Intrument", SqlDbType.VarChar, 25).Value = material.Instrument;
                    cmd.Parameters.Add("@Datum", SqlDbType.SmallDateTime).Value = material.Datum;
                    cmd.Parameters.Add("@Anteckning", SqlDbType.VarChar, 4000).Value = material.Anteckning;
                    conn.Open();  // ska inte vara öppen mer än vad som behövs, därför läggs den in här senare. 

                    //ExecuteNonQuery används för att exekvera den lp. 
                    cmd.ExecuteNonQuery();
                }

                catch
                {
                    throw new ApplicationException("Det gick inte att uppdatera låten i databasen");
                }
            }
        }
    }
}