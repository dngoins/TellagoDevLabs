using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tellago.BizTalk.REST.Resources
{
    public interface IUpdatableResource
    {
        void UpdateResource(string propertyName, object value);
        object UpdateUnderlyingObject(object baseObject);
    }
}
