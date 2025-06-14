using Microsoft.AspNetCore.SignalR;

namespace PetShop.Hubs
{
    public class OrderHub : Hub
    {
        public async Task JoinAdminGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        public async Task JoinUserGroup(string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);
        }
    }
}
