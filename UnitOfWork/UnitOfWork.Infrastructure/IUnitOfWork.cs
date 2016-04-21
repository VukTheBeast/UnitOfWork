using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWork.Infrastructure
{
    public interface IUnitOfWork
    {
        void RegistrujPromenu(IAggregateRoot entitet, IUnitOfWorkRepository uofRepo);
        void RegistrujNovi(IAggregateRoot entite, IUnitOfWorkRepository uofRepo);
        void RegistrujBrisanje(IAggregateRoot entitet, IUnitOfWorkRepository uofRepo);
        void Commit();
    }
}
