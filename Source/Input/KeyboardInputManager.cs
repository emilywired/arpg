using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputManager
{
    public event Action<Keys>? KeyPressed;
    public event Action<Keys>? KeyReleased;

    private KeyboardState keyboardState;
    private KeyboardState previousKeyboardState;

    public void Update(HashSet<Keys> hardBoundKeys, HashSet<Keys> boundKeys)
    {
        keyboardState = Keyboard.GetState();

        foreach (Keys key in hardBoundKeys)
        {
            ProcessKey(key);
        }

        foreach (Keys key in boundKeys)
        {
            ProcessKey(key);
        }

        previousKeyboardState = keyboardState;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessKey(Keys key)
    {
        bool isKeyDown = keyboardState.IsKeyDown(key);
        bool isPreviousKeyDown = previousKeyboardState.IsKeyDown(key);

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
