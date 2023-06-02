using AutoMapper;
using MovieAnalyzer.DTOs;
using MovieAnalyzer.Models;

namespace MovieAnalyzer
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AwardNomineeCsvEntry, AwardNomination>()
				.ForMember(x => x.HasProducers, x => x.Ignore());
		}
	}
}
