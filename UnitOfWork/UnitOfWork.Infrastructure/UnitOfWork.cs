using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace UnitOfWork.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<IAggregateRoot, IUnitOfWorkRepository> _dodatiEntiteti;
        private Dictionary<IAggregateRoot, IUnitOfWorkRepository> _promenjeniEntiteti;
        private Dictionary<IAggregateRoot, IUnitOfWorkRepository> _obrisaniEntiteti;

        public UnitOfWork()
        {
            _dodatiEntiteti = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _promenjeniEntiteti = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _obrisaniEntiteti = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
        }

        public void RegistrujPromenu(IAggregateRoot entitet, IUnitOfWorkRepository uofRepo)
        {
            if (!_promenjeniEntiteti.ContainsKey(entitet))
            {
                _promenjeniEntiteti.Add(entitet, uofRepo);
            }
        }

        public void RegistrujNovi(IAggregateRoot entitet, IUnitOfWorkRepository uofRepo)
        {
            if (!_dodatiEntiteti.ContainsKey(entitet))
            {
                _dodatiEntiteti.Add(entitet, uofRepo);
            }
        }

        public void RegistrujBrisanje(IAggregateRoot entitet, IUnitOfWorkRepository uofRepo)
        {
            if (!_obrisaniEntiteti.ContainsKey(entitet))
            {
                _obrisaniEntiteti.Add(entitet, uofRepo);
            }
        }

        public void Commit()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (IAggregateRoot entitet in this._dodatiEntiteti.Keys)
                {
                    _dodatiEntiteti[entitet].PersistCreateionOf(entitet);
                }

                foreach (IAggregateRoot entitet in _promenjeniEntiteti.Keys)
                {
                    _promenjeniEntiteti[entitet].PersistUpdateOf(entitet);
                }

                foreach (IAggregateRoot entitet in _obrisaniEntiteti.Keys)
                {
                    _obrisaniEntiteti[entitet].PersistDeletionOf(entitet);
                }

                scope.Complete();
            }
        }
    }
}
