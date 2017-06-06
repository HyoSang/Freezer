package com.example.light.freezer;

import android.app.Activity;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;

import android.graphics.drawable.Drawable;
import android.os.Bundle;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;

import android.widget.ImageView;

import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;

import java.util.List;


/**
 * Created by Suyoung on 2017-05-30.
 */
public class WhitelistActivity extends Activity {

    // 用来记录应用程序的信息
    List<AppsItemInfo> list;

    private ListView listView;
    private PackageManager pManager;
    //private ComponentName componet;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // TODO Auto-generated method stub
        super.onCreate(savedInstanceState);
        setContentView(R.layout.whitelist);


        // 取得ListView
        listView = (ListView) findViewById(R.id.lv_app_list);

        // 获取图片、应用名、包名
        pManager = WhitelistActivity.this.getPackageManager();
        List<PackageInfo> appList = getAllApps(WhitelistActivity.this);

        list = new ArrayList<AppsItemInfo>();

        for (int i = 0; i < appList.size(); i++) {
            PackageInfo pinfo = appList.get(i);
            AppsItemInfo shareItem = new AppsItemInfo();
            // 设置图片
            shareItem.setIcon(pManager
                    .getApplicationIcon(pinfo.applicationInfo));
            // 设置应用程序名字
            shareItem.setLabel(pManager.getApplicationLabel(
                    pinfo.applicationInfo).toString());
            // 设置应用程序的包名
            shareItem.setPackageName(pinfo.applicationInfo.packageName);

            list.add(shareItem);

        }

        // 设置gridview的Adapter
        listView.setAdapter(new baseAdapter());

        // 点击应用图标时，做出响应
        listView.setOnItemClickListener(new ClickListener());


    }

    public static List<PackageInfo> getAllApps(Context context) {

        List<PackageInfo> apps = new ArrayList<PackageInfo>();
        PackageManager pManager = context.getPackageManager();
        // 获取手机内所有应用
        List<PackageInfo> packlist = pManager.getInstalledPackages(0);
        for (int i = 0; i < packlist.size(); i++) {
            PackageInfo pak = (PackageInfo) packlist.get(i);

            // 判断是否为非系统预装的应用程序
            // 这里还可以添加系统自带的，这里就先不添加了，如果有需要可以自己添加
            // if()里的值如果<=0则为自己装的程序，否则为系统工程自带
            if ((pak.applicationInfo.flags & pak.applicationInfo.FLAG_SYSTEM) <= 0) {
                // 添加自己已经安装的应用程序
                apps.add(pak);
            }

        }
        return apps;
    }

    private class baseAdapter extends BaseAdapter {
        LayoutInflater inflater = LayoutInflater.from(WhitelistActivity.this);

        @Override
        public int getCount() {
            // TODO Auto-generated method stub
            return list.size();
        }

        @Override
        public Object getItem(int position) {
            // TODO Auto-generated method stub
            return null;
        }

        @Override
        public long getItemId(int position) {
            // TODO Auto-generated method stub
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            // TODO Auto-generated method stub
            ViewHolder holder;
            if (convertView == null) {
                // 使用View的对象itemView与R.layout.item关联
                convertView = inflater.inflate(R.layout.item_app_info, null);
                holder = new ViewHolder();
                holder.icon = (ImageView) convertView
                        .findViewById(R.id.iv_app_icon);
                holder.label = (TextView) convertView
                        .findViewById(R.id.tv_app_name);
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }

            holder.icon.setImageDrawable(list.get(position).getIcon());
            holder.label.setText(list.get(position).getLabel().toString());

            return convertView;

        }

    }

    private class ViewHolder{
        private ImageView icon;
        private TextView label;
    }

    // 当用户点击应用程序图标时，将对这个类做出响应
    private class ClickListener implements AdapterView.OnItemClickListener {

        @Override
        public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
                                long arg3) {

//			// 将应用所选的应用程序信息共享到Application中
//		MyApp appState = ((MyApp) getApplicationContext());
//			// 获取当前所在选项卡
//			String tab_id = appState.getTab_id();
//			// 设置所选应用程序信息
//			appState.set_AppInfo(tab_id, list.get(arg2).getLabel(), list.get(
//
// 		arg2).getIcon(), list.get(arg2).getPackageName());


            // String pkg = res.AppsItemInfo.packageName;


            //String cls = res.activityInfo.name;
            Intent intent = new Intent();
            intent = WhitelistActivity.this.getPackageManager().
                    getLaunchIntentForPackage(list.get(arg2).getPackageName());

            startActivity(intent);
            // 销毁当前Activity
            //finish();
        }

    }
    class AppsItemInfo {

        private Drawable icon; // 存放图片
        private String label; // 存放应用程序名
        private String packageName; // 存放应用程序包名

        public Drawable getIcon() {
            return icon;
        }

        public void setIcon(Drawable icon) {
            this.icon = icon;
        }

        public String getLabel() {
            return label;
        }

        public void setLabel(String label) {
            this.label = label;
        }

        public String getPackageName() {
            return packageName;
        }

        public void setPackageName(String packageName) {
            this.packageName = packageName;
        }

    }

    public void finish() {
        FreezingService.onResume();
        super.finish();
    }

    @Override
    public void onUserLeaveHint() {
        FreezingService.onResume();
    }
}
