using ModuleJS.Web.Mvc.DataAnnotations;

namespace ModuleJS.Web.Example.Models
{
    public class TimerModule
    {
        public bool HideMinutes { get; set; }
        public bool HideSeconds { get; set; }
        public bool HideHundredth { get; set; }

        [ModuleOption]
        public bool RememberTime { get; set; }
    }
}