using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public enum FixedGameAction
{
    LeftClick,
    RightClick,
    Close,
}

public enum RemappableGameAction
{
    CastBarOne,
    CastBarTwo,
    CastBarThree,
    OpenInventory,
    DebugMenu,
    CycleResolution,
    ToggleFullscreen,
    ToggleDisplayEnemyHealthBars
}

public class InputMapper
{
    private KeyboardInputManager _keyboardInputManager;
    private MouseInputManager _mouseInputManager;

    private Dictionary<MouseButtons, FixedGameAction> _fixedMouseKeybinds = [];
    private Dictionary<Keys, FixedGameAction> _fixedKeyboardKeybinds = [];
    private Dictionary<FixedGameAction, Action?> _fixedPressActionHandlers = [];
    private Dictionary<FixedGameAction, Action?> _fixedReleaseActionHandlers = [];
    private Dictionary<MouseButtons, RemappableGameAction> _remappableMouseKeybinds = [];
    private Dictionary<Keys, RemappableGameAction> _remappableKeyboardKeybinds = [];
    private Dictionary<RemappableGameAction, Action?> _remappablePressActionHandlers = [];
    private Dictionary<RemappableGameAction, Action?> _remappableReleaseActionHandlers = [];

    public InputMapper(
        KeyboardInputManager keyboardInputManager,
        MouseInputManager mouseInputManager
    )
    {
        _keyboardInputManager = keyboardInputManager;
        _mouseInputManager = mouseInputManager;

        foreach (FixedGameAction action in Enum.GetValues(typeof(FixedGameAction)))
        {
            _fixedPressActionHandlers[action] = null;
            _fixedReleaseActionHandlers[action] = null;
        }

        foreach (RemappableGameAction action in Enum.GetValues(typeof(RemappableGameAction)))
        {
            _remappablePressActionHandlers[action] = null;
            _remappableReleaseActionHandlers[action] = null;
        }

        _keyboardInputManager.KeyPressed += TriggerKeyboardKeyPressedAction;
        _keyboardInputManager.KeyReleased += TriggerKeyboardKeyReleasedAction;
        _mouseInputManager.KeyPressed += TriggerMouseKeyPressedAction;
        _mouseInputManager.KeyReleased += TriggerMouseKeyReleasedAction;
    }

    public void OnPress(FixedGameAction gameAction, Action handler)
    {
        _fixedPressActionHandlers[gameAction] += handler;
    }

    public void OnPress(RemappableGameAction gameAction, Action handler)
    {
        _remappablePressActionHandlers[gameAction] += handler;
    }

    public void OnRelease(FixedGameAction gameAction, Action handler)
    {
        _fixedReleaseActionHandlers[gameAction] += handler;
    }

    public void OnRelease(RemappableGameAction gameAction, Action handler)
    {
        _remappableReleaseActionHandlers[gameAction] += handler;
    }

    // TODO: unsubscribing

    public void BindKey(Keys key, RemappableGameAction gameAction)
    {
        _remappableKeyboardKeybinds.Add(key, gameAction);
    }

    public void BindKey(Keys key, FixedGameAction gameAction)
    {
        _fixedKeyboardKeybinds.Add(key, gameAction);
    }

    public void BindKey(MouseButtons button, RemappableGameAction gameAction)
    {
        _remappableMouseKeybinds.Add(button, gameAction);
    }

    public void BindKey(MouseButtons button, FixedGameAction gameAction)
    {
        _fixedMouseKeybinds.Add(button, gameAction);
    }

    public void UnbindKey(Keys key)
    {
        if (_remappableKeyboardKeybinds.TryGetValue(key, out _))
            _ = _remappableKeyboardKeybinds.Remove(key);
    }

    private FixedGameAction? GetFixedKeyboardKeybindAction(Keys key)
    {
        if (!_fixedKeyboardKeybinds.TryGetValue(key, out FixedGameAction fixedGameAction))
            return null;
        return fixedGameAction;
    }

    private FixedGameAction? GetFixedMouseKeybindAction(MouseButtons button)
    {
        return !_fixedMouseKeybinds.TryGetValue(button, out FixedGameAction fixedGameAction)
            ? null
            : fixedGameAction;
    }

    private RemappableGameAction? GetRemappableKeyboardKeybindAction(Keys key)
    {
        return !_remappableKeyboardKeybinds.TryGetValue(key, out RemappableGameAction remappableGameAction)
            ? null
            : remappableGameAction;
    }

    private RemappableGameAction? GetRemappableMouseKeybindAction(MouseButtons button)
    {
        return !_remappableMouseKeybinds.TryGetValue(button, out RemappableGameAction fixedGameAction)
            ? null
            : fixedGameAction;
    }

    private void TriggerKeyboardKeyPressedAction(Keys key)
    {
        FixedGameAction? fixedGameAction = GetFixedKeyboardKeybindAction(key);
        if (fixedGameAction is not null)
        {
            _fixedPressActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableKeyboardKeybindAction(key);
        if (remappableGameAction is not null)
        {
            _remappablePressActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }

    private void TriggerMouseKeyPressedAction(MouseButtons button)
    {
        FixedGameAction? fixedGameAction = GetFixedMouseKeybindAction(button);
        if (fixedGameAction is not null)
        {
            _fixedPressActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableMouseKeybindAction(button);
        if (remappableGameAction is not null)
        {
            _remappablePressActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }

    private void TriggerKeyboardKeyReleasedAction(Keys key)
    {
        FixedGameAction? fixedGameAction = GetFixedKeyboardKeybindAction(key);
        if (fixedGameAction is not null)
        {
            _fixedReleaseActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableKeyboardKeybindAction(key);
        if (remappableGameAction is not null)
        {
            _remappableReleaseActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }

    private void TriggerMouseKeyReleasedAction(MouseButtons button)
    {
        FixedGameAction? fixedGameAction = GetFixedMouseKeybindAction(button);
        if (fixedGameAction is not null)
        {
            _fixedReleaseActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableMouseKeybindAction(button);
        if (remappableGameAction is not null)
        {
            _remappableReleaseActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }
}
