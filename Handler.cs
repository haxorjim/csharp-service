using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Response
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string> { { "Access-Control-Allow-Origin", "*" }, { "Access-Control-Allow-Credentials", "True" } };

        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class Request
    {
        [JsonProperty("httpMethod")]
        public string HttpMethod { get; set; }

        [JsonProperty("queryStringParameters")]
        public Dictionary<string, string> QueryStringParameters { get; set; }
    }

    public class ExampleResponseBody
    {
      public string Message { get; set; }

      public int SomeValue { get; set; }
    }

    public class Handler
    {
       public Response Hello(Request request)
       {
          var name = "Jane Doe";

          if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("name"))
          {
              name = request.QueryStringParameters["name"];
          }

          var body = new ExampleResponseBody
          {
              Message = $"Go Serverless! Hello {name} from HTTP!",
              SomeValue = 1
          };

          return new Response
          {
              StatusCode = 200,
              Headers = new Dictionary<string, string>(),
              Body = JsonConvert.SerializeObject(body),
          };
       }

       public Response Hi(Request request)
       {
           var body = new ExampleResponseBody
           {
               Message = $"Go Serverless! Env var something is set to: {System.Environment.GetEnvironmentVariable("SOMETHING")}",
               SomeValue = 2
           };

           return new Response
           {
               StatusCode = 200,
               Headers = new Dictionary<string, string>(),
               Body = JsonConvert.SerializeObject(body),
           };
       }
    }
}
