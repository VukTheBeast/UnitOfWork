using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfWork.Infrastructure;

namespace UnitOfWork.Model
{
    public class Racun : IAggregateRoot
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public decimal StanjeRacuna { get; set; }
    }
}
