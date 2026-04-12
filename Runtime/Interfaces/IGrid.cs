using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public interface IGrid 
    {
        public bool MoveUnit(Vector2Int moveSpaces);
    }
}