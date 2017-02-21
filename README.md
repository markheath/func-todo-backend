# Azure Functions Todo Backend Implementation

An implementation of the [Todo-Backend API](http://todobackend.com/) using Azure Functions,
making use of six Azure functions in a single Azure Function app, using three languages (C#, JavaScript and F#).

The TODOs are stored in Azure Table Storage and accessed by the functions using Input and Output Bindings.

## Try it out Live

Continuous deployment is set up to deploy the Function App via Git to https://functodobackend.azurewebsites.net/api/todos/

You can [Run the unit tests](http://todobackend.com/specs/index.html?https://functodobackend.azurewebsites.net/api/todos/)
or [try out the client](http://todobackend.com/client/index.html?https://functodobackend.azurewebsites.net/api/todos/)

## Running Locally

You need the [Azure Functions CLI](https://www.npmjs.com/package/azure-functions-cli) installed.
You'll need to point at an Azure storage account so it has somewhere to store the todos Table. 
```
func settings add AzureWebJobsStorage "DefaultEndpointsProtocol=https;AccountName=<YOURSTORAGEAPP>;AccountKey=<YOURACCOUNTKEY>"
func settings add AzureWebJobsDashboard "DefaultEndpointsProtocol=https;AccountName=<YOURSTORAGEAPP>;AccountKey=<YOURACCOUNTKEY>"
```
Now we can run with
```
func host start
```
Note that if the todos table doesn't exist, you'll need to call the POST method first to create a TODO which will create the table
