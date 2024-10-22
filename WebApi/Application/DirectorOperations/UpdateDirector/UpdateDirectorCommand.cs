using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.UpdateDirector;

public class UpdateDirectorCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int DirectorId { get; set; }
    public UpdateDirectorModel Model { get; set; }

    public UpdateDirectorCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
        if (director is null)
            throw new InvalidOperationException("Yönetmen bulunamadı.");

        director = _mapper.Map<UpdateDirectorModel, Director>(Model, director);
        _context.Update(director);
        _context.SaveChanges();

    }
}

public class UpdateDirectorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsActive { get; set; }
}