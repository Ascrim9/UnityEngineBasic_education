using UnityEngine;

namespace _02.Scripts.Data
{
    public abstract class ItemData : ScriptableObject
    {
        public ItemType type;
        public int id;
        public int numMax;
        public Sprite icon;
        public string description;
        public GameObject model;

        [Header("Dropped rendering")]
        public Vector3 droppedRenderLocation;
        public Vector3 droppedRenderRotation;
        public Vector3 droppedRenderScale;
    }
}