using System.Collections.Generic;

namespace ModuleJS.Web.Mvc.Html.Builders
{
    /// <summary></summary>
    /// <remarks>
    /// @author: stho 01.02.2018
    /// </remarks>
    public interface IHtmlElement : INode
    {
        /// <summary>Gets the related parent of this html element</summary>
        INode Parent { get; }
        /// <summary>Gets the list of child html builders.</summary>
        List<INode> Children { get; }
        /// <summary>Gets or sets the inner html of the element</summary>
        string InnerHtml { get; set; }
        /// <summary>Gets or sets the inner text of the element</summary>
        string InnerText { get; }
    }
}