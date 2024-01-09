using UnityEngine;
using SpellModule.Utility;
using System.Collections.Generic;
using StatModule.Modifier;
using System;

namespace SpellModule.Core
{
    [Serializable] 
    public abstract class SpellBase : ScriptableObject
    {
        public abstract SpellType SpellType { get; }

    }
}
