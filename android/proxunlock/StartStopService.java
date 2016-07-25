package com.k.irunlock;

import android.app.KeyguardManager;
import android.os.Vibrator;
import android.app.ActivityManager;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.IBinder;
import android.util.Log;
import android.widget.Toast;

//Unlocking service running on background
public class StartStopService extends Service implements SensorEventListener {

    private boolean lockStatus = false;
    private float proxValue = 0;
    private int proxValueNew;
    SensorManager sm;
    Sensor proxSensor, oriSensor;
    private boolean keyguardDisable = false;
    KeyguardManager.KeyguardLock lock;
    private BroadcastReceiver mreceiver;
    private int angleValue;
    private int angleValueNew;

    @Override
    public void onCreate() {


        sm = (SensorManager) getSystemService(SENSOR_SERVICE);
        proxSensor = sm.getDefaultSensor(Sensor.TYPE_PROXIMITY);
        oriSensor = sm.getDefaultSensor(Sensor.TYPE_ORIENTATION);

        sm.registerListener(this, proxSensor, SensorManager.SENSOR_DELAY_NORMAL);
        sm.registerListener(this, oriSensor, SensorManager.SENSOR_DELAY_NORMAL);

        IntentFilter filter = new IntentFilter(Intent.ACTION_SCREEN_ON);
        filter.addAction(Intent.ACTION_SCREEN_OFF);
        filter.addAction(Intent.ACTION_USER_PRESENT);
        mreceiver = new ScreenReceiver();
        registerReceiver(mreceiver, filter);

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this, "Started Service", Toast.LENGTH_LONG).show();
        return START_STICKY;
    }

    @Override
    public void onDestroy() {
        Toast.makeText(this, "Stopped Service", Toast.LENGTH_LONG).show();
        if (mreceiver != null) {
            unregisterReceiver(mreceiver);
            mreceiver = null;
        }
    }

    /*
    The method where sensor changes are controlled.
     */

    @Override
    public void onSensorChanged(SensorEvent event) {
        Sensor sensor = event.sensor;
        if (sensor.getType() == Sensor.TYPE_PROXIMITY) {
            proxValue = event.values[0];
            Log.v("Prox Value:", String.valueOf(proxValue));
        }
        else if (sensor.getType() == Sensor.TYPE_ORIENTATION) {
            if (proxValue>0.5 && proxValue<4) {
                angleValue = Math.round(event.values[0]);
                Log.v("Angle Value:", String.valueOf(angleValue));
            }
            else {
                angleValueNew = Math.round(event.values[0]);
            }
        }
            //Changed rotation degree from 90 to 270 if user selects to rotate to left by 90 degrees.
            if(isMyServiceRunning(StartStopService.class) && lockStatus == true && CheckActivity.selItem.length() != 0) {
                String[] spinnerData = CheckActivity.selItem.split(" ");
                if (spinnerData[2].equals("left") && spinnerData[0].equals("90")) {
                    spinnerData[0] = "270";
                }
                if (proxValue < 4 && isMyServiceRunning(StartStopService.class) && lockStatus == true) {
                    proxValueNew = 2;
                    if (CheckActivity.vibrationStatus) {
                        Vibrator v = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
                        v.vibrate(100);
                    }
                }
                //if all requirements satisfied, start the activity that unlocks the phone
                if (proxValueNew == 2 && isMyServiceRunning(StartStopService.class) && lockStatus == true && ((angleValue + Integer.parseInt(spinnerData[0])) % 360) + 20 >= angleValueNew && (angleValue + Integer.parseInt(spinnerData[0])) % 360 - 20 <= angleValueNew) {

                    Intent dialogIntent = new Intent(this, UnlockActivity.class);
                    dialogIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                    startActivity(dialogIntent);

                    lockStatus = false;
                    proxValueNew = 0;

                }

            }
            }

/*
a function to check if a service currently running
*/
    private boolean isMyServiceRunning(Class<?> serviceClass) {
        ActivityManager manager = (ActivityManager) getSystemService(Context.ACTIVITY_SERVICE);
        for (ActivityManager.RunningServiceInfo service : manager.getRunningServices(Integer.MAX_VALUE)) {
            if (serviceClass.getName().equals(service.service.getClassName())) {
                return true;
            }
        }
        return false;
    }

    public class ScreenReceiver extends BroadcastReceiver {
        @Override
        public void onReceive(Context context, Intent intent) {

            if((intent.getAction().equals(Intent.ACTION_SCREEN_OFF)))
            {
                lockStatus = true;
                if(keyguardDisable){
                    keyguardDisable = false;
                    lock = null;
                }
            }
            else
            {
                lockStatus = false;
            }

        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }


    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

}