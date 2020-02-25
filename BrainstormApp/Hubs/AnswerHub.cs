using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BrainstormApp.Hubs
{
    public class AnswerHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("AnswerNotify");
        }
    }
}
