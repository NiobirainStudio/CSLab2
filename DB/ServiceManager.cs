using System;
using System.Collections.Generic;
using DB.Services;
using DB.Models;

namespace DB
{
    public class ServiceManager
    {
        // Store the table services and their types
        private List<BaseDataService<IModel>> dataServices = new List<BaseDataService<IModel>>();

        public List<Type> serviceDataTypes { get; } = new List<Type>(); // Artist
        public List<Type> serviceTypes { get; } = new List<Type>();     // ArtistDataService

        public int selectedService { get; set; } = -1;


        public void AddDataService<T>(BaseDataService<T> service) where T : IModel
        {
            serviceTypes.Add(service.GetType());
            serviceDataTypes.Add(typeof(T));


            //var v1 = (BaseDataService<IModel>)service;

            //dataServices.Add(v1);
        }

        public BaseDataService<IModel>? GetCurrentService() { if (selectedService != 1) return dataServices[selectedService]; else return null; }
        public Type? GetCurrentServiceDataType() { if (selectedService != 1) return serviceDataTypes[selectedService]; else return null; }
        public Type? GetCurrentServiceType() { if (selectedService != 1) return serviceTypes[selectedService]; else return null; }
    }
}
