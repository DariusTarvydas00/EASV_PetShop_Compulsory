using System.Collections.Generic;
using CrashCourseVideoMenu.Core.DomainService;
using CrashCourseVideoMenu.Core.Entity;

namespace CrashCourseVideoMenu.Infrastructure.Static.Data.Repositories
{
    public class VideoRepository: IVideoRepository
    {
        private static int _id = 0;
        private readonly List<Video> Videos = new List<Video>();
        public Video Create(Video video)
        {
            video.Id = _id++;
            Videos.Add(video);
            return video;
        }

        public Video Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Video Update(Video VideoToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public List<Video> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public Video ReadById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}