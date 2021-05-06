using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Adapter;
namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Adapter
{

    public class AutomapperTypeAdapterFactory
        : ITypeAdapterFactory
    {
        #region Constructor

        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory()
        {
            //scan all assemblies finding Automapper Profile

           
            var _profiles = AppDomain.CurrentDomain
                                  .GetAssemblies()
                                  .SelectMany(a => a.GetTypes())
                                  .Where(t => t.BaseType == typeof(Profile));



            //var _config = new MapperConfiguration(cfg =>
            //{

            //    //cfg.AddMaps(_profiles);
            //   // cfg.AddProfiles(_profiles);
            //});


            Mapper.Initialize(cfg =>
            {


                cfg.AddProfiles(Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(Profile)));
                cfg.ValidateInlineMaps = false;
                cfg.AllowNullDestinationValues = true;
                ///cfg.CreateMissingTypeMaps = false;
            });

            //Mapper.Initialize(cfg =>
            //{
            //    foreach (var item in _profiles)
            //    {
            //        if (item.FullName != "AutoMapper.SelfProfiler`2")
            //            cfg.AddProfile(Activator.CreateInstance(item) as Profile);
            //    }
            //});
        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter();
        }

        #endregion
    }
}
