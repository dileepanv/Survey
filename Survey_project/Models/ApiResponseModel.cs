﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Survey_project.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {

        }
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }

        public ApiResponse(string message, long statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }

    public class ServiceResponse<T> : ApiResponse
    {
        public ServiceResponse()
        {

        }

        [JsonProperty("data")]
        public T Data { get; set; }

        public ServiceResponse(long StatusCode, string Message, T Data)
        {
            this.Message = Message;
            this.Data = Data;
            this.StatusCode = StatusCode;
        }
    }

    public class LoginResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }
     
       
    }
}
