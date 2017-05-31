package com.java42.freezing;

import android.app.AlarmManager;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.res.Configuration;
import android.graphics.Color;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.os.IBinder;
import android.provider.ContactsContract;
import android.support.annotation.DrawableRes;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.util.DisplayMetrics;
import android.util.Log;
import android.util.TypedValue;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;

import java.util.Calendar;

public class FreezingService extends Service {
    public FreezingService() {
    }

    private static View mPopupView;							//항상 보이게 할 뷰
    private static WindowManager.LayoutParams mParams;		//layout params 객체. 뷰의 위치 및 크기를 지정하는 객체
    private static WindowManager.LayoutParams mParams2;
    private static WindowManager mWindowManager;			//윈도우 매니저
    private static Button usable;
    private FreezingReceiver mReceiver = null;
    private TelephonyManager telephonyManager = null;

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        return null;
    }

    //@Override
    public void onCreate() {
        super.onCreate();

        LayoutInflater inflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mPopupView = inflater.inflate(R.layout.freezing,null);

        //최상위 윈도우에 넣기 위한 설정
        mParams = new WindowManager.LayoutParams(
                WindowManager.LayoutParams.WRAP_CONTENT,
                WindowManager.LayoutParams.WRAP_CONTENT,
                WindowManager.LayoutParams.TYPE_PHONE,					//항상 최 상위에 있게. status bar 밑에 있음. 터치 이벤트 받을 수 있음.
                WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE,		//이 속성을 안주면 터치 & 키 이벤트도 먹게 된다.
                //포커스를 안줘서 자기 영역 밖터치는 인식 안하고 키이벤트를 사용하지 않게 설정
                PixelFormat.TRANSLUCENT);										//투명
        mParams.gravity = Gravity.LEFT | Gravity.TOP;						//왼쪽 상단에 위치하게 함.

        mWindowManager = (WindowManager) getSystemService(WINDOW_SERVICE);	//윈도우 매니저 불러옴.
        mWindowManager.addView(mPopupView, mParams);		//최상위 윈도우에 뷰 넣기. *중요 : 여기에 permission을 미리 설정해 두어야 한다. 매니페스트에

        usable = new Button(this);
        mParams2 = new WindowManager.LayoutParams(
                WindowManager.LayoutParams.WRAP_CONTENT,
                WindowManager.LayoutParams.WRAP_CONTENT,
                WindowManager.LayoutParams.TYPE_PHONE,					//항상 최 상위에 있게. status bar 밑에 있음. 터치 이벤트 받을 수 있음.
                WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE,		//이 속성을 안주면 터치 & 키 이벤트도 먹게 된다.
                //포커스를 안줘서 자기 영역 밖터치는 인식 안하고 키이벤트를 사용하지 않게 설정
                PixelFormat.TRANSLUCENT);
        mParams2.gravity = Gravity.CENTER | Gravity.BOTTOM;
        mWindowManager.addView(usable,mParams2);

        mReceiver = new FreezingReceiver();
        IntentFilter filter = new IntentFilter();
        registerReceiver(mReceiver,filter);

        telephonyManager = (TelephonyManager) getApplicationContext().getSystemService(Context.TELEPHONY_SERVICE);
        telephonyManager.listen(new PhoneStateListener(), PhoneStateListener.LISTEN_CALL_STATE);
    }

    @Override
    public int onStartCommand(final Intent intent, int flags, int startid) {
        startForeground(1, new Notification());

        final int hour = intent.getIntExtra("HOUR", 0);
        final int minute = intent.getIntExtra("MINUTE", 0);

        usable.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent2 = new Intent(getApplicationContext(), WhitelistActivity.class);
                intent2.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                if(mPopupView != null) mWindowManager.removeView(mPopupView);
                if(usable!=null) mWindowManager.removeView(usable);
                startActivity(intent2);
            }
        });

        telephonyManager = (TelephonyManager) getApplicationContext().getSystemService(Context.TELEPHONY_SERVICE);
        telephonyManager.listen(new PhoneStateListener(), PhoneStateListener.LISTEN_CALL_STATE);

        return super.onStartCommand(intent, flags, startid);
    }

    public static void onPause() {
        if(mWindowManager != null) {		//서비스 종료시 뷰 제거. *중요 : 뷰를 꼭 제거 해야함.
            if(mPopupView != null) mWindowManager.removeView(mPopupView);
            if(usable!=null) mWindowManager.removeView(usable);
        }
    }

    public static void onResume() {
        mWindowManager.addView(mPopupView, mParams);
        mWindowManager.addView(usable, mParams2);
    }

    @Override
    public void onDestroy() {
        if(mWindowManager != null) {		//서비스 종료시 뷰 제거. *중요 : 뷰를 꼭 제거 해야함.
            if(mPopupView != null) mWindowManager.removeView(mPopupView);
            if(usable!=null) mWindowManager.removeView(usable);
        }

        if(mReceiver!=null) {
            unregisterReceiver(mReceiver);
        }

        super.onDestroy();
    }
}