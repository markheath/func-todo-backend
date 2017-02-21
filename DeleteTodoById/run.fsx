#r "System.Net.Http"
#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

open System.Net
open System.Net.Http
open Microsoft.WindowsAzure.Storage.Table;
open Newtonsoft.Json

type Named = {
    name: string
}

let Run(req: HttpRequestMessage, id: string, todosTable: CloudTable, log: TraceWriter) =
    async {
        log.Info(sprintf "Request delete of todo %s." id)
        let todo = TableEntity("TODO", id)
        todo.ETag <- "*"
        let operation = TableOperation.Delete(todo)
        let awaitTask = Async.AwaitIAsyncResult >> Async.Ignore 
        do! todosTable.ExecuteAsync(operation) |> awaitTask;
        // returns success even if TODO doesn't exist
        return req.CreateResponse(HttpStatusCode.NoContent);
    } |> Async.RunSynchronously
