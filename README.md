# Azure Functions Todo Backend Implementation

An implementation of the [Todo-Backend API](http://todobackend.com/) using Azure Functions,
making use of six Azure functions in a single Azure Function app, using three languages (C#, JavaScript and F#).

More docs coming soon...

## Running Locally

You need the azure functions CLI installed.
Point at a storage account 
```
func settings add AzureWebJobsStorage "DefaultEndpointsProtocol=https;AccountName=<YOURSTORAGEAPP>;AccountKey=<YOURACCOUNTKEY>"
func settings add AzureWebJobsDashboard "DefaultEndpointsProtocol=https;AccountName=<YOURSTORAGEAPP>;AccountKey=<YOURACCOUNTKEY>"
```
Now we can run with
```
func host start
```
Note that if the todos table doesn't exist, you'll need to call the POST method first
