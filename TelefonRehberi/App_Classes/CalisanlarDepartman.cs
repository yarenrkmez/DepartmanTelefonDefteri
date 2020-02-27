using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.Models;

namespace TelefonRehberi.App_Classes
{
    public class CalisanlarDepartman
    {
        public IEnumerable<Calisanlar> calisanlars { get; set; }

        public IEnumerable<Departmanlar> departmanlars { get; set; }
        public IEnumerable<Yonetici> yoneticis { get; set; }
    }
}