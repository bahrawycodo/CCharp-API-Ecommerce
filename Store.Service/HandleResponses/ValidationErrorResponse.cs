﻿namespace Store.Service.HandleResponses
{
    public class ValidationErrorResponse : CustomException
    {
        public ValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
