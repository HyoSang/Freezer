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

        int hour = 16;
        int minute = 59;

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
    }
}
