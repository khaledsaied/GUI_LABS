using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAssignment
{
    public class PropertyFilter
    {
        public string PropertyName
        {
            get;
            set;
        }

        public PropertyFilter(string name)
        {
            PropertyName = name;
        }

        public bool FilterItem(Object item)
        {
            Agent agent = item as Agent;
            if (agent != null)
            {
                return (agent.Speciality == PropertyName);
            }

            return false;
        }
    } 
}
