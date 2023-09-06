using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Collection;

namespace GamePlay.DAL.MappingProfiles;

public class CollectionProfile : Profile {
    public CollectionProfile() {
        CreateMap<Collection, CollectionModel>();
        CreateMap<CollectionModel, Collection>();
        CreateMap<CreateCollectionModel, Collection>();
    }
}