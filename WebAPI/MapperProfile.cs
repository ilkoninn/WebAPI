﻿
namespace WebAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Brand, CreateBrandDTO>();
            CreateMap<CreateBrandDTO, Brand>();
            CreateMap<Brand, UpdateBrandDTO>();
            CreateMap<UpdateBrandDTO, Brand > ();
        }
    }
}
