using System.Collections;
using UnityEngine;

namespace ObjectOriented
{
    public class Player : MonoBehaviour
    {
        private Vector3 targetPosition;
        private Vector3 startPosition;
        private float moveSpeed = 2;
        private float startTime;
        private float journeyLength;
        
        private Coroutine coroutine;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (!hit.collider.CompareTag("Ground") && !hit.collider.CompareTag("FloorButton")) return;
                    if (targetPosition != hit.point)
                    {
                        InitMovingParams(hit.point);
                        Move();
                    }
                }
            }
        }

        private void InitMovingParams(Vector3 targetPosition)
        {
            startPosition = transform.position;
            this.targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, targetPosition);
        }

        private void Move()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(Moving());
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