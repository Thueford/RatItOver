using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(Player))]
public class InputController : MonoBehaviour
{
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

    
    private void Awake()
    {
        player = GetComponent<Player>();

        foreach (var action in gameplayActions) action.Enable();

        // moveAction = gameplayActions["Move"];
        // Mouse.current.position
        gameplayActions["Pause"].performed += ctx => { pause.enterPause(); };
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= SetDevice;
        gameplayActions.Disable();
        gameplayActions.RemoveAllBindingOverrides();
        foreach (var action in gameplayActions)
            action.RemoveAllBindingOverrides();
    }

    private void Start()
    {
        // SetDevice(InputSystem.devices[0], InputDeviceChange.SoftReset);
        InputSystem.onDeviceChange += SetDevice;
    }

    void Update()
    {
        if (skipInput) return;
    }

    private void SetDevice(InputDevice dev, InputDeviceChange ch)
    {
        Debug.Log($"Device {ch}: {dev.name}");
        StartCoroutine(nameof(UpdateDevice));
    }

    private IEnumerator UpdateDevice()
    {
        yield return null;
        gameplayActions.devices = new InputDevice[] { Keyboard.current, Mouse.current };        
        // gameplayActions.devices = new InputDevice[] { player == Player.player1 ? Gamepad.all[0] : Keyboard.current };
    }
}
