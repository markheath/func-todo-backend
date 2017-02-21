module.exports = function (context, req, todostable) {
    context.log("Retrieved todos:", todostable);
    todostable.forEach(t => { delete t.PartitionKey; delete t.RowKey; });
    res = {
        status: 200,
        body: todostable 
    }; 
    context.done(null, res);
};