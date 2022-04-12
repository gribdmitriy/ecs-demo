using UnityEngine;

namespace DataOriented.Components
{
    public struct MovableComponent
    {
        public int index;
        public Transform transform;
        public Vector3 startPosition;
        public Vector3 targetPosition;
        public float moveSpeed;
        public float startTime;
        public float journeyLength;
        public bool isMoving;
    }
}
