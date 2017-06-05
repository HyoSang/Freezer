package com.example.light.freezer;

import android.util.Log;

import java.util.Calendar;

public class TimeTable {
    private int freezeHour;
    private int freezeMinute;
    private int startHour;
    private int startMinute;

    public int getFreezeHour() {
        return freezeHour;
    }

    public int getFreezeMinute() {
        return freezeMinute;
    }

    public int getStartHour() {
        return startHour;
    }

    public int getStartMinute() {
        return startMinute;
    }

    public void SetTimeTable(String msg) {
        String binary = makeBinary(msg);

        Calendar cal = Calendar.getInstance();

        int day = cal.get(Calendar.DAY_OF_WEEK);
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        int minute = cal.get(Calendar.MINUTE);
        int lectureLength, startTime=0, startMinute=0, startType, addTime=0, readLength=7;


        for( int j=0; j<day-2;j++) {
            lectureLength=Integer.parseInt(binary.substring(readLength-3,readLength), 2);
            readLength=readLength+lectureLength*13+4;
        }
        lectureLength=Integer.parseInt(binary.substring(readLength-3,readLength), 2);
        for (int i = 0; i < lectureLength; i++) {
            startTime = Integer.parseInt(binary.substring(readLength, readLength + 5), 2);
            startMinute=Integer.parseInt(binary.substring(readLength+5, readLength+11), 2);
            startType=Integer.parseInt(binary.substring(readLength+11, readLength+13), 2);
            addTime= getTimeOfType(startType);
            if(hour<=startTime){
                break;
            }
            else if(hour*60+minute<startTime*60+startMinute+addTime){
                break;
            }
            readLength=readLength+13;
        }

        setFreezeTime(startTime, startMinute, addTime);
    }

    public void setFreezeTime(int hour, int minute, int addtime){
        startHour=hour;
        startMinute=minute;
        freezeHour=hour+(minute+addtime)/60;
        freezeMinute=(minute+addtime)%60;
    }

    public int getTimeOfType(int type){
        if(type==1)
            return 75;
        else if(type==2)
            return 60;
        else if(type==3)
            return 165;
        else
            return 0;
    }

    public String makeBinary(String str){
        String binary="";
        for(int i=0; i<str.length();i++) {
            int temp=Integer.parseInt(Character.toString(str.charAt(i)));
            int first=temp/4;
            temp=temp%4;
            int second=temp/2;
            int third=temp%2;

            binary=binary+first+second+third;
        }
        return binary;
    }
}
