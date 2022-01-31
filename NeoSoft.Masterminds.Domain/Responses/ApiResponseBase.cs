﻿using System.Collections.Generic;

namespace NeoSoft.Masterminds.Domain.Models.Responses
{
    public abstract class ApiResponseBase
    {
        public bool IsSuccess => StatusCode == 200;
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public IList<ValidationMessage> ValidationMessages { get; set; }
    }
}
