using Cozify.Devices;
using Cozify.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    public interface ICozifyClient
    {
        event EventHandler<ConnectionInfo> ConnectionInfoChanged;

        Response RequestPassword(string email);
        //void Connect(string email, string password);
        //void Connect(string remoteToken);
        void Connect();

        //Response<string> Login(string email, string password);
        //Response<string> RefreshToken();
        Response<User> GetUser(string email);
        //Response<string> GetLanIp();
        Response<Hub> GetHub();
        //Response<Dictionary<string,string>> GetHubTokens();
        Response<string> GetHubFeatures();
        Response<PollDelta> Poll();
        Response SendCommand(string deviceId, string type);
        Response SendCommand(Command command);
    }
}
