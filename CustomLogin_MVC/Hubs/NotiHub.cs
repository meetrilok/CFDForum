using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogin_MVC.Hubs
{
    [HubName("notiHub")]
    public class NotiHub:Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendNotifications(string message)
        {
            Clients.All.receiveNotification(message);
        }
    }
}
