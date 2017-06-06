package com.example.light.freezer;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStreamReader;
import java.util.Calendar;
import java.util.logging.Handler;

/**
 * Created by Suyoung on 2017-05-22.
 */
public class FreezingReceiver extends BroadcastReceiver {

    @Override
    public void onReceive(final Context context, Intent intent) {
        String action = intent.getAction();
        long end_time = 0;
        if (action.equals("android.intent.action.BOOT_COMPLETED")) {
            try {
                FileInputStream fis = context.openFileInput("Freeze.txt");
                BufferedReader reader = new BufferedReader(new InputStreamReader(fis));
                String str = reader.readLine();
                int hour = Integer.parseInt(str);
                str = reader.readLine();
                int min = Integer.parseInt(str);
                Calendar end_calendar = Calendar.getInstance();
                end_calendar.set(Calendar.HOUR_OF_DAY,hour);
                end_calendar.set(Calendar.MINUTE,min);
                end_calendar.set(Calendar.SECOND, 0);
                Calendar current_time = Calendar.getInstance();
                end_time = end_calendar.getTimeInMillis() - current_time.getTimeInMillis();
                Toast.makeText(context,""+end_time,Toast.LENGTH_SHORT).show();
            }
            catch (FileNotFoundException e) { end_time = 0; }
            catch (Exception o) { end_time = 0; }
        }
        if(end_time > 0) {
            final Intent i = new Intent(context,FreezingService.class);
            context.startService(i);
            android.os.Handler end_handler = new android.os.Handler();
            end_handler.postDelayed(new Runnable() {
                @Override
                public void run() {
                    context.stopService(i);
                }
            }, end_time);
        }
    }
}