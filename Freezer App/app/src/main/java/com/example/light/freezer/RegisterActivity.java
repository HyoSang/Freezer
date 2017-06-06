package com.example.light.freezer;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class RegisterActivity extends Activity{

    EditText editID, editPW, editPW2;


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

                if(password.equals(password2)){

                }
                else {
                    Toast.makeText(RegisterActivity.this, "비밀번호가 일치하지 않습니다!\n다시 입력해주세요!", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
}
