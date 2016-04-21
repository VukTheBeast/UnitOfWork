using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitOfWork.Model;
using UnitOfWork.Infrastructure;
using UnitOfWork.Repository;
using StructureMap;
using StructureMap.Configuration.DSL;
using UnitOfWork.Repository.EF;
using UnitOfWork.Repository.Xml;

namespace Client
{
    public class BootStraper
    {
        public static void ConfigureStructureMap()
        {
            ObjectFactory.Initialize(x => {
                x.AddRegistry<ModelRegistry>();
            });
        }
    }

    public class ModelRegistry : Registry
    {
        public ModelRegistry()
        {
           
            ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.HttpSession).AddConcreteType<UnitOfWork.Infrastructure.UnitOfWork>();
               
            ForRequestedType<IRacunRepository>().TheDefault.Is.OfConcreteType<RacunRepository>();
            
          
        }
    }
}