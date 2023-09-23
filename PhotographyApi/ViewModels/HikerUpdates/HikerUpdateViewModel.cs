using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public record AddHikerUpdateViewModel(
    string Title,
    PlaceHighlightType Type,
    string? Text,
    double Distance,
    int? AlbumId,
    int? PlaceId);
