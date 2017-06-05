package com.example.light.freezer;

import android.app.Activity;
import java.util.Calendar;

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
                    startService(intent);
                    Handler end_handler = new Handler();
                    end_handler.postDelayed(new Runnable() {
                        @Override
                        public void run() {
                            stopService(intent);
                        }
                    }, end_time);
                    finish();
                }
            }
        });
    }
}
