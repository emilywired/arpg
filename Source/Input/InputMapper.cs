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
    ToggleDisplayEnemyHealthBars,
}

public class InputMapper
{
    private KeyboardInputManager keyboardInputManager;
    private MouseInputManager mouseInputManager;
    private Dictionary<MouseButtons, FixedGameAction> fixedMouseKeybinds = [];
    private Dictionary<Keys, FixedGameAction> fixedKeyboardKeybinds = [];
    private Dictionary<FixedGameAction, Action?> fixedPressActionHandlers = [];
    private Dictionary<FixedGameAction, Action?> fixedReleaseActionHandlers = [];
    private Dictionary<MouseButtons, RemappableGameAction> remappableMouseKeybinds = [];
    private Dictionary<Keys, RemappableGameAction> remappableKeyboardKeybinds = [];
    private Dictionary<RemappableGameAction, Action?> remappablePressActionHandlers = [];
    private Dictionary<RemappableGameAction, Action?> remappableReleaseActionHandlers = [];

    public InputMapper(
        KeyboardInputManager _keyboardInputManager,
        MouseInputManager _mouseInputManager
    )
    {
        keyboardInputManager = _keyboardInputManager;
        mouseInputManager = _mouseInputManager;

        foreach (FixedGameAction action in Enum.GetValues(typeof(FixedGameAction)))
        {
            fixedPressActionHandlers[action] = null;
            fixedReleaseActionHandlers[action] = null;
        }

        foreach (RemappableGameAction action in Enum.GetValues(typeof(RemappableGameAction)))
        {
            remappablePressActionHandlers[action] = null;
            remappableReleaseActionHandlers[action] = null;
        }

        keyboardInputManager.KeyPressed += TriggerKeyboardKeyPressedAction;
        keyboardInputManager.KeyReleased += TriggerKeyboardKeyReleasedAction;
        mouseInputManager.KeyPressed += TriggerMouseKeyPressedAction;
        mouseInputManager.KeyReleased += TriggerMouseKeyReleasedAction;
    }

    public void OnPress(FixedGameAction gameAction, Action handler)
    {
        fixedPressActionHandlers[gameAction] += handler;
    }

    public void OnPress(RemappableGameAction gameAction, Action handler)
    {
        remappablePressActionHandlers[gameAction] += handler;
    }

    public void OnRelease(FixedGameAction gameAction, Action handler)
    {
        fixedReleaseActionHandlers[gameAction] += handler;
    }

    public void OnRelease(RemappableGameAction gameAction, Action handler)
    {
        remappableReleaseActionHandlers[gameAction] += handler;
    }

    // TODO: unsubscribing

    public void BindKey(Keys key, RemappableGameAction gameAction)
    {
        remappableKeyboardKeybinds.Add(key, gameAction);
    }

    public void BindKey(Keys key, FixedGameAction gameAction)
    {
        fixedKeyboardKeybinds.Add(key, gameAction);
    }

    public void BindKey(MouseButtons button, RemappableGameAction gameAction)
    {
        remappableMouseKeybinds.Add(button, gameAction);
    }

    public void BindKey(MouseButtons button, FixedGameAction gameAction)
    {
        fixedMouseKeybinds.Add(button, gameAction);
    }

    public void UnbindKey(Keys key)
    {
        if (remappableKeyboardKeybinds.TryGetValue(key, out _))
            _ = remappableKeyboardKeybinds.Remove(key);
    }

    private FixedGameAction? GetFixedKeyboardKeybindAction(Keys key)
    {
        if (!fixedKeyboardKeybinds.TryGetValue(key, out FixedGameAction fixedGameAction))
            return null;
        return fixedGameAction;
    }

    private FixedGameAction? GetFixedMouseKeybindAction(MouseButtons button)
    {
        return !fixedMouseKeybinds.TryGetValue(button, out FixedGameAction fixedGameAction)
            ? null
            : fixedGameAction;
    }

    private RemappableGameAction? GetRemappableKeyboardKeybindAction(Keys key)
    {
        return !remappableKeyboardKeybinds.TryGetValue(
            key,
            out RemappableGameAction remappableGameAction
        )
            ? null
            : remappableGameAction;
    }

    private RemappableGameAction? GetRemappableMouseKeybindAction(MouseButtons button)
    {
        return !remappableMouseKeybinds.TryGetValue(
            button,
            out RemappableGameAction fixedGameAction
        )
            ? null
            : fixedGameAction;
    }

    private void TriggerKeyboardKeyPressedAction(Keys key)
    {
        FixedGameAction? fixedGameAction = GetFixedKeyboardKeybindAction(key);
        if (fixedGameAction is not null)
        {
            fixedPressActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableKeyboardKeybindAction(key);
        if (remappableGameAction is not null)
        {
            remappablePressActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }

    private void TriggerMouseKeyPressedAction(MouseButtons button)
    {
        FixedGameAction? fixedGameAction = GetFixedMouseKeybindAction(button);
        if (fixedGameAction is not null)
        {
            fixedPressActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableMouseKeybindAction(button);
        if (remappableGameAction is not null)
        {
            remappablePressActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }

    private void TriggerKeyboardKeyReleasedAction(Keys key)
    {
        FixedGameAction? fixedGameAction = GetFixedKeyboardKeybindAction(key);
        if (fixedGameAction is not null)
        {
            fixedReleaseActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableKeyboardKeybindAction(key);
        if (remappableGameAction is not null)
        {
            remappableReleaseActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }

    private void TriggerMouseKeyReleasedAction(MouseButtons button)
    {
        FixedGameAction? fixedGameAction = GetFixedMouseKeybindAction(button);
        if (fixedGameAction is not null)
        {
            fixedReleaseActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableMouseKeybindAction(button);
        if (remappableGameAction is not null)
        {
            remappableReleaseActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }
}
