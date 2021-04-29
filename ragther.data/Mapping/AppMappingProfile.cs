using AutoMapper;
using ragther.entity.ViewModels;

namespace ragther.data.Mapping
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile()
        {
            // USER ENTITY --START--
            CreateMap<entity.User, VMUserLoginPost>();
            CreateMap<VMUserLoginPost, entity.User>();

            CreateMap<entity.User, VMUserRegisterPost>();
            CreateMap<VMUserRegisterPost, entity.User>();

            CreateMap<entity.User, VMUserProfileGet>();
            CreateMap<VMUserProfileGet, entity.User>();

            CreateMap<entity.User, VMInnerUserInfo>();
            CreateMap<VMInnerUserInfo, entity.User>();
            // USER ENTITY --END--

            // PROFILE DETAIL ENTITY --START--
            CreateMap<entity.ProfileDetail, VMUserProfileDetailGet>();
            CreateMap<VMUserProfileDetailGet, entity.ProfileDetail>();
            // PROFILE DETAIL ENTITY --END--

            //TO-DO ENTITY --START--
            CreateMap<entity.Todo, VMNewTodoPost>();
            CreateMap<VMNewTodoPost, entity.Todo>();

            CreateMap<entity.Todo, VMTodoGet>();
            CreateMap<VMTodoGet, entity.Todo>();

            CreateMap<VMTodoUpdatePost, entity.Todo>();
            CreateMap<entity.Todo, VMTodoUpdatePost>();
            //TO-DO ENTITY --END--
        }
    }
}