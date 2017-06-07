package com.example.light.freezer;

import android.app.AlarmManager;
import android.app.PendingIntent;
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
        Calendar end_calendar = Calendar.getInstance();
        if (action.equals("android.intent.action.BOOT_COMPLETED")) {
            try {
                FileInputStream fis = context.openFileInput("Freeze.txt");
                BufferedReader reader = new BufferedReader(new InputStreamReader(fis));
                String str = reader.readLine();
                int hour = Integer.parseInt(str);
                str = reader.readLine();
                int min = Integer.parseInt(str);
                end_calendar = Calendar.getInstance();
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
            Intent intent1 = new Intent(context,FreezingService.class);
            context.startService(intent1);
            Intent i2 = new Intent(context, ServiceStop.class);
            AlarmManager am = (AlarmManager) context.getSystemService(Context.ALARM_SERVICE);
            PendingIntent pIntent = PendingIntent.getActivity(context, 0, i2, 0);
            am.set(AlarmManager.RTC_WAKEUP,end_calendar.getTimeInMillis(),pIntent);
        }
    }
}