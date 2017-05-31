package com.example.light.freezer;

        import android.os.Bundle;
        import android.util.Log;
        import android.view.View;
        import android.widget.Button;
        import android.app.Activity;
        import android.content.Intent;

public class MainActivity extends Activity
{
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Button btnTagActivity= (Button) findViewById(R.id.btnTagActivity);
        Button btnP2PActivity = (Button) findViewById(R.id.btnP2PActivity);
        Button btnUIACctivity = (Button) findViewById(R.id.btnUI);
        btnTagActivity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), TagActivity.class);
                startActivity(intent);
            }
        });

        btnP2PActivity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), P2PActivity.class);
                startActivity(intent);
            }
        });

        btnUIACctivity.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), UIActivity.class);
                startActivity(intent);
            }
        });
    }
}

