package com.java42.freezing;

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
    private static String mLastState;

    @Override
    public void onReceive(Context context, Intent intent) {
        String state = intent.getStringExtra(TelephonyManager.EXTRA_STATE);
        if (state.equals(mLastState)) {
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
            FreezingService.onPause();
    }

    private static PhoneStateListener phoneListener = new PhoneStateListener() {
        @Override
        public void onCallStateChanged(int state, String incomingNumber) {
            switch (state) {
                case TelephonyManager.CALL_STATE_IDLE :
                    FreezingService.onResume();
                    break;
                case TelephonyManager.CALL_STATE_RINGING:
                    FreezingService.onPause();
                    break;
                case TelephonyManager.CALL_STATE_OFFHOOK:

                    break;
            }
        }
    };
}

// http://blog.naver.com/PostView.nhn?blogId=tempests05&logNo=20142503735