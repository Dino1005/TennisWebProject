using Autofac;
using Backend.Repository.Common;

namespace Backend.Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<CourtRepository>().As<ICourtRepository>();
            builder.RegisterType<ReservationRepository>().As<IReservationRepository>();
            builder.RegisterType<ReservationMemberRepository>().As<IReservationMemberRepository>();
        }
    }
}
