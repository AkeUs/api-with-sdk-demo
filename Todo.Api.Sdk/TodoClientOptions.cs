using System;

namespace Todo.Api.Sdk {
    public class TodoClientOptions {
        public string Url { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}