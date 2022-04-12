using UnityEngine;
using Zenject;

namespace DataOriented
{
    public class GeneralInstaller : MonoInstaller
    {
        [SerializeField] private InitData initData;
        [SerializeField] private Camera mainCamera;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Entities>()
                .FromInstance(new Entities())
                .AsSingle();
        
            Container
                .Bind<InitData>()
                .FromInstance(initData)
                .AsSingle();

            Container
                .Bind<Camera>()
                .FromInstance(mainCamera)
                .AsSingle();
        }
    }
}