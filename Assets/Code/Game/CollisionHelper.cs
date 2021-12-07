using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SMB.Game
{
    internal static class CollisionHelper
    {
        /// <summary>
        /// Note: Objects must not be within each other! The outcome will not necessarily be correct, if so.
        /// </summary>
        internal static CollisionSide GetCollisionSide(Collision2D col)
        {
            Vector2 contactPoint = col.GetContact(0).point;
            Vector2 povColCenter = col.otherCollider.bounds.center;

            bool isVerticallyOverlapping = _DoSidesOverlap(
                col.otherCollider.bounds.min.x,
                col.otherCollider.bounds.max.x,
                col.collider.bounds.min.x,
                col.collider.bounds.max.x
            );

            if (isVerticallyOverlapping)
            {
                if (contactPoint.y > povColCenter.y)    return CollisionSide.Top;
                else                                    return CollisionSide.Bottom;
            }
            else // Is horizontally overlapping.
            {
                if (contactPoint.x > povColCenter.x)    return CollisionSide.Right;
                else                                    return CollisionSide.Left;
            }
        }

        internal static CollisionSide GetCollisionSideFromTrigger(Collider2D povCol, Collider2D otherCol)
        {
            bool isVerticallyOverlapping = _DoSidesOverlap(
                povCol.bounds.min.x,
                povCol.bounds.max.x,
                otherCol.bounds.min.x,
                otherCol.bounds.max.x
            );

            if (isVerticallyOverlapping)
            {
                if (otherCol.bounds.center.y > povCol.bounds.center.y) return CollisionSide.Top;
                else return CollisionSide.Bottom;
            }
            else // Is horizontally overlapping.
            {
                if (otherCol.bounds.center.x > povCol.bounds.center.x) return CollisionSide.Right;
                else return CollisionSide.Left;
            }
        }

        private static bool _DoSidesOverlap(float povColLower, float povColUpper, float otherColLower, float otherColUpper)
        {
            /* This code should be read as such:
             * bool ValueIsBetween(value, lower, upper) {...}
             * ValueIsBetween(povColUpper, otherColLower, otherColUpper) ||
             * ValueIsBetween(povColLower, otherColLower, otherColUpper) ||
             * ValueIsBetween(otherColUpper, povColLower, povColUpper) ||
             * ValueIsBetween(otherColLower, povColLower, povColUpper)
             */
            return  (otherColLower <= povColUpper && povColUpper <= otherColUpper) ||
                    (otherColLower <= povColLower && povColLower <= otherColUpper) ||
                    (povColLower <= otherColUpper && otherColUpper <= povColUpper) ||
                    (povColLower <= otherColLower && otherColLower <= povColUpper);
        }
    }
}
