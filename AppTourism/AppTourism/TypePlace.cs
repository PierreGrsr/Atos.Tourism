using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTourism
{

    public class TypePlace
    {
        private string type;
    
        public TypePlace() { }

        public TypePlace(string type)
        {
            this.type = type;
        }

        public TypePlace Museum()
        {
            return new TypePlace("museum");
        }
        public TypePlace Gallery()
        {
            return new TypePlace("gallery");
        }
        

    }
}
