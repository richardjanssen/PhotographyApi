using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public record HikerUpdateViewModel(
    string Title,
    PlaceHighlightType Type,
    string? Text,
    double Distance,
    int? AlbumId);
