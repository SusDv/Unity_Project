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
            return Vector3.up * _capsuleCollider.height / 2f + _capsuleCollider.transform.position;
        }

        public Vector3 GetCharacterCenter(float forwardOffset)
        {
            return Vector3.forward * forwardOffset + GetCharacterCenter();
        }

        public Vector3 GetCharacterTop()
        {
            return Vector3.up * (_capsuleCollider.height - _capsuleCollider.radius) + _capsuleCollider.transform.position;
        }
    }
}