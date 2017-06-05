package com.example.light.freezer;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStreamReader;

/**
 * Created by YunSang on 2017-06-03.
 */

public class PCFreezing {
    Person person;
    String ID,Pass;
    PCFreezing(String ID, String Pass)
    {
        this.ID = ID;
        this.Pass = Pass;
    }
    public void startPCFreezing()
    {
        new HttpAsyncTask().execute("http://110.15.205.4:8005/start");
    }
    public void endPCFreezing()
    {
        new HttpAsyncTask().execute("http://110.15.205.4:8005/end");
    }

    private class HttpAsyncTask extends AsyncTask<String, Void, String> {


        @Override
        protected String doInBackground(String... datas) {
            person = new Person();
            person.setID(ID);
            person.setPass(Pass);

            return Post.POST(datas[0],person);
        }
    }
}
