using System.Collections.Generic;
using CrashCourseVideoMenu.Core.Entity;

namespace CrashCourseVideoMenu.Core.DomainService
{
    public interface IVideoRepository
    {
        Video Create(Video video);
        Video Delete(int id);
        Video Update(Video VideoToUpdate);
        List<Video> ReadAll();
        Video ReadById(int id);
    }
}