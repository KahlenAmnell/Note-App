using AutoMapper;
using Note_App_API.Entities;
using Note_App_API.Models;

namespace Note_App_API.Profiles;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<Note, NoteDto>();
    }
}