package com.example.light.freezer;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.os.Bundle;
import android.os.Message;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.net.Uri;
import android.os.Handler;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;

public class WhitelistActivity extends Activity implements Runnable,AdapterView.OnItemClickListener{
    private List<Map<String, Object>> list = null;
    private ListView softlist = null;
    private ProgressDialog pd;
    private Context mContext;
    private PackageManager mPackageManager;
    private List<ResolveInfo> mAllApps;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        //TODO Auto-generated method stub
        setContentView(R.layout.whitelist);
        Intent intent = getIntent();

        mContext = this;
        mPackageManager = getPackageManager();

        softlist = (ListView) findViewById(R.id.lv_app_list);

        pd = ProgressDialog.show(this, "Please wait..", "Collect software information...", true,false);
        Thread thread = new Thread(this);
        thread.start();

        super.onCreate(savedInstanceState);
    }

    private void bindMsg(){
        Intent mainIntent = new Intent(Intent.ACTION_MAIN, null);
        mainIntent.addCategory(Intent.CATEGORY_LAUNCHER);
        mAllApps = mPackageManager.queryIntentActivities(mainIntent, 0);
        softlist.setAdapter(new MyAdapter(mContext, mAllApps));
        softlist.setOnItemClickListener(this);
        Collections.sort(mAllApps, new ResolveInfo.DisplayNameComparator(mPackageManager));
    }

    @Override
    public void run() {
        bindMsg();
        handler.sendEmptyMessage(0);
    }

    private Handler handler = new Handler() {
        public void handleMessage(Message msg) {
            pd.dismiss();
        }
    };

    class MyAdapter extends BaseAdapter {

        private Context context;
        private List<ResolveInfo> resInfo;
        private ResolveInfo res;
        private LayoutInflater infater=null;

        public MyAdapter(Context context, List<ResolveInfo> resInfo) {
            this.context = context;
            this.resInfo = resInfo;
            infater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        }

        @Override
        public int getCount() {
            return resInfo.size();
        }

        @Override
        public Object getItem(int arg0) {
            return arg0;
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {

            ViewHolder holder = null;
            if (convertView == null || convertView.getTag() == null) {
                convertView = infater.inflate(R.layout.item_app_info, null);
                holder = new ViewHolder(convertView);
                convertView.setTag(holder);
            }
            else{
                holder = (ViewHolder) convertView.getTag() ;
            }
            res = resInfo.get(position);
            holder.appIcon.setImageDrawable(res.loadIcon(mPackageManager));
            holder.tvAppLabel.setText(res.loadLabel(mPackageManager).toString());
            holder.tvPkgName.setText(res.activityInfo.packageName+'\n'+res.activityInfo.name);
            return convertView;
        }
    }

    class ViewHolder {
        ImageView appIcon;
        TextView tvAppLabel;
        TextView tvPkgName;

        public ViewHolder(View view) {
            this.appIcon = (ImageView) view.findViewById(R.id.img);
            this.tvAppLabel = (TextView) view.findViewById(R.id.name);
            this.tvPkgName = (TextView) view.findViewById(R.id.desc);
        }
    }

    @Override
    public void onItemClick(AdapterView<?> arg0, View arg1, int position, long arg3) {

        ResolveInfo res = mAllApps.get(position);
        String pkg = res.activityInfo.packageName;
        String cls = res.activityInfo.name;

        ComponentName componet = new ComponentName(pkg, cls);

        Intent i = new Intent();
        i.setComponent(componet);
        startActivity(i);
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
