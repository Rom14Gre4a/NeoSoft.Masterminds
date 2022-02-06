﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models
{
    public static class FileCommon
    {
        public static string GetPhotoPath(int profilePhotoId, HttpRequest request)
        {
            // https://localhost:5001/api/file/3
            return $"{request.Scheme}://{request.Host.Value}/api/file/{profilePhotoId}";
        }
    }
}
