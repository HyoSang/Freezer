package com.example.light.freezer;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.NdefMessage;
import android.nfc.NfcAdapter;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.util.List;

public class TagActivity extends Activity
{
    private NfcAdapter _nfcAdapter;
    private PendingIntent _pendingIntent;
    private IntentFilter[] _intentFilters;
    //
    private final String _MIME_TYPE = "text/plain";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tag);

        Button btnReturn = (Button) findViewById(R.id.btnReturn);
        btnReturn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        _init();
        Intent intent = getIntent();
        handleIntent(intent);
    }

    private void _init()
    {
        _nfcAdapter = NfcAdapter.getDefaultAdapter(this);


        if (_nfcAdapter == null)
        {
            Toast.makeText(this, "This device does not support NFC.", Toast.LENGTH_LONG).show();
            return;
        }
        if (_nfcAdapter.isEnabled())
        {
            _pendingIntent = PendingIntent.getActivity(this, 0, new Intent(this, getClass()).addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP), 0);

            IntentFilter ndefDetected = new IntentFilter(NfcAdapter.ACTION_NDEF_DISCOVERED);
            try
            {
                ndefDetected.addDataType(_MIME_TYPE);
            } catch (IntentFilter.MalformedMimeTypeException e)
            {
                Log.e(this.toString(), e.getMessage());
            }

            _intentFilters = new IntentFilter[] { ndefDetected };
        }
    }

    @Override
    protected void onResume()
    {
        super.onResume();
        _enableNdefExchangeMode();
    }

    private void _enableNdefExchangeMode()
    {
        EditText messageTextField = (EditText) findViewById(R.id.message_text_field);
        String stringMessage = " " + messageTextField.getText().toString();
        NdefMessage message = NFCUtils.getNewMessage(_MIME_TYPE, stringMessage.getBytes());
        _nfcAdapter.setNdefPushMessage(message, this);
        if(_nfcAdapter!=null){
            _nfcAdapter.enableForegroundDispatch(this, _pendingIntent, _intentFilters, null);
        }
    }

    @Override
    protected void onNewIntent(Intent intent)
    {
        super.onNewIntent(intent);

        handleIntent(intent);
    }

    protected void handleIntent(Intent intent)
    {
        Log.i("aaa", "handleintent");
        Log.i("aaa", intent.toString());
        Log.i("aaa", intent.getAction());

        if(NfcAdapter.ACTION_NDEF_DISCOVERED.equals(intent.getAction()))
        {
            List<String> msgs = NFCUtils.getStringsFromNfcIntent(intent);

            Toast.makeText(this, "Message received : "+msgs.get(0), Toast.LENGTH_LONG).show();

            //TimeTable timeTable= new TimeTable(msgs.get(0));

        }
    }

//    @Override
//    protected void onPause() {
//        super.onPause();
//        if(_nfcAdapter!=null){
//            _nfcAdapter.disableForegroundDispatch(this);
//            finish();
//        }
//    }
}