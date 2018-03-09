using ModuleJS.Web.Mvc.DataAnnotations;

namespace ModuleJS.Web.Example.Models
{
    public class TimerModule
    {
        [ModuleOption]
        public long StartTime { get; set; }
    }
}