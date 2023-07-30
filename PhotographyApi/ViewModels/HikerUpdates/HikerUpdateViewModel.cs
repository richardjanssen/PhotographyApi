using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public record HikerUpdateViewModel(
    DateTime Date,
    string Title,
    PlaceHighlightType Type,
    string? Text,
    double Distance,
    int? AlbumId);
