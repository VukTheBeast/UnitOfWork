using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using UnitOfWork.Model;
using UnitOfWork.Infrastructure;

namespace UnitOfWork.Repository
{
    public class RacunRepository : IRacunRepository, IUnitOfWorkRepository
    {
        private IUnitOfWork _unitOfWork;
        static private string _connectionString = "data source=SRVLJR;Initial Catalog=Proba;User ID=sa;Password=Vladaibajka07";
        //DI
        public RacunRepository(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public void Sacuvaj(Racun racun)
        {
            _unitOfWork.RegistrujPromenu(racun,this);
        }

        public void Dodaj(Racun racun)
        {
            _unitOfWork.RegistrujNovi(racun, this);
        }

        public void Obrisi(Racun racun)
        {
            _unitOfWork.RegistrujBrisanje(racun, this);
        }

        public void PersistCreateionOf(IAggregateRoot entitet)
        {
           // ADO.NET kod za kreiranje entiteta
           
            
        }

        public void PersistUpdateOf(IAggregateRoot entitet)
        {
            // ADO.NET kod za azuriranje entiteta
            SqlConnection conn = new SqlConnection(_connectionString);
            String sqlUpit = "UPDATE Racun SET StanjeRacuna = @StanjeRacuna WHERE Id =@Id";

            SqlCommand cmd = new SqlCommand(sqlUpit, conn);
            cmd.Parameters.AddWithValue("@StanjeRacuna", (entitet as Racun).StanjeRacuna);
            cmd.Parameters.AddWithValue("@Id", ((Racun)entitet).Id); //(entitet as Racun).Id);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void PersistDeletionOf(IAggregateRoot entitet)
        {
            // ADO.NET kod za brisanje entiteta
        }

        public List<Racun> PronadjiSve()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            string sqlUpit = "Select * from Racun";

            SqlCommand cmd = new SqlCommand(sqlUpit, conn);
            SqlDataReader reader;
            List<Racun> ListRacun = new List<Racun>();
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Racun racun = new Racun();
                    racun.Id = Convert.ToInt16(reader["Id"]);
                    racun.StanjeRacuna = Convert.ToDecimal(reader["StanjeRacuna"]);

                    ListRacun.Add(racun);

                }
                
            }
            catch (Exception e)
            {

                throw e;
            }
            finally {
                conn.Close();
            }
            return ListRacun;
        }

        public Racun PronadjiRacun(int Id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            string sqlUpit = "Select * from Racun where Id = @Id";

            SqlCommand cmd = new SqlCommand(sqlUpit, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader reader;
            Racun racun = new Racun();
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    racun.Id = Convert.ToInt16(reader["Id"]);
                    racun.StanjeRacuna = Convert.ToDecimal(reader["StanjeRacuna"]);
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Close();
            }
            return racun;
        }
    }
}
