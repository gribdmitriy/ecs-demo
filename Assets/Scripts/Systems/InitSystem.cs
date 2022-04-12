using Common;
using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class InitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        
        private Entities entities;
        private InitData initData;

        public void Init()
        {
            var player = _world.NewEntity();
            player.Get<MovableComponent>();
            
            var doors = new[]
            {
                _world.NewEntity(), 
                _world.NewEntity()
            };

            doors[0].Get<MovableComponent>();
            doors[1].Get<MovableComponent>();
            
            var floorButtons = new[]
            {
                _world.NewEntity(),
                _world.NewEntity()
            };
            
            floorButtons[0].Get<MovableComponent>();
            floorButtons[1].Get<MovableComponent>();
            
            for (var i = 0; i < doors.Length; i++)
            {
                doors[i].Get<MovableComponent>().transform = initData.doors[i].transform;
                doors[i].Get<MovableComponent>().moveSpeed = 1;
                doors[i].Get<MovableComponent>().index = doors[i].GetInternalId();
                doors[i].Get<DoorComponent>();
                entities.doorAndButton[i].door = doors[i].GetInternalId();
            }
            
            for (var i = 0; i < floorButtons.Length; i++)
            {
                floorButtons[i].Get<MovableComponent>().transform = initData.floorButtons[i].transform;
                floorButtons[i].Get<MovableComponent>().moveSpeed = 3;
                floorButtons[i].Get<MovableComponent>().index = floorButtons[i].GetInternalId();
                floorButtons[i].Get<FloorButtonComponent>();
                entities.doorAndButton[i].floorButton = floorButtons[i].GetInternalId();
            }
            
            player.Get<MovableComponent>().transform = initData.player.transform;
            player.Get<MovableComponent>().moveSpeed = 2;
            player.Get<MovableComponent>().index = player.GetInternalId();
            entities.player = player.GetInternalId();
        }
    }
}