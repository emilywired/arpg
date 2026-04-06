using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputManager
{
    public event Action<Keys>? KeyPressed;
    public event Action<Keys>? KeyReleased;

    private KeyboardState _keyboardState;
    private KeyboardState _previousKeyboardState;

    public void Update(HashSet<Keys> hardBoundKeys, HashSet<Keys> boundKeys)
    {
        _keyboardState = Keyboard.GetState();

        foreach (Keys key in hardBoundKeys)
        {
            ProcessKey(key);
        }

        foreach (Keys key in boundKeys)
        {
            ProcessKey(key);
        }

        _previousKeyboardState = _keyboardState;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessKey(Keys key)
    {
        bool isKeyDown = _keyboardState.IsKeyDown(key);
        bool isPreviousKeyDown = _previousKeyboardState.IsKeyDown(key);

        bool isNewKeyPress = isKeyDown && !isPreviousKeyDown;
        bool IsKeyReleased = !isKeyDown && isPreviousKeyDown;

        if (isNewKeyPress)
        {
            KeyPressed?.Invoke(key);
        }
        else if (IsKeyReleased)
        {
            KeyReleased?.Invoke(key);
        }
    }
}
