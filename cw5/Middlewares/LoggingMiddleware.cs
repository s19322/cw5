﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw5.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                string path = httpContext.Request.Path; 
                string querystring = httpContext.Request?.QueryString.ToString();
                string method = httpContext.Request.Method.ToString();
                string bodyStr = "co tam";

                using (StreamReader reader
                 = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                 
                    httpContext.Request.Body.Position = 0;
                }
                string file="File/RequestLog";
                using (var fileWrite = File.AppendText(file))
                {
                    string text = $"[{DateTime.Now}] {method} {path}{querystring}\n{bodyStr}";
                    fileWrite.WriteLine(text);
                }
                
            }


            if(_next!=null)
            await _next(httpContext);
        }
    }

}
