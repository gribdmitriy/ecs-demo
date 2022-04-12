using System.Collections;
using UnityEngine;

namespace ObjectOriented
{
    public class Door : MonoBehaviour
    {
        private Vector3 targetPosition;
        private Vector3 startPosition;
        private float moveSpeed = 1;
        private float startTime;
        private float journeyLength;
        private Vector3 size;

        private Coroutine coroutine;
        
        private void Start()
        {
            size = GetComponent<MeshFilter>().mesh.bounds.size;
            Debug.Log( GetComponent<MeshRenderer>().material.color);
        }

        public void Open()
        {
            InitializeMovingParams(new Vector3(transform.position.x, -size.y, transform.position.z));
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(Moving());
        }

        public void Close()
        {
            InitializeMovingParams(new Vector3(transform.position.x, default, transform.position.z));
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(Moving());
        }

        private void InitializeMovingParams(Vector3 targetPosition)
        {
            startPosition = transform.position;
            this.targetPosition = targetPosition;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, targetPosition);
        }
        
        private IEnumerator Moving()
        {
            while (true)
            {
                var distCovered = (Time.time - startTime) * moveSpeed;
                var fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
                yield return null;
                if (fractionOfJourney >= 1) break;
            }
        }
    }
}