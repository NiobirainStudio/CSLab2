using System;
using System.Collections.Generic;
using DB.Services;
using DB.Models;
using System.Linq;

namespace DB
{
    public class ModelBlock
    {
        public DataServiceBase dataService;
        public Dictionary<string, Type> variables = new Dictionary<string, Type>();
        public Dictionary<string, Type> externalKeys = new Dictionary<string, Type>();

        public ModelBlock(DataServiceBase service)
        {
            dataService = service;
        }
    }



    public class ServiceManager
    {
        public Dictionary<Type, ModelBlock> modelSpace { get; } = new Dictionary<Type, ModelBlock>();

        public ServiceManager()
        {
        }


        public void AddDataService<T>(DataServiceBase<T> service) where T : IModel
        {
            if (!modelSpace.ContainsKey(typeof(T)))
                modelSpace.Add(typeof(T), new ModelBlock(service));
        }

        // Go through all the models and link them together
        public void InitSpace()
        {
            // For each model space service
            foreach (var item in modelSpace)
            {
                // For each variable in the data service
                foreach (var ti in item.Value.dataService.typesInfo)
                {
                    // Types: Artists / Genres / Albums
                    if (modelSpace.ContainsKey(ti.Value))
                    {
                        // Find the ModelBlock with the located type name and take the first variable
                        var block = modelSpace.FirstOrDefault(t => t.Key == ti.Value);

                        var key = block.Value.dataService.typesInfo.FirstOrDefault();

                        // Check if it exists localy
                        if (
                            item.Value.dataService.typesInfo.Values.Contains(key.Value) &&
                            item.Value.dataService.typesInfo.Keys.Contains(key.Key))
                        {
                            // External reference exists!
                            item.Value.externalKeys.Add(ti.Key, ti.Value);
                        }
                    } else if (
                        // Select the first variables of all models and check if the current variable is not one of them
                        modelSpace.Values.Select(t => t.dataService.typesInfo.FirstOrDefault())
                        .Where(t => t.Key == ti.Key && t.Value == ti.Value)
                        .Count() == 0)
                    {
                        item.Value.variables.Add(ti.Key, ti.Value);
                    }
                }
            }
        }
    }
}
