using Business.Entities;

namespace Business.Interfaces;

public interface IAddPhotoQuery
{
    Photo Execute(AddPhoto photo); 
}
