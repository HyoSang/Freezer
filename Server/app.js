﻿var express = require('express');
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
var connection_List = [];
app.use(bodyParser.json());
app.get("/", function (req, res) {
    fs.readFile('test.html', function (error, data) {
        res.writeHead(200, { 'Content-Type': 'text/html' });
        res.end(data);
    });
      
});

MongoClient.connect(url, function (err, db) {
    assert.equal(null, err);
    db.collection('users', function (err, users) {
        users.update({ Connect: 'T' }, { $set: { Connect: 'F' } }, {}, function (err) {
            db.close();
        });
        
    });

});


app.post('/start', function (req, res) {
    var cont;
    async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, collection) {
                   collection
                   .findOne({ "ID": req.body.ID, "Pass": req.body.Pass }, function (err, data) {
                       assert.equal(err, null);
                       if (data != null) {
                           cont = data.Connect;
                           db.close();
                           callback(null, "success");
                       }
                       else {
                           db.close();
                           callback(null, "fail");
                       }
                   })

               })

               db.close();
           });
       }
    ],
   function (callback, message) {
       if (message == "success") {
           if (cont=='F') {
               connection_List[req.body.ID] = true;
               res.end("fail");
           }
           else {
               io.to(socket_List[req.body.ID]).emit("start", "test");
               res.end("success");
           }
       }
       else {
           res.end("fail");
           connection_List[req.body.ID] = true;
       }

   });
        
});

app.post('/end', function (req, res) {
    async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, collection) {
                   collection
                   .findOne({ "ID": req.body.ID, "Pass": req.body.Pass }, function (err, data) {
                       assert.equal(err, null);
                       if (data != null) {
                           cont = data.Connect;
                           db.close();
                           callback(null, "success");
                       }
                       else {
                           db.close();
                           callback(null, "fail");
                       }
                   })
                               
                 

               })

              
           });
       }
    ],
   function (callback, message) {
       if (message == "success") {
           if (cont=='F') {
               connection_List[req.body.ID] = false;
               res.end("fail");
           }
           else {
               io.to(socket_List[req.body.ID]).emit("end", "test");
               res.end("success");
           }
       }
       else {
           res.end("fail");
           
       }

   });
});

app.post('/mobileLogin', function (req, res) {
    
    async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, collection) {
                   collection
                   .find({ "ID": req.body.ID, "Pass": req.body.Pass })
                   .toArray(function (err, items) {
                       assert.equal(err, null);
                       if (items.length != 0) {
                           callback(null, "success");
                           console.log(req.body.ID + " 모바일 로그인");
                       }
                       else callback(null, "fail");
                   });

               })

               db.close();
           });
       }
    ],
   function (callback, message) {
       res.end(message);

   });
           
});

app.post('/Register', function (req, res) {
    async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, collection) {
                   collection
                   .find({ "ID": req.body.ID, "Pass": req.body.Pass })
                   .toArray(function (err, items) {
                       assert.equal(err, null);
                       if (items.length == 0) {

                           collection.insert({ ID: req.body.ID, Pass: req.body.Pass, Connect: 'F' }, function (err) {
                               db.close();
                               callback(null, "success");
                               console.log(req.body.ID + " 회원가입");

                           });

                       }
                       else {
                           db.close();
                           callback(null, "fail");
                       }
                   });

               })

               
           });
       }
    ],
   function (callback, message) {
       res.end(message);

   });

});


io.on('connection', function (socket)
{
    socket.on('LoginReq', function (data) {
        var jsonObj = JSON.parse(data);
        async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, users) {
                   users
                   .findOne({ ID: jsonObj.id, Pass: jsonObj.pass }, function (err, data2) {
                       if (data2 != null) {
                           if (data2.Connect == 'F') {
                               users
                               .update({ ID: jsonObj.id }, { $set: { Connect: 'T' } }, {}, function (err, tasks) {
                                   db.close();
                                   callback(null, "success");
                               });
                           }
                           else
                           {
                               db.close();
                               callback(null, "duplicate");
                           }
                        }
                       else
                       {
                           db.close();
                           callback(null, "fail");
                       }
                   });
                   
               });
                                             
           });
       }
        ],
   function (callback, message) {
       socket.emit("LoginRes", message);
       
   });

    });
    socket.on('setSocket', function (data) {
        console.log("PC에서 "+data+" 로그인");
        socket_List[data] = socket.id;
        ID_List[socket.id] = data;
        MongoClient.connect(url, function (err, db) {
            assert.equal(null, err);

            db.collection('users', function (err, users) {
                users
                .findOne({ ID: ID_List[socket.id] }, function (err, data2) {
                    if (data2 != null) {
                        users
                        .update({ ID: ID_List[socket.id] }, { $set: { Connect: 'T' } }, {}, function (err, tasks) {
                            db.close();
                        });
                    }
                    else {
                        db.close();
                    }
                });

            });

        });
        if(connection_List[data]!="undefined")
        {
            if(connection_List[data])
            {
                io.to(socket_List[data]).emit("start", "test");
                connection_List[data] = "undefined";
            }
            else
            {
                io.to(socket_List[data]).emit("end", "test");
                connection_List[data] = "undefined";
            }
        }
           
    });
    socket.on('disconnect', function (data) {
        console.log("PC에서 " + ID_List[socket.id] + " 로그 아웃");
        socket_List[ID_List[socket.id]] = null;
        async.waterfall([

       function (callback) {
           MongoClient.connect(url, function (err, db) {
               assert.equal(null, err);

               db.collection('users', function (err, users) {
                   users
                   .findOne({ ID: ID_List[socket.id] }, function (err, data2) {
                       if (data2 != null) {
                           users
                           .update({ ID: ID_List[socket.id] }, { $set: { Connect: 'F' } }, {}, function (err, tasks) {
                               db.close();
                               callback(null, "success");
                           });
                       }
                       else {
                           db.close();
                           callback(null, "fail");
                       }
                   });
                   
               });
               
           });
       }
        ],
   function (callback, message) {

   });
    });

    socket.on('Force', function (data) {
        io.to(socket_List[data]).emit("start", "test");
    });

    
});


