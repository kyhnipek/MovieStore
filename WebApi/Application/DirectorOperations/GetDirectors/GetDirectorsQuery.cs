using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.GetDirectors;

public class GetDirectorsQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetDirectorsQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<DirectorsViewModel> Handle()
    {
        var directorList = _context.Directors.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Director>();
        List<DirectorsViewModel> vm = _mapper.Map<List<DirectorsViewModel>>(directorList);
        return vm;

    }
}

public class DirectorsViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
}