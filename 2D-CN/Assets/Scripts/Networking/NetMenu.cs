using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMenu : Bolt.GlobalEventListener {

    public void StartServer()
    {
        BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.isServer)
            BoltNetwork.LoadScene("Multiplayer_Testing");
        else BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));
    }
}
