using System;
using System.Collections.Generic;


// TODO: fix this retarded way of adding new handlers
public class GameInputController
{
    private readonly List<Func<bool>> _escapeHandlers = [];
    private readonly List<Func<bool>> _leftClickHandlers = [];
    private readonly List<Func<bool>> _leftClickReleaseHandlers = [];
    private readonly List<Func<bool>> _rightClickHandlers = [];
    private readonly List<Func<bool>> _rightClickReleaseHandlers = [];

    public GameInputController()
    {
        Game1.InputManager.OnPress(FixedGameAction.Close, OnClose);
        Game1.InputManager.OnPress(FixedGameAction.LeftClick, OnLeftClick);
        Game1.InputManager.OnRelease(FixedGameAction.LeftClick, OnLeftClickRelease);
        Game1.InputManager.OnPress(FixedGameAction.RightClick, OnRightClick);
        Game1.InputManager.OnRelease(FixedGameAction.RightClick, OnRightClickRelease);
        Game1.InputManager.OnPress(RemappableGameAction.DebugMenu, ToggleDebugMode);
        Game1.InputManager.OnPress(RemappableGameAction.CycleResolution, CycleResolution);
        Game1.InputManager.OnPress(RemappableGameAction.ToggleFullscreen, ToggleFullscreen);
    }

    public void RegisterOnClose(Func<bool> handler)
    {
        _escapeHandlers.Add(handler);
    }

    public void RegisterOnLeftClick(Func<bool> handler)
    {
        _leftClickHandlers.Add(handler);
    }

    public void RegisterOnLeftClickRelease(Func<bool> handler)
    {
        _leftClickReleaseHandlers.Add(handler);
    }

    public void RegisterOnRightClick(Func<bool> handler)
    {
        _rightClickHandlers.Add(handler);
    }

    public void RegisterOnRightClickRelease(Func<bool> handler)
    {
        _rightClickReleaseHandlers.Add(handler);
    }

    private void OnClose()
    {
        HandleEventPropagation(_escapeHandlers);
    }

    private void OnLeftClick()
    {
        HandleEventPropagation(_leftClickHandlers);
    }

    private void OnLeftClickRelease()
    {
        HandleEventPropagation(_leftClickReleaseHandlers);
    }

    private void OnRightClick()
    {
        HandleEventPropagation(_rightClickHandlers);
    }

    private void OnRightClickRelease()
    {
        HandleEventPropagation(_rightClickReleaseHandlers);
    }

    private void HandleEventPropagation(List<Func<bool>> handlers)
    {
        foreach (Func<bool> handler in handlers)
        {
            bool propagationStopped = handler();
            if (propagationStopped)
                break;
        }
    }

    // TODO: move these somewhere else?
    private void ToggleDebugMode()
    {
        GameState.IsDebugMode = !GameState.IsDebugMode;
    }

    private void CycleResolution()
    {
        int scale = (Game1.Config.Scale % 3) + 1;
        Game1.Config.ChangeResolutionScale(scale);
        Game1.Config.ApplyChanges();
    }

    private void ToggleFullscreen()
    {
        Game1.Config.ToggleFullScreen();
        Game1.Config.ApplyChanges();
    }
}
