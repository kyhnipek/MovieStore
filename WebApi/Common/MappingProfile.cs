using AutoMapper;
using WebApi.Application.ActorOperations.CreateActor;
using WebApi.Application.ActorOperations.UpdateActor;
using WebApi.Application.ActorOperations.GetActorDetail;
using WebApi.Application.ActorOperations.GetActors;
using WebApi.Application.DirectorOperations.CreateDirector;
using WebApi.Application.DirectorOperations.UpdateDirector;
using WebApi.Application.DirectorOperations.GetDirectorDetail;
using WebApi.Application.DirectorOperations.GetDirectors;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.Application.GenreOperations.GetGenreDetail;
using WebApi.Application.GenreOperations.GetGenres;
using WebApi.Application.MovieOperations.CreateMovie;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.Application.MovieOperations.GetMovies;
using WebApi.Application.OrderOperations.GetOrderDetail;
using WebApi.Application.OrderOperations.GetOrders;
using WebApi.Application.UserOperations.SignUp;
using WebApi.Entities;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Movie, MoviesViewModel>();
        CreateMap<Movie, MovieDetailViewModel>();
        CreateMap<Movie, MovieDetailViewModelShort>();
        CreateMap<CreateMovieModel, Movie>();


        CreateMap<Actor, ActorsViewModel>();
        CreateMap<CreateActorModel, Actor>();
        CreateMap<Actor, ActorDetailViewModel>();
        CreateMap<UpdateActorModel, Actor>();

        CreateMap<Director, DirectorsViewModel>();
        CreateMap<CreateDirectorModel, Director>();
        CreateMap<Director, DirectorDetailViewModel>();
        CreateMap<UpdateDirectorModel, Director>();

        CreateMap<Genre, GenresViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
        CreateMap<CreateGenreModel, Genre>();
        CreateMap<UpdateGenreModel, Genre>();

        CreateMap<Order, OrdersViewModel>();
        CreateMap<Order, OrderDetailViewModel>();

        CreateMap<SignUpModel, User>();
    }
}