using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game.Items
{
    internal abstract class ConsumableItem : MonoBehaviour
    {
        internal abstract void Consume();
    }
}
