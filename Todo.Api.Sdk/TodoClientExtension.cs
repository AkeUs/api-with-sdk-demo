using System;
using Microsoft.Extensions.DependencyInjection;
using Todo.Api.Sdk.Contracts;

namespace Todo.Api.Sdk {
    
    public static class TodoClientExtension {

        public static void AddTodoClient(this IServiceCollection services, TodoClientOptions options) {
            services.AddSingleton(options);
            services.AddHttpClient<ITodoClient, TodoClient>(client => {
                client.BaseAddress = new Uri(options.Url);
                client.Timeout = options.Timeout;
            });
        }
    }
}
