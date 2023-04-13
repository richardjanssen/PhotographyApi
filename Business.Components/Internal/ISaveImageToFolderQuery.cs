using Business.Entities;
using Business.Entities.Dto;
using Microsoft.AspNetCore.Http;

namespace Business.Components.Internal;

public interface ISaveImageToFolderQuery
{
    IReadOnlyCollection<Image> Execute(IFormFile file, IReadOnlyCollection<Size> maxDimensions);
}
