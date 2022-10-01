using Business.Entities;
using Microsoft.AspNetCore.Http;

namespace Business.Components.Internal;

public interface ISaveImageToFolderQuery
{
    Image Execute(IFormFile file);
}
