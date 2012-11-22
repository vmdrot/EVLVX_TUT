//#define __USE_REMOTE_SRV__
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkTut.Models;
//using DummyDBRemote.Models;
using DummyDB.DAL.Models;

namespace EntityFrameworkTut
{
    class Program
    {
        static void Main(string[] args)
        {
#if __USE_REMOTE_SRV__
            using (dbRuthenorumPR360SourceNewsContext db = new dbRuthenorumPR360SourceNewsContext())
            {
                tbl_news_item ni = db.tbl_news_item.Find(1);
                if (ni != null)
                    Console.WriteLine("{1}\t{0}", ni.news_item_id, ni.title);
            }
#else
            string nm = args[0];
            bool isValid = bool.Parse(args[1]);
            double prix = double.Parse(args[2]);
            string description = args[3];
            int iCnt = int.Parse(args[4]);

            using (dummydbContext db = new dummydbContext())
            {
                tbl1 newItem = new tbl1() { is_valid = isValid, descr = description, price = (decimal)prix, cnt = iCnt };
                newItem.nm = nm;
                db.tbl1.Add(newItem);
                db.SaveChanges();

                tbl1 item = db.tbl1.Find(1);
                if (item != null)
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", item.id, item.nm, item.is_valid, item.price, item.descr, item.price);

                //tbl_news_item ni = db.tbl_news_item.Find(1);
                //if (ni != null)
                //    Console.WriteLine("{1}\t{0}", ni.news_item_id, ni.title);
            }
#endif
            Console.Read();
        }
    }
}
