#r "System.Net.Http"
#r "Newtonsoft.Json"

open System.Net
open System.Net.Http
open Newtonsoft.Json

type Todo = {
    [<JsonIgnore>]
    PartitionKey: string;
    [<JsonIgnore>]
    RowKey: string;
    id: string;
    title: string;
    url: string;
    order: System.Nullable<int>;
    completed: bool
}

let Run(req: HttpRequestMessage, log: TraceWriter, todosTable: IAsyncCollector<Todo>) =
    async {        
        let! data = req.Content.ReadAsStringAsync() |> Async.AwaitTask
        log.Info(sprintf "Got a task: %s" data)
        let todo = JsonConvert.DeserializeObject<Todo>(data)
        let newId =  Guid.NewGuid().ToString("N")
        let newUrl = req.RequestUri.GetLeftPart(UriPartial.Path) + newId;
        let tableEntity = { todo with PartitionKey="TODO"; RowKey=newId; id=newId; url=newUrl }
        let awaitTask = Async.AwaitIAsyncResult >> Async.Ignore 
        do! todosTable.AddAsync(tableEntity) |> awaitTask
        log.Info(sprintf "Table entity %A." tableEntity)
        let respJson = JsonConvert.SerializeObject(tableEntity);
        let resp = new HttpResponseMessage(HttpStatusCode.OK)

        resp.Content <- new StringContent(respJson)
        return resp
    } |> Async.RunSynchronously
