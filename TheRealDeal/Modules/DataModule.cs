using Autofac;
using RecreateMe.Profiles;
using RecreateMeSql.Repositories;

namespace TheRealDeal.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
        }
    }
}