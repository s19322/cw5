using cw5.ModelsFrameWorkCore;
using System;
using System.Linq;



namespace cw5
{
    public class KlasaMain
    {
        static void Main(String[] args)
        {//select * from doctor 
            var db = new s19322Context();
            //db.dispose-> nie musimy sami tego robic bo to autoamtycznie sie robi w contex 
            var res = db.Doctor.ToList();

        }

    }
}
