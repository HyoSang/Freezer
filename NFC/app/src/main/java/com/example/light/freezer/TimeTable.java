package com.example.light.freezer;

import android.util.Log;

import java.util.Calendar;

public class TimeTable {
    private int freezeHour;
    private int freezeMinute;

    public int getFreezeHour() {
        return freezeHour;
    }

    public int getFreezeMinute() {
        return freezeMinute;
    }

    public void SetTimeTable(String msg) {
        String binary = stringToBinary(msg);

        binary="1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
        Log.i("aaa", binary);

        Calendar cal = Calendar.getInstance();

        int day = cal.get(Calendar.DAY_OF_WEEK);
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        int minute = cal.get(Calendar.MINUTE);
        int lectureLength, startTime=0, startMinute=0, startType, addTime=0, readLength=7;


        for( int j=0; j<day-2;j++) {
            lectureLength=Integer.parseInt(binary.substring(readLength-3,readLength), 2);
            readLength=readLength+lectureLength*12+4;
        }
        Log.i("aaa", Integer.toString(readLength));
        lectureLength=Integer.parseInt(binary.substring(readLength-3,readLength), 2);
        for (int i = 0; i < lectureLength; i++) {
            startTime = Integer.parseInt(binary.substring(readLength, readLength + 4), 2);
            startMinute=Integer.parseInt(binary.substring(readLength+4, readLength+10), 2);
            startType=Integer.parseInt(binary.substring(readLength+10, readLength+12), 2);
            addTime= getTimeOfType(startType);
            if(hour<=startTime){
                break;
            }
            else if(hour*60+minute<startTime*60+startMinute+addTime){
                break;
            }
        }

        setFreezeTime(startTime, startMinute, addTime);
    }

    public void setFreezeTime(int hour, int minute, int addtime){
        freezeHour=hour+(minute+addtime)/60;
        freezeMinute=(minute+addtime)%60;
    }

    public int getTimeOfType(int type){
        if(type==0)
            return 75;
        else if(type==1)
            return 60;
        else if(type==2)
            return 50;
        else
            return 165;
    }

    public String stringToBinary(String str) {
        byte[] bytes = str.getBytes();
        StringBuilder binary = new StringBuilder();
        for (byte b : bytes) {
            int val = b;
            for (int i = 0; i < 8; i++) {
                binary.append((val & 128) == 0 ? 0 : 1);
                val <<= 1;
            }
        }
        return binary.toString();
    }
}
