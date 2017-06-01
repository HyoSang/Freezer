package com.java42.freezing;

import android.annotation.TargetApi;
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
import android.view.WindowManager;
import android.widget.Button;
import android.widget.Toast;

import java.util.Calendar;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Button startfreezing = (Button) findViewById(R.id.freezing);
        Button stopfreezing = (Button) findViewById(R.id.stop);
        final Intent intent = new Intent(this, FreezingService.class);

        int start_hour = 19;
        int start_minute = 27;

        final Calendar start_calender = Calendar.getInstance();
        start_calender.set(Calendar.HOUR_OF_DAY, start_hour);
        start_calender.set(Calendar.MINUTE,start_minute);
        start_calender.set(Calendar.SECOND,0);

        int end_hour = 20;
        int end_minute = 25;

        final Calendar end_calender = Calendar.getInstance();
        end_calender.set(Calendar.HOUR_OF_DAY, end_hour);
        end_calender.set(Calendar.MINUTE,end_minute);
        start_calender.set(Calendar.SECOND,0);

        Calendar current_time = Calendar.getInstance();
        current_time.set(Calendar.SECOND, 0);

        long start_time = start_calender.getTimeInMillis() - current_time.getTimeInMillis();
        final long end_time = end_calender.getTimeInMillis() - current_time.getTimeInMillis();

        if(start_time < 0) start_time = 0;

        Handler start_handler = new Handler();
        start_handler.postDelayed(new Runnable() {
            @Override
            public void run() {
                startService(intent);
            }
        },start_time);

        Handler end_handler = new Handler();
        end_handler.postDelayed(new Runnable() {
            @Override
            public void run() {
                stopService(intent);
            }
        }, end_time);

        stopfreezing.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                stopService(intent);
            }
        });
    }
}
