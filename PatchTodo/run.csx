#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Todo: TableEntity
{
    public string id { get; set; }
    public string title { get; set; }
    public string url { get; set; }
    public int? order { get; set; }
    public bool completed { get; set; }
}

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, string id, Todo todo, CloudTable todoTable, TraceWriter log)
{
    log.Info($"Patching {id}");
    if (todo == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

    var patch = await req.Content.ReadAsAsync<JObject>();
    log.Info($"Patching with id={patch["id"]}|title={patch["title"]}|url={patch["url"]}|order={patch["order"]}|completed={patch["completed"]}|");
    if (patch["title"] != null)
        todo.title = (string)patch["title"];
    if (patch["order"] != null)
        todo.order = (int)patch["order"];
    if (patch["completed"] != null)
        todo.completed = (bool)patch["completed"];
    //todo.ETag = "*";
    var operation = TableOperation.Replace(todo);
    await todoTable.ExecuteAsync(operation);

    var resp = new HttpResponseMessage(HttpStatusCode.OK);
    
    var json = JsonConvert.SerializeObject(new { todo.id, todo.title, todo.order, todo.completed, todo.url });
    resp.Content = new StringContent(json);
    return resp;
}