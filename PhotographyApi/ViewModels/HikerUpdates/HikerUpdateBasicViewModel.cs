using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public record HikerUpdateBasicViewModel(int Id, string Title, PlaceHighlightType Type, double Distance);
