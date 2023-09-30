using Common.Common.Enums;

namespace PhotographyApi.ViewModels.HikerUpdates;

public record HikerUpdateBasicViewModel(int Id, DateTime Date, string Title, PointHighlightType Type, double? Distance);
