using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.EntityFramwork;

namespace MyEvernote.DataAccessLayer.EntityFramework.Concrete
{
    //Singleton Pattern Entegrasyonu
    public class RepositoryBase
    {

        protected static DatabaseContext context;
            
        private static object obj=new object();

        //Sınıfın new()'lenmesini engelledik ama statik sınıfta yapmadık
        protected RepositoryBase()
        {
            CreatContext();
        }

        private static void CreatContext()
        {
          
            if (context == null)  /*Multithread uygulamalarda aynı anda
                                     birden fazla context gelebildiği için
                                     burada kitleme işlemi uygulayarak aynı anda iki nesnenin instance oluşturmasını
                                     engelliyoruz*/
            {
                lock (obj)                                      
                {                                            
                    if (context == null)                       
                    {
                        context = new DatabaseContext();           
                    }                                           
                }                                                   

            }

            
        }


    }
}
