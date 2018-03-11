using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuleJS.Web.Mvc.Html.Builders
{
    public class HtmlElement : IHtmlElement
    {
        /**
        * TODO: 
        *   As a improvment it would be nice to have a light weight version of zen coding to the 
        *   create element functions: e.g. div.class-name, .class-name, div#some-id, #some-id...
        **/

        //**********************************************
        //** statics:
        //**********************************************

        /// <summary>
        /// Creates an "element" that has no element wrapper.
        /// </summary>
        /// <returns></returns>
        public static HtmlElement CreateFragment() => new HtmlElement();

        /// <summary>
        /// Creates an element with given tagname as wrapper.
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static HtmlElement CreateElement(string tagName) => new HtmlElement(tagName);

        /// <summary>
        /// Creates an element with given tagname as wrapper.
        /// </summary>
        /// <param name="tagName">tag name</param>
        /// <param name="rootAction">A action that is invoked with root as parameter.</param>
        /// <returns>Root element</returns>
        public static HtmlElement CreateElement(string tagName, Action<HtmlElement> rootAction)
        {
            var root = new HtmlElement(tagName);
            rootAction(root);
            return root;
        }

        /// <summary>
        /// Creates an element with given tagname as wrapper.
        /// </summary>
        /// <param name="rootAction">
        /// A action that is invoked with root as parameter.
        /// Note: Paramter name decides tag name. 
        /// </param>
        /// <returns>Root element</returns>
        public static HtmlElement CreateElement(Action<HtmlElement> rootAction)
        {
            var tagName = rootAction.Method.GetParameters().FirstOrDefault().Name;
            if (tagName == null)
                throw new InvalidOperationException();

            return CreateElement(tagName, rootAction);
        }

        //**********************************************
        //** properties:
        //**********************************************

        /// <summary>Gets the parent builder of this element. Null means this is the root</summary>
        public INode Parent { get; private set; }

        /// <summary>Gets the list of child html builders.</summary>
        public List<INode> Children { get; }

        /// <summary>Sets or gets the inner html of the element.</summary>
        public string InnerHtml
        {
            get => _builder?.InnerHtml;
            set
            {
                if (_builder != null)
                    _builder.InnerHtml = value;
            }
        }

        /// <summary>Sets or gets the inner text</summary>
        public string InnerText
        {
            get => _innerText;
            set
            {
                if (_builder != null)
                    _builder.SetInnerText(value);
                _innerText = value;
            }
        }

        /// <summary>Gets the tagname</summary>
        public string TagName => _builder?.TagName;

        /// <summary>
        /// Sets the render mode
        /// Note: 
        ///     Unsupported render modes:
        ///     * StartTag
        ///     * EndTag
        /// </summary>
        /// <remarks>
        /// TODO: Check if there is any cases where we need to use StarTag or EndTag..
        /// </remarks>
        public TagRenderMode RenderMode { get; set; }

        //**********************************************
        //** fields:
        //**********************************************

        /// <summary>The tagbuilder instance for creating the acctual html tag</summary>
        private readonly TagBuilder _builder;
        private string _innerText;

        /// <summary>A html builder without container</summary>
        public HtmlElement() : this(null) { }

        /// <summary>A builder with a container</summary>
        /// <param name="tagName"></param>
        public HtmlElement(string tagName) : this(tagName, null) { }

        /// <summary></summary>
        /// <param name="tagName"></param>
        /// <param name="parent"></param>
        public HtmlElement(string tagName, HtmlElement parent)
        {
            if (tagName != null)
                _builder = new TagBuilder(tagName);

            RenderMode = TagRenderModeResolver.Resolve(tagName);
            Parent = parent;
            Children = new List<INode>();
        }

        //**********************************************
        //** public:
        //**********************************************

        /// <summary>Attaches a new child to the builder.</summary>
        /// <param name="tagName">The tag name of element.</param>
        /// <returns></returns>
        public HtmlElement AppendElement(string tagName) => AppendElement(tagName, null);

        /// <summary>Sets a new child to the builder.</summary>
        /// <param name="tagName">The tag name of element.</param>
        /// <param name="childAction">An action that exposes the child instance.</param>
        /// <returns>The parent html element</returns>
        public HtmlElement AppendElement(string tagName, Action<HtmlElement> childAction)
        {
            var child = new HtmlElement(tagName, this);
            Children.Add(child);
            childAction?.Invoke(child);
            return this;
        }

        /// <summary>
        /// Appends a child element to the element. 
        /// Note: action parameter name deciding the tagname.
        /// </summary>
        /// <param name="childAction"></param>
        /// <returns>The parent html element</returns>
        public HtmlElement AppendElement(Action<HtmlElement> childAction)
        {
            var tagName = childAction.Method.GetParameters().FirstOrDefault().Name;
            if (tagName == null)
                throw new InvalidOperationException();

            return AppendElement(tagName, childAction);
        }

        /// <summary>Attaches a comment to the list.</summary>
        /// <param name="comment">The comment</param>
        /// <returns>The parent html element</returns>
        public HtmlElement AppendComment(string comment)
        {
            var child = new HtmlComment { Comment = comment };
            Children.Add(child);
            return this;
        }

        /// <summary>Attaches a raw html string to the element.</summary>
        /// <param name="html">The html</param>
        /// <returns>The parent html element</returns>
        public HtmlElement AppendRawHtml(string html)
        {
            var htmlString = new RawHtml(html);
            Children.Add(htmlString);
            return this;
        }

        /// <summary>Attaches a raw html string to the element.</summary>
        /// <param name="html">The html</param>
        /// <returns>The parent html element</returns>
        public HtmlElement AppendRawHtml(HtmlString html)
        {
            var htmlString = new RawHtml(html.ToHtmlString());
            Children.Add(htmlString);
            return this;
        }

        /// <summary>Sets the inner element's text content</summary>
        /// <param name="txt">The text inside the element</param>
        /// <returns>The parent html element</returns>
        public HtmlElement SetInnerText(string txt)
        {
            InnerText = txt;
            return this;
        }

        /// <summary>Renders the html to a string</summary>
        /// <returns>html as string</returns>
        public override string ToString()
        {
            if (_builder == null)
            {
                var content = new List<string>();
                foreach (var child in Children)
                    content.Add(RenderRecursive(child));

                return string.Concat(content);
            }
            else
                return RenderRecursive(this);
        }

        /// <summary>Render element</summary>
        /// <returns></returns>
        public string Render() => _builder.ToString(RenderMode);

        /// <summary>Adds a css class to the element.</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement AddCssClass(string value)
        {
            _builder?.AddCssClass(value);
            return this;
        }

        /// <summary>Merge an attribute on the element</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement MergeAttribute(string key, string value) => MergeAttribute(key, value, false);

        /// <summary>Merge an attribute on the element</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HtmlElement MergeAttribute(string key, string value, bool replaceExisting)
        {
            _builder?.MergeAttribute(key, value, false);
            return this;
        }

        /// <summary>
        /// Sets the render mode
        /// Note: 
        ///     Unsupported render modes:
        ///     * StartTag
        ///     * EndTag
        /// </summary>
        /// <remarks>
        /// TODO: Check if there is any cases where we need to use StarTag or EndTag..
        /// </remarks>
        /// <param name="renderMode"></param>
        /// <returns></returns>
        public HtmlElement SetRenderMode(TagRenderMode renderMode)
        {
            RenderMode = renderMode;
            return this;
        }

        /// <summary>Sets element's style attribute with value: "display:none"</summary>
        /// <returns></returns>
        public HtmlElement DisplayNone()
        {
            MergeAttribute("style", "display:none;");
            return this;
        }

        //**********************************************
        //** private:
        //**********************************************

        /// <summary>Traverse the builder three and create a html structure as a string</summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string RenderRecursive(INode current)
        {
            var innerHtmlElements = new List<string>();
            if (current is IHtmlElement element)
            {
                foreach (var child in element.Children)
                    if (child is IHtmlElement childElement && childElement.Children.Count() > 0)
                        innerHtmlElements.Add(RenderRecursive(child));
                    else
                        innerHtmlElements.Add(child.Render());

                element.InnerHtml = string.Concat(innerHtmlElements);
            }

            return current.Render();
        }

        //**********************************************
        //** inner classes/structs/enums
        //**********************************************

        /// <summary>
        /// A resolver that evaluates a tagname and returns
        /// the normaly used TagRenderMode for the corresponding tag/element.
        /// </summary>
        /// <remarks>
        /// This is private for now.. not shure if we want this amongst all other class types..
        /// </remarks>
        private static class TagRenderModeResolver
        {
            public static readonly string[] SelfColosingTags = new[]
            {
                "img", "hr","br","area","base","col","command",
                "embed","input","keygen","link","meta","param","source",
                "track","wbr",
            };

            public static TagRenderMode Resolve(string tagName)
            {
                if (SelfColosingTags.Contains(tagName))
                    return TagRenderMode.SelfClosing;

                return TagRenderMode.Normal;
            }
        }
    }
}
