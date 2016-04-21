using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Model;
using UnitOfWork.Infrastructure;


namespace UnitOfWork.Repository.EF
{
    public class RacunRepositoryEF : IRacunRepository, IUnitOfWorkRepository
    {
        private IUnitOfWork _unitOfWork;

        public RacunRepositoryEF(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public List<Model.Racun> PronadjiSve()
        {
            using (ProbaEntities db = new ProbaEntities())
            {
                List<Model.Racun> rac = new List<Model.Racun>();
                try
                {
                    IEnumerable<Racun> racuni = db.Racun.ToList();
                    foreach (Racun item in racuni)
                    {
                        rac.Add(new Model.Racun { 
                            Id = item.Id,
                            Ime = item.Ime,
                            StanjeRacuna = item.StanjeRacuna
                        });
                    }
                   
                }
                catch (Exception)
                {

                    throw;
                }
                finally {
                    db.Dispose();
                }
                return rac;
            }
        }

        public Model.Racun PronadjiRacun(int Id)
        {
            using (ProbaEntities db = new ProbaEntities())
            {
                Model.Racun rac = new Model.Racun();
                try
                {
                    Racun racun = db.Racun.Find(Id);
                   
                    rac.Id = racun.Id;
                    rac.Ime = racun.Ime;
                    rac.StanjeRacuna = racun.StanjeRacuna;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    db.Dispose();
                }
                return rac;
            }
        }

        public void Sacuvaj(Model.Racun racun)
        {
            _unitOfWork.RegistrujPromenu(racun, this);
        }

        public void Dodaj(Model.Racun racun)
        {
            _unitOfWork.RegistrujNovi(racun, this);
        }

        public void Obrisi(Model.Racun racun)
        {
            _unitOfWork.RegistrujBrisanje(racun, this);
        }

        public void PersistCreateionOf(IAggregateRoot entitet)
        {
            throw new NotImplementedException();
        }

        public void PersistUpdateOf(IAggregateRoot entitet)
        {
            using (ProbaEntities db = new ProbaEntities())
            {
                Racun racun = entitet as Racun;

                try
                {
                    db.Racun.Add(racun);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                finally {
                    db.Dispose();
                   
                }
            }
        }

        public void PersistDeletionOf(IAggregateRoot entitet)
        {
            throw new NotImplementedException();
        }
    }
}
