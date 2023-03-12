using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(Player))]
public class InputController : MonoBehaviour
{
    public static InputController self;
    [SerializeField] private InputDevice device;

    private bool skipInput => GameController.noInputs;

    private Player player;

    //  InputAction Properties:
    //  Disabled    The Action is disabled and can't receive input.
    //  Waiting     The Action is enabled and is actively waiting for input.
    //  Started     The Input System has received input that started an Interaction with the Action.
    //  Performed   An Interaction with the Action has been completed.
    //  Canceled    An Interaction with the Action has been canceled.
    public InputActionMap gameplayActions;
    public Pause pause;

    internal InputAction moveAction, dirAction, placeAction, respawnAction;

    private void Awake()
    {
        self = this;
        player = GetComponent<Player>();

        foreach (var action in gameplayActions) action.Enable();

        moveAction = gameplayActions["Move"];
        dirAction = gameplayActions["Direction"];
        placeAction = gameplayActions["PlaceBomb"];
        respawnAction = gameplayActions["Respawn"];

        // moveAction = gameplayActions["Move"];
        // Mouse.current.position
        gameplayActions["Pause"].performed += ctx => pause.enterPause();
        gameplayActions["Respawn"].performed += ctx => Player.player.Respawn();
    }

    void OnDisable()
    {
        gameplayActions.Disable();
        gameplayActions.RemoveAllBindingOverrides();
        foreach (var action in gameplayActions)
            action.RemoveAllBindingOverrides();
    }

    private void Start()
    {
        InputSystem.onDeviceChange += SetDevice;
    }

    private void SetDevice(InputDevice dev, InputDeviceChange ch)
    {
        // Debug.Log($"Device {ch}: {dev.name}"); // {Keyboard.current} {Mouse.current}");
        gameplayActions.devices = new InputDevice[] { Keyboard.current, Mouse.current };
    }

    bool inpState = true;
    void Update()
    {
        if (skipInput == inpState) return;
        if (skipInput) InputController.self.gameplayActions.Disable();
        else InputController.self.gameplayActions.Enable();
        inpState = skipInput;
    }
}
