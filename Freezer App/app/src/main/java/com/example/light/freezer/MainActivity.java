package com.example.light.freezer;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.app.TimePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.Uri;
import android.nfc.NfcAdapter;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TimePicker;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.util.Calendar;
import java.util.List;

public class MainActivity extends Activity{

    private NfcAdapter _nfcAdapter;
    private PendingIntent _pendingIntent;
    private IntentFilter[] _intentFilters;
    //
    private final String _MIME_TYPE = "text/plain";
    private TimeTable timeTable= new TimeTable();

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

        if(!hasWindowOverlayPermission(getApplicationContext()))
        {
            Uri uri = Uri.fromParts("package",getPackageName(),null);
            Intent intent2 = new Intent(Settings.ACTION_MANAGE_OVERLAY_PERMISSION,uri);
            startActivityForResult(intent2,3333);
        }

        _init();
        Intent intent3 = getIntent();
        handleIntent(intent3);

        final Intent intent = new Intent(this, FreezingService.class);

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

        btnWhite.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent3 = new Intent(getApplicationContext(), AppUsageStatisticsActivity.class);
                startActivity(intent3);
            }
        });
    }

    private void _init()
    {
        _nfcAdapter = NfcAdapter.getDefaultAdapter(this);


        if (_nfcAdapter == null)
        {
            Toast.makeText(this, "This device does not support NFC.", Toast.LENGTH_LONG).show();
            return;
        }
        if (_nfcAdapter.isEnabled())
        {
            _pendingIntent = PendingIntent.getActivity(this, 0, new Intent(this, getClass()).addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP), 0);

            IntentFilter ndefDetected = new IntentFilter(NfcAdapter.ACTION_NDEF_DISCOVERED);
            try
            {
                ndefDetected.addDataType(_MIME_TYPE);
            } catch (IntentFilter.MalformedMimeTypeException e)
            {
                Log.e(this.toString(), e.getMessage());
            }

            _intentFilters = new IntentFilter[] { ndefDetected };
        }
    }
    @Override
    protected void onNewIntent(Intent intent)
    {
        super.onNewIntent(intent);

        handleIntent(intent);
    }

    protected void handleIntent(Intent intent)
    {

        if(NfcAdapter.ACTION_NDEF_DISCOVERED.equals(intent.getAction()))
        {
            List<String> msgs = NFCUtils.getStringsFromNfcIntent(intent);

            final Intent intent2 = new Intent(this, FreezingService.class);

            Toast.makeText(this, "Message received : "+msgs.get(0), Toast.LENGTH_LONG).show();

            if(msgs.get(0).charAt(0)=='1') {
                timeTable.SetTimeTable(msgs.get(0));

                int start_hour = timeTable.getStartHour();
                int start_minute = timeTable.getStartMinute();

                final Calendar start_calender = Calendar.getInstance();
                start_calender.set(Calendar.HOUR_OF_DAY, start_hour);
                start_calender.set(Calendar.MINUTE, start_minute);
                start_calender.set(Calendar.SECOND, 0);

                int end_hour = timeTable.getFreezeHour();
                int end_minute = timeTable.getFreezeMinute();

                final Calendar end_calender = Calendar.getInstance();
                end_calender.set(Calendar.HOUR_OF_DAY, end_hour);
                end_calender.set(Calendar.MINUTE, end_minute);
                start_calender.set(Calendar.SECOND, 0);

                FreezingService.service_end_time = end_calender;

                Calendar current_time = Calendar.getInstance();

                long start_time = start_calender.getTimeInMillis() - current_time.getTimeInMillis();
                long end_time = end_calender.getTimeInMillis() - current_time.getTimeInMillis();

                if (start_time < 0) start_time = 0;
                if (end_time < 0) end_time = 0;

                if (end_time > 0) {
                    Handler start_handler = new Handler();
                    try {
                        FileInputStream fs = getApplicationContext().openFileInput("LoginID");
                        BufferedReader buf = new BufferedReader(new InputStreamReader(fs));
                        intent2.putExtra("ID",buf.readLine());
                        fs.close();
                        buf.close();
                        fs = getApplicationContext().openFileInput("LoginPass");
                        buf = new BufferedReader(new InputStreamReader(fs));
                        intent2.putExtra("Pass",buf.readLine());
                        fs.close();
                        buf.close();
                    }catch(Exception e){}
                    start_handler.postDelayed(new Runnable() {
                        @Override
                        public void run() {
                            startService(intent2);
                        }
                    }, start_time);

                    try{
                        FileOutputStream fos = openFileOutput("Freeze.txt", 0);
                        PrintWriter writer = new PrintWriter(fos);
                        writer.println(0);
                        writer.println(end_hour);
                        writer.println(end_minute);
                        writer.close();
                    }
                    catch (IOException ie) {}

                    Intent i2 = new Intent(this, ServiceStop.class);
                    AlarmManager am = (AlarmManager) getApplicationContext().getSystemService(Context.ALARM_SERVICE);
                    PendingIntent pIntent = PendingIntent.getActivity(getApplicationContext(), 0, i2, 0);
                    am.set(AlarmManager.RTC_WAKEUP,end_calender.getTimeInMillis(),pIntent);
                }
            }
            else{
                try {
                    FileInputStream fs = getApplicationContext().openFileInput("LoginID");
                    BufferedReader buf = new BufferedReader(new InputStreamReader(fs));
                    intent2.putExtra("ID",buf.readLine());
                    fs.close();
                    buf.close();
                    fs = getApplicationContext().openFileInput("LoginPass");
                    buf = new BufferedReader(new InputStreamReader(fs));
                    intent2.putExtra("Pass",buf.readLine());
                    fs.close();
                    buf.close();
                }catch(Exception e){}
                if(FreezingService.flag2)
                    stopService(intent2);
                else {
                    try {
                        FileOutputStream fos = openFileOutput("Freeze.txt", 0);
                        PrintWriter writer = new PrintWriter(fos);
                        writer.println(1);
                        writer.println(0);
                        writer.println(0);
                        writer.close();
                    } catch (IOException ie) {
                    }
                    startService(intent2);
                }
            }
        }
    }

//    @Override
//    protected void onPause() {
//        super.onPause();
//        if(_nfcAdapter!=null){
//            _nfcAdapter.disableForegroundDispatch(this);
//            finish();
//        }
//    }
}
