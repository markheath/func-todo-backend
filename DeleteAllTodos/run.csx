#r "Microsoft.WindowsAzure.Storage"
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

class Todo : TableEntity
{
    public string title { get; set; }
}

public static HttpResponseMessage Run(HttpRequestMessage req, CloudTable todosTable, TraceWriter log)
{
    log.Info("Request delete all todos");
    var allTodos = todosTable.ExecuteQuery<Todo>(new TableQuery<Todo>())
                    .ToList();
    foreach(var todo in allTodos) {
        log.Info($"Deleting {todo.RowKey} {todo.title}");
        var operation = TableOperation.Delete(todo);
        todosTable.Execute(operation);
    }

    return req.CreateResponse(HttpStatusCode.OK);
}
