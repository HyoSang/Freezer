package com.example.light.freezer;

import android.app.Activity;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.util.Calendar;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.view.View;
import android.widget.Button;
import android.widget.TimePicker;
import android.widget.Toast;

/**
 * Created by Suyoung on 2017-06-03.
 */

public class TimeSettingActivity extends Activity {
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.timepicker);
        final TimePicker mTimePicker = (TimePicker) findViewById(R.id.time);
        Button cancel = (Button) findViewById(R.id.btncancel);
        Button start = (Button) findViewById(R.id.btnsetting);
        final Intent intent = new Intent(this, FreezingService.class);
        final Intent i = new Intent(this, ServiceStop.class);

        cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        start.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int hour, min;
                if(Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.M) {
                    hour = mTimePicker.getHour();
                    min = mTimePicker.getMinute();
                } else {
                    hour = mTimePicker.getCurrentHour();
                    min = mTimePicker.getCurrentMinute();
                }

                Calendar end_calendar = Calendar.getInstance();
                end_calendar.set(Calendar.HOUR_OF_DAY,hour);
                end_calendar.set(Calendar.MINUTE,min);
                end_calendar.set(Calendar.SECOND,0);
                Calendar current_time = Calendar.getInstance();

                final long end_time = end_calendar.getTimeInMillis() - current_time.getTimeInMillis();
                if(end_time > 0) {
                    try {
                        FileInputStream fs = getApplicationContext().openFileInput("LoginID");
                        BufferedReader buf = new BufferedReader(new InputStreamReader(fs));
                        intent.putExtra("ID",buf.readLine());
                        fs.close();
                        buf.close();
                        fs = getApplicationContext().openFileInput("LoginPass");
                        buf = new BufferedReader(new InputStreamReader(fs));
                        intent.putExtra("Pass",buf.readLine());
                        fs.close();
                        buf.close();
                    }catch(Exception e){}
                    startService(intent);

                    AlarmManager am = (AlarmManager) getApplicationContext().getSystemService(Context.ALARM_SERVICE);
                    PendingIntent pIntent = PendingIntent.getActivity(getApplicationContext(), 0, i, 0);
                    am.set(AlarmManager.RTC_WAKEUP,end_calendar.getTimeInMillis(),pIntent);

                    File file = new File("Freeze.txt");
                    try{
                        FileOutputStream fos = openFileOutput("Freeze.txt", 0);
                        PrintWriter writer = new PrintWriter(fos);
                        writer.println(0);
                        writer.println(hour);
                        writer.println(min);
                        writer.close();
                    }
                    catch (IOException ie) {}
                }
            }
        });
    }
}
