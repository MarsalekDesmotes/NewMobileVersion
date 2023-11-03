using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Threading.Tasks;


public class CreateRoomWithLobbyPropertiesExample : MonoBehaviour, INetworkRunnerCallbacks
{
    
    String SessName;
    int odas;
    public int userId = 1;


    private void Start()
    {
        
        SessName = "oda" + odas.ToString();
    }



    public enum GameType : int
    {
        FreeForAll,
        Team,
        Timed
    }

    public enum GameMap : int
    {
        Forest,
        City,
        Desert
    }

    // Utility method to start a Host using a defined GameMap and GameType
    public async Task StartServer1(NetworkRunner runner, GameMap gameMap, GameType gameType)
    {
        
        var customProps = new Dictionary<string, SessionProperty>();

        customProps["rank"] = userId; //3kere build alıp her birine farklı bir id gir.
        //customProps["gameMap"] = SeçilenMap;

        var result = await runner.StartGame(new StartGameArgs()
        {
            //SessionName = name,
            GameMode = GameMode.Shared,
            //Address = NetAddress.CreateFromIpPort("192.168.0.10", 27015),
            SessionProperties = customProps,
            PlayerCount = 2//Oturuma maks kaç kişi girebilir.
        }) ;

        if (result.Ok)
        {
            // all good
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }



    public async Task StartServer2(NetworkRunner runner, GameMap gameMap, GameType gameType)
    {

        var customProps = new Dictionary<string, SessionProperty>();

        customProps["map"] = (int)gameMap;
        customProps["type"] = (int)gameType;

        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionProperties = customProps,           
        });

        if (result.Ok)
        {
            // all good
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }
}
