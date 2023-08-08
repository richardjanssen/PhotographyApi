﻿using Business.Entities.Dto;
using Common.Common.Enums;

namespace Business.Entities.Highlights;

public class PointHighlight
{
    public PointHighlight(int id, DateTime date, PlaceHighlightType placeType, string title, double? distance, bool isManual)
    {
        Id = id;
        Date = date;
        PlaceType = placeType;
        Title = title;
        Distance = distance;
        IsManual = isManual;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public PlaceHighlightType PlaceType { get; }
    public string Title { get; }
    public double? Distance { get; }
    public bool IsManual { get; }
}
