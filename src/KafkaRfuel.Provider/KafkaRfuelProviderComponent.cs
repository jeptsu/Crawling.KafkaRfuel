using Castle.MicroKernel.Registration;

using CluedIn.Core;
using CluedIn.Core.Providers;
// 
using CluedIn.Crawling.KafkaRfuel.Core;
using CluedIn.Crawling.KafkaRfuel.Infrastructure.Installers;
// 
using ComponentHost;
using CluedIn.Core.Server;

namespace CluedIn.Provider.KafkaRfuel
{
    [Component(KafkaRfuelConstants.ProviderName, "Providers", ComponentType.Service, ServerComponents.ProviderWebApi, Components.Server, Components.DataStores, Isolation = ComponentIsolation.NotIsolated)]
    public sealed class KafkaRfuelProviderComponent : ServiceApplicationComponent<IBusServer>
    {
        public KafkaRfuelProviderComponent(ComponentInfo componentInfo)
            : base(componentInfo)
        {
            // Dev. Note: Potential for compiler warning here ... CA2214: Do not call overridable methods in constructors
            //   this class has been sealed to prevent the CA2214 waring being raised by the compiler
            Container.Register(Component.For<KafkaRfuelProviderComponent>().Instance(this));
        }

        public override void Start()
        {
            Container.Install(new InstallComponents());
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            Container.Register(Types.FromAssembly(asm).BasedOn<IProvider>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromAssembly(asm).BasedOn<IEntityActionBuilder>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());



            State = ServiceState.Started;
        }

        public override void Stop()
        {
            if (State == ServiceState.Stopped)
                return;

            State = ServiceState.Stopped;
        }
    }
}
