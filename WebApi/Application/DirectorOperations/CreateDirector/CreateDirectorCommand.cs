using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.CreateDirector;

public class CreateDirectorCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public CreateDirectorModel Model { get; set; }

    public CreateDirectorCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var director = _context.Directors.FirstOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
        if (director is not null)
            throw new InvalidOperationException("Yönetmen zaten mevcut.");

        director = _mapper.Map<Director>(Model);
        _context.Add(director);
        _context.SaveChanges();

    }
}

public class CreateDirectorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
}