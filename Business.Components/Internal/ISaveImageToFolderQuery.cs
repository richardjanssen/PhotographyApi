using Business.Entities;
using Microsoft.AspNetCore.Http;

namespace Business.Components.Internal;

public interface ISaveImageToFolderQuery
{
    IReadOnlyCollection<Image> Execute(IFormFile file, IReadOnlyCollection<Size> maxDimensions);
}
