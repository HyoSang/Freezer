package com.example.light.freezer;

import android.app.Activity;
import android.app.TimePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.view.View;
import android.widget.Button;
import android.widget.TimePicker;
import android.widget.Toast;

import java.util.Calendar;

public class MainActivity extends Activity{

    public static boolean hasWindowOverlayPermission(Context context){
        if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.M){
            if(!Settings.canDrawOverlays(context)){
                return false;
            }
        }
        return true;
    }
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Button btnFreezing = (Button) findViewById(R.id.btnFreezing);
        Button btnPC = (Button) findViewById(R.id.btnPC);
        Button btnWhite = (Button) findViewById(R.id.btnWhite);
        Button Stop = (Button) findViewById(R.id.btnStop);

        if(!hasWindowOverlayPermission(getApplicationContext()))
        {
            Uri uri = Uri.fromParts("package",getPackageName(),null);
            Intent intent2 = new Intent(Settings.ACTION_MANAGE_OVERLAY_PERMISSION,uri);
            startActivityForResult(intent2,3333);
        }

        btnPC.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), LoginActivity.class);
                startActivity(intent);
            }
        });

        btnFreezing.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent2 = new Intent(getApplicationContext(), TimeSettingActivity.class);
                startActivity(intent2);
            }
        });

        Stop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FreezingService.onPause();
            }
        });
    }
}
