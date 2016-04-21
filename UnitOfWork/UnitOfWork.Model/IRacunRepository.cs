using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWork.Model
{
    public interface IRacunRepository
    {

        List<Racun> PronadjiSve();

        Racun PronadjiRacun(int Id);
        void Sacuvaj(Racun racun);
        void Dodaj(Racun racun);
        void Obrisi(Racun racun);

        
    }
}
