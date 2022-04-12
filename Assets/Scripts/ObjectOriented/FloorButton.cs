using System.Collections;
using UnityEngine;

namespace ObjectOriented
{
    public class FloorButton : MonoBehaviour
    {
        [SerializeField] private Door door;
        
        private Vector3 targetPosition;
        private Vector3 startPosition;
        private float moveSpeed = 3;
        private float startTime;
        private float journeyLength;
        private Vector3 size;

        private Coroutine animation;
        
        private void Start()
        {
            size = GetComponent<MeshFilter>().mesh.bounds.size;
        }
        
        private void Press()
        {
            InitializeMovingParams(new Vector3(transform.position.x, -size.y + 0.01f, transform.position.z));
            if (animation != null) StopCoroutine(animation);
            animation = StartCoroutine(Animation());
        }

        private void Release()
        {
            InitializeMovingParams(new Vector3(transform.position.x, 0, transform.position.z));
            if (animation != null) StopCoroutine(animation);
            animation = StartCoroutine(Animation());
        }

        private void InitializeMovingParams(Vector3 targetPosition)
        {
            startPosition = transform.position;
            this.targetPosition = targetPosition;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, targetPosition);
        }
        
        private IEnumerator Animation()
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Press();
            door.Open();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Release();
            door.Close();
        }
    }
}