using AutoMapper;
using Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public static class ExtensionMethods
    {
        public static void InitFromWmType<T>(this IWMResponse response, T fromObj)
        {
            try
            {
                var fromType = typeof(T);//.FullName;
                var toType = response.GetType();//.FullName;
                if (true)
                {
                    var vvv = (T)fromObj;
                    Log.Debug("Initializing auto mapper");
                    Mapper.Initialize(cfg => cfg.CreateMap(fromType,toType));
                    Log.Debug("Mapping {fromType} to {toType}", fromType.Name, toType.Name);
                    Mapper.Map(vvv, response);
                }
            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }
    }
}
