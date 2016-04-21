using System;
using System.Web;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Infrastructure;
using UnitOfWork.Model;
using System.Data;

namespace UnitOfWork.Repository.Xml
{
    public class RacunRepositoryXml : IRacunRepository, IUnitOfWorkRepository
    {
        //private string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/RacunData.xml");

        private string xmlData = "D:\\Master\\UnitOfWork\\UnitOfWork.Repository.EF\\App_Data\\RacunData.xml";

        private IUnitOfWork _unitOfWork;
        //DI
        public RacunRepositoryXml(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public List<Racun> PronadjiSve()
        {

            DataSet ds = new DataSet();

            ds.ReadXml(xmlData);
            var racuni = new List<Racun>();

            racuni = (from rows in ds.Tables[0].AsEnumerable()
                      select new Racun
                      {
                          Id = Convert.ToInt16(rows[0].ToString()),
                          Ime = Convert.ToString(rows[1].ToString()),
                          StanjeRacuna = Convert.ToDecimal(rows[2].ToString())
                      }
                          ).ToList();

            return racuni;
        }

        public Racun PronadjiRacun(int Id)
        {
            return PronadjiSve().Find(x => x.Id == Id);
        }

        public void Sacuvaj(Racun racun)
        {
            _unitOfWork.RegistrujPromenu(racun, this);
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
            throw new NotImplementedException();
        }

        public void PersistUpdateOf(IAggregateRoot entitet)
        {
            Racun racun = entitet as Racun;
            
            XElement xelem = XElement.Load(xmlData);
            IEnumerable<XElement> elem = xelem.Elements();

            foreach (var item in elem)
            {
                if (item.Element("id").Value.Equals(racun.Id.ToString())) {
                    item.Element("stanjeracuna").Value = racun.StanjeRacuna.ToString();
                }
            }

            xelem.Save(xmlData);

        }

        public void PersistDeletionOf(IAggregateRoot entitet)
        {
            throw new NotImplementedException();
        }
    }
}
