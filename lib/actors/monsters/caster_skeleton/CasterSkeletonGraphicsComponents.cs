// using System;
// 
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// public class CasterSkeletonGraphicsComponent
// {
//     private Asset _idleAsset = Assets.Monsters.Skeleton.Idle;
//     private Asset _attackAsset = Assets.Monsters.Skeleton.Attack;
//     private Asset _walkAsset = Assets.Monsters.Skeleton.Walk;
//     private Asset _deathAsset = Assets.Monsters.Skeleton.Death; // TODO: add one of the two corpse frames
//     private readonly float _frameTime = 0.15f;
//     private int _currentFrame = 0;
//     private float _elapsedTime = 0f;

//     public void Update(CasterSkeleton monster, GameTime gameTime)
//     {
//         _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

//         if (_elapsedTime >= _frameTime)
//         {
//             _elapsedTime = 0f;

//             var asset = (monster.State, monster.ActionState) switch
//             {
//                 (_, ActorActionState.Casting) => _attackAsset,
//                 (ActorState.Idling, _) => _idleAsset,
//                 (ActorState.Walking, _) => _walkAsset,
//                 (ActorState.Dead, _) => _deathAsset,
//                 _ => throw new SystemException("Unhandled ActorState"),
//             };

//             if (asset == _deathAsset)
//             {
//                 if (_currentFrame != asset.Frames.Count - 1)
//                     _currentFrame++;
//             }
//             else
//             {
//                 _currentFrame = (_currentFrame + 1) % asset.Frames.Count;
//             }
//         }
//     }

//     public void Draw(CasterSkeleton monster, SpriteBatch spriteBatch)
//     {
//         Asset asset = (monster.State, monster.ActionState) switch
//         {
//             (_, ActorActionState.Swinging) => _attackAsset,
//             (ActorState.Idling, _) => _idleAsset,
//             (ActorState.Walking, _) => _walkAsset,
//             (ActorState.Dead, _) => _deathAsset,
//             _ => throw new SystemException("Unhandled ActorState"),
//         };

//         var effect =
//             monster.Facing == ActorFacing.Right
//                 ? SpriteEffects.None
//                 : SpriteEffects.FlipHorizontally;

//         spriteBatch.Draw(
//             asset.Texture,
//             new((int)monster.Position.X, (int)monster.Position.Y),
//             asset.Frames[_currentFrame],
//             Color.White,
//             0f,
//             new Vector2(asset.Texture.Width / asset.Frames.Count / 2, asset.Texture.Height / 2),
//             1f,
//             effect,
//             Layer.Monster
//         );

//         if (GameState.IsDebugMode)
//         {
//             if (monster.Hitbox is RectangleHitbox rectangleHitbox)
//             {
//                 spriteBatch.Draw(
//                     Assets.RectangleTexture,
//                     rectangleHitbox.Bounds,
//                     null,
//                     Color.Yellow,
//                     0f,
//                     Vector2.Zero,
//                     SpriteEffects.None,
//                     Layer.Hitbox
//                 );
//             }
//             else
//             {
//                 throw new NotImplementedException("Unhandled hitbox type");
//             }
//         }

//         string monsterHealth = $"{Math.Floor(monster.Stats.Health)}";
//         Vector2 monsterHealthOrigin = Assets.Fonts.MonogramExtened.MeasureString(monsterHealth);

//         spriteBatch.DrawString(
//             Assets.Fonts.MonogramExtened,
//             monsterHealth,
//             new((int)monster.Position.X, (int)monster.Position.Y + 30),
//             Color.White,
//             0f,
//             new((int)(monsterHealthOrigin.X / 2), (int)(monsterHealthOrigin.Y / 2)),
//             1f,
//             SpriteEffects.None,
//             Layer.Text
//         );
//     }

//     public void ResetFrames()
//     {
//         _currentFrame = 0;
//     }
// }
