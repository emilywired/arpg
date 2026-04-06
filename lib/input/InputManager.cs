using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    private MouseInputManager _mouseInputManager = new();
    private KeyboardInputManager _keyboardInputManager = new();
    private InputMapper _inputMapper;

    private HashSet<Keys> _hardBoundKeys = [];
    private HashSet<Keys> _boundKeys = [];

    public InputManager()
    {
        _inputMapper = new(_keyboardInputManager, _mouseInputManager);

        // hardcoded keybinds
        BindKey(Keys.Escape, FixedGameAction.Close);
        BindKey(MouseButtons.LeftButton, FixedGameAction.LeftClick);

        // remappable keybinds
        BindKey(Keys.Q, RemappableGameAction.CastBarOne);
        BindKey(Keys.E, RemappableGameAction.CastBarTwo);
        BindKey(Keys.R, RemappableGameAction.CastBarThree);
        BindKey(Keys.F1, RemappableGameAction.DebugMenu);
        BindKey(Keys.OemTilde, RemappableGameAction.OpenInventory);
        BindKey(Keys.I, RemappableGameAction.OpenInventory);
        BindKey(Keys.F10, RemappableGameAction.CycleResolution);
        BindKey(Keys.F11, RemappableGameAction.ToggleFullscreen);
        // BindKey([Keys.LeftAlt, Keys.Enter], RemappableGameAction.ToggleFullscreen);
    }

    public void Update()
    {
        _mouseInputManager.Update();
        _keyboardInputManager.Update(_hardBoundKeys, _boundKeys);
    }

    public void OnPress(FixedGameAction gameAction, Action handler)
    {
        _inputMapper.OnPress(gameAction, handler);
    }

    public void OnPress(RemappableGameAction gameAction, Action handler)
    {
        _inputMapper.OnPress(gameAction, handler);
    }

    public void OnRelease(FixedGameAction gameAction, Action handler)
    {
        _inputMapper.OnRelease(gameAction, handler);
    }

    public void OnRelease(RemappableGameAction gameAction, Action handler)
    {
        _inputMapper.OnRelease(gameAction, handler);
    }

    private void BindKey(Keys key, FixedGameAction gameAction)
    {
        _hardBoundKeys.Add(key);
        _inputMapper.BindKey(key, gameAction);
    }

    public void BindKey(Keys key, RemappableGameAction gameAction)
    {
        _boundKeys.Add(key);
        _inputMapper.BindKey(key, gameAction);
    }

    public void BindKey(MouseButtons button, FixedGameAction gameAction)
    {
        _inputMapper.BindKey(button, gameAction);
    }

    public void BindKey(MouseButtons button, RemappableGameAction gameAction)
    {
        _inputMapper.BindKey(button, gameAction);
    }

    public void UnbindKey(Keys key)
    {
        if (_hardBoundKeys.Contains(key))
            throw new InvalidOperationException("Cannot unbind a fixed keybind.");
        _boundKeys.Remove(key);
        _inputMapper.UnbindKey(key);
    }
}
