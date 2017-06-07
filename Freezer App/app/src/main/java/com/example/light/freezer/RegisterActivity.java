package com.example.light.freezer;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.io.FileOutputStream;

public class RegisterActivity extends Activity{

    EditText editID, editPW, editPW2;
    Person person;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.register);

        editID = (EditText)findViewById(R.id.edit_ID);
        editPW = (EditText)findViewById(R.id.edit_PW);
        editPW2 = (EditText)findViewById(R.id.edit_PW2);
        Button btnRegister = (Button)findViewById(R.id.btn_Register);

        btnRegister.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String ID=editID.getText().toString();
                String password=editPW.getText().toString();
                String password2=editPW2.getText().toString();

                if(ID.equals("")){
                    Toast.makeText(RegisterActivity.this, "ID를 입력하세요.", Toast.LENGTH_LONG).show();
                    return;
                }

                if(password.equals("")){
                    Toast.makeText(RegisterActivity.this, "비밀번호를 입력하세요.", Toast.LENGTH_LONG).show();
                    return;
                }

                if(password.equals(password2)){
                     switch(v.getId()){
                        case R.id.btn_Register:
                            new HttpAsyncTask().execute("http://110.15.205.4:8005/Register",ID,password);
                            break;
                    }
                }
                else {
                    Toast.makeText(RegisterActivity.this, "비밀번호가 일치하지 않습니다!\n다시 입력해주세요!", Toast.LENGTH_LONG).show();
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
                Toast.makeText(getBaseContext(), "회원가입 성공\n로그인 해 주세요.", Toast.LENGTH_LONG).show();
                finish();

            }
            else
            {
                Toast.makeText(getBaseContext(), "이미 존재하는 ID 입니다.", Toast.LENGTH_LONG).show();
            }


        }
    }

}
