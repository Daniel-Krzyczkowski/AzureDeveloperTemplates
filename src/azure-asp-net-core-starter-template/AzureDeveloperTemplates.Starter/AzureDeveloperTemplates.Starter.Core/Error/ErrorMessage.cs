using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.Starter.Core.Error
{
    public class ErrorMessage
    {
        public string Title { get; }
        public string Message { get; }


        public ErrorMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
