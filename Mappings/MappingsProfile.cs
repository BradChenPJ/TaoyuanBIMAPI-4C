﻿using AutoMapper;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.Model;
using TaoyuanBIMAPI.ViewModel;

namespace TaoyuanBIMAPI.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile() 
        {
            this.CreateMap<BookmarkParameter, Bookmark>();
            this.CreateMap<Bookmark, BookmarkViewModel>();
            this.CreateMap<LayerParameter, Layer>();
        }
    }
}
