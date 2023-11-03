using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A simple example of Fusion input collection. This component should be on the same GameObject as the <see cref="NetworkRunner"/>.
/// </summary>
///
public struct MyInput : INetworkInput {
public NetworkButtons buttons;
}


[ScriptHelp(BackColor = EditorHeaderBackColor.Steel)]
public class InputBehaviourPrototype : Fusion.Behaviour, INetworkRunnerCallbacks ,INetworkInput {




    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;
    public NetworkRunner runner;

    public NetworkButtons buttons;

    public MyInput input;
    // Input yapısını tut
    //private NetworkInput input;


   


    enum MyButtons
    {
        Forward = 0,
        Backward = 1,
        Left = 2,
        Right = 3,
    }


    private void Awake()
    {
        /*
        forwardButton.onClick.AddListener(onForwardButton);
        forwardButton.onClick.AddListener(OnForwardButtonUp);
        backwardButton.onClick.AddListener(OnBackwardButtonDown);
        backwardButton.onClick.AddListener(OnBackwardButtonUp);
        leftButton.onClick.AddListener(OnLeftButtonDown);
        leftButton.onClick.AddListener(OnLeftButtonUp);
        rightButton.onClick.AddListener(OnRightButtonDown);
        rightButton.onClick.AddListener(OnRightButtonUp);
        */

        // Input yapısını sıfırlama

        input = new MyInput();
        input.buttons = new NetworkButtons();

        
    }

    void onForwardButton()
    {
        
        input.buttons.Set((int)MyButtons.Forward, true);
    }
    public void OnForwardButtonUp()
    {
        input.buttons.Set((int)MyButtons.Forward, false);
    }

    public void OnBackwardButtonDown()
    {
        input.buttons.Set((int)MyButtons.Backward, true);
    }

    public void OnBackwardButtonUp()
    {
        input.buttons.Set((int)MyButtons.Backward, false);
    }

    public void OnLeftButtonDown()
    {
        input.buttons.Set((int)MyButtons.Left, true);
    }

    public void OnLeftButtonUp()
    {
        input.buttons.Set((int)MyButtons.Left, false);
    }

    public void OnRightButtonDown()
    {
        input.buttons.Set((int)MyButtons.Right, true);
    }

    public void OnRightButtonUp()
    {
        input.buttons.Set((int)MyButtons.Right, false);
    }

    

    public void OnInput(NetworkRunner runner, NetworkInput input) {
    var frameworkInput = new NetworkInputPrototype();

        

        if (Input.GetKey(KeyCode.W)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_FORWARD, true);
    }

    if (Input.GetKey(KeyCode.S)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_BACKWARD, true);
    }

    if (Input.GetKey(KeyCode.A)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_LEFT, true);
    }

    if (Input.GetKey(KeyCode.D)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_RIGHT, true);
    }

    if (Input.GetKey(KeyCode.Space)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_JUMP, true);
    }

    if (Input.GetKey(KeyCode.C)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_CROUCH, true);
    }

    if (Input.GetKey(KeyCode.E)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_ACTION1, true);
    }

    if (Input.GetKey(KeyCode.Q)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_ACTION2, true);
    }

    if (Input.GetKey(KeyCode.F)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_ACTION3, true);
    }

    if (Input.GetKey(KeyCode.G)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_ACTION4, true);
    }

    if (Input.GetKey(KeyCode.R)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_RELOAD, true);
    }

    if (Input.GetMouseButton(0)) {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_FIRE, true);
    }

    if (Input.GetKey(KeyCode.H))
    {
      frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_ACTION5, true);
    }
    
    /*if (UiButtonController.isLeft)
    {
      frameworkInput.Buttons.Set(NetworkInputPrototype.leftButton, true);
    }*/
    
        input.Set(frameworkInput);
  }





  public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

  public void OnConnectedToServer(NetworkRunner runner) { }
  public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
  public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
  public void OnDisconnectedFromServer(NetworkRunner runner) { }
  public void OnPlayerJoined(NetworkRunner          runner, PlayerRef            player)                                                           { }
  public void OnPlayerLeft(NetworkRunner            runner, PlayerRef            player)                                                           { }
  public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)                                                          { }
  public void OnShutdown(NetworkRunner              runner, ShutdownReason       shutdownReason) { }
  public void OnSessionListUpdated(NetworkRunner    runner, List<SessionInfo>    sessionList)    {  }
  public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {
  }
    

    public void OnSceneLoadDone(NetworkRunner runner) {
    
  }

  public void OnSceneLoadStart(NetworkRunner runner) {
  }

  public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {
  }

  public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {

  }

    
}

/// <summary>
/// Example definition of an INetworkStruct.
/// </summary>
public struct NetworkInputPrototype : INetworkInput {

  public const int BUTTON_USE      = 0;
  public const int BUTTON_FIRE     = 1;
  public const int BUTTON_FIRE_ALT = 2;

  public const int BUTTON_FORWARD  = 3;
  public const int BUTTON_BACKWARD = 4;
  public const int BUTTON_LEFT     = 5;
  public const int BUTTON_RIGHT    = 6;

  public const int BUTTON_JUMP     = 7;
  public const int BUTTON_CROUCH   = 8;
  public const int BUTTON_WALK     = 9;

  public const int BUTTON_ACTION1  = 10;
  public const int BUTTON_ACTION2  = 11;
  public const int BUTTON_ACTION3  = 12;
  public const int BUTTON_ACTION4  = 14;
    public const int BUTTON_ACTION5 = 16;
    public const int leftButton = 17;

    public const int BUTTON_RELOAD   = 15;

  public NetworkButtons Buttons;
  public byte Weapon;
  public Angle Yaw;
  public Angle Pitch;

  public bool IsUp(int button) {
    return Buttons.IsSet(button) == false;
  }

  public bool IsDown(int button) {
    return Buttons.IsSet(button);
  }
}
