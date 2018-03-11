namespace ModuleJS.Web.Mvc.Html.Builders
{
    /// <summary>A node that contains raw html</summary>
    /// <remarks>
    /// @author: stho 01.02.2018
    /// </remarks>
    public class RawHtml : INode
    {
        private readonly string _html;
        public RawHtml(string html) => _html = html;
        public string Render() => _html;
    }
}