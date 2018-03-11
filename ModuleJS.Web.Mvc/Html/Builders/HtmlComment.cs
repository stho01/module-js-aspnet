namespace ModuleJS.Web.Mvc.Html.Builders
{
    /// <summary>A node that renders a html comment.</summary>
    /// <remarks>
    /// @author: stho 01.02.2018
    /// </remarks>
    public class HtmlComment : INode
    {
        public string Comment { get; set; }
        public string Render() => $"<!-- {Comment} -->";
    }
}