using Common;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class MovingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private Entities entities;
        
        public void Run()
        {
            var movablePool = _world.GetPool<MovableComponent>();

            foreach (var item in movablePool.Items)
            {
                if (!item.isMoving) continue;
            
                var distCovered = (Time.time - item.startTime) * item.moveSpeed;
                
                var fractionOfJourney = distCovered / item.journeyLength;
                
                item.transform.position = Vector3.Lerp(item.startPosition, item.targetPosition, fractionOfJourney);

                if (fractionOfJourney >= 1)
                {
                   ref var movableComponent = ref movablePool.GetItem(item.index);
                   movableComponent.isMoving = false;
                }
            }
        }
    }
}