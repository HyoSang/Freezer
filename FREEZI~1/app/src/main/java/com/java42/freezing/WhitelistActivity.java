package com.java42.freezing;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.net.Uri;
import android.os.Handler;
import android.widget.Toast;

/**
 * Created by Suyoung on 2017-05-30.
 */
public class WhitelistActivity extends Activity{
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.intro);
        Intent intent = getIntent();
    }

    public void finish() {
        FreezingService.onResume();
        super.finish();
    }

    @Override
    public void onUserLeaveHint() {
        FreezingService.onResume();
        super.finish();
    }
}
