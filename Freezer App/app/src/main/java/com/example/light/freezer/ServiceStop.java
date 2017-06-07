package com.example.light.freezer;

        import android.app.Activity;
        import android.content.Intent;
        import android.os.Bundle;
        import android.support.annotation.Nullable;

/**
 * Created by Suyoung on 2017-06-07.
 */

public class ServiceStop extends Activity {
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Intent i = new Intent(this, FreezingService.class);
        startService(i);
        stopService(i);
        finish();
    }
}
