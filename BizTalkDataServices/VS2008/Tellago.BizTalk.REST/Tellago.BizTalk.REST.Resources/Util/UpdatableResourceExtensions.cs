using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Services;

namespace Tellago.BizTalk.REST.Resources.Util
{
    public static class UpdatableResourceExtensions
    {
        public static void Update(this IUpdatableResource targetResource, string propertyName, object propertyValue)
        {
            Type t = targetResource.GetType();

            PropertyInfo pi = GetPropertyInfoForType(t, propertyName, true);

            try
            {
                if (pi != null && pi.CanWrite)
                {
                    pi.SetValue(targetResource, propertyValue, null);
                }
            }
            catch (Exception ex)
            {
                throw new DataServiceException(string.Format("Error setting property {0} to {1}", propertyName, propertyValue), ex);
            }
        }

        private static PropertyInfo GetPropertyInfoForType(Type t, string propertyName, bool setter)
        {
            PropertyInfo pi = null;

            try
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                flags |= setter ? BindingFlags.SetProperty : BindingFlags.GetProperty;

                pi = t.GetProperty(propertyName, flags);

                if (pi == null)
                {
                    throw new DataServiceException(string.Format("Failed to find property {0} on type {1}",
                      propertyName, t.Name));
                }
            }
            catch (Exception exception)
            {
                throw new DataServiceException( string.Format("Error finding property {0}", propertyName), exception);
            }
            return (pi);
        }
    }
}
