using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {    
        public static IDal getDal() 
        {
          // return MyDal.getMyDal();
            return Dal_xml.getDal_XML();
        }
    }
}
