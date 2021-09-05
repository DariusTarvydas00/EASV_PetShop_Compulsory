using System;
using System.Collections;

namespace CrashCourseVideoMenu.Core.Entity
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string StoryLine { get; set; }
        public string Genre { get; set; }
       
    }
}