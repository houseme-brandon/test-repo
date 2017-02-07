using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessControl.Domain
{
    public class CodedUrlBuilder
    {
        private readonly string templateUrl;
        private readonly string placeholderText;
                
        public CodedUrlBuilder(string templateUrl, string placeholderText)
        {
            if (!templateUrl.Contains(placeholderText))
            {
                throw new ArgumentOutOfRangeException(nameof(templateUrl), $"Given {templateUrl} and {placeholderText} - template url doesn't contain the placeholder text");
            }

            this.templateUrl = templateUrl;
            this.placeholderText = placeholderText;            
        }

        public string BuiltUrl (string code)
        {
            return templateUrl.Replace(placeholderText, code);
        }
    }
}
