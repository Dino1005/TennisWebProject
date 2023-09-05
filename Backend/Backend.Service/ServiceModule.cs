using Autofac;
using Backend.Repository;
using Backend.Service.Common;

namespace Backend.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<CourtService>().As<ICourtService>();
            builder.RegisterType<ReservationService>().As<IReservationService>();
        }
    }
}
