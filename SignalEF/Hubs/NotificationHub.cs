using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalEF.Models.EF;

namespace SignalEF.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        public string broadcastMessage = string.Empty;
        [HubMethodName("sendNotification")]
        public async Task<string> sendNotification()
        {

            BroadcastTestEntities db = new BroadcastTestEntities();
            var result = from a in db.Broadcasts
                         select a.BroadcastMessage;
            foreach (var item in result)
            {
                broadcastMessage += item;
            }
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            //var connectionId = Context.ConnectionId;

            return await context.Clients.All.ReciveNotifivation(broadcastMessage);
        }

        public async Task<string> sendToUser(string connctionId,string message)
        {
            var serializer = new JavaScriptSerializer();
             connctionId = serializer.Deserialize<string>(connctionId);
             message = serializer.Deserialize<string>(message);


            broadcastMessage = message;
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            return await context.Clients.Client(connctionId).userNotifivation(broadcastMessage);
        }
        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;


            return base.OnConnected();
        }

        public void Connected()
        {


        }
    }
}