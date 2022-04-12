using DataOriented.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace DataOriented
{
    public class Startup : MonoBehaviour
    {
        private InitData initData;
        private Entities entities;
        
        private EcsWorld _world;
        private EcsSystems _systems;

        [Inject]
        private void Construct(Entities _entities, InitData _initData)
        {
            entities = _entities;
            initData = _initData;
        }
        
        private void Start()
        {
            _world = new EcsWorld();
            
            _systems = new EcsSystems(_world)
                .Add(new InitSystem())
                .Add(new InputSystem())
                .Add(new MovingSystem())
                .Add(new CollisionSystem());
            
            AddInjections();

            _systems.Init();
        }

        private void AddInjections()
        {
            _systems.Inject(entities);
            _systems.Inject(initData);
        }
        
        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}