using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.SlotMachine
{
    [System.Serializable]
    public class Face
    {
        public FaceType faceType;

        public FaceController facePrefab;
    }

    public enum FaceType
    {
        None,
        Apple,
        Bell,
        Bar
    }
}