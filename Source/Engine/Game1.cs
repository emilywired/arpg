using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Game1 : Game
{
    public static Config Config { get; private set; } = null!;

    public struct NativeResolution
    {
        public const int Width = 640;
        public const int Height = 360;
    }

    public static World World = null!;
    public static LootSystem LootSystem = null!;
    public static new GraphicsDevice GraphicsDevice = null!;
    public static InputManager InputManager = null!;

    private GraphicsDeviceManager graphics;
    private RenderTarget2D renderTarget = null!;
    private SpriteBatch spriteBatch = null!;
    private Background background = null!;
    private LootUI lootUI = null!;
    private PauseMenu pauseMenu = null!;
    private Hud hud = null!;
    private GameUI gameUI = null!;
    private GameInputController gameInputController = null!;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        InputManager = new InputManager();
        Content.RootDirectory = "Content";
        Window.Title = "Path of Exile 4";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GraphicsDevice = base.GraphicsDevice;
        renderTarget = new RenderTarget2D(
            GraphicsDevice,
            NativeResolution.Width,
            NativeResolution.Height
        );
        Assets.Load(Content, GraphicsDevice);
        Config = new Config(graphics, GraphicsDevice, renderTarget);

        var player = new Player();
        World = new World(player);
        LootSystem = new LootSystem();
        background = new Background();
        lootUI = new LootUI();
        hud = new Hud();
        gameUI = new GameUI(player);
        pauseMenu = new PauseMenu();

        World.AddEntity(new TestAnimation());

        gameInputController = new GameInputController();
        gameInputController.RegisterOnClose(gameUI.OnClose);
        gameInputController.RegisterOnClose(pauseMenu.OnClose);

        gameInputController.RegisterOnLeftClick(pauseMenu.OnLeftClick);
        gameInputController.RegisterOnLeftClick(gameUI.OnLeftClick);
        gameInputController.RegisterOnLeftClick(World.Stash.OnLeftClick);
        gameInputController.RegisterOnLeftClick(World.OnLeftClick);
        gameInputController.RegisterOnLeftClick(World.Player.InputComponent.OnLeftClick);

        gameInputController.RegisterOnLeftClickRelease(
            World.Player.InputComponent.OnLeftClickRelease
        );

        gameInputController.RegisterOnRightClick(gameUI.OnRightClick);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();

        if (GameState.IsRunning)
        {
            World.Update(gameTime);
            gameUI.Update(gameTime);
            hud.Update(gameTime);
        }

        Camera.Follow(World.Player);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // TODO: non-scaled render target

        GraphicsDevice.SetRenderTarget(renderTarget);
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );
        background.Draw(spriteBatch);
        spriteBatch.End();

        // world space rendering
        spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            transformMatrix: Camera.Transform,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        foreach (Entity entity in World.Entities)
        {
            entity.Draw(spriteBatch);
        }

        World.Stash.Draw(spriteBatch);

        lootUI.Draw(spriteBatch);

        spriteBatch.End();

        // screen space rendering
        spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        hud.Draw(spriteBatch);
        gameUI.Draw(spriteBatch);
        pauseMenu.Draw(spriteBatch);

        spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.White);

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(renderTarget, GraphicsDevice.Viewport.Bounds, Color.White);
        spriteBatch.End();

        base.Draw(gameTime);
    }

    // STYLE: move somewhere else?
    public static void DrawText(
        SpriteBatch spriteBatch,
        string text,
        Vector2 position,
        float layer,
        Color color,
        bool center = false
    )
    {
        Vector2 _position = new(position.X, position.Y);

        if (center)
        {
            float textWidth = Assets.Fonts.MonogramExtened.MeasureString(text).X;
            _position.X -= MathF.Round(textWidth / 2);
        }

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            text,
            _position,
            color,
            0.0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            layer
        );
    }
}
