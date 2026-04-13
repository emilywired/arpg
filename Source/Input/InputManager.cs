using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    private MouseInputManager mouseInputManager = new();
    private KeyboardInputManager keyboardInputManager = new();
    private InputMapper inputMapper;

    private HashSet<Keys> hardBoundKeys = [];
    private HashSet<Keys> boundKeys = [];

    public InputManager()
    {
        inputMapper = new(keyboardInputManager, mouseInputManager);

        // hardcoded keybinds
        BindKey(Keys.Escape, FixedGameAction.Close);
        BindKey(MouseButtons.LeftButton, FixedGameAction.LeftClick);
        BindKey(MouseButtons.RightButton, FixedGameAction.RightClick);

        // remappable keybinds
        BindKey(Keys.Q, RemappableGameAction.CastBarOne);
        BindKey(Keys.E, RemappableGameAction.CastBarTwo);
        BindKey(Keys.R, RemappableGameAction.CastBarThree);
        BindKey(Keys.F1, RemappableGameAction.DebugMenu);
        BindKey(Keys.OemTilde, RemappableGameAction.OpenInventory);
        BindKey(Keys.I, RemappableGameAction.OpenInventory);
        BindKey(Keys.F9, RemappableGameAction.ToggleDisplayEnemyHealthBars);
        BindKey(Keys.F10, RemappableGameAction.CycleResolution);
        BindKey(Keys.F11, RemappableGameAction.ToggleFullscreen);
        // BindKey([Keys.LeftAlt, Keys.Enter], RemappableGameAction.ToggleFullscreen);
    }

    public void Update()
    {
        mouseInputManager.Update();
        keyboardInputManager.Update(hardBoundKeys, boundKeys);
    }

    public void OnPress(FixedGameAction gameAction, Action handler)
    {
        inputMapper.OnPress(gameAction, handler);
    }

    public void OnPress(RemappableGameAction gameAction, Action handler)
    {
        inputMapper.OnPress(gameAction, handler);
    }

    public void OnRelease(FixedGameAction gameAction, Action handler)
    {
        inputMapper.OnRelease(gameAction, handler);
    }

    public void OnRelease(RemappableGameAction gameAction, Action handler)
    {
        inputMapper.OnRelease(gameAction, handler);
    }

    private void BindKey(Keys key, FixedGameAction gameAction)
    {
        _ = hardBoundKeys.Add(key);
        inputMapper.BindKey(key, gameAction);
    }

    public void BindKey(Keys key, RemappableGameAction gameAction)
    {
        _ = boundKeys.Add(key);
        inputMapper.BindKey(key, gameAction);
    }

    public void BindKey(MouseButtons button, FixedGameAction gameAction)
    {
        inputMapper.BindKey(button, gameAction);
    }

    public void BindKey(MouseButtons button, RemappableGameAction gameAction)
    {
        inputMapper.BindKey(button, gameAction);
    }

    public void UnbindKey(Keys key)
    {
        if (hardBoundKeys.Contains(key))
            throw new InvalidOperationException("Cannot unbind a fixed keybind.");
        _ = boundKeys.Remove(key);
        inputMapper.UnbindKey(key);
    }
}
