using DataOriented.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace DataOriented.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private Entities entities;
        private Camera mainCamera;
        
        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit))
                {
                    var movablePool = _world.GetPool<MovableComponent>();
                    ref var playerMovableComponent = ref movablePool.GetItem(entities.player);
                    playerMovableComponent.startTime = Time.time;
                    playerMovableComponent.startPosition = playerMovableComponent.transform.position;
                    playerMovableComponent.targetPosition = hit.point;
                    playerMovableComponent.journeyLength = Vector3.Distance(playerMovableComponent.startPosition, hit.point);
                    playerMovableComponent.isMoving = true;
                }
            }
        }
    }
}