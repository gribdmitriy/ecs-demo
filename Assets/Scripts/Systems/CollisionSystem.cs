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
            var buttonPool = _world.GetPool<FloorButtonComponent>();
            var doorPool = _world.GetPool<DoorComponent>();
            
            ref var playerMovableComponent = ref movablePool.GetItem(entities.player);
            
            for (var i = 0; i < entities.doorAndButton.Length; i++)
            {
                ref var buttonMovableComponent = ref movablePool.GetItem(entities.doorAndButton[i].floorButton);
                ref var buttonComponent = ref buttonPool.GetItem(entities.doorAndButton[i].floorButton);
                ref var doorMovableComponent = ref movablePool.GetItem(entities.doorAndButton[i].door);
                ref var doorComponent = ref doorPool.GetItem(entities.doorAndButton[i].door);
                
                var playerHalfSize = playerMovableComponent.transform.GetComponent<MeshFilter>().mesh.bounds.size / 2;
                var buttonHalfSize = buttonMovableComponent.transform.GetComponent<MeshFilter>().mesh.bounds.size / 2;
                var doorHalfSize = doorMovableComponent.transform.GetComponent<MeshFilter>().mesh.bounds.size / 2;
                
                var collisionDistanceX = playerHalfSize.x + buttonHalfSize.x;
                var collisionDistanceZ = playerHalfSize.z + buttonHalfSize.z;
                var actualDistance = Vector3.Distance(playerMovableComponent.transform.position,
                    buttonMovableComponent.transform.position);
                
                if (actualDistance < collisionDistanceX || actualDistance < collisionDistanceZ)
                {
                    if (!buttonMovableComponent.isMoving && buttonComponent.state == FloorButtonState.Press)
                    {
                        buttonMovableComponent.startPosition = buttonMovableComponent.transform.position;
                        buttonMovableComponent.startTime = Time.time;
                        buttonMovableComponent.targetPosition = new Vector3(buttonMovableComponent.startPosition.x,
                            buttonMovableComponent.startPosition.y - (buttonHalfSize * 2).y + 0.01f, buttonMovableComponent.startPosition.z);
                        buttonMovableComponent.journeyLength = Vector3.Distance(buttonMovableComponent.startPosition, buttonMovableComponent.targetPosition);
                        buttonMovableComponent.isMoving = true;
                        buttonComponent.state = FloorButtonState.Release;
                        
                        doorMovableComponent.startPosition = doorMovableComponent.transform.position;
                        doorMovableComponent.startTime = Time.time;
                        doorMovableComponent.targetPosition = new Vector3(doorMovableComponent.startPosition.x,
                            doorMovableComponent.startPosition.y - (doorHalfSize * 2).y + 0.05f, doorMovableComponent.startPosition.z);
                        doorMovableComponent.journeyLength = Vector3.Distance(doorMovableComponent.startPosition, doorMovableComponent.targetPosition);
                        doorMovableComponent.isMoving = true;
                        doorComponent.state = DoorState.Open;
                    }
                }
                else
                {
                    if (!buttonMovableComponent.isMoving && buttonComponent.state == FloorButtonState.Release)
                    {
                        buttonMovableComponent.startPosition = buttonMovableComponent.transform.position;
                        buttonMovableComponent.startTime = Time.time;
                        buttonMovableComponent.targetPosition = new Vector3(buttonMovableComponent.startPosition.x,0, buttonMovableComponent.startPosition.z);
                        buttonMovableComponent.journeyLength = Vector3.Distance(buttonMovableComponent.startPosition, buttonMovableComponent.targetPosition);
                        buttonMovableComponent.isMoving = true;
                        buttonComponent.state = FloorButtonState.Press;
                        
                        doorMovableComponent.startPosition = doorMovableComponent.transform.position;
                        doorMovableComponent.startTime = Time.time;
                        doorMovableComponent.targetPosition = new Vector3(doorMovableComponent.startPosition.x,0, doorMovableComponent.startPosition.z);
                        doorMovableComponent.journeyLength = Vector3.Distance(doorMovableComponent.startPosition, doorMovableComponent.targetPosition);
                        doorMovableComponent.isMoving = true;
                        doorComponent.state = DoorState.Close;
                    }
                }
            }
        }
    }
}