using UnityEngine;
using Zenject;

namespace DataOriented
{
    public class GeneralInstaller : MonoInstaller
    {
        [SerializeField] private InitData initData;
    
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
        }
    }
}