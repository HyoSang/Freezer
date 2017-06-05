package com.example.light.freezer;

import android.graphics.drawable.Drawable;

public class MyAppInfo {
    private Drawable image;
    private String packageName;
    private String AppName;

    public MyAppInfo(Drawable image, String appName) {
        this.image = image;
        this.packageName = appName;
    }

    public MyAppInfo() {

    }

    public Drawable getImage() {
        return image;
    }

    public void setImage(Drawable image) {
        this.image = image;
    }

    public String getPackageName() {
        return packageName;
    }

    public void setPackageName(String appName) {
        this.packageName = appName;
    }

    public String getAppName() {
        return AppName;
    }

    public void setAppName(String appName) {
        AppName = appName;
    }
}
