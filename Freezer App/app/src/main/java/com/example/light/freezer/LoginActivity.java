package com.example.light.freezer;

import android.app.Activity;
import android.content.Context;
import android.icu.util.Output;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;


public class LoginActivity extends Activity {

    EditText editID, editPW;
    Person person;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);

        editID = (EditText)findViewById(R.id.editID);
        editPW = (EditText)findViewById(R.id.editPW);
        Button btnLogin = (Button)findViewById(R.id.btnLogin);

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String ID=editID.getText().toString();
                String password=editPW.getText().toString();
                switch(v.getId()){
                    case R.id.btnLogin:
                        new HttpAsyncTask().execute("http://110.15.205.4:8005/mobileLogin",ID,password);
                        break;
                }
            }
        });

    }




    private class HttpAsyncTask extends AsyncTask<String, Void, String> {
        String ID,Pass;

        @Override
        protected String doInBackground(String... datas) {

            person = new Person();
            ID = datas[1];
            Pass = datas[2];
            person.setID(ID);
            person.setPass(Pass);

            return Post.POST(datas[0],person);
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(String result) {
            if(result.equals("success"))
            {
                try {
                    FileOutputStream fos = openFileOutput("LoginID", Context.MODE_PRIVATE);
                    fos.write(ID.getBytes());
                    fos.close();
                }catch(Exception e){ }
                try {
                    FileOutputStream fos = openFileOutput("LoginPass", Context.MODE_PRIVATE);
                    fos.write(Pass.getBytes());
                    fos.close();
                }catch(Exception e){ }
                Toast.makeText(getBaseContext(), "로그인 성공", Toast.LENGTH_LONG).show();
                finish();

            }
            else
            {
                Toast.makeText(getBaseContext(), "ID 혹은 Password가 틀렸습니다.", Toast.LENGTH_LONG).show();
            }


        }
    }



}
