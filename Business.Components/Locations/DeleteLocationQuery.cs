using Business.Entities.Dto;
using Business.Interfaces.Locations;
using Data.Interfaces;

namespace Business.Components.Locations;

public class DeleteLocationQuery : IDeleteLocationQuery
{ 
    private readonly IPhotographyRepository _photographyRepository;

    public DeleteLocationQuery(IPhotographyRepository photographyRepository) => _photographyRepository = photographyRepository;

    public async Task Execute(int id) => await _photographyRepository.DeleteLocation(id);
}
