package com.k.irunlock;

import android.app.Activity;
import android.app.ActivityManager;
import android.content.Context;
import android.content.Intent;
import android.graphics.PixelFormat;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.net.Uri;
import android.os.Binder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.VideoView;

public class CheckActivity extends AppCompatActivity {

    public static String selItem;
    public int firstPos;
    public TextView fRotate;
    private Switch mySwitch, mySwitch2;
    public static boolean vibrationStatus;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_check);

        mySwitch = (Switch) findViewById(R.id.switch1);
        if(isMyServiceRunning(StartStopService.class)){
            mySwitch.setChecked(true);
        }
        mySwitch.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (isChecked) {
                    Intent intent = new Intent(CheckActivity.this, StartStopService.class);
                    startService(intent);
                } else {
                    Intent intent = new Intent(CheckActivity.this, StartStopService.class);
                    stopService(intent);
                }
            }
        });

        mySwitch2 = (Switch) findViewById(R.id.switch2);
        if(vibrationStatus){
            mySwitch2.setChecked(true);
        }
        mySwitch2.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (isChecked) {
                    vibrationStatus = true;
                } else {
                    vibrationStatus = false;
                }
            }
        });

        Spinner dropdown = (Spinner)findViewById(R.id.spinner1);
        String[] items = new String[]{"90 degrees left", "90 degrees right","180 degrees turn"};
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_dropdown_item, items);
        dropdown.setAdapter(adapter);
        if(selItem == "90 degrees left"){
            dropdown.setSelection(0);
        }
        else if(selItem =="90 degrees right"){
            dropdown.setSelection(1);
        }
        else if(selItem == "180 degrees turn"){
            dropdown.setSelection(2);
        }
        else{
            dropdown.setSelection(0);
        }
        dropdown.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                public void onItemSelected(AdapterView<?> parent, View arg1,
                               int arg2, long arg3) {

                         selItem = parent.getSelectedItem().toString();
    }

                public void onNothingSelected(AdapterView<?> arg0) {
                    // TODO Auto-generated method stub
    }
});


    }

    public void startService(View view){
        Intent intent =new Intent(this,StartStopService.class);
        startService(intent);
    }

    public void stopService(View view){
        Intent intent =new Intent(this,StartStopService.class);
        stopService(intent);
    }

    private boolean isMyServiceRunning(Class<?> serviceClass) {
        ActivityManager manager = (ActivityManager) getSystemService(Context.ACTIVITY_SERVICE);
        for (ActivityManager.RunningServiceInfo service : manager.getRunningServices(Integer.MAX_VALUE)) {
            if (serviceClass.getName().equals(service.service.getClassName())) {
                return true;
            }
        }
        return false;
    }


}




