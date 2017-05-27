package com.example.light.freezer;

import java.util.Calendar;
import java.util.List;

public class TimeTable {
    private int roomType;
    private List<Integer> lectureNumber;
    private List<Integer> startTime;
    private List<Integer> startMinute;
    private List<Integer> lectureType;
    private int freezeHour;
    private int freezeMinute;

    public TimeTable(String msg) {
        int readLength=0, i=0;
        String binary = StringToBinary(msg);
        roomType = Integer.parseInt(binary.substring(0,3));
        for( int j=0; j<5;j++) {
            lectureNumber.add(Integer.parseInt(binary.substring(3+readLength,6+readLength)));
            for (i = 0; i < lectureNumber.get(j); i++) {
                startTime.add(Integer.parseInt(binary.substring(6 + 12 * i, 10 + 12 * i)));
                startMinute.add(Integer.parseInt(binary.substring(10 + 12 * i, 16 + 12 * i)));
                lectureType.add(Integer.parseInt(binary.substring(16 + 12 * i, 18 + 12 * i)));
            }
            readLength=18+12*i;
        }

        Calendar cal = Calendar.getInstance();

        int day = cal.get(Calendar.DAY_OF_MONTH);
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        int minute = cal.get(Calendar.MINUTE);

        if(day==2){
            
        }
        else if(day==3){

        }
        else if(day==4){

        }
        else if(day==5){

        }
        else if(day==6){

        }

    }

    public String StringToBinary(String str) {
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
