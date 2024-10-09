using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherersExterior
{
    [CreateAssetMenu(fileName = "AnimationSet_4Dir", menuName = "GatherersExterior/AnimationSet4Dir")]
    public class AnimationSet4Dir : ScriptableObject
    {
        [field: SerializeField] public AnimationClip Up { get; private set; }
        [field: SerializeField] public AnimationClip Down { get; private set; }
        [field: SerializeField] public AnimationClip Right { get; private set; }
        [field: SerializeField] public AnimationClip Left { get; private set; }
    }
}