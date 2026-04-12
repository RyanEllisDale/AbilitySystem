// Dependancies
using UnityEngine;
using UnityEditor;

namespace AbilitySystem
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void Activate(GameObject parent, IUnit target);  
    }
}