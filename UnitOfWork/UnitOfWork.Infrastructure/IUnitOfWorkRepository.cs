using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWork.Infrastructure
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreateionOf(IAggregateRoot entitet);
        void PersistUpdateOf(IAggregateRoot entitet);
        void PersistDeletionOf(IAggregateRoot entitet);
    }
}
