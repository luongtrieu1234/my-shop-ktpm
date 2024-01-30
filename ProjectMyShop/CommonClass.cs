using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop
{
    public class CommonClass
    {
        public delegate void NotifyEventHandler(object sender, EventArgs e);
        public static event NotifyEventHandler NotifyEvent;
        public CommonClass()
        {
            Debug.WriteLine("CommonClass");

        }
        public static void OnNotifyEvent()
        {
            NotifyEvent?.Invoke(null, new EventArgs());
            Debug.WriteLine("OnNotifyEvent");
        }
    }
}
