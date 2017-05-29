var express = require('express');
var http = require('http');
var app = express();
var fs = require('fs');
var bodyParser = require('body-parser');
var MongoClient = require('mongodb').MongoClient;
var ObjectID = require('mongodb').ObjectID;
var assert = require('assert');
var url = 'mongodb://localhost:27017/Freezer';
var async = require('async');
var httpServer = http.createServer(app).listen(8005, function (req, res) {
    console.log("Server on!");
});
var io = require("socket.io").listen(httpServer, {'pingInterval':2000,'pingTimeout':5000});
var socket_List = [];
var ID_List = [];
app.use(bodyParser.json());
app.get("/", function (req, res) {
    fs.readFile('test.html', function (error, data) {
        res.writeHead(200, { 'Content-Type': 'text/html' });
        res.end(data);
    });
      
});

app.post('/start', function (req, res) {
    io.to(socket_List[req.body.ID]).emit("start", "test");
    res.end();
});

app.post('/end', function (req, res) {
    io.to(socket_List[req.body.ID]).emit("end", "test");
    res.end();
});

app.post('/mobileLogin', function (req, res) {
    
           
});

io.on('connection', function (socket)
{
    socket.on('LoginReq', function (data) {
        var jsonObj = JSON.parse(data);
        async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, collection) {
                   collection
                   .find({ "ID": jsonObj.id, "Pass": jsonObj.pass })
                   .toArray(function (err, items) {
                       assert.equal(err, null);
                       if (items.length != 0) {
                           callback(null, "success");
                       }
                       else callback(null, "fail");
                   });

               })
                              
               db.close();
           });
       }
        ],
   function (callback, message) {
       socket.emit("LoginRes", message);
       
   });

    });
    socket.on('setSocket', function (data) {
        console.log(socket.id);
        socket_List[data] = socket.id;
        ID_List[socket.id] = data;

    });
    socket.on('disconnect', function (data) {
        socket_List[ID_List[socket.id]] = null;
    });

    
});


