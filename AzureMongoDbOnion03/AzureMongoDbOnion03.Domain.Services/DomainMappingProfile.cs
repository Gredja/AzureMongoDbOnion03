using AutoMapper;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto;

namespace AzureMongoDbOnion03.Domain.Services
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<Credit, Dto.Credit>();
            CreateMap<Dto.Credit, Credit>();
            CreateMap<Debtor, Dto.Debtor>();
            CreateMap<Dto.Debtor, Debtor>();
        }
    }
}
