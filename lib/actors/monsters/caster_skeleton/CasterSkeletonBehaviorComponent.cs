// using System;
// using arpg;
// using Microsoft.Xna.Framework;

// public class CasterSkeletonBehaviorComponent
// {
//     private float _corpseDespawnTime = 10f;
//     private bool _isCasting = false;
//     private float _timeSinceDeath = 0f;
//     private const double _CAST_DURATION = 1d;
//     private double _castTimer = 0d;

//     // private double _afterCastIdleTime = 0.5d;

//     public void Update(CasterSkeleton monster, GameTime time)
//     {
//         double elapsedTime = time.ElapsedGameTime.TotalSeconds;

//         // TODO: reuse
//         if (monster.State == ActorState.Dead)
//         {
//             _timeSinceDeath += (float)elapsedTime;

//             if (_timeSinceDeath >= _corpseDespawnTime)
//             {
//                 Game1.World.RemoveActor(monster);
//             }

//             monster.ActionState = ActorActionState.None;
//             return;
//         }

//         float x1 = monster.Position.X;
//         float y1 = monster.Position.Y;
//         float x2 = Game1.World.Player.Position.X;
//         float y2 = Game1.World.Player.Position.Y;
//         double angle = Math.Atan2(y2 - y1, x2 - x1);

//         // TODO: reuse???
//         if (x1 < x2)
//         {
//             monster.Facing = ActorFacing.Right;
//         }
//         else if (x1 > x2)
//         {
//             monster.Facing = ActorFacing.Left;
//         }

//         double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

//         if (distance <= 200)
//         {
//             monster.IsLeashed = true;
//         }

//         if (!monster.IsLeashed)
//         {
//             monster.TransitionState(ActorState.Idling);
//             return;
//         }

//         // is leashed

//         if (distance > 200)
//         {
//             // follow player
//             double x = monster.Position.X + (monster.Stats.Speed * elapsedTime * Math.Cos(angle));
//             double y = monster.Position.Y + (monster.Stats.Speed * elapsedTime * Math.Sin(angle));

//             monster.TransitionState(ActorState.Walking);
//             monster.Position = new((float)x, (float)y);
//         }
//         else
//         {
//             _castTimer += elapsedTime;

//             if (_castTimer >= _CAST_DURATION)
//             {
//                 if (!_isCasting)
//                 {
//                     _isCasting = true;
//                     FireballEntity fireballEntity = new(monster)
//                     {
//                         Position = new(monster.Position.X, monster.Position.Y),
//                         Angle = angle,
//                     };
//                     monster.TransitionState(ActorState.Idling);
//                     monster.TransitionState(ActorActionState.Casting);
//                 }

//                 _castTimer -= _CAST_DURATION;
//                 _isCasting = false;
//                 monster.TransitionState(ActorActionState.None);
//             }
//         }
//     }
// }
