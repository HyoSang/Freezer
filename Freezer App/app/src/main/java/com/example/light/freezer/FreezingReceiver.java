package com.example.light.freezer;

import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.widget.Toast;

/**
 * Created by Suyoung on 2017-05-22.
 */
public class FreezingReceiver extends BroadcastReceiver {
    private TelephonyManager telephonyManager = null;
    private boolean isPhoneidle = true;
    private String mLastState;

    @Override
    public void onReceive(Context context, Intent intent) {
        String state = intent.getStringExtra(TelephonyManager.EXTRA_STATE);
        /*if (state.equals(mLastState)) {
            return;
        } else {
            mLastState = state;
        }
        if(telephonyManager == null) {
            telephonyManager = (TelephonyManager) context.getSystemService(Context.TELEPHONY_SERVICE);
            telephonyManager.listen(phoneListener, PhoneStateListener.LISTEN_CALL_STATE);
        }

        if(isPhoneidle) {

        }
        else
            FreezingService.onPause();*/
    }
}

// http://blog.naver.com/PostView.nhn?blogId=tempests05&logNo=20142503735