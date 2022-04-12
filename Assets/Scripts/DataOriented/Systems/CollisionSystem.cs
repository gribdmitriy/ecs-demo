using DataOriented.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace DataOriented.Systems
{
    public class CollisionSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private Entities entities;

        public void Run()
        {
            var movablePool = _world.GetPool<MovableComponent>();
            var buttonPool = _world.GetPool<ButtonComponent>();
            
            ref var playerMovableComponent = ref movablePool.GetItem(entities.player);
            
            for (var i = 0; i < entities.doorAndButton.Length; i++)
            {
                ref var buttonMovableComponent = ref movablePool.GetItem(entities.doorAndButton[i].floorButton);
                ref var buttonComponent = ref buttonPool.GetItem(entities.doorAndButton[i].floorButton);
                
                var playerHalfSize = playerMovableComponent.transform.GetComponent<MeshFilter>().mesh.bounds.size / 2;
                var buttonHalfSize = buttonMovableComponent.transform.GetComponent<MeshFilter>().mesh.bounds.size / 2;
                var collisionDistanceX = playerHalfSize.x + buttonHalfSize.x;
                var collisionDistanceZ = playerHalfSize.z + buttonHalfSize.z;
                var actualDistance = Vector3.Distance(playerMovableComponent.transform.position,
                    buttonMovableComponent.transform.position);
                
                if (actualDistance < collisionDistanceX || actualDistance < collisionDistanceZ)
                {
                    if (!buttonMovableComponent.isMoving && buttonComponent.state == ButtonState.Press)
                    {
                        buttonMovableComponent.startPosition = buttonMovableComponent.transform.position;
                        buttonMovableComponent.startTime = Time.time;
                        buttonMovableComponent.targetPosition = new Vector3(buttonMovableComponent.startPosition.x,
                            buttonMovableComponent.startPosition.y - (buttonHalfSize * 2).y + 0.01f, buttonMovableComponent.startPosition.z);
                        buttonMovableComponent.journeyLength = Vector3.Distance(buttonMovableComponent.startPosition, buttonMovableComponent.targetPosition);
                        buttonMovableComponent.isMoving = true;
                        buttonComponent.state = ButtonState.Release;
                    }
                }
                else
                {
                    if (!buttonMovableComponent.isMoving && buttonComponent.state == ButtonState.Release)
                    {
                        buttonMovableComponent.startPosition = buttonMovableComponent.transform.position;
                        buttonMovableComponent.startTime = Time.time;
                        buttonMovableComponent.targetPosition = new Vector3(buttonMovableComponent.startPosition.x,0, buttonMovableComponent.startPosition.z);
                        buttonMovableComponent.journeyLength = Vector3.Distance(buttonMovableComponent.startPosition, buttonMovableComponent.targetPosition);
                        buttonMovableComponent.isMoving = true;
                        buttonComponent.state = ButtonState.Press;
                    }
                }
            }
        }
    }
}