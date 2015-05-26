using System;

namespace MVC5
{
    internal class WebInvokeAttribute : Attribute
    {
        public string Method { get; set; }
        public string UriTemplate { get; set; }
    }
}