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

    @Override
    public void onReceive(Context context, Intent intent) {
        if(telephonyManager == null) {
            telephonyManager = (TelephonyManager) context.getSystemService(Context.TELEPHONY_SERVICE);
            telephonyManager.listen(phoneListener, PhoneStateListener.LISTEN_CALL_STATE);
        }

        if(isPhoneidle) {

        }

        else
            FreezingService.onPause();
    }

    private PhoneStateListener phoneListener = new PhoneStateListener() {
        @Override
        public void onCallStateChanged(int state, String incomingNumber) {
            switch (state) {
                case TelephonyManager.CALL_STATE_IDLE :
                    isPhoneidle = true;
                    break;
                case TelephonyManager.CALL_STATE_RINGING:
                    isPhoneidle = false;
                    break;
                case TelephonyManager.CALL_STATE_OFFHOOK:
                    isPhoneidle = false;
                    break;
            }
        }
    };
}
