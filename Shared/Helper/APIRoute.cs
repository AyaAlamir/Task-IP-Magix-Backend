using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Helper
{
        public static class APIRoute
        {
            public const string Template = "api/[controller]/[action]";
            public const string FileTemplate = "api/[controller]";
            public const string ControllerTemplate = "api/{0}";
            public const string ActionTemplate = "/{0}";
        }
}
