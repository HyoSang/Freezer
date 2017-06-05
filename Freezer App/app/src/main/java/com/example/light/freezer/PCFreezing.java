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
    Activity mActivity;
    Person person;
    Context mContext;
    PCFreezing(Activity activity)
    {
        mActivity = activity;
    }
    public void startPCFreezing()
    {
        new HttpAsyncTask().execute("http://110.15.205.4:8005/strat");
    }
    public void endPCFreezing()
    {
        new HttpAsyncTask().execute("http://110.15.205.4:8005/end");
    }

    private class HttpAsyncTask extends AsyncTask<String, Void, String> {
        String ID,Pass;

        @Override
        protected String doInBackground(String... datas) {
            mContext = mActivity.getApplicationContext();
            person = new Person();
            try {
               FileInputStream fs = mContext.openFileInput("LoginID");
                BufferedReader buf = new BufferedReader(new InputStreamReader(fs));
                ID = buf.readLine();
                fs.close();
                buf.close();
               fs = mContext.openFileInput("LoginPass");
               buf = new BufferedReader(new InputStreamReader(fs));
                Pass = buf.readLine();
                fs.close();
                buf.close();
            }catch(Exception e){}
            person.setID(ID);
            person.setPass(Pass);

            return Post.POST(datas[0],person);
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(String result) {
            if(result.equals("success"))
            {
                Toast.makeText(mActivity.getBaseContext(), "PC 연동 성공", Toast.LENGTH_LONG).show();
            }
            else
            {
                Toast.makeText(mActivity.getBaseContext(), "PC 연동 실패", Toast.LENGTH_LONG).show();
            }


        }
    }
}
