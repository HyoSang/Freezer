package com.example.light.freezer;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.Nullable;

public class UIActivity extends Activity{
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ui);
    }
}
