using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;

public class MonsterSpawner(Player player, double frequency, int offscreenDistance)
{
    public double Frequency = frequency;
    public Rectangle SpawnEdge
    {
        get =>
            new(
                (int)player.Position.X - _offscreenDistance - HALF_WINDOW_WIDTH,
                (int)player.Position.Y - _offscreenDistance - HALF_WINDOW_HEIGHT,
                Game1.NativeResolution.Width + _offscreenDistance * 2,
                Game1.NativeResolution.Height + _offscreenDistance * 2
            );
    }
    private const int HALF_WINDOW_WIDTH = Game1.NativeResolution.Width / 2;
    private const int HALF_WINDOW_HEIGHT = Game1.NativeResolution.Height / 2;
    private int _offscreenDistance = offscreenDistance;
    private double _timer = 0f;

    public void Update(GameTime gameTime)
    {
        double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
        _timer += elapsed;

        if (_timer >= Frequency)
        {
            Rectangle spawnEdge = SpawnEdge;
            Vector2 topLeftCorner = new(spawnEdge.Left, spawnEdge.Top);
            Vector2 topRightCorner = new(spawnEdge.Right, spawnEdge.Top);
            Vector2 bottomLeftCorner = new(spawnEdge.Left, spawnEdge.Bottom);
            Vector2 bottomRightCorner = new(spawnEdge.Right, spawnEdge.Bottom);

            List<(Vector2, Vector2)> lineSegments =
            [
                (topLeftCorner, topRightCorner),
                (bottomRightCorner, topRightCorner),
                (bottomLeftCorner, bottomRightCorner),
                (bottomLeftCorner, topLeftCorner),
            ];

            Random rng = new();
            int index = rng.Next(lineSegments.Count);

            var (start, end) = lineSegments[index];

            bool isHorizontalEdge = start.Y == end.Y;

            int x;
            int y;

            if (isHorizontalEdge)
            {
                int rngMin = (int)Math.Min(start.X, end.X);
                int rngMax = (int)Math.Max(start.X, end.X);
                x = rng.Next(rngMin, rngMax);
                y = (int)start.Y;
            }
            else
            {
                x = (int)start.X;
                int rngMin = (int)Math.Min(start.Y, end.Y);
                int rngMax = (int)Math.Max(start.Y, end.Y);
                y = rng.Next(rngMin, rngMax);
            }

            Skeleton skeleton = new(Random.Shared.Next(1, 3)) { Position = new(x, y) };
            Game1.World.Actors.Add(skeleton);

            _timer -= Frequency;
        }
    }
}
