using System;
using UnityEngine;

namespace CharacterModule.Utility
{
    [Serializable]
    public class SizeHelper
    {
        [SerializeField]
        private CapsuleCollider _capsuleCollider;

        public Vector3 GetCharacterCenter()
        {
            return _capsuleCollider.bounds.center + _capsuleCollider.transform.position;
        }

        public Vector3 GetCharacterTop()
        {
            return Vector3.up * (_capsuleCollider.height - _capsuleCollider.radius) + _capsuleCollider.transform.position;
        }
    }
}