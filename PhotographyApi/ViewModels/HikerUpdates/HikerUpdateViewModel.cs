using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public record AddHikerUpdateViewModel(
    int? Id,
    string Title,
    PlaceHighlightType Type,
    string? Text,
    double? Distance,
    int? AlbumId,
    int? PlaceId);
