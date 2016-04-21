using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfWork.Infrastructure;

namespace UnitOfWork.Model
{
    public class RacunService
    {
        private IRacunRepository _racunRepository;
        private IUnitOfWork _unitOfWork;

        //DI
        public RacunService(IRacunRepository racunRepo, IUnitOfWork uof)
        {
            _racunRepository = racunRepo;
            _unitOfWork = uof;
        }

        public Racun getRacun(int id) {

            Racun trazeniRacun = _racunRepository.PronadjiRacun(id);
            return trazeniRacun;
        }

        public IEnumerable<Racun> GetRacuni()
        {
            IEnumerable<Racun> listaRacuna = _racunRepository.PronadjiSve();

            return listaRacuna;
        }

        public void Transfer(Racun od, Racun za, decimal kolicinaNovca){
            if (od.StanjeRacuna >= kolicinaNovca)
            {
                od.StanjeRacuna -= kolicinaNovca;
                za.StanjeRacuna += kolicinaNovca;

                _racunRepository.Sacuvaj(od);
                _racunRepository.Sacuvaj(za);
                _unitOfWork.Commit();
            }
        }
    }
}
