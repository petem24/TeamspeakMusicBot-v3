using System;
using System.Windows.Forms;
using TentacleSoftware.TeamSpeakQuery;
using TentacleSoftware.TeamSpeakQuery.ServerQueryResult;

namespace TeamspeakMusicBot_v3
{
    class Query
    {

        static String host = "85.236.100.137";
        static int port = 9994;
        static String user = "memebotSV";
        static String password = "pCPI7Orv";


        public static void ConnectQueryAsync()
        {
            ServerQueryClient client = new ServerQueryClient(host, port, TimeSpan.FromSeconds(1));

            ServerQueryBaseResult connected = client.Initialize().Result;
            if (connected.Success)
                MessageBox.Show("connected");

            ServerQueryBaseResult login = client.Login(user, password).Result;
            if (!login.Success)
                MessageBox.Show(login.ErrorMessage);

            ServerQueryBaseResult use = client.Use(UseServerBy.Port, 10677).Result;
            if (!use.Success)
                MessageBox.Show(use.ErrorMessage);

            //client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, 3, "please respond");

            ClientListResult clientList = client.ClientList().Result;
            if (!clientList.Success)
                MessageBox.Show(clientList.ErrorMessage);

            client.NotifyTextMessage += (source, notification) => MessageBox.Show(notification.Invokername + ": " + notification.Msg);
            
            ServerQueryBaseResult registerTextChannel = client.ServerNotifyRegister(Event.textprivate).Result;
            if (!registerTextChannel.Success)
                MessageBox.Show(registerTextChannel.ErrorMessage);

            client.KeepAlive(TimeSpan.FromMinutes(10));
            client.NotifyChannelEdited += (source, notification) => MessageBox.Show(notification.Invokername + ": " + notification.Invokerid);
            


            for (int i = 0; i < clientList.Values.Count; i++)
             {
                 ClientInfoResult clientInfoResult = clientList.Values[i];
                 int id = clientInfoResult.ClientId;
                 String name = clientInfoResult.ClientNickname;

                if (!name.Contains("Sharix"))
                {
                    client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, id, "you are a nonce lol");
                    MessageBox.Show(id.ToString() + " " + name);
                }

             }


        }
    }
}
