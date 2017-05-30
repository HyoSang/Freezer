package com.java42.freezing;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Handler;
import android.provider.Settings;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import java.util.Calendar;

public class MainActivity extends AppCompatActivity {

    public static boolean hasWindowOverlayPermission(Context context){
        if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.M){
            if(!Settings.canDrawOverlays(context)){
                return false;
            }
        }
        return true;
    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Button startfreezing = (Button) findViewById(R.id.freezing);
        Button stopfreezing = (Button) findViewById(R.id.stop);
        final Intent intent = new Intent(this, FreezingService.class);

        int hour = 24;
        int minute = 15;

        intent.putExtra("HOUR", hour);
        intent.putExtra("MINUTE", minute);

        final Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.HOUR_OF_DAY, hour);
        calendar.set(Calendar.MINUTE,minute);

        Calendar calendar2 = Calendar.getInstance();
        final long i = calendar.getTimeInMillis() - calendar2.getTimeInMillis();

        startfreezing.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startService(intent);
                Handler handler = new Handler();
                handler.postDelayed(new Runnable() {
                    @Override
                    public void run() {
                        stopService(intent);
                    }
                }, i);
            }
        });

        stopfreezing.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                stopService(intent);
            }
        });

        if(!hasWindowOverlayPermission(getApplicationContext()))
        {
            Uri uri = Uri.fromParts("package",getPackageName(),null);
            Intent intent2 = new Intent(Settings.ACTION_MANAGE_OVERLAY_PERMISSION,uri);
            startActivityForResult(intent2,3333);
        }
    }

}
