module.exports = function (context, req, todo) {
    context.log("Retrieving todo", req.params.id, todo);
    if (todo) {
        delete todo.RowKey;
        delete todo.PartitionKey; 
        res = {
            status: 200,
            body: todo 
        }; 

    }
    else {
        res = {
            status: 404,
            body: todo 
        }; 

    }
    context.done(null, res);
};